using OggVorbisEncoder;
using System;
using System.IO;

namespace Headquarters4DCS.Tools
{
    /// <summary>
    /// Uses OggVorbisEncoder to convert a .wav file into an .ogg Vorbis file.
    /// Based on code by Steve Lillis - https://github.com/SteveLillis/.NET-Ogg-Vorbis-Encoder
    /// </summary>
    public sealed class TTSWavToOgg : IDisposable
    {
        private const int SampleSize = 256;

        public TTSWavToOgg() { }

        public byte[] ConvertWaveBytes(byte[] waveBytes)
        {
            // Stores all the static vorbis bitstream settings
            VorbisInfo info = VorbisInfo.InitVariableBitRate(2, 44100, 0.1f);

            // set up our packet->stream encoder
            int serial = new Random().Next();
            OggStream oggStream = new OggStream(serial);

            MemoryStream streamIn = new MemoryStream(waveBytes);
            MemoryStream streamOut = new MemoryStream();

            // Vorbis streams begin with three headers; the initial header (with
            // most of the codec setup parameters) which is mandated by the Ogg
            // bitstream spec.  The second header holds any comment fields.  The
            // third header holds the bitstream codebook.
            var headerBuilder = new HeaderPacketBuilder();

            var comments = new Comments();
            var infoPacket = headerBuilder.BuildInfoPacket(info);
            var commentsPacket = headerBuilder.BuildCommentsPacket(comments);
            var booksPacket = headerBuilder.BuildBooksPacket(info);

            oggStream.PacketIn(infoPacket);
            oggStream.PacketIn(commentsPacket);
            oggStream.PacketIn(booksPacket);

            // Flush to force audio data onto its own page per the spec
            OggPage page;
            while (oggStream.PageOut(out page, true))
            {
                streamOut.Write(page.Header, 0, page.Header.Length);
                streamOut.Write(page.Body, 0, page.Body.Length);
            }

            var processingState = ProcessingState.Create(info);

            var buffer = new float[info.Channels][];
            buffer[0] = new float[SampleSize];
            buffer[1] = new float[SampleSize];

            var readbuffer = new byte[SampleSize];
            while (!oggStream.Finished)
            {
                int bytes = streamIn.Read(readbuffer, 0, readbuffer.Length);

                if (bytes == 0)
                {
                    processingState.WriteEndOfStream();
                }
                else
                {
                    var samples = bytes;

                    for (var i = 1; i < samples; i += 2)
                    {
                        buffer[0][i] = (short)((readbuffer[i] << 8) | (0x00ff & readbuffer[i - 1])) / 32768f;
                        buffer[1][i] = buffer[0][i];
                    }

                    processingState.WriteData(buffer, bytes);
                }

                while (!oggStream.Finished
                       && processingState.PacketOut(out OggPacket packet))
                {
                    oggStream.PacketIn(packet);

                    while (!oggStream.Finished
                           && oggStream.PageOut(out page, false))
                    {
                        streamOut.Write(page.Header, 0, page.Header.Length);
                        streamOut.Write(page.Body, 0, page.Body.Length);
                    }
                }
            }

            byte[] oggBytes = streamOut.ToArray();

            streamIn.Close();
            streamOut.Close();

            return oggBytes;
        }

        public void Dispose() { }
    }
}

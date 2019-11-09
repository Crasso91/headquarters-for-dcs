using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;

namespace Headquarters4DCS.Tools
{
    /// <summary>
    /// Main form for the standalong radio message maker.
    /// </summary>
    internal partial class FormRadioMessageGenerator : Form
    {
        // ===============================================
        // PRIVATE FIELDS
        // ===============================================

        //private readonly HQ4DCS HQ;

        private WaveFileReader WaveFile = null;
        private WaveOutEvent WavePlayer = null;
        private MemoryStream WaveStream = null;

        private string LastSavePath = HQTools.PATH;

        private readonly TTSRadioMessageGenerator RadioMsgMaker = null;

        // ===============================================
        // CONSTRUCTOR
        // ===============================================

        internal FormRadioMessageGenerator()
        {
            InitializeComponent();

            RadioMsgMaker = new TTSRadioMessageGenerator();
        }

        // ===============================================
        // PRIVATE WINFORM EVENT METHODS
        // ===============================================

        private void Frm_RadioMessageMaker_Load(object sender, EventArgs e)
        {
            VoiceComboBox.Items.Clear();
            foreach (string v in RadioMsgMaker.GetAllVoices()) VoiceComboBox.Items.Add(v);
            if (VoiceComboBox.Items.Count > 0) VoiceComboBox.SelectedIndex = 0;

            if (VoiceComboBox.Items.Count == 0)
            {
                //MessageBox.Show(HQ.Language.Get("userInterface.message.noVoices"), HQ.Language.Get("userInterface.message.error"), MessageBoxButtons.OK);
                PlayButton.Enabled = false;
                SaveToWavButton.Enabled = false;
            }
        }

        private void RadioMessageMakerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopSound();
        }

        private void Event_ButtonClick(object sender, EventArgs e)
        {
            if (VoiceComboBox.Items.Count == 0) return;

            if (sender == PlayButton)
            {
                StopSound();

                byte[] bytes = RadioMsgMaker.GenerateRadioMessageWavBytes(MessageTextbox.Text, GetVoiceNameOnlyFromCombobox());

                WaveStream = new MemoryStream(bytes);
                WaveFile = new WaveFileReader(WaveStream);

                WavePlayer = new WaveOutEvent();
                WavePlayer.Init(WaveFile);
                WavePlayer.Play();
                return;
            }
            else if ((sender == SaveToWavButton) || (sender == SaveToOggButton))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.InitialDirectory = LastSavePath;
                    if (sender == SaveToOggButton)
                    { sfd.Filter = "Ogg Vorbis files (*.ogg)|*.ogg"; sfd.FileName = "NewRadioMessage.ogg"; }
                    else
                    { sfd.Filter = "PCM Wav files (*.wav)|*.wav"; sfd.FileName = "NewRadioMessage.wav"; }
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    byte[] bytes = RadioMsgMaker.GenerateRadioMessageWavBytes(MessageTextbox.Text, GetVoiceNameOnlyFromCombobox());

                    if (sender == SaveToOggButton)
                    {
                        using (TTSWavToOgg wav2oggConverter = new TTSWavToOgg())
                        { bytes = wav2oggConverter.ConvertWaveBytes(bytes); }
                    }

                    File.WriteAllBytes(sfd.FileName, bytes);
                    LastSavePath = Path.GetDirectoryName(sfd.FileName);
                }
                return;
            }
        }

        private void StopSound()
        {
            if (WaveStream != null)
            {
                WaveStream.Close();
                WaveStream.Dispose();
            }

            if (WavePlayer != null)
            {
                WavePlayer.Stop();
                WavePlayer.Dispose();
            }

            if (WaveFile != null)
            {
                WaveFile.Close();
                WaveFile.Dispose();
            }
        }

        private string GetVoiceNameOnlyFromCombobox()
        {
            string voiceStr = VoiceComboBox.SelectedItem.ToString();
            return voiceStr.Substring(0, voiceStr.IndexOf(","));
        }
    }
}

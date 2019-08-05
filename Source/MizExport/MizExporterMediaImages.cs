/*
==========================================================================
This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
Eagle Dynamics' DCS World flight simulator.

HQ4DCS was created by Ambroise Garel (@akaAgar).
You can find more information about the project on its GitHub page,
https://akaAgar.github.io/headquarters-for-dcs

HQ4DCS is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

HQ4DCS is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with HQ4DCS. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using Headquarters4DCS.GeneratedMission;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Headquarters4DCS.MizExport
{
    /// <summary>
    /// Creates all images file required for the mission except for Kneeboard makers, which are handled differently (see MIZMediaKneeboardMaker).
    /// </summary>
    public sealed class MizExporterMediaImages : IDisposable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MizExporterMediaImages() { }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Creates all mission images and returns them as a dictionary of byte arrays.
        /// </summary>
        /// <param name="missHQ">An HQ4DCS mission.</param>
        /// <returns>A dictionary of entries to include the .miz file. Key is the entry name, value is an array holding the bytes of the entry.</returns>
        public Dictionary<string, byte[]> MakeImages(Mission missHQ)
        {
            DebugLog.Instance.Log($"Adding images files...");

            Dictionary<string, byte[]> imagesFiles = new Dictionary<string, byte[]>
            {
                { $"l10n/DEFAULT/title.jpg", GetTitleImage(missHQ.BriefingName) }
            };

            return imagesFiles;
        }

        /// <summary>
        /// Generates the title image for the mission.
        /// </summary>
        /// <param name="missionName">The name of the mission.</param>
        /// <returns>The mission title image, as an array of bytes for a JPEG file.</returns>
        private byte[] GetTitleImage(string missionName)
        {
            byte[] imageBytes;
            Rectangle rect;
            int x, y;

            using (Image titleImage = GetImageIfItExists("Jpg\\Title.jpg"))
            {
                using (Graphics g = Graphics.FromImage(titleImage))
                {
                    using (Font font = new Font("Arial", 48, FontStyle.Regular, GraphicsUnit.Point))
                    {
                        for (x = -1; x <= 1; x++)
                            for (y = -1; y <= 1; y++)
                            {
                                if ((x == 0) && (y == 0)) continue;
                                rect = new Rectangle(x * 1, y * 1, 512, 512);
                                TextRenderer.DrawText(g, missionName, font, rect, Color.Black, HQTools.CENTER_TEXT_FLAGS);
                            }

                        rect = new Rectangle(0, 0, 512, 512);
                        TextRenderer.DrawText(g, missionName, font, rect, Color.White, HQTools.CENTER_TEXT_FLAGS);
                    }
                }

                imageBytes = HQTools.ImageToBytes(titleImage, ImageFormat.Jpeg);
            }

            return imageBytes;
        }

        /// <summary>
        /// Returns an image from the HQ4DCS\Include directory, if it exists.
        /// If it doesn't, return an solid black image of size defaultWidth by defaultHeight.
        /// </summary>
        /// <param name="imageFilePath">Relative ath to the image file from the HQ4DCS\Include directory.</param>
        /// <param name="defaultWidth">The default width, in case image doesn't exist.</param>
        /// <param name="defaultHeight">The default height, in case image doesn't exist.</param>
        /// <returns>A WinForm image.</returns>
        private Image GetImageIfItExists(string imageFilePath, int defaultWidth = 512, int defaultHeight = 512)
        {
            if (File.Exists(HQTools.PATH_INCLUDE + imageFilePath))
                return Image.FromFile(HQTools.PATH_INCLUDE + imageFilePath);

            Bitmap bitmap = new Bitmap(defaultWidth, defaultHeight);
            using (Graphics g = Graphics.FromImage(bitmap)) { g.Clear(Color.Black); }
            return bitmap;
        }
    }
}

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

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Headquarters4DCS.Forms
{
    /// <summary>
    /// Exports an HTML mission briefing to various file formats.
    /// </summary>
    public sealed class GUIBriefingExporter : IDisposable
    {
        /// <summary>
        /// Minimum image height (in pixels).
        /// </summary>
        private const int IMAGE_MIN_HEIGHT = 1024;

        /// <summary>
        /// Image width (in pixels). Equals to IMAGE_MIN_HEIGHT / sqrt(2) to ensure an A4 paper format, no matter how short the briefing is.
        /// </summary>
        private const int IMAGE_WIDTH = (int)(IMAGE_MIN_HEIGHT / 1.41421f);

        /// <summary>
        /// Margin between the rendered HTML text and the image borderh (in pixels).
        /// </summary>
        private const int IMAGE_MARGIN = 20;

        /// <summary>
        /// Background image color.
        /// </summary>
        private static readonly Color BACKGROUND_COLOR = Color.White;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GUIBriefingExporter() { }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Exports the briefing to an HTML file. Just a call to File.WriteAllText() enclosed in a try/catch.
        /// </summary>
        /// <param name="filePath">Path to the HTML file.</param>
        /// <param name="briefingHTML">HTML string of the briefing.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
        public bool ExportToHTML(string filePath, string briefingHTML)
        {
            try { File.WriteAllText(filePath, briefingHTML); }
            catch (Exception) { return false; }

            return true;
        }

        /// <summary>
        /// Exports the briefing to a JPEG file.
        /// </summary>
        /// <param name="filePath">Path to the JPEG file.</param>
        /// <param name="briefingHTML">HTML string of the briefing.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
        public bool ExportToJPEG(string filePath, string briefingHTML)
        {
            Image htmlImage = ExportToImage(briefingHTML);
            if (htmlImage == null) return false;

            bool returnValue;
            try
            {
                htmlImage.Save(filePath, ImageFormat.Jpeg);
                htmlImage.Dispose();

                returnValue = true;
            }
            catch (Exception) { returnValue = false; }

            return returnValue;
        }

        /// <summary>
        /// Exports the briefing to a PNG file.
        /// </summary>
        /// <param name="filePath">Path to the PNG file.</param>
        /// <param name="briefingHTML">HTML string of the briefing.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
        public bool ExportToPNG(string filePath, string briefingHTML)
        {
            Image htmlImage = ExportToImage(briefingHTML);
            if (htmlImage == null) return false;

            bool returnValue;
            try
            {
                htmlImage.Save(filePath, ImageFormat.Png);
                htmlImage.Dispose();

                returnValue = true;
            }
            catch (Exception) { returnValue = false; }

            return returnValue;
        }

        /// <summary>
        /// Renders an HTML page to a System.Drawing.Image.
        /// </summary>
        /// <param name="briefingHTML">HTML string of the briefing.</param>
        /// <returns>A valid System.Drawing.Image if successful, null if something went wrong.</returns>
        public Image ExportToImage(string briefingHTML, int width = IMAGE_WIDTH)
        {
            Bitmap htmlBitmap = null;

            try
            {
                // Create a huge temporary bitmap to render the HTML page to, so we can measure the actual size of the rendered page.
                Size htmlTextSize;
                using (Bitmap bmpTemp = new Bitmap(width, 4096))
                {
                    using (Graphics g = Graphics.FromImage(bmpTemp))
                    {
                        htmlTextSize = HtmlRender.MeasureGdiPlus(g, briefingHTML, IMAGE_WIDTH - IMAGE_MARGIN * 2).ToSize();
                    }
                }

                // Now we now how large the bitmap should be, create an empty white bitmap of the proper size,
                // render the HTML page to an image and draw the image on the bitmap with proper margins.
                Image htmlTextImage = HtmlRender.RenderToImageGdiPlus(briefingHTML, htmlTextSize);
                htmlBitmap = new Bitmap(IMAGE_WIDTH, Math.Max(IMAGE_MIN_HEIGHT, htmlTextSize.Height + 2 * IMAGE_MARGIN));
                using (Graphics g = Graphics.FromImage(htmlBitmap))
                {
                    g.Clear(BACKGROUND_COLOR);
                    g.DrawImage(htmlTextImage, new Point(IMAGE_MARGIN, IMAGE_MARGIN));
                }
                htmlTextImage.Dispose();
            }
            catch (Exception)
            {
                if (htmlBitmap != null) { htmlBitmap.Dispose(); htmlBitmap = null; }
            }

            return htmlBitmap;
        }

        // TODO: ExportToMultipleImagePages
    }
}

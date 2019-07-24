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

using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    /// <summary>
    /// A "toolbox" static class with some useful methods to help with the user interface.
    /// </summary>
    public static class UITools
    {
        /// <summary>
        /// "Shortcut" method to set all parameters of an OpenFileDialog and display it.
        /// </summary>
        /// <param name="fileExtension">The desired file extension.</param>
        /// <param name="initialDirectory">The initial directory of the dialog.</param>
        /// <param name="fileTypeDescription">A description of the file type (e.g. "Windows PCM wave files")</param>
        /// <returns>The path to the file to load, or null if no file was selected.</returns>
        public static string ShowOpenFileDialog(string fileExtension, string initialDirectory, string fileTypeDescription = null)
        {
            string fileName = null;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = initialDirectory;
                if (string.IsNullOrEmpty(fileTypeDescription)) fileTypeDescription = $"{fileExtension.ToUpperInvariant()} files";
                ofd.Filter = $"{fileTypeDescription} (*.{fileExtension})|*.{fileExtension}";
                if (ofd.ShowDialog() == DialogResult.OK) fileName = ofd.FileName;
            }

            return fileName;
        }

        /// <summary>
        /// "Shortcut" method to set all parameters of a SaveFileDialog and display it.
        /// </summary>
        /// <param name="fileExtension">The desired file extension.</param>
        /// <param name="initialDirectory">The initial directory of the dialog.</param>
        /// <param name="defaultFileName">The defaule file name.</param>
        /// <param name="fileTypeDescription">A description of the file type (e.g. "Windows PCM wave files")</param>
        /// <returns>The path to the file to save to, or null if no file was selected.</returns>
        public static string ShowSaveFileDialog(string fileExtension, string initialDirectory, string defaultFileName = "", string fileTypeDescription = null)
        {
            string fileName = null;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = initialDirectory;
                sfd.FileName = defaultFileName;
                if (string.IsNullOrEmpty(fileTypeDescription)) fileTypeDescription = $"{fileExtension.ToUpperInvariant()} files";
                sfd.Filter = $"{fileTypeDescription} (*.{fileExtension})|*.{fileExtension}";
                if (sfd.ShowDialog() == DialogResult.OK) fileName = sfd.FileName;
            }

            return fileName;
        }

        /// <summary>
        /// Returns an icon from an embedded resource.
        /// </summary>
        /// <param name="resourcePath">Relative path to the icon from Headquarters4DCS.Resources.</param>
        /// <returns>An icon or null if no resource was found.</returns>
        public static Icon GetIconFromResource(string resourcePath)
        {
            Icon icon = null;

            using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream($"Headquarters4DCS.Resources.{resourcePath}"))
            {
                if (stream == null) return null;
                icon = new Icon(stream);
            }

            return icon;
        }

        /// <summary>
        /// Returns an image from an embedded resource.
        /// </summary>
        /// <param name="resourcePath">Relative path to the image from Headquarters4DCS.Resources.</param>
        /// <returns>An image or null if no resource was found.</returns>
        public static Image GetImageFromResource(string resourcePath)
        {
            Image image = null;

            using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream($"Headquarters4DCS.Resources.{resourcePath}"))
            {
                if (stream == null) return null;
                image = Image.FromStream(stream);
            }

            return image;
        }

        /// <summary>
        /// Tries to load an image from a file. If the file doesn't exist, return null.
        /// </summary>
        /// <param name="filePath">Full path to the image file.</param>
        /// <returns>The image, or null if file doesn't exist.</returns>
        public static Image TryImageFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            return Image.FromFile(filePath);
        }
    }
}

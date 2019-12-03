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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    /// <summary>
    /// A "toolbox" static class with some useful methods to help with the user interface.
    /// </summary>
    public static class GUITools
    {
        /// <summary>
        /// Path to the assembly namespace where embedded resources are stored.
        /// </summary>
        private const string EMBEDDED_RESOURCES_PATH = "Headquarters4DCS.Resources.";

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

            using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream($"{EMBEDDED_RESOURCES_PATH}{resourcePath}"))
            {
                if (stream == null) return null;
                image = Image.FromStream(stream);
            }

            return image;
        }

        /// <summary>
        /// Turns a camel cased enum name into a string with spaces between words.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="enumValue">Enum value</param>
        /// <returns>A string</returns>
        public static string SplitEnumCamelCase<T>(T enumValue) where T : struct, IConvertible
        {
            string enumString = enumValue.ToString();
            if (typeof(T) == typeof(TimePeriod)) enumString = enumString.Substring("Decade".Length) + "s";

            string[] words = Regex.Split(enumString, "(?<!(^|[A-Z]))(?=[A-Z])|(?<!^)(?=[A-Z][a-z])");
            for (int i = 1; i < words.Length; i++) words[i] = words[i].ToLowerInvariant();
            return string.Join(" ", words);
        }

        /// <summary>
        /// Join the words from a string split by GUITools.SplitEnumCamelCase and turn it back into an enum value
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="enumString">A string split by SplitEnumCamelCase</param>
        /// <returns>A value of enum T</returns>
        public static T JoinEnumCamelCase<T>(string enumString) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(enumString)) return default(T);
            enumString = enumString.Replace(" ", "");
            if (typeof(T) == typeof(TimePeriod)) enumString = "Decade" + enumString.Substring(0, enumString.Length - 1);
            if (Enum.TryParse(enumString, true, out T parsedEnum)) return parsedEnum;
            return default(T);
        }

    }
}

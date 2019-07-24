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

using System.Text.RegularExpressions;

namespace Headquarters4DCS.Library
{
    /// <summary>
    /// Stores a language. Unlike other definitions, this class just encapsulates the INIFile containing all localized strings.
    /// </summary>
    public sealed class DefinitionLanguage : Definition
    {
        /// <summary>
        /// The definition INI file.
        /// </summary>
        private INIFile LanguageINI = null;

        /// <summary>
        /// A shortcut for GetString("Misc", "Semicolon");
        /// </summary>
        public string Semicolon { get { return GetString("Misc", "Semicolon"); } }

        /// <summary>
        /// Loads data required by this definition.
        /// </summary>
        /// <param name="ini">The ini file to load from.</param>
        /// <returns>True is successful, false if an error happened.</returns>
        protected override bool OnLoad(string path)
        {
            LanguageINI = new INIFile(path);
            return true;
        }

        /// <summary>
        /// Returns a localized string.
        /// </summary>
        /// <param name="section">The ini file section.</param>
        /// <param name="key">The ini file key.</param>
        /// <returns>The string, or "string.[key]" if the string was null or empty.</returns>
        public string GetString(string section, string key)
        {
            string str = LanguageINI.GetValue<string>(section, key);
            return string.IsNullOrEmpty(str) ? $"string.{key}" : str;
        }

        /// <summary>
        /// Returns an array of strings.
        /// </summary>
        /// <param name="section">The ini file section.</param>
        /// <param name="key">The ini file key.</param>
        /// <param name="separator">The separator character to use between strings. Default is ",".</param>
        /// <returns>The string array, or an array of size 1 with "string.[key]" as its only value if the array was empty.</returns>
        public string[] GetStringArray(string section, string key, char? separator = null)
        {
            string[] strArray = LanguageINI.GetValueArray<string>(section, key, separator);
            return strArray.Length == 0 ? new string[] { $"string.{key}" } : strArray;
        }

        /// <summary>
        /// Returns a randomized string from a properly formatted string.
        /// Format is: "This is a string where {an element|another element|yet another element} is selected."
        /// </summary>
        /// <param name="section">The ini file section.</param>
        /// <param name="key">The ini file key.</param>
        /// <returns></returns>
        public string GetStringRandom(string section, string key)
        {
            string str = LanguageINI.GetValue<string>(section, key);
            if (string.IsNullOrEmpty(str)) return $"string.{key}";
            return ParseRandomString(str);
        }

        /// <summary>
        /// Returns a string array containing all unit names.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLocalizedUnitTypes()
        {
            return LanguageINI.GetKeysInSection("UnitNames");
        }

        /// <summary>
        /// Randomizes parts of a string.
        /// </summary>
        /// <param name="rndStr">The string to randomize</param>
        /// <returns>A randomized string.</returns>
        private string ParseRandomString(string rndStr)
        {
            Regex regex = new Regex("{.*?}");
            if (!regex.IsMatch(rndStr))
                return rndStr.Replace("{", "").Replace("}", "").Replace("|", "");

            int lastOpen = -1;

            for (int i = 0; i < rndStr.Length; i++)
            {
                char c = rndStr[i];

                if (c == '{') lastOpen = i;
                else if (c == '}')
                {
                    if (lastOpen == -1) continue;

                    string subStr = rndStr.Substring(lastOpen + 1, i - lastOpen - 1);
                    subStr = HQTools.RandomFrom(subStr.Split('|'));

                    string outStr = rndStr.Substring(0, lastOpen) + subStr + rndStr.Substring(i + 1);
                    return ParseRandomString(outStr);
                }
            }

            return rndStr.Replace("{", "").Replace("}", "").Replace("|", "");
        }

        /// <summary>
        /// Returns a string representing the ordinal adjective (1st, 2nd, 3rd, 4th...) for a given integer.
        /// </summary>
        /// <param name="number">The integer.</param>
        /// <returns>A string with the ordinal adjective.</returns>
        public string GetOrdinalAdjective(int number)
        {
            string template = "";
            string numberStr = HQTools.ValToString(number);

            for (int i = 0; i < numberStr.Length; i++)
            {
                template = LanguageINI.GetValue<string>("OrdinalAdjectives", $"EndsWith{numberStr.Substring(i)}");
                if (!string.IsNullOrEmpty(template)) break;
            }

            if (string.IsNullOrEmpty(template))
                template = LanguageINI.GetValue<string>("OrdinalAdjectives", "Default");

            return template.Replace("$N$", numberStr);
        }
    }
}

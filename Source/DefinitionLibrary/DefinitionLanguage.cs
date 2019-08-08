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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Stores a language. Unlike other definitions, this class just encapsulates the INIFile containing all localized strings.
    /// </summary>
    public sealed class DefinitionLanguage : Definition
    {
        /// <summary>
        /// Default separator to use between sections and keys in the MakeStringKey method.
        /// </summary>
        private const string SECTION_KEY_SEPARATOR = "***";

        /// <summary>
        /// A shortcut for GetString("Misc", "Semicolon");
        /// </summary>
        public string Semicolon { get { return GetString("Misc", "Semicolon"); } }

        /// <summary>
        /// Main language dictionary. All strings are stored here.
        /// </summary>
        private readonly Dictionary<string, string> Strings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Loads strings from all .ini files in the provided path.
        /// </summary>
        /// <param name="path">The path to load from.</param>
        /// <returns>True is successful, false if an error happened.</returns>
        protected override bool OnLoad(string path)
        {
            Strings.Clear();

            foreach (string iniFilePath in Directory.GetFiles(path, "*.ini"))
                LoadStringsFromINIFile(iniFilePath);

            return true;
        }

        /// <summary>
        /// Loads all strings for 
        /// </summary>
        /// <param name="iniFilePath"></param>
        private void LoadStringsFromINIFile(string iniFilePath)
        {
            using (INIFile ini = new INIFile(iniFilePath))
            {
                foreach (string section in ini.GetSections())
                {
                    foreach (string key in ini.GetKeysInSection(section))
                    {
                        string stringKey = MakeStringKey(section, key);

                        if (Strings.ContainsKey(stringKey))
                            Strings[stringKey] = ini.GetValue(section, key, "");
                        else
                            Strings.Add(stringKey, ini.GetValue(section, key, ""));
                    }
                }
            }
        }

        /// <summary>
        /// Retruns a default string in the [{section}.{key}] format is no string was found.
        /// </summary>
        /// <param name="section">String's section.</param>
        /// <param name="key">String's key.</param>
        /// <returns>A default string in the [{section}.{key}] format.</returns>
        private string MakeDefaultValue(string section, string key)
        {
            return $"[{section}.{key}]";
        }

        /// <summary>
        /// Merges a section and key strings into a single key for use in the Strings dictionary.
        /// </summary>
        /// <param name="section">String's section.</param>
        /// <param name="key">String's key.</param>
        /// <returns>A single string to use as a key for the Strings dictionary, where section and key a concatenaded with SECTION_KEY_SEPARATOR in between.</returns>
        private string MakeStringKey(string section, string key)
        {
            return $"{section}{SECTION_KEY_SEPARATOR}{key}";
        }

        /// <summary>
        /// Searches for a key in the Strings dictionary. Used by all public GetString* methods.
        /// </summary>
        /// <param name="section">String's section.</param>
        /// <param name="key">String's key.</param>
        /// <returns>The string, or null if string wasn't found.</returns>
        private string FindString(string section, string key)
        {
            string stringKey = MakeStringKey(section, key);
            if (Strings.ContainsKey(stringKey)) return Strings[stringKey];
            return null;
        }

        /// <summary>
        /// Returns a localized string from the UserInterface section. A shortcut method for GetString("UserInterface"...)
        /// </summary>
        /// <param name="key">The ini file key.</param>
        /// <param name="replacements">A even-numbered list of replacements to make in the string. #0 is the dollar-enclosed uppercase key (without the $ signs), #1 is the value to replace the key with, #2 is the next key, etc.</param>
        /// <returns>The string, or "string.[key]" if the string was null or empty.</returns>
        public string GetStringUI(string key, params string[] replacements) { return GetString("UserInterface", key, replacements); }

        /// <summary>
        /// Returns a localized string.
        /// </summary>
        /// <param name="section">String's section.</param>
        /// <param name="key">String's key.</param>
        /// <param name="replacements">A even-numbered list of replacements to make in the string. #0 is the dollar-enclosed uppercase key (without the $ signs), #1 is the value to replace the key with, #2 is the next key, etc.</param>
        /// <returns>The string, or a defaule string if the string was null or empty.</returns>
        public string GetString(string section, string key, params string[] replacements)
        {
            string value = FindString(section, key);
            if (string.IsNullOrEmpty(value)) return MakeDefaultValue(section, key);
            value = DoReplacements(value, replacements);
            return value;
        }

        /// <summary>
        /// Searches for special keys (uppercase words encased in dollar signes, e.g. "$NAME$") to replace with various values in a string.
        /// </summary>
        /// <param name="baseString">Base string in which to perform replacements</param>
        /// <param name="replacements">A even-numbered list of replacements to make in the string. #0 is the dollar-enclosed uppercase key (without the $ signs), #1 is the value to replace the key with, #2 is the next key, etc.</param>
        /// <returns>A string with all replacements done</returns>
        private string DoReplacements(string baseString, string[] replacements)
        {
            for (int i = 0; i < replacements.Length - 1; i++)
                baseString = baseString.Replace($"${replacements[i].ToUpperInvariant()}$", replacements[i + 1]);

            return baseString;
        }

        /// <summary>
        /// Returns an array of strings.
        /// </summary>
        /// <param name="section">String's section.</param>
        /// <param name="key">String's key.</param>
        /// <param name="separator">The separator character to use between strings. Default is ",".</param>
        /// <returns>The string array, or an array of size 1 with "string.[key]" as its only value if the array was empty.</returns>
        public string[] GetStringArray(string section, string key, char separator = ',')
        {
            string value = FindString(section, key);
            if (!string.IsNullOrEmpty(value)) return value.Split(separator);
            return new string[0];
        }

        /// <summary>
        /// Returns a randomized string from a properly formatted string.
        /// Format is: "This is a string where {an element|another element|yet another element} is selected."
        /// </summary>
        /// <param name="section">The ini file section.</param>
        /// <param name="key">The ini file key.</param>
        /// <param name="replacements">A even-numbered list of replacements to make in the string. #0 is the dollar-enclosed uppercase key (without the $ signs), #1 is the value to replace the key with, #2 is the next key, etc.</param>
        /// <returns></returns>
        public string GetStringRandom(string section, string key, params string[] replacements)
        {
            string value = FindString(section, key);
            if (string.IsNullOrEmpty(value)) return MakeDefaultValue(section, key);
            value = ParseRandomString(value);
            value = DoReplacements(value, replacements);
            return value;
        }

        ///// <summary>
        ///// Returns a string array containing all unit IDs.
        ///// </summary>
        ///// <returns>An array of strings</returns>
        public string[] GetAllLocalizedUnitTypes()
        {
            return (from string k in Strings.Keys where k.StartsWith($"UnitTypes{SECTION_KEY_SEPARATOR}") select k.Substring(9 + SECTION_KEY_SEPARATOR.Length)).ToArray();
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
        /// Returns the localized name of a DCS World unit.
        /// </summary>
        /// <param name="unitType">The internal DCS World unit type name.</param>
        /// <param name="useNATOcallsigns">Should units be named according to their NATO callsigns when available (e.g. SA-10, not S-300)?</param>
        /// <returns>Localized name of the unit.</returns>
        public string GetUnitName(string unitType, bool useNATOcallsigns)
        {
            string[] unitNames = GetStringArray("UnitNames", unitType, '|');

            switch (unitNames.Length)
            {
                case 0: return "";
                case 1: return unitNames[0];
                default: return unitNames[useNATOcallsigns ? 0 : 1];
            }
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
                template = FindString("OrdinalAdjectives", $"EndsWith{numberStr.Substring(i)}");
                if (!string.IsNullOrEmpty(template)) break;
            }

            if (string.IsNullOrEmpty(template))
                template = GetString("OrdinalAdjectives", "Default");

            return template.Replace("$N$", numberStr);
        }

        public string GetEnum<T>(T enumValue) where T : struct, IConvertible
        {
            return GetString("Enumerations", $"{typeof(T).Name}.{enumValue}");
        }
    }
}

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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Headquarters4DCS
{
    /// <summary>
    /// Stores a series of key/value pairs read from a .ini file.
    /// </summary>
    public sealed class INIFile : IDisposable
    {
        /// <summary>
        /// A list of values.
        /// </summary>
        private readonly List<INIFileValue> Values = new List<INIFileValue>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public INIFile() { }

        /// <summary>
        /// Constructor. Loads values from the ini file passed as parameter.
        /// </summary>
        /// <param name="filePath">Path to the ini file.</param>
        public INIFile(string filePath) { LoadFromFile(filePath); }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { Clear(); }

        /// <summary>
        /// Clear all values.
        /// </summary>
        public void Clear() { Values.Clear(); }

        /// <summary>
        /// Loads the values for the ini file passes in parameter.
        /// </summary>
        /// <param name="filePath">Path to the ini file.</param>
        public void LoadFromFile(string filePath)
        {
            Clear();

            if (!File.Exists(filePath)) return;

            string iniString = File.ReadAllText(filePath, Encoding.UTF8);

            ParseINIString(iniString);
        }

        /// <summary>
        /// Saves all values to an ini file.
        /// </summary>
        /// <param name="filePath">Path to the ini file.</param>
        /// <returns>True if everything went right, false if something went wrong.</returns>
        public bool SaveToFile(string filePath)
        {
            try
            {
                string fileContent = "";

                Values.Sort();
                string lastSection = null;

                foreach (INIFileValue val in Values)
                {
                    if (lastSection != val.Section)
                    {
                        if (lastSection != null) fileContent += "\r\n";
                        fileContent += $"[{val.Section.ToUpperInvariant()}]\r\n";
                        lastSection = val.Section;
                    }

                    fileContent += $"{val.Key}={val.Value}\r\n";
                }

                File.WriteAllText(filePath, fileContent, Encoding.UTF8);
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Returns the name of all sections in the INI file.
        /// </summary>
        /// <returns>The sections.</returns>
        public string[] GetSections()
        {
            return (from INIFileValue v in Values select v.Section).Distinct().OrderBy(x => x).ToArray();
        }

        /// <summary>
        /// Gets a value from the ini file.
        /// </summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="section">The section the value belongs to.</param>
        /// <param name="key">The key to the value.</param>
        /// <param name="defaultValue">The default value to return if no value was found. Optional.</param>
        /// <returns>The value, or defaultValue if the value doesn't exist.</returns>
        public T GetValue<T>(string section, string key, T defaultValue = default(T))
        {
            if (!ValueExists(section, key))
            {
                if ((defaultValue == null) && (typeof(T) == typeof(string))) return (T)Convert.ChangeType("", typeof(T));
                return defaultValue;
            }

            object val = ReadValue(section, key) ?? "";

            try
            {
                if (typeof(T) == typeof(string)) val = val.ToString();
                else if (typeof(T) == typeof(bool)) val = HQTools.StringToBool((string)val);
                else if (typeof(T) == typeof(int)) val = HQTools.StringToInt((string)val);
                else if (typeof(T) == typeof(float)) val = HQTools.StringToFloat((string)val);
                else if (typeof(T) == typeof(double)) val = HQTools.StringToDouble((string)val);
                else if (typeof(T) == typeof(Coordinates)) val = new Coordinates(val.ToString());
                else if (typeof(T) == typeof(MinMaxI)) val = new MinMaxI(val.ToString());
                else if (typeof(T) == typeof(MinMaxD)) val = new MinMaxD(val.ToString());
                else if (typeof(T).IsEnum) val = Enum.Parse(typeof(T), val.ToString(), true);

                return (T)Convert.ChangeType(val, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Reads all values in a section as string arrays and returns one big joined array with all values.
        /// </summary>
        /// <param name="section">The section from where keys should be read.</param>
        /// <param name="separator">Character used to separate strings. Default is ','.</param>
        /// <returns>The string array.</returns>
        public string[] GetAllValuesInSectionAsStringArray(string section, char? separator = null)
        {
            List<string> allStrings = new List<string>();
            foreach (string k in GetKeysInSection(section))
                allStrings.AddRange(GetValueArray<string>(section, k, separator));

            return allStrings.ToArray();
        }

        /// <summary>
        /// Gets an array value from the ini file.
        /// </summary>
        /// <typeparam name="T">The type of value array to return.</typeparam>
        /// <param name="section">The section the value array belongs to.</param>
        /// <param name="key">The key to the value array.</param>
        /// <param name="separator">The character used to separate values. Optional.</param>
        /// <returns>The value array, or an empty array of type T if the value doesn't exist.</returns>
        public T[] GetValueArray<T>(string section, string key, char? separator = null)
        {
            if (string.IsNullOrEmpty(GetValue<string>(section, key))) return new T[0];

            object val = ReadValue(section, key) ?? "";

            if (!separator.HasValue)
            {
                if (typeof(T) == typeof(Coordinates)) separator = ';';
                else separator = ',';
            }

            try
            {
                if (typeof(T) == typeof(string)) val = val.ToString().Split(separator.Value);
                else if (typeof(T) == typeof(bool)) val = ConvertArray<bool>(val.ToString().Split(separator.Value));
                else if (typeof(T) == typeof(int)) val = ConvertArray<int>(val.ToString().Split(separator.Value));
                else if (typeof(T) == typeof(float)) val = ConvertArray<float>(val.ToString().Split(separator.Value));
                else if (typeof(T) == typeof(double)) val = ConvertArray<double>(val.ToString().Split(separator.Value));
                else if (typeof(T).IsEnum) val = ConvertArray<T>(val.ToString().Split(separator.Value));

                return (T[])Convert.ChangeType(val, typeof(T[]));
            }
            catch (Exception)
            {
                return default(T[]);
            }
        }

        /// <summary>
        /// Sets or update a value in the ini file.
        /// </summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="section">The section the value belongs to.</param>
        /// <param name="key">The key to the value.</param>
        /// <param name="val">The value.</param>
        public void SetValue<T>(string section, string key, T val)
        {
            object oVal = val;

            if (typeof(T) == typeof(string)) WriteValue(section, key, (string)oVal);
            else if (typeof(T) == typeof(bool)) WriteValue(section, key, HQTools.ValToString((bool)oVal));
            else if (typeof(T) == typeof(int)) WriteValue(section, key, HQTools.ValToString((int)oVal));
            else if (typeof(T) == typeof(float)) WriteValue(section, key, HQTools.ValToString((float)oVal));
            else if (typeof(T) == typeof(double)) WriteValue(section, key, HQTools.ValToString((double)oVal));
            else WriteValue(section, key, val.ToString());
        }

        public void SetValueArray<T>(string section, string key, T[] val, char separator = ',')
        {
            object oVal = val;

            if (typeof(T) == typeof(string)) WriteValue(section, key, string.Join(separator.ToString(), (string[])oVal));
            else WriteValue(section, key, val.ToString());
        }

        /// <summary>
        /// Gets the names of all keys in a section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>The keys, as a string array.</returns>
        public string[] GetKeysInSection(string section)
        {
            if (string.IsNullOrEmpty(section)) return new string[0];
            section = section.ToLowerInvariant();
            return (from INIFileValue v in Values where v.Section == section select v.Key).Distinct().OrderBy(x => x).ToArray();
        }

        /// <summary>
        /// Gets all "top level" keys in a section.
        /// "Top-level" keys are parts of the key names before the first dot.
        /// (e.g. if the sections contains "value1.name", "value1.cost" and "value2.name", top level values will be "value1" and "value2")
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>The top level keys, as a string array.</returns>
        public string[] GetTopLevelKeysInSection(string section)
        {
            List<string> keys = new List<string>(GetKeysInSection(section));

            for (int i = 0; i < keys.Count; i++)
                if (keys[i].Contains("."))
                    keys[i] = keys[i].Substring(0, keys[i].IndexOf("."));

            return keys.Distinct().ToArray();
        }

        /// <summary>
        /// Does a value exist?
        /// </summary>
        /// <param name="section">The section the value belongs to.</param>
        /// <param name="key">The key to the value.</param>
        /// <returns>True if the value exists, false if it doesn't.</returns>
        public bool ValueExists(string section, string key)
        {
            return ReadValue(section, key) != null;
        }

        /// <summary>
        /// Reads a value as a string.
        /// </summary>
        /// <param name="section">The section the value belongs to.</param>
        /// <param name="key">The key to the value.</param>
        /// <returns>The value, or null if value doesn't exist.</returns>
        private string ReadValue(string section, string key)
        {
            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key)) return null;
            section = section.ToLowerInvariant(); key = key.ToLowerInvariant();

            IEnumerable<INIFileValue> values = (from INIFileValue v in Values where v.Section == section && v.Key == key select v);
            if (values.Count() == 0) return null;
            return values.First().Value;
        }

        /// <summary>
        /// Adds or updates a value.
        /// </summary>
        /// <param name="section">The section the value belongs to.</param>
        /// <param name="key">The key to the value.</param>
        /// <param name="value">The value, as as string</param>
        /// <returns>True if everything went right, false if something went wrong.</returns>
        private bool WriteValue(string section, string key, string value)
        {
            section = (section ?? "").ToLowerInvariant().Trim();
            key = (key ?? "").ToLowerInvariant().Trim();
            value = value ?? "";

            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key)) return false;

            Values.Add(new INIFileValue(section, key, value));
            return true;
        }

        /// <summary>
        /// Parses the ini file string to 
        /// </summary>
        /// <param name="iniString">The content of the ini file, as a single string.</param>
        private void ParseINIString(string iniString)
        {
            string[] lines = (iniString + "\n").Replace("\r\n", "\n").Split('\n');

            Values.Clear();
            string section = null;

            foreach (string li in lines)
            {
                string l = li.Trim(' ', '\t'); // Trim line
                if (l.StartsWith(";")) continue; // Line is a comment

                if (l.StartsWith("[")) // found a new section
                {
                    // try to get section name, make sure it's valid
                    section = l.Trim('[', ']', ' ', '\t').ToLowerInvariant();
                    if (string.IsNullOrEmpty(section)) { section = null; continue; }

                    continue;
                }

                if (l.Contains('=')) // The line contains an "equals" sign, it means we found a value
                {
                    if (section == null) continue; // we're not in a section, ignore

                    string[] v = l.Split(new char[] { '=' }, 2); // Split the line at the first "equal" sign: key = value
                    if (v.Length < 2) continue;

                    string key = v[0].ToLowerInvariant();
                    if (string.IsNullOrEmpty(key)) continue;

                    Values.Add(new INIFileValue(section, key, v[1]));
                }
            }
        }

        /// <summary>
        /// Converts a string array to an array of type T.
        /// </summary>
        /// <typeparam name="T">The type of the source array.</typeparam>
        /// <param name="sourceArray">The source string array.</param>
        /// <returns>The converted array</returns>
        private T[] ConvertArray<T>(string[] sourceArray)
        {
            try
            {
                T[] arr = new T[sourceArray.Length];

                for (int i = 0; i < sourceArray.Length; i++)
                {
                    object o = default(T);

                    if (typeof(T) == typeof(bool)) o = HQTools.StringToBool(sourceArray[i]);
                    else if (typeof(T) == typeof(int)) o = HQTools.StringToInt(sourceArray[i]);
                    else if (typeof(T) == typeof(double)) o = HQTools.StringToDouble(sourceArray[i]);
                    else if (typeof(T) == typeof(float)) o = HQTools.StringToFloat(sourceArray[i]);
                    else if (typeof(T).IsEnum) o = Enum.Parse(typeof(T), sourceArray[i].ToString(), true);

                    arr[i] = (T)Convert.ChangeType(o, typeof(T));
                }

                return arr;
            }
            catch (Exception)
            {
                return new T[0];
            }
        }
    }
}

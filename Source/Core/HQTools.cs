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
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Headquarters4DCS
{
    /// <summary>
    /// A static "toolbox" class with various methods and constants used by HQ4DCS
    /// </summary>
    public static class HQTools
    {
        /// <summary>
        /// The total count of mission script scopes.
        /// </summary>
        public static readonly int MISSION_SCRIPT_SCOPE_COUNT = Enum.GetValues(typeof(FeatureScriptScope)).Length;

        /// <summary>
        /// Flags required to center text properly.
        /// </summary>
        public const TextFormatFlags CENTER_TEXT_FLAGS = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;

        /// <summary>
        /// The project website's URL.
        /// </summary>
        public const string WEBSITE_URL = "https://akaAgar.github.io/headquarters-for-dcs";

        /// <summary>
        /// Degrees to radians multipier constant.
        /// </summary>
        public const double DEGREES_TO_RADIANS = 0.0174533;

        /// <summary>
        /// Radians to degrees multipier constant.
        /// </summary>
        public const double RADIANS_TO_DEGREES = 57.2958;

        /// <summary>
        /// Meters to nautical miles multipier constant.
        /// </summary>
        public const double METERS_TO_NM = 0.000539957;

        /// <summary>
        /// Nautical miles to meters multipier constant.
        /// </summary>
        public const double NM_TO_METERS = 1852.0;

        /// <summary>
        /// The total number of seconds in a day.
        /// </summary>
        public const int SECONDS_PER_DAY = 24 * 3600;

        /// <summary>
        /// Feet to meters multiplier.
        /// </summary>
        public const double FEET_TO_METERS = 0.3048;

        /// <summary>
        /// Knots to meters per second multiplier.
        /// </summary>
        public const double KNOTS_TO_METERSPERSECOND = 0.514444;

        /// <summary>
        /// The default language for missions and UI. HQ4DCS will not start if this language is not found in Library\Languages.
        /// </summary>
        public const string DEFAULT_LANGUAGE = "English";


        /// <summary>
        /// Path to the application.
        /// </summary>
        public static readonly string PATH = NormalizeDirectoryPath(Application.StartupPath);

#if DEBUG
        /// <summary>
        /// Path to the "debug output" directory.
        /// </summary>
        public static readonly string PATH_DEBUG = PATH + "(DebugOutput)\\";
#endif

        /// <summary>
        /// Path to the Include subdirectory.
        /// </summary>
        public static readonly string PATH_INCLUDE = PATH + "Include\\";

        /// <summary>
        /// Path to the Library subdirectory.
        /// </summary>
        public static readonly string PATH_LIBRARY = PATH + "Library\\";

        /// <summary>
        /// Path to the Logs subdirectory.
        /// </summary>
        public static readonly string PATH_LOGS = PATH + "Logs\\";

        /// <summary>
        /// Path to the Media subdirectory.
        /// </summary>
        public static readonly string PATH_MEDIA = PATH + "Media\\";

        /// <summary>
        /// Path to the Templates subdirectory.
        /// </summary>
        public static readonly string PATH_TEMPLATES = PATH + "Templates\\";

        /// <summary>
        /// Path to the Windows user directory.
        /// </summary>
        public static readonly string PATH_USER = NormalizeDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

        /// <summary>
        /// Path to the Windows "My Documents" directory.
        /// </summary>
        public static readonly string PATH_USER_DOCS = NormalizeDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        /// <summary>
        /// An instance of the Random class for all randomization methods.
        /// </summary>
        private static readonly Random Rnd = new Random();

        /// <summary>
        /// Parses a string to an array of enums.
        /// </summary>
        /// <typeparam name="T">The type of enum to parse to.</typeparam>
        /// <param name="enumString">The string.</param>
        /// <param name="separator">The character used to separate values. Default is comma (,).</param>
        /// <param name="prefix">A prefix to add at the beginning of each value in the string. Default is none.</param>
        /// <returns>An array of enums of type T.</returns>
        public static T[] ParseEnumString<T>(string enumString, char separator = ',', string prefix = "") where T : struct
        {
            if ((enumString == null) || (enumString.Length == 0)) return new T[0];

            string[] strParts = enumString.Split(separator);

            List<T> enumValues = new List<T>();
            foreach (string s in strParts)
            {
                if (Enum.TryParse(prefix + s, true, out T e))
                    enumValues.Add(e);
            }

            return enumValues.ToArray();
        }

        /// <summary>
        /// Returns the number of values in an enum. Basically a shortcut for "Enum.GetValues(typeof(T)).Length".
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <returns>The number of values.</returns>
        public static int EnumCount<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        /// <summary>
        /// Returns the UnitCategory an UnitFamily belongs to.
        /// </summary>
        /// <param name="family">The unit family.</param>
        /// <returns>The unit category.</returns>
        public static UnitCategory GetUnitCategoryFromUnitFamily(UnitFamily family)
        {
            string roleStr = family.ToString().ToLowerInvariant();

            if (roleStr.StartsWith("helicopter")) return UnitCategory.Helicopter;
            if (roleStr.StartsWith("plane")) return UnitCategory.Plane;
            if (roleStr.StartsWith("ship")) return UnitCategory.Ship;
            if (roleStr.StartsWith("static")) return UnitCategory.Static;
            return UnitCategory.Vehicle;
        }

        /// <summary>
        /// Removes invalid/reserved Windows path characters from a filename.
        /// </summary>
        /// <param name="fileName">The filename.</param>
        /// <returns>The filename without any </returns>
        public static string RemoveInvalidFileNameCharacters(string fileName)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(fileName, "");
        }

        /// <summary>
        /// Creates a directory if it doesn't exist already.
        /// </summary>
        /// <param name="path">The directory path.</param>
        /// <returns>True if successful or if directory was already present, false if creation was required and failed.</returns>
        public static bool CreateDirectoryIfMissing(string path)
        {
            if (Directory.Exists(path)) return true;

            try { Directory.CreateDirectory(path); }
            catch (Exception) { return false; }

            return true;
        }

        /// <summary>
        /// Converts a boolean to a string. Basically, a shortcut for ToString(NumberFormatInfo.InvariantInfo).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value as a string.</returns>
        public static string ValToString(bool value)
        {
            return value.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Returns a System.Drawing.Image as bytes for an image file of the specified format.
        /// </summary>
        /// <param name="image">An image</param>
        /// <param name="format">An image format</param>
        /// <returns>Bytes of an image file</returns>
        public static byte[] ImageToBytes(Image image, ImageFormat format)
        {
            byte[] imageBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                imageBytes = ms.ToArray();
            }

            return imageBytes;
        }

        /// <summary>
        /// Converts an integer to a string. Basically, a shortcut for ToString(NumberFormatInfo.InvariantInfo).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value as a string.</returns>
        public static string ValToString(int value, string stringFormat = null)
        {
            if (string.IsNullOrEmpty(stringFormat))
                return value.ToString(NumberFormatInfo.InvariantInfo);
            else
                return value.ToString(stringFormat, NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Replaces all instance of "$KEY$" in a Lua script by value.
        /// </summary>
        /// <param name="lua">The Lua script.</param>
        /// <param name="key">The key to replace, without the dollar signs.</param>
        /// <param name="value">The value to replace the key with.</param>
        public static void ReplaceKey(ref string lua, string key, bool value) { ReplaceKey(ref lua, key, HQTools.ValToString(value).ToLowerInvariant()); }

        /// <summary>
        /// Replaces all instance of "$KEY$" in a Lua script by value.
        /// </summary>
        /// <param name="lua">The Lua script.</param>
        /// <param name="key">The key to replace, without the dollar signs.</param>
        /// <param name="value">The value to replace the key with.</param>
        /// <param name="stringFormat">The string format string to use when converting the value to a string.</param>
        public static void ReplaceKey(ref string lua, string key, int value, string stringFormat = null) { ReplaceKey(ref lua, key, HQTools.ValToString(value, stringFormat)); }

        /// <summary>
        /// Replaces all instance of "$KEY$" in a Lua script by value.
        /// </summary>
        /// <param name="lua">The Lua script.</param>
        /// <param name="key">The key to replace, without the dollar signs.</param>
        /// <param name="value">The value to replace the key with.</param>
        /// <param name="stringFormat">The string format string to use when converting the value to a string.</param>
        public static void ReplaceKey(ref string lua, string key, float value, string stringFormat = null) { ReplaceKey(ref lua, key, HQTools.ValToString(value, stringFormat)); }

        /// <summary>
        /// Replaces all instance of "$KEY$" in a Lua script by value.
        /// </summary>
        /// <param name="lua">The Lua script.</param>
        /// <param name="key">The key to replace, without the dollar signs.</param>
        /// <param name="value">The value to replace the key with.</param>
        /// <param name="stringFormat">The string format string to use when converting the value to a string.</param>
        public static void ReplaceKey(ref string lua, string key, double value, string stringFormat = null) { ReplaceKey(ref lua, key, HQTools.ValToString(value, stringFormat)); }

        /// <summary>
        /// Replaces all instance of "$KEY$" in a Lua script by value.
        /// </summary>
        /// <param name="lua">The Lua script.</param>
        /// <param name="key">The key to replace, without the dollar signs.</param>
        /// <param name="value">The value to replace the key with.</param>
        public static void ReplaceKey(ref string lua, string key, string value)
        { lua = lua.Replace($"${key.ToUpperInvariant()}$", value); }

        /// <summary>
        /// Converts a double to a string. Basically, a shortcut for ToString(NumberFormatInfo.InvariantInfo).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value, as a string.</returns>
        public static string ValToString(double value, string stringFormat = null)
        {
            if (string.IsNullOrEmpty(stringFormat))
                return value.ToString(NumberFormatInfo.InvariantInfo);
            else
                return value.ToString(stringFormat, NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a string to a double. Basically, a shortcut for Convert.ToDouble(NumberFormatInfo.InvariantInfo).
        /// </summary>
        /// <param name="stringValue">The string to convert.</param>
        /// <param name="defaultValue">The default value to return if the string parsing fails.</param>
        /// <returns>The double.</returns>
        public static double StringToDouble(string stringValue, double defaultValue = 0.0)
        {
            try { return Convert.ToDouble(stringValue.Trim(), NumberFormatInfo.InvariantInfo); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts a string to a boolean. Basically, a shortcut for Convert.ToBoolean(NumberFormatInfo.InvariantInfo).
        /// </summary>
        /// <param name="stringValue">The string to convert.</param>
        /// <param name="defaultValue">The default value to return if the string parsing fails.</param>
        /// <returns>The bool.</returns>
        public static bool StringToBool(string stringValue, bool defaultValue = false)
        {
            try { return Convert.ToBoolean(stringValue, NumberFormatInfo.InvariantInfo); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts a string to a float. Basically, a shortcut for Convert.ToSingle(NumberFormatInfo.InvariantInfo).
        /// </summary>
        /// <param name="stringValue">The string to convert.</param>
        /// <param name="defaultValue">The default value to return if the string parsing fails.</param>
        /// <returns>The float.</returns>
        public static float StringToFloat(string stringValue, float defaultValue = 0.0f)
        {
            try { return Convert.ToSingle(stringValue.Trim(), NumberFormatInfo.InvariantInfo); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts a string to an integer. Basically, a shortcut for Convert.ToInt32(NumberFormatInfo.InvariantInfo).
        /// </summary>
        /// <param name="stringValue">The string to convert.</param>
        /// <param name="defaultValue">The default value to return if the string parsing fails.</param>
        /// <returns>The integer.</returns>
        public static int StringToInt(string stringValue, int defaultValue = 0)
        {
            try { return Convert.ToInt32(stringValue.Trim(), NumberFormatInfo.InvariantInfo); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Search for the DCS world custom mission path ([User]\Saved Games\DCS\Missions\)
        /// Looks first for DCS.earlyaccess, then DCS.openbeta, then DCS.
        /// </summary>
        /// <returns>The path, or the user My document folder if none is found.</returns>
        public static string GetDCSMissionPath()
        {
            string[] possibleDCSPaths = new string[] { "DCS.earlyaccess", "DCS.openbeta", "DCS" };

            for (int i = 0; i < possibleDCSPaths.Length; i++)
            {
                string dcsPath = PATH_USER + "Saved Games\\" + possibleDCSPaths[i] + "\\Missions\\";
                if (Directory.Exists(dcsPath)) return dcsPath;
            }

            return PATH_USER_DOCS;
        }

        /// <summary>
        /// Normalize a Windows directory path. Make sure all slashes are backslashes and that the directory ends with a backslash.
        /// </summary>
        /// <param name="path">The directory path to normalize.</param>
        /// <returns>The normalized directory path.</returns>
        public static string NormalizeDirectoryPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return "";
            return path.Replace('/', '\\').TrimEnd('\\') + "\\";
        }

        /// <summary>
        /// If an AmountNR is set to "Random", returns a random value, else return the original AmountNR value.
        /// </summary>
        /// <param name="amount">The AmountNR to check for random values.</param>
        /// <returns>A non-random AmountNR</returns>
        public static AmountNR ResolveRandomAmount(AmountNR amount)
        {
            if (amount != AmountNR.Random) return amount;
            return (AmountNR)RandomInt(EnumCount<AmountNR>() - 1);
        }

        /// <summary>
        /// Returns a linear interpolated value between value1 and value 2.
        /// </summary>
        /// <param name="value1">The first double.</param>
        /// <param name="value2">The second double.</param>
        /// <param name="linearInterpolation">Lerp parameter.</param>
        /// <returns>The value</returns>
        public static double Lerp(double value1, double value2, double linearInterpolation)
        {
            return value1 * (1 - linearInterpolation) + value2 * linearInterpolation;
        }

        /// <summary>
        /// Clamps a value between min and max.
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        /// <summary>
        /// Clamps a value between min and max.
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        /// <summary>
        /// Clamps a value between min and max.
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static double Clamp(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        /// <summary>
        /// Returns a random value from an array of type T.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array.</param>
        /// <returns>A random value, or the default value of type T if the array was empty or null.</returns>
        public static T RandomFrom<T>(params T[] array)
        {
            if ((array == null) || (array.Length == 0)) return default(T);
            return array[Rnd.Next(array.Length)];
        }

        /// <summary>
        /// Returns a random value from a list of type T.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="list">The list.</param>
        /// <returns>A random value, or the default value of type T if the list was empty or null.</returns>
        public static T RandomFrom<T>(List<T> list)
        {
            if ((list == null) || (list.Count == 0)) return default(T);
            return list[Rnd.Next(list.Count)];
        }

        /// <summary>
        /// Returns true one time out of oneOutOf. Return false the rest of the time.
        /// </summary>
        /// <returns>True or false.</returns>
        public static bool RandomChance(int oneOutOf)
        { return Rnd.Next(oneOutOf) == 0; }

        /// <summary>
        /// Returns a random integer between 0 and Int32.MaxValue.
        /// </summary>
        /// <returns>A random integer.</returns>
        public static int RandomInt()
        { return Rnd.Next(); }

        /// <summary>
        /// Returns a random integer between 0 and max (excluded).
        /// </summary>
        /// <param name="max">Maximum value (excluded).</param>
        /// <returns>A random integer.</returns>
        public static int RandomInt(int max)
        { return Rnd.Next(max); }

        /// <summary>
        /// Returns a random integer between min (included) and max (excluded).
        /// </summary>
        /// <param name="min">Minimum value (included).</param>
        /// <param name="max">Maximum value (excluded).</param>
        /// <returns>A random integer.</returns>
        public static int RandomInt(int min, int max)
        { return Rnd.Next(min, max); }

        /// <summary>
        /// Returns a random integer between min (included) and max (included).
        /// </summary>
        /// <param name="min">Minimum value (included).</param>
        /// <param name="max">Maximum value (included).</param>
        /// <returns>A random integer.</returns>
        public static int RandomMinMax(int min, int max)
        { return Rnd.Next(min, max + 1); }

        /// <summary>
        /// Returns a random double between 0.0 and 1.0.
        /// </summary>
        /// <returns>A random double.</returns>
        public static double RandomDouble()
        { return Rnd.NextDouble(); }

        /// <summary>
        /// Returns a random double between 0.0 and max (included).
        /// </summary>
        /// <param name="max">Maximum value (included).</param>
        /// <returns>A random double.</returns>
        public static double RandomDouble(double max)
        { return Rnd.NextDouble() * max; }

        /// <summary>
        /// Returns a random double between min (included) and max (included).
        /// </summary>
        /// <param name="min">Minimum value (included).</param>
        /// <param name="max">Maximum value (included).</param>
        /// <returns>A random double.</returns>
        public static double RandomDouble(double min, double max)
        { return (Rnd.NextDouble() * (max - min)) + min; }

        /// <summary>
        /// Reads the content of a Lua file in the Include\Lua directory.
        /// </summary>
        /// <param name="filePath">Path to Lua file from [HQ4DCS Path]\Include\Lua\</param>
        /// <returns>The content of the Lua file, or an empty string if the file was not found.</returns>
        public static string ReadIncludeLuaFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return "";
            if (!filePath.ToLowerInvariant().EndsWith(".lua")) filePath += ".lua";
            filePath = $"{PATH_INCLUDE}Lua\\{filePath}";
            if (!File.Exists(filePath)) return "";
            return File.ReadAllText(filePath);
        }
    }
}

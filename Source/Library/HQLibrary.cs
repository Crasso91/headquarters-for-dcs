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
using System.Windows.Forms;

namespace Headquarters4DCS.Library
{
    /// <summary>
    /// Stores all definitions and settings from the .ini files in the Library subdirectory.
    /// </summary>
    public sealed class HQLibrary : IDisposable
    {
        public static HQLibrary Instance
        {
            get
            {
                if (_Instance == null) _Instance = new HQLibrary();
                return _Instance;
            }
        }

        private static HQLibrary _Instance = null;

        /// <summary>
        /// Default blue coalition for mission templates.
        /// </summary>
        public const string DEFAULT_BLUE_COALITION = "USA";

        /// <summary>
        /// Default red coalition for mission templates.
        /// </summary>
        public const string DEFAULT_RED_COALITION = "Russia";

        /// <summary>
        /// Common settings loaded from files in the Library directory.
        /// </summary>
        //public CommonSettings Common { get; private set; }

        /// <summary>
        /// Definitions are stored by type in a dictionary of dictionaries.
        /// </summary>
        private readonly Dictionary<Type, Dictionary<string, Definition>> Definitions = new Dictionary<Type, Dictionary<string, Definition>>();

        /// <summary>
        /// An array of player-controllable aircraft. Must be static because it's used by PlayerAircraftListTypeConverter.
        /// </summary>
        public static string[] PlayerAircraft = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public HQLibrary() { }

        /// <summary>
        /// Loads all values from the .ini files.
        /// </summary>
        /// <returns>True if successful, false if an error happened.</returns>
        public bool LoadAll()
        {
            bool loadedSuccessfully = true;

            try
            {
                HQDebugLog.Clear();
                HQDebugLog.Log("Loading HQ4DCS library...");
                HQDebugLog.Log();

                // Loads common settings
                //Common = new CommonSettings();

                // Load definitions
                LoadDefinitions<DefinitionCoalition>("Coalitions", false);
                LoadDefinitions<DefinitionLanguage>("Languages", false); // TODO: load from directory
                LoadDefinitions<DefinitionNodeFeature>("NodeFeatures", false);
                LoadDefinitions<DefinitionObjective>("Objectives", false);
                LoadDefinitions<DefinitionTheater>("Theaters", true);
                LoadDefinitions<DefinitionUnit>("Units", false);

                // Check default values are present
                //CheckDefaultValuesExist<DefinitionLanguage>(HQTools.DEFAULT_LANGUAGE);
                //CheckDefaultValuesExist<DefinitionCoalition>(DEFAULT_BLUE_COALITION);
                //CheckDefaultValuesExist<DefinitionCoalition>(DEFAULT_RED_COALITION);

                // Creates a dictionary of player-controlled aircraft
                //PlayerAircraft = (from DefinitionUnit ac in Definitions[typeof(DefinitionUnit)].Values where ac.AircraftPlayerControllable select ac.ID).ToArray();
                //HQDebugLog.Log();
                //HQDebugLog.Log($"Found {PlayerAircraft.Length} player-controllable aircraft.: {string.Join(", ", PlayerAircraft)}");

                HQDebugLog.Log();
                HQDebugLog.Log("Library loaded successfully.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Critical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                loadedSuccessfully = false;
            }

            HQDebugLog.SaveToFileAndClear("Startup");
            return loadedSuccessfully;
        }

        /// <summary>
        /// Throws an exception if a default definition is not found.
        /// </summary>
        /// <typeparam name="T">The definition type.</typeparam>
        /// <param name="definitionID">The id of the definition.</param>
        private void CheckDefaultValuesExist<T>(string definitionID) where T : Definition
        {
            if (!DefinitionExists<T>(definitionID))
                throw new Exception($"Default {typeof(T).Name} ({definitionID}) not found in Library.\r\n\r\nHQ4DCS will now terminate.");
        }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Returns all IDs of definition of a certain type.
        /// </summary>
        /// <typeparam name="T">Definition type.</typeparam>
        /// <returns>An array of ID strings.</returns>
        public string[] GetAllDefinitionIDs<T>() where T : Definition
        { return Definitions[typeof(T)].Keys.ToArray(); }

        /// <summary>
        /// Returns all definitions of a certain type.
        /// </summary>
        /// <typeparam name="T">Definition type.</typeparam>
        /// <returns>An array of definitions.</returns>
        public T[] GetAllDefinitions<T>() where T : Definition
        { return (from d in Definitions[typeof(T)].Values select (T)d).ToArray(); }

        /// <summary>
        /// Returns the definition of type T with unique ID id.
        /// </summary>
        /// <typeparam name="T">The type of the definition.</typeparam>
        /// <param name="id">The unique ID of the definition (case insensitive)</param>
        /// <returns>The definition, or null is no definition with this ID exists.</returns>
        public T GetDefinition<T>(string id) where T : Definition
        {
            if (!Definitions[typeof(T)].ContainsKey(id)) return null;
            return (T)Definitions[typeof(T)][id];
        }

        /// <summary>
        /// Does a definition exist?
        /// </summary>
        /// <typeparam name="T">The type of the definition.</typeparam>
        /// <param name="id">The unique ID of the definition (case insensitive)</param>
        /// <returns>True if the definition exist, false if it doesn't.</returns>
        public bool DefinitionExists<T>(string id) where T : Definition
        { return Definitions[typeof(T)].ContainsKey(id); }


        /// <summary>
        /// Creates and populate a definition dictionary.
        /// </summary>
        /// <typeparam name="T">The type of the definition</typeparam>
        /// <param name="path">Path to the definition.</param>
        /// <param name="fromDirectory">Should the definition be loaded from a directory (true) or a single INI file (false)?</param>
        private void LoadDefinitions<T>(string path, bool fromDirectory) where T : Definition, new()
        {
            Dictionary<string, Definition> dictionary = new Dictionary<string, Definition>(StringComparer.InvariantCultureIgnoreCase);

            if (fromDirectory)
            {
                foreach (string d in Directory.GetDirectories(HQTools.PATH_LIBRARY + path))
                {
                    T def = new T();
                    if (!def.Load(Path.GetFileName(d), HQTools.NormalizeDirectoryPath(d))) continue;

                    // ID is null/empty or already exists - must be after def.Load because some definition override the default ID
                    if (string.IsNullOrEmpty(def.ID) || dictionary.ContainsKey(def.ID)) continue;
                    dictionary.Add(def.ID, def);
                }
            }
            else
            {
                foreach (string f in Directory.GetFiles(HQTools.PATH_LIBRARY + path, "*.ini"))
                {
                    T def = new T();
                    if (!def.Load(Path.GetFileNameWithoutExtension(f), f)) continue;

                    // ID is null/empty or already exists - must be after def.Load because some definition override the default ID
                    if (string.IsNullOrEmpty(def.ID) || dictionary.ContainsKey(def.ID)) continue;
                    dictionary.Add(def.ID, def);
                }
            }

            Definitions.Add(typeof(T), dictionary);

            //MessageBox.Show(typeof(T).Name + " - " + dictionary.Count);

            //HQDebugLog.Log($"Loaded {dictionary.Keys.Count} {typeof(T).Name.Replace("Definition", "").ToUpperInvariant()} definition(s): {string.Join(", ", dictionary.Keys)}");
            Console.WriteLine($"Loaded {dictionary.Keys.Count} {typeof(T).Name.Replace("Definition", "").ToUpperInvariant()} definition(s): {string.Join(", ", dictionary.Keys)}");
        }

        /// <summary>
        /// Returns the name of the Library subdirectory where .ini files for this definition are stored.
        /// Has to be static, because it's used by INIFileListTypeConverter to get a list of available .ini files.
        /// </summary>
        /// <typeparam name="T">The type of definition.</typeparam>
        /// <returns>The name of the directory.</returns>
        public static string GetDirectoryFromType<T>() where T : Definition
        {
            Type type = typeof(T);

            //if (type == typeof(ArmyDefinition)) return "Armies";
            if (type == typeof(DefinitionCoalition)) return "Coalitions";
            if (type == typeof(DefinitionNodeFeature)) return "Features";
            if (type == typeof(DefinitionLanguage)) return "Languages";
            //if (type == typeof(DefinitionMissionObjective)) return "MissionObjectives";
            if (type == typeof(DefinitionTheater)) return "Theaters";
            if (type == typeof(DefinitionUnit)) return "Units";

            return type.Name;
        }

        //public static string[] GetDefinitionFiles<T>() where T : Definition
        //{
        //    string definitionsDirectory = GetDirectoryFromType<T>();

        //    if (!Directory.Exists(iniDirectory)) return;

        //    List<string> fileNames = new List<string>();
        //    AddINIFilesToList(fileNames, $"{HQTools.PATH_LIBRARY}\\{definitionsDirectory}");
        //    fileNames.Sort();
        //    return fileNames.ToArray();
        //}

        //private static void AddINIFilesToList(List<string> fileNames, string iniDirectory)
        //{
        //    if (!Directory.Exists(iniDirectory)) return;

        //    foreach (string f in Directory.GetFiles(iniDirectory, "*.ini"))
        //    {
        //        string id = Path.GetFileNameWithoutExtension(f);
        //        if (fileNames.Contains(id.ToLowerInvariant())) continue;
        //        fileNames.Add(id);
        //    }

        //    foreach (string iniSubDirectory in Directory.GetDirectories(iniDirectory))
        //        AddINIFilesToList(fileNames, iniSubDirectory);
        //}
    }
}

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

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Stores all definitions and settings from the .ini files in the Library subdirectory.
    /// </summary>
    public sealed class Library : IDisposable
    {
        /// <summary>
        /// Default language
        /// </summary>
        public const string DEFAULT_LANGUAGE = "English";

        /// <summary>
        /// HQLibrary singleton.
        /// </summary>
        public static Library Instance
        {
            get
            {
                if (_Instance == null) _Instance = new Library();
                return _Instance;
            }
        }

        /// <summary>
        /// HQLibrary private singleton object.
        /// </summary>
        private static Library _Instance = null;

        /// <summary>
        /// Common HQ4DCS settings (default units types, etc.) loaded from Library/Settings.ini.
        /// </summary>
        public LibraryCommonSettings Common { get; private set; }

        /// <summary>
        /// Definitions are stored by type in a dictionary of dictionaries.
        /// </summary>
        private readonly Dictionary<Type, Dictionary<string, Definition>> Definitions = new Dictionary<Type, Dictionary<string, Definition>>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public Library() { }

        /// <summary>
        /// Loads all values from the .ini files.
        /// </summary>
        /// <returns>True if successful, false if an error happened.</returns>
        public bool LoadAll()
        {
            bool loadedSuccessfully = true;

            try
            {
                DebugLog.Instance.Clear();
                DebugLog.Instance.Log("Loading HQ4DCS library...");
                DebugLog.Instance.Log();

                Common = new LibraryCommonSettings();

                // Load definitions
                LoadDefinitions<DefinitionCoalition>("Coalitions", false);
                LoadDefinitions<DefinitionLanguage>("Languages", true);
                LoadDefinitions<DefinitionFeature>("Features", false);
                LoadDefinitions<DefinitionObjective>("Objectives", false);
                LoadDefinitions<DefinitionTheater>("Theaters", false);
                LoadDefinitions<DefinitionUnit>("Units", false);

                // Check default values are present
                CheckDefaultValuesExist<DefinitionLanguage>(Common.DefaultLanguage);
                CheckDefaultValuesExist<DefinitionCoalition>(Common.DefaultCoalitionBlue);
                CheckDefaultValuesExist<DefinitionCoalition>(Common.DefaultCoalitionRed);
                CheckDefaultValuesExist<DefinitionObjective>(Common.DefaultObjective);
                CheckDefaultValuesExist<DefinitionUnit>(Common.DefaultPlayerAircraft);
                if (!GetDefinition<DefinitionUnit>(Common.DefaultPlayerAircraft).AircraftPlayerControllable) throw new HQ4DCSException("Default player aircraft is not player-controllable.");

                DebugLog.Instance.Log();
                DebugLog.Instance.Log("Library .ini files loaded successfully.");
            }
#if DEBUG
            catch (HQ4DCSException e)
#else
            catch (Exception e)
#endif
            {
                MessageBox.Show($"{e.Message}\r\n\r\nHQ4DCS will now terminate.", "Critical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                loadedSuccessfully = false;
            }

            DebugLog.Instance.SaveToFileAndClear("Startup");
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
                throw new HQ4DCSException($"Default {typeof(T).Name} ({definitionID}) not found in library.");
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

        // TODO: description
        public T[] GetMultipleDefinitions<T>(params string[] ids) where T : Definition
        {
            return (from T definition in Definitions[typeof(T)] where ids.Contains(definition.ID) select definition).ToArray();
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

                    // ID is null/empty or already exists - must be after def.Load because some definitions override the default ID
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

                    // ID is null/empty or already exists - must be after def.Load because some definitions override the default ID
                    if (string.IsNullOrEmpty(def.ID) || dictionary.ContainsKey(def.ID)) continue;
                    dictionary.Add(def.ID, def);
                }
            }

            Definitions.Add(typeof(T), dictionary);

            DebugLog.Instance.Log($"Loaded {dictionary.Keys.Count} {typeof(T).Name.Replace("Definition", "").ToUpperInvariant()} definition(s): {string.Join(", ", dictionary.Keys)}");
        }
    }
}

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

using Headquarters4DCS.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Headquarters4DCS.Template
{
    /// <summary>
    /// An HQ4DCS mission template, which can be loaded or saved from/to a file, or used to generate a mission using the MissionGenerator class.
    /// </summary>
    public sealed class MissionTemplate : IDisposable
    {
        /// <summary>
        /// Theater in which the mission takes place.
        /// </summary>
        public string Theater { get; private set; } = HQLibrary.Instance.Common.DefaultTheater;

        /// <summary>
        /// Global settings for the mission.
        /// </summary>
        public readonly MissionTemplateSettings Settings = null;

        /// <summary>
        /// All locations on the map. Each location can feature units, scripts, player flight groups...
        /// </summary>
        public Dictionary<string, MissionTemplateLocation> Locations = new Dictionary<string, MissionTemplateLocation>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public MissionTemplate()
        {
            Settings = new MissionTemplateSettings();
            Clear(HQLibrary.Instance.Common.DefaultTheater);
        }

        /// <summary>
        /// Sets a new "clean" template for the given theater. Resets all global settings, and recreate template nodes from the theater library definition.
        /// </summary>
        /// <param name="theaterID">The theater to use.</param>
        public void Clear(string theaterID)
        {
            Theater = HQLibrary.Instance.DefinitionExists<DefinitionTheater>(theaterID) ? theaterID : HQLibrary.Instance.Common.DefaultTheater;
            DefinitionTheater theaterDefinition = HQLibrary.Instance.GetDefinition<DefinitionTheater>(Theater);

            Locations.Clear();

            foreach (DefinitionTheaterLocation n in theaterDefinition.Nodes.Values)
                Locations.Add(n.ID, new MissionTemplateLocation(n));
        }

        /// <summary>
        /// Switch the coalition of all nodes on the map. Red nodes become blue, and vice-versa. Neutral nodes are not affected.
        /// </summary>
        public void InvertCoalitions()
        {
            foreach (string k in Locations.Keys)
            {
                switch (Locations[k].Coalition)
                {
                    case CoalitionNeutral.Blue: Locations[k].Coalition = CoalitionNeutral.Red; break;
                    case CoalitionNeutral.Red: Locations[k].Coalition = CoalitionNeutral.Blue; break;
                }
            }
        }

        /// <summary>
        /// Loads the template from an HQT file.
        /// </summary>
        /// <param name="filePath">The path of the file to load from.</param>
        /// <returns>True if everything went right, false if an error happened.</returns>
        public bool LoadFromFile(string filePath)
        {
            using (INIFile ini = new INIFile(filePath))
            {
                string theater = ini.GetValue<string>("Settings", "Theater");
                if (!HQLibrary.Instance.DefinitionExists<DefinitionTheater>(theater))
                {
                    Clear(HQLibrary.Instance.Common.DefaultTheater);
                    return false;
                }
                Clear(theater);

                DefinitionTheater theaterDefinition = HQLibrary.Instance.GetDefinition<DefinitionTheater>(Theater);

                foreach (string k in Locations.Keys)
                    Locations[k].LoadFromFile(ini);
            }

            return true;
        }

        /// <summary>
        /// Save the template to an HQT file.
        /// </summary>
        /// <param name="filePath">The path of the file to save to.</param>
        /// <returns>True if everything went right, false if an error happened.</returns>
        public bool SaveToFile(string filePath)
        {
            using (INIFile ini = new INIFile())
            {
                ini.SetValue("Settings", "Theater", Theater);
                Settings.SaveToFile(ini);

                foreach (string k in Locations.Keys)
                    Locations[k].SaveToFile(ini);

                ini.SaveToFile(filePath);
            }

            return true;
        }

        /// <summary>
        /// Returns the total number of client-controlled aircraft (not flight groups) in this template.
        /// </summary>
        /// <returns>Number of players</returns>
        public int GetPlayerCount()
        {
            int playerCount = 0;

            foreach (MissionTemplateLocation node in Locations.Values)
                foreach (MissionTemplatePlayerFlightGroup fg in node.PlayerFlightGroups)
                    playerCount += fg.AIWingmen ? 1 : fg.Count;

            return playerCount;
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { }
    }
}

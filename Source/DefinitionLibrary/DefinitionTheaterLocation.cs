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

using System.Collections.Generic;
using System.IO;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Theater node, stores information about a "node" (location) on the theater map.
    /// Class is abstract and inherited by all DefinitionTheaterNode* classes.
    /// </summary>
    public sealed class DefinitionTheaterLocation
    {
        /// <summary>
        /// Generates an ID string for this node from the name an .ini file.
        /// </summary>
        /// <param name="filePath">Full path to the node's .ini file.</param>
        /// <returns>A node ID, as a string.</returns>
        public static string GetNodeIDFromINIFileName(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return "";
            return Path.GetFileNameWithoutExtension(filePath).Substring(5).ToLowerInvariant();
        }

        /// <summary>
        /// Unique ID of this node. Basically, the case-insensitive name of the node ini file, without the extension.
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Name to display. If empty or null, ID is displayed instead.
        /// </summary>
        public string DisplayName { get; private set; }
        
        /// <summary>
        /// A list of spawn points (precise coordinates where to spawn units) for this node.
        /// </summary>
        public DefinitionTheaterLocationSpawnPoint[] SpawnPoints { get; private set; }

        /// <summary>
        /// Position on the HQ4DCS GUI map.
        /// </summary>
        public Coordinates MapPosition { get; private set; }

        /// <summary>
        /// Position on the DCS World map.
        /// </summary>
        public Coordinates Position { get; private set; }

        /// <summary>
        /// Which coalition does this node belong to?
        /// Only used for airbases at the moment, but might be used with other node types later one, so it's here and not in DefinitionTheaterNodeAirbaseInfo.
        /// </summary>
        public CoalitionNeutral Coalition { get; private set; }

        /// <summary>
        /// The type of this node, as a value of the TheaterNodeType enum.
        /// </summary>
        public TheaterLocationType NodeType { get; private set; }

        /// <summary>
        /// Country in which this node is located.
        /// </summary>
        public Country Country { get; private set; }

        /// <summary>
        /// Information about the airbase. Only used in nodes of type TheaterNodeType.Airbase.
        /// </summary>
        public DefinitionTheaterLocationAirbaseInfo Airbase { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID of the node.</param>
        /// <param name="iniFilePath">Path to the node's ini file.</param>
        public DefinitionTheaterLocation(string iniFilePath)
        {
            ID = GetNodeIDFromINIFileName(iniFilePath);

            using (INIFile ini = new INIFile(iniFilePath))
            {
                DisplayName = ini.GetValue<string>("Node", "DisplayName");
                if (string.IsNullOrEmpty(DisplayName)) DisplayName = ID;
                MapPosition = ini.GetValue<Coordinates>("Node", "Position.Map");
                Position = ini.GetValue<Coordinates>("Node", "Position.DCS");
                NodeType = ini.GetValue<TheaterLocationType>("Node", "Type");
                Country = ini.GetValue<Country>("Node", "Country");
                Coalition = ini.GetValue("Node", "Coalition", CoalitionNeutral.Neutral);

                switch (NodeType)
                {
                    case TheaterLocationType.Airbase:
                        Airbase = new DefinitionTheaterLocationAirbaseInfo(ini);
                        break;
                    default:
                        Coalition = CoalitionNeutral.Neutral;
                        Airbase = new DefinitionTheaterLocationAirbaseInfo();
                        break;
                }

                // Load spawn points
                string[] spawnPointsKeys = ini.GetKeysInSection("SpawnPoints");
                List<DefinitionTheaterLocationSpawnPoint> spawnPointList = new List<DefinitionTheaterLocationSpawnPoint>();
                foreach (string k in spawnPointsKeys)
                {
                    DefinitionTheaterLocationSpawnPoint sp = new DefinitionTheaterLocationSpawnPoint(ini, "SpawnPoints", k);
                    if (!sp.IsValid) continue;
                    spawnPointList.Add(sp);
                }
                SpawnPoints = spawnPointList.ToArray();
            }
        }
    }
}

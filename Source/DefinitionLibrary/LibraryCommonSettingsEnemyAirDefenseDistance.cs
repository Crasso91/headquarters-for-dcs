/*
==========================================================================
This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
Eagle Dynamics' DCS World flight simulator.

HQ4DCS has been created by Ambroise Garel (@akaAgar).
You can find more information about the project on its GitHub page,
https://akaAgar.github.io/Headquarters4DCS

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
using System.Linq;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Stores information about the location where air defense should be spawned.
    /// </summary>
    public struct LibraryCommonSettingsEnemyAirDefenseDistance
    {
        /// <summary>
        /// The min/max distance (in nm) this type of air defense should be from the objective.
        /// </summary>
        public readonly MinMaxD DistanceFromObjective;

        /// <summary>
        /// The minimum distance between air defense of this type and the players' start location.
        /// </summary>
        public readonly double MinDistanceFromTakeOffLocation;

        /// <summary>
        /// The type of nodes where air defense of this type can be spawned.
        /// </summary>
        public readonly TheaterLocationSpawnPointType[] NodeTypes;

        /// <summary>
        /// Constructor. Load data from a .ini file.
        /// </summary>
        /// <param name="ini">The .ini file to load from.</param>
        /// <param name="section">The .ini file section.</param>
        /// <param name="key">The top level .ini key to load from.</param>
        public LibraryCommonSettingsEnemyAirDefenseDistance(INIFile ini, string section, string key)
        {
            key = (key == null) ? "" : $"{key}.";

            DistanceFromObjective = ini.GetValue<MinMaxD>(section, $"{key}DistanceFromTarget");
            DistanceFromObjective = new MinMaxD(Math.Max(0.0, DistanceFromObjective.Min), Math.Max(0.0, DistanceFromObjective.Max)) * HQTools.NM_TO_METERS;

            MinDistanceFromTakeOffLocation =
                Math.Max(0, ini.GetValue<double>(section, $"{key}MinDistanceFromTakeOffLocation")) * HQTools.NM_TO_METERS;

            NodeTypes = ini.GetValueArray<TheaterLocationSpawnPointType>(section, $"{key}SpawnPointTypes").Distinct().ToArray();
            if (NodeTypes.Length == 0) NodeTypes = new TheaterLocationSpawnPointType[] { TheaterLocationSpawnPointType.LandMedium, TheaterLocationSpawnPointType.LandLarge };
        }
    }
}

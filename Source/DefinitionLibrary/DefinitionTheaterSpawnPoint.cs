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
using System.Linq;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Theater location spawn point: a set of X,Y coordinates where a group of unit can be spawned.
    /// </summary>
    public struct DefinitionTheaterSpawnPoint
    {
        /// <summary>
        /// ID of this spawn point. Must be unique at each location.
        /// </summary>
        public readonly string UniqueID;

        /// <summary>
        /// Is this spawn point valid? Set by the constructor.
        /// If false, the spawn point will be ignored by DefinitionTheaterLocation constructor.
        /// </summary>
        public readonly bool IsValid;

        /// <summary>
        /// DCS map coordinates of this spawn point.
        /// </summary>
        public readonly Coordinates Coordinates;

        /// <summary>
        /// The type of spawn point.
        /// </summary>
        public readonly TheaterLocationSpawnPointType PointType;

        /// <summary>
        /// DCS World Country this spawn point is located in.
        /// </summary>
        public readonly DCSCountry Country;

        public DefinitionTheaterSpawnPoint(INIFile ini, string key) : this()
        {
            string[] vals = ini.GetValueArray<string>("SpawnPoints", key, ',');
            IsValid = true;
            UniqueID = key;

            if (vals.Length < 4) { IsValid = false; return; }

            try
            {
                Coordinates = new Coordinates(HQTools.StringToDouble(vals[0]), HQTools.StringToDouble(vals[1]));
                PointType = (TheaterLocationSpawnPointType)Enum.Parse(typeof(TheaterLocationSpawnPointType), vals[2], true);
                Country = (DCSCountry)Enum.Parse(typeof(DCSCountry), vals[3], true);
            }
            catch (Exception)
            {
                IsValid = false;
            }
        }
    }
}

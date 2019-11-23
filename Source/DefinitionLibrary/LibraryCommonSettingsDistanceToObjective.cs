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

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Stores parameters about the distance between an objective and the start location/other objectives.
    /// </summary>
    public struct LibraryCommonSettingsDistanceToObjective
    {
        /// <summary>
        /// Min/max distance from takeoff location, in nautical miles (for objective #1).
        /// </summary>
        public readonly MinMaxD DistanceFromStartLocation;

        /// <summary>
        /// Min/max distance from previous objective, in nautical miles (for objectives #2+).
        /// </summary>
        public readonly MinMaxD DistanceBetweenTargets;

        /// <summary>
        /// Constructor. Loads data from a .ini file.
        /// </summary>
        /// <param name="ini">The .ini file to load from.</param>
        /// <param name="section">The .ini section to load from.</param>
        /// <param name="key">The top level .ini key to load from.</param>
        public LibraryCommonSettingsDistanceToObjective(INIFile ini, string section, string key)
        {
            DistanceFromStartLocation = ini.GetValue<MinMaxD>(section, $"{key}.DistanceFromStartLocation");
            DistanceFromStartLocation = new MinMaxD(
                Math.Max(0, DistanceFromStartLocation.Min),
                Math.Max(0, DistanceFromStartLocation.Max)) * HQTools.NM_TO_METERS;

            DistanceBetweenTargets = ini.GetValue<MinMaxD>(section, $"{key}.DistanceBetweenTargets");
            DistanceBetweenTargets = new MinMaxD(
                Math.Max(0, DistanceBetweenTargets.Min),
                Math.Max(0, DistanceBetweenTargets.Max)) * HQTools.NM_TO_METERS;
        }
    }
}

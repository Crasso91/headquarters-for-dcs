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

namespace Headquarters4DCS.Mission
{
    /// <summary>
    /// Stores information about a mission objective location.
    /// </summary>
    public struct DCSMissionObjectiveLocation
    {
        /// <summary>
        /// X,Y coordinates of the objective (NOT the waypoint for this objective).
        /// </summary>
        public readonly Coordinates Coordinates;

        /// <summary>
        /// Name of this objective.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Altitude multiplier for this objective.
        /// </summary>
        public readonly double Altitude;

        /// <summary>
        /// ID of the airdrome linked to this objective, if any.
        /// </summary>
        public readonly int AirdromeID;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="coordinates">X,Y coordinates of the objective (NOT the waypoint for this objective).</param>
        /// <param name="name">Name of this objective.</param>
        /// <param name="altitude">Altitude multiplier for this objective.</param>
        /// <param name="airdromeID">ID of the airdrome linked to this objective, if any.</param>
        public DCSMissionObjectiveLocation(Coordinates coordinates, string name = "", double altitude = 1f, int airdromeID = 0)
        {
            Coordinates = coordinates;
            Name = name.ToUpperInvariant();
            Altitude = HQTools.Clamp(altitude, 0f, 2f);
            AirdromeID = airdromeID;
        }
    }
}

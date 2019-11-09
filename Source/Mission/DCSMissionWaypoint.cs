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
    /// Stores information about a mission waypoint.
    /// </summary>
    public struct DCSMissionWaypoint
    {
        /// <summary>
        /// Minimum altitude multiplier.
        /// </summary>
        private const double MIN_ALTITUDE_MULTIPLIER = 0.0;
        
        /// <summary>
        /// Maxmimum altitude multiplier.
        /// </summary>
        private const double MAX_ALTITUDE_MULTIPLIER = 2.0;

        /// <summary>
        /// Minimum speed multiplier.
        /// </summary>
        private const double MIN_SPEED_MULTIPLIER = 0.0;

        /// <summary>
        /// Maximum speed multiplier.
        /// </summary>
        private const double MAX_SPEED_MULTIPLIER = 2.0;

        /// <summary>
        /// Default WP action.
        /// </summary>
        private const string DEFAULT_WP_ACTION = "Turning Point";

        /// <summary>
        /// Default WP action type.
        /// </summary>
        private const string DEFAULT_WP_ACTION_TYPE = "Turning Point";

        /// <summary>
        /// ID of the airdrome to use for this waypoint, if any.
        /// </summary>
        public readonly int AirdromeID;

        /// <summary>
        /// Altitude multiplier (1.0 = default altitude for aircraft, 0.0 = ground)
        /// </summary>
        public readonly double AltitudeMultiplier;

        /// <summary>
        /// X,Y coordinates of the waypoint.
        /// </summary>
        public readonly Coordinates Coordinates;

        /// <summary>
        /// Name of the waypoint.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Speed multiplier (1.0 = default speed for aircraft, 0.0 = no speed)
        /// </summary>
        public readonly double SpeedMultiplier;

        /// <summary>
        /// Waypoint action (as written in Mission.lua file)
        /// </summary>
        public readonly string WPAction;

        /// <summary>
        /// Waypoint action type (as written in Mission.lua file)
        /// </summary>
        public readonly string WPType;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="coordinates">X,Y coordinates of the waypoint.</param>
        /// <param name="name">Name of the waypoint.</param>
        /// <param name="altitudeMultiplier">Altitude multiplier (1.0 = default altitude for aircraft, 0.0 = ground)</param>
        /// <param name="speedMultiplier">Speed multiplier (1.0 = default speed for aircraft, 0.0 = no speed)</param>
        /// <param name="wpAction">Waypoint action (as written in Mission.lua file)</param>
        /// <param name="wpType">Waypoint action type (as written in Mission.lua file)</param>
        /// <param name="airdromeID">ID of the airdrome to use for this waypoint, if any.</param>
        public DCSMissionWaypoint(
            Coordinates coordinates,
            string name = "WP$INDEX$",
            double altitudeMultiplier = 1.0,
            double speedMultiplier = 1.0,
            string wpAction = DEFAULT_WP_ACTION,
            string wpType = DEFAULT_WP_ACTION_TYPE,
            int airdromeID = 0)
        {
            Coordinates = coordinates;
            Name = name;
            AltitudeMultiplier = HQTools.Clamp(altitudeMultiplier, MIN_ALTITUDE_MULTIPLIER, MAX_ALTITUDE_MULTIPLIER);
            SpeedMultiplier = HQTools.Clamp(speedMultiplier, MIN_SPEED_MULTIPLIER, MAX_SPEED_MULTIPLIER);
            WPAction = wpAction;
            WPType = wpType;
            AirdromeID = airdromeID;
        }
    }
}

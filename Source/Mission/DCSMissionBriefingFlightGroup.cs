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
    /// Stores information about a flight group to be displayed in the mission briefing.
    /// </summary>
    public struct DCSMissionBriefingFlightGroup
    {
        /// <summary>
        /// Callsign of the flight group.
        /// </summary>
        public readonly string Callsign;

        /// <summary>
        /// The number of units in the flight group.
        /// </summary>
        public readonly int UnitCount;

        /// <summary>
        /// Name of the airbase this flight group is operating from.
        /// </summary>
        public readonly string AirbaseName;

        /// <summary>
        /// The DCS unit type of the flight group.
        /// </summary>
        public readonly string UnitType;

        /// <summary>
        /// The task assigned to this flight group.
        /// </summary>
        public readonly DCSAircraftTask Task;

        /// <summary>
        /// The radio frequency of this flight group.
        /// </summary>
        public readonly float Frequency;

        /// <summary>
        /// The TACAN frequency of this flight group. Mostly for tankers.
        /// </summary>
        public readonly string TACAN;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="callsign">Callsign of the flight group.</param>
        /// <param name="unitType">The task assigned to this flight group.</param>
        /// <param name="task">The task assigned to this flight group.</param>
        /// <param name="airbaseName">Name of the airbase this flight group is operating from.</param>
        /// <param name="unitCount">The number of units in this flight group.</param>
        /// <param name="frequency">The radio frequency of this flight group.</param>
        /// <param name="tacan">The TACAN frequency of this flight group. Mostly for tankers.</param>
        public DCSMissionBriefingFlightGroup(string callsign, string unitType, DCSAircraftTask task, string airbaseName, int unitCount = 1, float frequency = 0.0f, string tacan = null)
        {
            Callsign = callsign;
            UnitType = unitType;
            UnitCount = unitCount;
            Task = task;
            AirbaseName = airbaseName;
            Frequency = frequency;
            TACAN = string.IsNullOrEmpty(tacan) ? "---" : tacan;
        }

        /// <summary>
        /// True if flight group is assigned a support task (AWACS, Tanker...), false if it is part of the mission package.
        /// </summary>
        public bool IsSupport
        {
            get { return (Task == DCSAircraftTask.AWACS) || (Task == DCSAircraftTask.Refueling) || (Task == DCSAircraftTask.Transport); }
        }
    }
}

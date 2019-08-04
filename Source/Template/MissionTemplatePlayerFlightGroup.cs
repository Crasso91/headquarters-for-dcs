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

namespace Headquarters4DCS.Template
{
    /// <summary>
    /// A player flight group template.
    /// </summary>
    public struct MissionTemplatePlayerFlightGroup
    {
        /// <summary>
        /// Maximum number of aircraft in a player flight group.
        /// </summary>
        private const int MAX_AIRCRAFT_COUNT = 4;

        /// <summary>
        /// ID of the aircraft type.
        /// </summary>
        public readonly string AircraftType;

        /// <summary>
        /// Number of aircraft in the flight group.
        /// </summary>
        public readonly int Count;

        /// <summary>
        /// Task assigned to the flight group.
        /// </summary>
        public readonly PlayerFlightGroupTask Task;

        /// <summary>
        /// If true, all aircraft but the first will be AI-controlled. If false, all aircraft will be client-controlled.
        /// </summary>
        public readonly bool AIWingmen;

        /// <summary>
        /// Where should the flight group start from?
        /// </summary>
        public readonly PlayerFlightGroupStartLocation StartLocation;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ini">The .ini file to load from</param>
        /// <param name="section">The .ini section to save to. Should be the MissionTemplateLocation.INISection</param>
        /// <param name="groupIndex">Zero-based player flight group index for this group at this location.</param>
        public MissionTemplatePlayerFlightGroup(INIFile ini, string section, int groupIndex)
        {
            string key = GetINIKey(groupIndex);

            AircraftType = ini.GetValue<string>(section, $"{key}.Type");
            Count = ini.GetValue<int>(section, $"{key}.Count");
            Task = ini.GetValue<PlayerFlightGroupTask>(section, $"{key}.Task");
            AIWingmen = ini.GetValue<bool>(section, $"{key}.AIWingmen");
            StartLocation = ini.GetValue<PlayerFlightGroupStartLocation>(section, $"{key}.StartLocation");
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="aircraftType">ID of the aircraft type.</param>
        /// <param name="count">Number of aircraft in the flight group.</param>
        /// <param name="task">Task assigned to the flight group.</param>
        /// <param name="aiWingmen">If true, all aircraft but the first will be AI-controlled. If false, all aircraft will be client-controlled.</param>
        /// <param name="startLocation">Where should the flight group start from?</param>
        public MissionTemplatePlayerFlightGroup(string aircraftType, int count, PlayerFlightGroupTask task, bool aiWingmen, PlayerFlightGroupStartLocation startLocation)
        {
            AircraftType = aircraftType;
            Count = HQTools.Clamp(count, 1, MAX_AIRCRAFT_COUNT);
            Task = task;
            AIWingmen = aiWingmen;
            StartLocation = startLocation;
        }

        /// <summary>
        /// Saves the flight group to an .ini file.
        /// </summary>
        /// <param name="ini"></param>
        /// <param name="section">The .ini section to save to. Should be the MissionTemplateLocation.INISection</param>
        /// <param name="groupIndex">Zero-based player flight group index for this group at this location.</param>
        public void SaveToFile(INIFile ini, string section, int groupIndex)
        {
            string key = GetINIKey(groupIndex);

            ini.SetValue(section, $"{key}.Type", AircraftType);
            ini.SetValue(section, $"{key}.Count", Count);
            ini.SetValue(section, $"{key}.Task", Task);
            ini.SetValue(section, $"{key}.AIWingmen", AIWingmen);
            ini.SetValue(section, $"{key}.StartLocation", StartLocation);
        }

        // TODO: remove?
        //public override string ToString()
        //{
        //    return $"{HQTools.ValToString(Count)}x {AircraftType} ({Task.ToString()})";
        //}

        /// <summary>
        /// Returns an unique .ini key from the group index.
        /// </summary>
        /// <param name="groupIndex">The index of the group</param>
        /// <returns>An ini key string.</returns>
        private static string GetINIKey(int groupIndex) // Has to be static because it's used in the struct constructor
        {
            return $"PlayerFlightGroup{HQTools.ValToString(groupIndex + 1, "00")}";
        }
    }
}

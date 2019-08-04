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
    public struct MissionTemplatePlayerFlightGroup
    {
        public const PlayerFlightGroupTask DEFAULT_TASK = PlayerFlightGroupTask.CAP;
        public const PlayerFlightGroupStartLocation DEFAULT_START_LOCATION = PlayerFlightGroupStartLocation.FromParking;

        private const int MAX_AIRCRAFT_COUNT = 4;

        public readonly string AircraftType;
        public readonly int Count;
        public readonly PlayerFlightGroupTask Task;
        public readonly bool AIWingmen;
        public readonly PlayerFlightGroupStartLocation StartLocation;

        public MissionTemplatePlayerFlightGroup(INIFile ini, string section, int groupIndex)
        {
            string key = GetINIKey(groupIndex);

            AircraftType = ini.GetValue<string>(section, $"{key}.Type");
            Count = ini.GetValue<int>(section, $"{key}.Count");
            Task = ini.GetValue<PlayerFlightGroupTask>(section, $"{key}.Task");
            AIWingmen = ini.GetValue<bool>(section, $"{key}.AIWingmen");
            StartLocation = ini.GetValue<PlayerFlightGroupStartLocation>(section, $"{key}.StartLocation");
        }

        public MissionTemplatePlayerFlightGroup(string aircraftType, int count, PlayerFlightGroupTask task, bool aiWingmen, PlayerFlightGroupStartLocation startLocation)
        {
            AircraftType = aircraftType;
            Count = HQTools.Clamp(count, 1, MAX_AIRCRAFT_COUNT);
            Task = task;
            AIWingmen = aiWingmen;
            StartLocation = startLocation;
        }

        public void SaveToFile(INIFile ini, string section, int groupIndex)
        {
            string key = GetINIKey(groupIndex);

            ini.SetValue(section, $"{key}.Type", AircraftType);
            ini.SetValue(section, $"{key}.Count", Count);
            ini.SetValue(section, $"{key}.Task", Task);
            ini.SetValue(section, $"{key}.AIWingmen", AIWingmen);
            ini.SetValue(section, $"{key}.StartLocation", StartLocation);
        }

        public override string ToString()
        {
            return $"{HQTools.ValToString(Count)}x {AircraftType} ({Task.ToString()})";
        }

        private static string GetINIKey(int groupIndex) // Has to be static because it's used in the struct constructor
        {
            return $"PlayerFlightGroup{HQTools.ValToString(groupIndex, "00")}";
        }
    }
}

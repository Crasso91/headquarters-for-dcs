using Headquarters4DCS.Enums;

namespace Headquarters4DCS.Template
{
    public struct HQTemplatePlayerFlightGroup
    {
        public const PlayerFlightGroupTask DEFAULT_TASK = PlayerFlightGroupTask.CAP;
        public const PlayerFlightGroupStartLocation DEFAULT_START_LOCATION = PlayerFlightGroupStartLocation.FromParking;

        private const int MAX_AIRCRAFT_COUNT = 4;

        public readonly string AircraftType;
        public readonly int Count;
        public readonly PlayerFlightGroupTask Task;
        public readonly bool AIWingmen;
        public readonly PlayerFlightGroupStartLocation StartLocation;

        public HQTemplatePlayerFlightGroup(INIFile ini, string section, int groupIndex)
        {
            string key = GetINIKey(groupIndex);

            AircraftType = ini.GetValue<string>(section, $"{key}.Type");
            Count = ini.GetValue<int>(section, $"{key}.Count");
            Task = ini.GetValue<PlayerFlightGroupTask>(section, $"{key}.Task");
            AIWingmen = ini.GetValue<bool>(section, $"{key}.AIWingmen");
            StartLocation = ini.GetValue<PlayerFlightGroupStartLocation>(section, $"{key}.StartLocation");
        }

        public HQTemplatePlayerFlightGroup(string aircraftType, int count, PlayerFlightGroupTask task, bool aiWingmen, PlayerFlightGroupStartLocation startLocation)
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

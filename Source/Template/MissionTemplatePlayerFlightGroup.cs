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

using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Forms;
using Headquarters4DCS.TypeConverters;
using System;
using System.ComponentModel;

namespace Headquarters4DCS.Template
{
    /// <summary>
    /// A player flight group template.
    /// </summary>
    public sealed class MissionTemplatePlayerFlightGroup : IDisposable
    {
        /// <summary>
        /// Maximum number of aircraft in a player flight group.
        /// </summary>
        public const int MAX_AIRCRAFT_COUNT = 4;

        /// <summary>
        /// ID of the aircraft type.
        /// </summary>
        [DisplayName("Aircraft type"), Description("Which unit system should be used in the mission briefing?")]
        [TypeConverter(typeof(DefinitionsStringConverterPlayerAircraft))]
        public string AircraftType { get; set; }

        /// <summary>
        /// Number of aircraft in the flight group.
        /// </summary>
        [DisplayName("Count"), Description("Number of aircraft in this flight group.")]
        //[TypeConverter(typeof(IntegerMinMaxValueTypeConverter)), MinMaxValue(1, 4)]
        public int Count { get { return _Count; } set { _Count = HQTools.Clamp(value, 1, 4); } }
        private int _Count = 1;

        /// <summary>
        /// Task assigned to the flight group.
        /// </summary>
        [DisplayName("Task"), Description("The task assigned to this flight group. Can be the primary mission or escort.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<PlayerFlightGroupTask>))]
        public PlayerFlightGroupTask Task { get; set; }

        /// <summary>
        /// Where should this flight group start from?
        /// </summary>
        [DisplayName("Start location"), Description("Where should this flight group start from?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<PlayerFlightGroupStartLocation>))]
        public PlayerFlightGroupStartLocation StartLocation { get; set; }

        /// <summary>
        /// Which units in this flight group are controlled by players, which are controlled by the AI?
        /// </summary>
        [DisplayName("Wigmen are AI"), Description("If enabled, all aircraft except the first one will be controlled by AI pilots. If disabled, all aircraft in this flight group will be player-controlled.")]
        [TypeConverter(typeof(BooleanYesNoTypeConverter))]
        public bool WingmenAI { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MissionTemplatePlayerFlightGroup()
        {
            AircraftType = Library.Instance.Common.DefaultPlayerAircraft;
            Count = 1;
            Task = PlayerFlightGroupTask.PrimaryMission;
            WingmenAI = false;
            StartLocation = Library.Instance.Common.DefaultPlayerFlightGroupStartLocation;
        }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ini">The .ini file to load from</param>
        /// <param name="section">The .ini section to load from.</param>
        /// <param name="key">The .ini key to load from.</param>
        public MissionTemplatePlayerFlightGroup(INIFile ini, string section, string key)
        {
            AircraftType = ini.GetValue<string>(section, $"{key}.Type");
            Count = ini.GetValue<int>(section, $"{key}.Count");
            Task = ini.GetValue<PlayerFlightGroupTask>(section, $"{key}.Task");
            WingmenAI = ini.GetValue<bool>(section, $"{key}.AIWingmen");
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
            WingmenAI = aiWingmen;
            StartLocation = startLocation;
        }

        /// <summary>
        /// Saves the flight group to an .ini file.
        /// </summary>
        /// <param name="ini"></param>
        /// <param name="section">The .ini section to save to.</param>
        /// <param name="key">The .ini key to save to.</param>
        public void SaveToFile(INIFile ini, string section, string key)
        {
            ini.SetValue(section, $"{key}.Type", AircraftType);
            ini.SetValue(section, $"{key}.Count", Count);
            ini.SetValue(section, $"{key}.Task", Task);
            ini.SetValue(section, $"{key}.AIWingmen", WingmenAI);
            ini.SetValue(section, $"{key}.StartLocation", StartLocation);
        }

        /// <summary>
        /// Converts 
        /// </summary>
        /// <returns>A string to display in the PropertyGrid.</returns>
        public override string ToString()
        {
            string acName = Library.Instance.DefinitionExists<DefinitionUnit>(AircraftType) ? Library.Instance.GetDefinition<DefinitionUnit>(AircraftType).DisplayName : AircraftType;

            return $"{HQTools.ValToString(Count)}x {acName}, {GUITools.SplitEnumCamelCase(Task)} " +
                $"({GUITools.SplitEnumCamelCase(StartLocation).ToLowerInvariant()}{(WingmenAI ? ", AI wingmen" : "")})";
        }

        /// <summary>
        /// Returns the number of player-controlled aircraft in this flight group.
        /// </summary>
        /// <returns>Number of player-controlled aircraft</returns>
        public int GetPlayerCount()
        {
            return Math.Min(Count, WingmenAI ? 1 : Count);
        }
    }
}

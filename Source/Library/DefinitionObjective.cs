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

using Headquarters4DCS.Enums;
using System;
using System.Linq;

namespace Headquarters4DCS.Library
{
    /// <summary>
    /// The definition of a mission objective type, to be select in the mission template.
    /// </summary>
    public sealed class DefinitionObjective : Definition
    {
        /// <summary>
        /// The DCS World task to assign to groups tasked with this mission.
        /// </summary>
        public DCSAircraftTask DCSTask { get; private set; }

        /// <summary>
        /// The type of payload aircraft tasked with the mission should be carrying.
        /// </summary>
        public AircraftPayloadType MissionPayload { get; private set; }

        /// <summary>
        /// The string ID (in the language definition .ini) of the objective type (e.g. "interdiction", "close air support"...).
        /// </summary>
        public string BriefingName { get; private set; }

        /// <summary>
        /// The string ID (in the language definition .ini) of the briefing description.
        /// </summary>
        public string BriefingDescription { get; private set; }

        /// <summary>
        /// The string ID (in the language definition .ini) of the message in the briefing tasks section (shown once for each objective).
        /// </summary>
        public string BriefingTask { get; private set; }

        /// <summary>
        /// An array of string IDs (in the language definition .ini) of remarks to add in the briefing "remarks" section.
        /// </summary>
        public string[] BriefingRemarks { get; private set; }

        /// <summary>
        /// Should objective waypoints be named?
        /// </summary>
        public bool WaypointsNamed { get; private set; }

        /// <summary>
        /// Altitude for the objective waypoints. 1.0=default altitude for the aircraft (20000 feet for most planes), 0.0=ground, 2.0=twice as high
        /// </summary>
        public double WaypointAltitude { get; private set; }

        /// <summary>
        /// Type of theater nodes that can be used for the objective waypoints.
        /// </summary>
        public TheaterNodeType[] WaypointNodeTypes { get; private set; }

        /// <summary>
        /// Should extra WPs be added before?
        /// </summary>
        public bool WaypointExtraBefore { get; private set; }

        /// <summary>
        /// Should extra WPs be added after?
        /// </summary>
        public bool WaypointExtraAfter { get; private set; }

        /// <summary>
        /// Min/Max offset (in nautical miles) between the waypoint (as displayed in the players' flight plan/briefing) and the actual objective.
        /// </summary>
        public MinMaxD WaypointOffset { get; private set; }

        /// <summary>
        /// An array of ogg files (located in Include\Ogg\) to include in the .MIZ file.
        /// </summary>
        public string[] MediaOgg { get; private set; }

        /// <summary>
        /// The script to include ONCE in the mission.
        /// </summary>
        public string[] ScriptMission { get; private set; } = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];

        /// <summary>
        /// The script to include ONCE FOR EACH OBJECTIVE in the mission.
        /// </summary>
        public string[] ScriptObjective { get; private set; } = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];

        /// <summary>
        /// Unit groups to spawn at EACH of the objective locations.
        /// </summary>
        public DefinitionObjectiveUnitGroup UnitGroupObjective { get; private set; }

        /// <summary>
        /// Unit groups to spawn ONCE.
        /// </summary>
        public DefinitionObjectiveUnitGroup UnitGroupMission { get; private set; }

        /// <summary>
        /// Loads data required by this definition.
        /// </summary>
        /// <param name="ini">The ini file to load from.</param>
        /// <returns>True is successful, false if an error happened.</returns>
        protected override bool OnLoad(string path)
        {
            using (INIFile ini = new INIFile(path + "Theater.ini"))
            {
                // --------------------------
                // [MissionObjective] section
                // --------------------------
                DCSTask = ini.GetValue<DCSAircraftTask>("MissionObjective", "DCSTask");
                MissionPayload = ini.GetValue<AircraftPayloadType>("MissionObjective", "Payload");

                // ------------------
                // [Briefing] section
                // ------------------
                BriefingName = ini.GetValue<string>("Briefing", "Name");
                BriefingDescription = ini.GetValue<string>("Briefing", "Description");
                BriefingTask = ini.GetValue<string>("Briefing", "Task");
                BriefingRemarks = ini.GetValueArray<string>("Briefing", "Remarks");

                // -------------------
                // [Waypoints] section
                // -------------------
                WaypointsNamed = ini.GetValue<bool>("Waypoints", "Named");
                WaypointAltitude = HQTools.Clamp(ini.GetValue<double>("Waypoints", "Altitude"), 0.0, 2.0);
                WaypointNodeTypes = ini.GetValueArray<TheaterNodeType>("Waypoints", "NodeTypes").Distinct().ToArray();
                if (WaypointNodeTypes.Length == 0) WaypointNodeTypes = (TheaterNodeType[])Enum.GetValues(typeof(TheaterNodeType));
                WaypointExtraBefore = ini.GetValue<bool>("Waypoints", "ExtraBefore");
                WaypointExtraAfter = ini.GetValue<bool>("Waypoints", "ExtraAfter");
                WaypointOffset = ini.GetValue<MinMaxD>("Waypoints", "Offset") * HQTools.NM_TO_METERS;

                // -----------------
                // [Scripts] section
                // -----------------
                ScriptMission = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];
                ScriptObjective = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];

                for (int i = 0; i < HQTools.MISSION_SCRIPT_SCOPE_COUNT; i++)
                {
                    string scriptM = "";
                    string scriptO = "";

                    string[] missionScripts = ini.GetValueArray<string>("Scripts", $"Mission.{((MissionScriptScope)i).ToString()}");
                    string[] objectiveScripts = ini.GetValueArray<string>("Scripts", $"Objective.{((MissionScriptScope)i).ToString()}");

                    foreach (string s in missionScripts) scriptM += HQTools.ReadIncludeLuaFile("Script\\" + s) + "\n";
                    foreach (string s in objectiveScripts) scriptO += HQTools.ReadIncludeLuaFile("Script\\" + s) + "\n";

                    ScriptMission[i] = scriptM;
                    ScriptObjective[i] = scriptO;
                }

                // ---------------
                // [Media] section
                // ---------------
                MediaOgg = ini.GetValueArray<string>("Media", "Ogg");

                // --------------------------
                // [UnitGroupMission] section
                // --------------------------
                // Unit group, spawn only once
                UnitGroupMission = new DefinitionObjectiveUnitGroup(ini, "UnitGroupMission", false);

                // ----------------------------
                // [UnitGroupObjective] section
                // ----------------------------
                // Unit group, spawn once for each objective
                UnitGroupObjective = new DefinitionObjectiveUnitGroup(ini, "UnitGroupObjective", true);
            }

            return true;
        }
    }
}

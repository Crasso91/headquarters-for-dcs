using Headquarters4DCS.Enums;
using System;
using System.Linq;

namespace Headquarters4DCS.Library
{
    public sealed class DefinitionNodeFeature : Definition
    {
        public TheaterNodeType[] ValidNodeTypes { get; private set; }
        public NodeFeatureFlags[] Flags { get; private set; }

        public string[] ScriptsOnce { get; private set; } = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];
        public string[] ScriptsEach { get; private set; } = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];
        public string[] MediaOgg { get; private set; } = new string[0];

        public bool WaypointEnabled { get; private set; }
        public MinMaxD WaypointInaccuracy { get; private set; }

        public DefinitionObjectiveUnitGroup UnitGroup { get; private set; } = new DefinitionObjectiveUnitGroup();

        protected override bool OnLoad(string path)
        {
            using (INIFile ini = new INIFile(path))
            {
                ValidNodeTypes = ini.GetValueArray<TheaterNodeType>("Definition", "NodeTypes").Distinct().ToArray();
                if (ValidNodeTypes.Length == 0) ValidNodeTypes = (TheaterNodeType[])Enum.GetValues(typeof(TheaterNodeType));

                Flags = ini.GetValueArray<NodeFeatureFlags>("Definition", "Flags").Distinct().ToArray();

                ScriptsOnce = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];
                ScriptsEach = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];

                for (int i = 0; i < HQTools.MISSION_SCRIPT_SCOPE_COUNT; i++)
                {
                    ScriptsOnce = ini.GetValueArray<string>("Definition", $"Scripts.Once.{((MissionScriptScope)i).ToString()}");
                    ScriptsEach = ini.GetValueArray<string>("Definition", $"Scripts.Each.{((MissionScriptScope)i).ToString()}");
                }

                MediaOgg = ini.GetValueArray<string>("Definition", "Media.Ogg");

                //// --------------------------
                //// [MissionObjective] section
                //// --------------------------
                //DCSTask = ini.GetValue<DCSAircraftTask>("MissionObjective", "DCSTask");
                //MissionPayload = ini.GetValue<AircraftPayloadType>("MissionObjective", "Payload");

                //// ------------------
                //// [Briefing] section
                //// ------------------
                //BriefingName = ini.GetValue<string>("Briefing", "Name");
                //BriefingDescription = ini.GetValue<string>("Briefing", "Description");
                //BriefingTask = ini.GetValue<string>("Briefing", "Task");
                //BriefingRemarks = ini.GetValueArray<string>("Briefing", "Remarks");

                //// -------------------
                //// [Waypoints] section
                //// -------------------
                //WaypointsNamed = ini.GetValue<bool>("Waypoints", "Named");
                //WaypointAltitude = HQTools.Clamp(ini.GetValue<double>("Waypoints", "Altitude"), 0.0, 2.0);
                //WaypointNodeTypes = ini.GetValueArray<TheaterNodeType>("Waypoints", "NodeTypes").Distinct().ToArray();
                //if (WaypointNodeTypes.Length == 0) WaypointNodeTypes = (TheaterNodeType[])Enum.GetValues(typeof(TheaterNodeType));
                //WaypointExtraBefore = ini.GetValue<bool>("Waypoints", "ExtraBefore");
                //WaypointExtraAfter = ini.GetValue<bool>("Waypoints", "ExtraAfter");
                //WaypointOffset = ini.GetValue<MinMaxD>("Waypoints", "Offset") * HQTools.NM_TO_METERS;

                //// -----------------
                //// [Scripts] section
                //// -----------------
                //ScriptMission = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];
                //ScriptObjective = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];

                //for (int i = 0; i < HQTools.MISSION_SCRIPT_SCOPE_COUNT; i++)
                //{
                //    string scriptM = "";
                //    string scriptO = "";

                //    string[] missionScripts = ini.GetValueArray<string>("Scripts", $"Mission.{((MissionScriptScope)i).ToString()}");
                //    string[] objectiveScripts = ini.GetValueArray<string>("Scripts", $"Objective.{((MissionScriptScope)i).ToString()}");

                //    foreach (string s in missionScripts) scriptM += HQTools.ReadIncludeLuaFile("Script\\" + s) + "\n";
                //    foreach (string s in objectiveScripts) scriptO += HQTools.ReadIncludeLuaFile("Script\\" + s) + "\n";

                //    ScriptMission[i] = scriptM;
                //    ScriptObjective[i] = scriptO;
                //}

                //// ---------------
                //// [Media] section
                //// ---------------

                //// --------------------------
                //// [UnitGroupMission] section
                //// --------------------------
                //// Unit group, spawn only once
                //UnitGroupMission = new DefinitionObjectiveUnitGroup(ini, "UnitGroupMission", false);

                //// ----------------------------
                //// [UnitGroupObjective] section
                //// ----------------------------
                //// Unit group, spawn once for each objective
                //UnitGroupObjective = new DefinitionObjectiveUnitGroup(ini, "UnitGroupObjective", true);
            }

            return true;
        }

        //protected override bool OnLoad(string path)
        //{
        //    using (INIFile ini = new INIFile(path))
        //    {
        //        UnitGroup = new DefinitionObjectiveUnitGroup(ini, "UnitGroup", false);

        //        MediaOgg = ini.GetValueArray<string>("Media", "OggFiles");
        //    }

        //    return true;
        //}
    }
}

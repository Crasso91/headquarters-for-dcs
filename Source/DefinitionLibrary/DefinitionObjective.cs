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

using System;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Definition of a mission objective.
    /// </summary>
    public sealed class DefinitionObjective : Definition
    {
        /// <summary>
        /// Key of the language string to use for the briefing description.
        /// </summary>
        public string BriefingDescription { get; private set; }

        /// <summary>
        /// Keys of the language string to use for the briefing remarks.
        /// </summary>
        public string[] BriefingRemarks { get; private set; }

        /// <summary>
        /// Key of the language string to use for the briefing task.
        /// </summary>
        public string BriefingTask { get; private set; }

        /// <summary>
        /// DCS tasking assigned to aircraft for this mission
        /// </summary>
        public DCSFlightGroupTask FGTasking { get; private set; }

        /// <summary>
        /// Valid spawn point types for this objective.
        /// </summary>
        public TheaterLocationSpawnPointType[] SpawnPointType { get; private set; }

        /// <summary>
        /// How far the waypoint will be from the spawned units, in nautical miles.
        /// </summary>
        public MinMaxD WaypointInaccuracy { get; private set; }

        /// <summary>
        /// Should the waypoint be set on the ground?
        /// </summary>
        public bool WaypointOnGround { get; private set; }

        /// <summary>
        /// Ogg Vorbis files to include.
        /// </summary>
        public string[] IncludeOgg { get; private set; }

        /// <summary>
        /// Scripts files to include.
        /// </summary>
        public string[,][] IncludeLua { get; private set; }

        /// <summary>
        /// Unit groups to spawn for this mission objective.
        /// </summary>
        public DefinitionObjectiveUnitGroup[] UnitGroups { get; private set; }

        protected override bool OnLoad(INIFile ini)
        {
            BriefingDescription = ini.GetValue<string>("Objective", "Briefing.Description");
            BriefingRemarks = ini.GetValueArray<string>("Objective", "Briefing.Remarks");
            BriefingTask = ini.GetValue<string>("Objective", "Briefing.Task");

            FGTasking = ini.GetValue<DCSFlightGroupTask>("Objective", "FlightGroup.Tasking");

            IncludeLua = new string[HQTools.EnumCount<ObjectiveScriptRepetition>(), HQTools.EnumCount<ObjectiveScriptScope>()][];
            foreach (ObjectiveScriptRepetition rep in HQTools.EnumValues<ObjectiveScriptRepetition>())
                foreach (ObjectiveScriptScope scope in HQTools.EnumValues<ObjectiveScriptScope>())
                    IncludeLua[(int)rep, (int)scope] = ini.GetValueArray<string>("Include", $"Lua.{rep}.{scope}");
            IncludeOgg = ini.GetValueArray<string>("Include", "Ogg");

            SpawnPointType = ini.GetValueArray<TheaterLocationSpawnPointType>("Objective", "SpawnPoint.Type");
            if (SpawnPointType.Length == 0) SpawnPointType = new TheaterLocationSpawnPointType[] { TheaterLocationSpawnPointType.LandMedium, TheaterLocationSpawnPointType.LandLarge };

            WaypointInaccuracy = ini.GetValue<MinMaxD>("Objective", "Waypoint.Inaccuracy");
            WaypointOnGround = ini.GetValue<bool>("Objective", "Waypoint.OnGround");

            UnitGroups = new DefinitionObjectiveUnitGroup[HQTools.EnumCount<ObjectiveUnitGroupRole>()];

            foreach (ObjectiveUnitGroupRole role in HQTools.EnumValues<ObjectiveUnitGroupRole>())
                UnitGroups[(int)role] = new DefinitionObjectiveUnitGroup(ini, role.ToString());

            return true;
        }
    }
}

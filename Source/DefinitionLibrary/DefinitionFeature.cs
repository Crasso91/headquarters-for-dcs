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
using System.Collections.Generic;
using System.Linq;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Definition of a mission feature: an objective, group of support units, script, etc. assigned to a theater node in a mission template.
    /// </summary>
    public sealed class DefinitionFeature : Definition, IComparable
    {
        /// <summary>
        /// Description of the feature to display in the UI.
        /// </summary>
        public string DisplayDescription { get; private set; }

        /// <summary>
        /// Localized string ID of the task message to add to the briefing.
        /// </summary>
        public string BriefingTask { get; private set; }

        /// <summary>
        /// Localized string ID of the remark message to add to the briefing.
        /// </summary>
        public string BriefingRemark { get; private set; }

        /// <summary>
        /// Category this feature belongs to.
        /// </summary>
        public FeatureCategory FeatureCategory { get; private set; }

        /// <summary>
        /// Valid theater node types for this feature.
        /// </summary>
        public TheaterLocationType[] FeatureLocationTypes { get; private set; }

        /// <summary>
        /// Special flags for this mission feature.
        /// </summary>
        public FeatureFlag[] FeatureFlags { get; private set; }

        /// <summary>
        /// Ogg files to include in the mission.
        /// </summary>
        public string[] MediaOgg { get; private set; } = new string[0];

        /// <summary>
        /// Scripts to include ONCE in the mission.
        /// </summary>
        public string[] ScriptsOnce { get; private set; } = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];

        /// <summary>
        /// Scripts to included once each type this feature is included.
        /// </summary>
        public string[] ScriptsEach { get; private set; } = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];

        /// <summary>
        /// A list of unit groups
        /// </summary>
        public DefinitionFeatureUnitGroup[] UnitGroups { get; private set; }

        /// <summary>
        /// Are waypoints enabled 
        /// </summary>
        public bool WaypointEnabled { get; private set; }
        public MinMaxD WaypointInaccuracy { get; private set; }
        public bool WaypointOnGround { get; private set; }

        protected override bool OnLoad(string path)
        {
            using (INIFile ini = new INIFile(path))
            {
                // [Info] section
                ID = ini.GetValue("Info", "ID", ID);
                DisplayName = ini.GetValue<string>("Info", "DisplayName");
                DisplayDescription = ini.GetValue<string>("Info", "DisplayDescription");

                // [Briefing] section
                BriefingTask = ini.GetValue<string>("Briefing", "Task");
                BriefingRemark = ini.GetValue<string>("Briefing", "Remark");

                // [Feature] section
                FeatureCategory = ini.GetValue<FeatureCategory>("Feature", "Category");
                FeatureFlags = ini.GetValueArray<FeatureFlag>("Feature", "Flags").Distinct().ToArray();
                FeatureLocationTypes = ini.GetValueArray<TheaterLocationType>("Feature", "LocationTypes").Distinct().ToArray();
                if (FeatureLocationTypes.Length == 0) FeatureLocationTypes = (TheaterLocationType[])Enum.GetValues(typeof(TheaterLocationType));

                // [Media] section
                MediaOgg = ini.GetValueArray<string>("Media", "Ogg");

                // [Scripts] section
                ScriptsOnce = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];
                ScriptsEach = new string[HQTools.MISSION_SCRIPT_SCOPE_COUNT];

                for (int i = 0; i < HQTools.MISSION_SCRIPT_SCOPE_COUNT; i++)
                {
                    ScriptsOnce = ini.GetValueArray<string>("Scripts", $"Once.{((MissionScriptScope)i).ToString()}");
                    ScriptsEach = ini.GetValueArray<string>("Scripts", $"Each.{((MissionScriptScope)i).ToString()}");
                }

                // [UnitGroups] section
                List<DefinitionFeatureUnitGroup> unitGroupsList = new List<DefinitionFeatureUnitGroup>();
                foreach (string k in ini.GetTopLevelKeysInSection("UnitGroups"))
                    unitGroupsList.Add(new DefinitionFeatureUnitGroup(ini, "UnitGroups", k, FeatureCategory == FeatureCategory.Objective));
                UnitGroups = unitGroupsList.ToArray();

                // Features of category "objective" can only include one unit group.
                if ((FeatureCategory == FeatureCategory.Objective) && (UnitGroups.Length > 0))
                    UnitGroups = UnitGroups.Take(1).ToArray();

                // [Waypoint] section
                WaypointEnabled = ini.GetValue<bool>("Waypoint", "Enabled");
                WaypointInaccuracy = ini.GetValue<MinMaxD>("Waypoint", "Inaccuracy");
                WaypointOnGround = ini.GetValue<bool>("Waypoint", "OnGround");
            }

            return true;
        }

        /// <summary>
        /// Returns the display name of the feature with the feature category.
        /// </summary>
        /// <returns>The display name of the feature, with the name of the category.</returns>
        public string GetDisplayNameWithCategory()
        {
            return $"{FeatureCategory} - {DisplayName}";
        }

        /// <summary>
        /// IComparable implementation. A feature's category decides its priority in the case all features can't be spawned on a given theater node.
        /// </summary>
        /// <param name="other">The other DefinitionFeature this one should be compared to.</param>
        /// <returns>Less than 0 if higher priority, more than 0 if lower priority, 0 if same priority.</returns>
        public int CompareTo(object other)
        {
            if (other is DefinitionFeature otherFeature)
                return (int)FeatureCategory - (int)otherFeature.FeatureCategory;

            return 0;
        }
    }
}

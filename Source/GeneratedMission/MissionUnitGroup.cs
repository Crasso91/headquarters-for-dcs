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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Headquarters4DCS.GeneratedMission
{
    /// <summary>
    /// Stores information about an unit group of the mission.
    /// </summary>
    public sealed class MissionUnitGroup : IDisposable
    {
        /// <summary>
        /// Default distance between units, in meters.
        /// </summary>
        private const double DEFAULT_DISTANCE_BETWEEN_UNITS = 25.0;

        /// <summary>
        /// Name of the group.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Unique ID of the group in DCS World.
        /// </summary>
        public int GroupID { get; set; } = 0;

        /// <summary>
        /// Coalition this group belongs to.
        /// </summary>
        public Coalition Coalition { get; set; } = Coalition.Blue;

        /// <summary>
        /// Category of the units of this group.
        /// </summary>
        public UnitCategory Category { get; set; } = UnitCategory.Vehicle;

        /// <summary>
        /// Lua file (from HQ4DCS\Include\Lua\Mission) to use as a template for the group.
        /// Filename must NOT have the ".lua" extension.
        /// </summary>
        public string LuaGroup { get; private set; } = "";

        /// <summary>
        /// It this unit group hidden on the F10 map?
        /// </summary>
        public bool Hidden { get; private set; } = false;

        /// <summary>
        /// Lua file (from HQ4DCS\Include\Lua\Mission) to use as a template for the units of the group.
        /// Filename must NOT have the ".lua" extension.
        /// </summary>
        public string LuaUnit { get; private set; } = "";

        /// <summary>
        /// X,Y coordinates of this unit group.
        /// </summary>
        public Coordinates Coordinates { get; set; } = new Coordinates(0, 0);

        /// <summary>
        /// The unit formation of this unit group.
        /// </summary>
        public UnitGroupFormation Formation { get; set; } = UnitGroupFormation.Random;

        /// <summary>
        /// List of the units (as internal DCS names) of this group.
        /// </summary>
        public List<string> Units = new List<string>();

        /// <summary>
        /// Skill level of this group.
        /// </summary>
        public DCSSkillLevel UnitsSkill { get; set; } = DCSSkillLevel.Good;

        /// <summary>
        /// Special flags for this group.
        /// </summary>
        public List<UnitGroupFlag> Flags { get; set; } = new List<UnitGroupFlag>();

        /// <summary>
        /// The number of units in this group. Read-only.
        /// </summary>
        public int UnitCount { get { return Units.Count; } }

        /// <summary>
        /// Radio frequency of this unit group.
        /// </summary>
        public float RadioFrequency { get; set; } = 0.0f;

        /// <summary>
        /// Custom values for this unit group, to
        /// </summary>
        public Dictionary<MissionUnitGroupCustomValueKey, string> CustomValues = new Dictionary<MissionUnitGroupCustomValueKey, string>();

        /// <summary>
        /// Returns the rotation (in radians) of an unit according to its formation and its index.
        /// </summary>
        /// <param name="unitIndex">The 0-based index of the unit</param>
        /// <returns>The rotation of the unit, in radians.</returns>
        public double GetUnitHeading(int unitIndex)
        {
            // TODO: return proper rotation according to formation
            return HQTools.RandomInt(360) * HQTools.DEGREES_TO_RADIANS;
        }

        /// <summary>
        /// Gets the coordinates of an unit of the group according to its formation and its index.
        /// </summary>
        /// <param name="unitIndex">The 0-based index of the unit</param>
        /// <returns>The coordinates of the unit.</returns>
        public Coordinates GetUnitCoordinates(int unitIndex)
        {
            // TODO: return proper position according to formation
            return Coordinates + new Coordinates(
                DEFAULT_DISTANCE_BETWEEN_UNITS * unitIndex,
                DEFAULT_DISTANCE_BETWEEN_UNITS * unitIndex);
        }

        public MissionUnitGroup(
            string luaGroup, string luaUnit, UnitCategory category, 
            int groupID, Coalition coalition, Coordinates coordinates,
            params string[] units)
        {
            LuaGroup = luaGroup;
            LuaUnit = luaUnit;
            Category = category;
            Coalition = coalition;
            GroupID = groupID;
            Units = units.ToList();
            Name = $"Group #{HQTools.ValToString(GroupID)}";
            Coordinates = coordinates;

            if (units.Length == 0)
                DebugLog.Instance.Log($"    WARNING: Tried to create an empty unit group at {Coordinates.ToString("F0")}.");
            else
                DebugLog.Instance.Log($"    Added a group of {units.Length} unit(s) ({string.Join(", ", Units)}) with ID #{GroupID} at {Coordinates.ToString("F0")}.");

            CheckAbsolueMaxUnitCount();
        }

        public static MissionUnitGroup FromCoalitionArmyAndUnitFamily(
            string luaGroup, string luaUnit,
            DefinitionCoalition army, TimePeriod timePeriod, UnitFamily family, int unitCount,
            int groupID, Coalition coalition, Coordinates coordinates)
        {
            List<string> unitList = new List<string>();

            UnitCategory category = HQTools.GetUnitCategoryFromUnitFamily(family);

            // FIXME: extendSearchToUnitsOfOtherFamilies and returnDefaultIfNoneFound should be parameters
            string[] availableUnits = army.GetUnits(timePeriod, family, false, false);

            if (availableUnits.Length == 0)
            {
                DebugLog.Instance.Log($"    WARNING: Cannot spawn unit group, no units of type {family.ToString().ToUpperInvariant()} for coalition {army.DisplayName.ToUpperInvariant()} in {timePeriod.ToString().ToUpperInvariant()}.");
                return null;
            }

            do
            {
                unitList.AddRange((from u in HQTools.RandomFrom(availableUnits).Split('|') select u.Trim()));
            } while (unitList.Count < unitCount);

            // Some unit categories (helicopters and planes) require all units to be the same type
            if ((category == UnitCategory.Helicopter) || (category == UnitCategory.Plane))
                for (int i = 1; i < unitList.Count; i++) unitList[i] = unitList[0];

            MissionUnitGroup uGroup = new MissionUnitGroup(
                luaGroup, luaUnit,
                category, groupID, coalition, coordinates,
                unitList.ToArray());

            return uGroup;
        }

        // Adds "embedded" air defense to ground vehicle groups
        //public void AddAirDefenseUnits(
        //    HQLibrary library, DefinitionCoalition army, TimePeriod timePeriod,
        //    CommonSettingsEnemyAirDefense airDefenseSettings)
        //{
        //    if (Category != UnitCategory.Vehicle) return;

        //    List<string> unitList = new List<string>(Units);

        //    if (HQTools.RandomDouble() < airDefenseSettings.EmbeddedChance)
        //    {
        //        int airDefCount = airDefenseSettings.EmbeddedCount.GetValue();

        //        for (int i = 0; i < airDefCount; i++)
        //        {
        //            string airDefUnit =
        //                HQTools.RandomFrom(
        //                    army.GetUnits(library, timePeriod,
        //                    HQTools.RandomFrom(airDefenseSettings.EmbeddedFamilies), true, false));
        //            if (airDefUnit == null) continue;

        //            unitList.Add(airDefUnit);
        //        }
        //    }

        //    // Randomize list order (so air defense units are not always at the end)
        //    Units = unitList.OrderBy(x => HQTools.RandomInt()).ToList();

        //    CheckAbsolueMaxUnitCount();
        //}

        private void CheckAbsolueMaxUnitCount()
        {
            int absoluteMaxUnitCount = 1;
            switch (Category)
            {
                case UnitCategory.Helicopter: absoluteMaxUnitCount = 4; break;
                case UnitCategory.Plane: absoluteMaxUnitCount = 4; break;
                case UnitCategory.Ship: absoluteMaxUnitCount = 1; break;
                case UnitCategory.Static: absoluteMaxUnitCount = 1; break;
                case UnitCategory.Vehicle: absoluteMaxUnitCount = 8; break;
            }

            if (UnitCount > absoluteMaxUnitCount)
                Units = Units.Take(absoluteMaxUnitCount).ToList();
        }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }
    }
}

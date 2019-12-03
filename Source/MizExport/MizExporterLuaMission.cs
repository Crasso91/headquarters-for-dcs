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

using Headquarters4DCS.Mission;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Headquarters4DCS.Miz
{
    /// <summary>
    /// Creates the "Mission" entry in the MIZ file.
    /// </summary>
    public sealed class MizExporterLuaMission : IDisposable
    {
        /// <summary>
        /// The random variaton applied to some special coordinates, such as map center, to make sure all units are not
        /// spawned in the same place (in meters).
        /// </summary>
        private const double UNIT_RANDOM_WAYPOINT_VARIATION = 150.0;

        /// <summary>
        /// Current unique unit ID. Incremented each time a new unit is added.
        /// </summary>
        private int UnitID = 1;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MizExporterLuaMission() { }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Generates the content of the Lua file.
        /// </summary>
        /// <param name="mission">An HQ4DCS mission.</param>
        /// <returns>The contents of the Lua file.</returns>
        public string MakeLua(DCSMission mission)
        {
            UnitID = 1;

            int i;

            string lua = HQTools.ReadIncludeLuaFile("Mission.lua");

            HQTools.ReplaceKey(ref lua, "MissionName", mission.BriefingName);

            HQTools.ReplaceKey(ref lua, "CenterX", mission.MapCenter.X);
            HQTools.ReplaceKey(ref lua, "CenterY", mission.MapCenter.Y);

            HQTools.ReplaceKey(ref lua, "TheaterID", mission.TheaterDefinition);

            HQTools.ReplaceKey(ref lua, "DateDay", mission.DateDay);
            HQTools.ReplaceKey(ref lua, "DateMonth", (int)mission.DateMonth + 1);
            HQTools.ReplaceKey(ref lua, "DateYear", mission.DateYear);

            HQTools.ReplaceKey(ref lua, "MissionTime", mission.TimeTotalSeconds);

            HQTools.ReplaceKey(ref lua, "WeatherCloudsBase", mission.WeatherCloudBase);
            HQTools.ReplaceKey(ref lua, "WeatherCloudsDensity", mission.WeatherCloudsDensity);
            HQTools.ReplaceKey(ref lua, "WeatherCloudsPrecipitation", (int)mission.WeatherCloudsPrecipitation);
            HQTools.ReplaceKey(ref lua, "WeatherCloudsThickness", mission.WeatherCloudsThickness);
            HQTools.ReplaceKey(ref lua, "WeatherDustDensity", mission.WeatherDustDensity);
            HQTools.ReplaceKey(ref lua, "WeatherDustEnabled", mission.WeatherDustEnabled);
            HQTools.ReplaceKey(ref lua, "WeatherFogEnabled", mission.WeatherTurbulence);
            HQTools.ReplaceKey(ref lua, "WeatherFogThickness", mission.WeatherFogThickness);
            HQTools.ReplaceKey(ref lua, "WeatherFogVisibility", mission.WeatherFogVisibility);
            HQTools.ReplaceKey(ref lua, "WeatherGroundTurbulence", mission.WeatherTurbulence);
            HQTools.ReplaceKey(ref lua, "WeatherQNH", mission.WeatherQNH);
            HQTools.ReplaceKey(ref lua, "WeatherTemperature", mission.WeatherTemperature);
            HQTools.ReplaceKey(ref lua, "WeatherVisibilityDistance", mission.WeatherVisibility);
            for (i = 0; i < 3; i++)
            {
                HQTools.ReplaceKey(ref lua, $"WeatherWind{i + 1}", mission.WeatherWindSpeed[i]);
                HQTools.ReplaceKey(ref lua, $"WeatherWind{i + 1}Dir", mission.WeatherWindDirection[i]);
            }

            foreach (Coalition c in (Coalition[])Enum.GetValues(typeof(Coalition)))
            {
                HQTools.ReplaceKey(ref lua, $"Countries{c}", MakeCountryList(mission, c));
                HQTools.ReplaceKey(ref lua, $"Coalition{c}", MakeCoalitionTable(mission, c));
            }

            HQTools.ReplaceKey(ref lua, "CustomOptions", GetCustomDCSOptionsLua(mission));

            HQTools.ReplaceKey(ref lua, "BriefingDescription", mission.BriefingRawText);

            if (mission.CoalitionPlayer == Coalition.Blue)
            {
                HQTools.ReplaceKey(ref lua, "BriefingPicturesBlue", "[1] = \"ResKey_TitleImage\",");
                HQTools.ReplaceKey(ref lua, "BriefingPicturesRed", "");
            }
            else
            {
                HQTools.ReplaceKey(ref lua, "BriefingPicturesBlue", "");
                HQTools.ReplaceKey(ref lua, "BriefingPicturesRed", "[1] = \"ResKey_TitleImage\",");
            }

            // Red/blue briefings are not used for the moment. The full briefing is located in BriefingDescription. 
            HQTools.ReplaceKey(ref lua, "BriefingBlue", "");
            HQTools.ReplaceKey(ref lua, "BriefingRed", "");

            HQTools.ReplaceKey(ref lua, "BullseyeRedX", mission.Bullseye[(int)Coalition.Blue].X);
            HQTools.ReplaceKey(ref lua, "BullseyeRedY", mission.Bullseye[(int)Coalition.Blue].Y);
            HQTools.ReplaceKey(ref lua, "BullseyeBlueX", mission.Bullseye[(int)Coalition.Red].X);
            HQTools.ReplaceKey(ref lua, "BullseyeBlueY", mission.Bullseye[(int)Coalition.Red].Y);

            HQTools.ReplaceKey(ref lua, "FailureEnabled", ""); // TODO: change
            HQTools.ReplaceKey(ref lua, "Waypoints", CreatePlayerWaypointsLua(mission.Waypoints));
            HQTools.ReplaceKey(ref lua, "FinalPlayerWPIndex", mission.Waypoints.Length + 2);

            HQTools.ReplaceKey(ref lua, "CenterX", mission.MapCenter.X);
            HQTools.ReplaceKey(ref lua, "CenterY", mission.MapCenter.Y);

            return lua;
        }

        private string GetCustomDCSOptionsLua(DCSMission missionHQ)
        {
            string optionsLua = "";

            // FIXME: find proper Lua keys for the options
            optionsLua += AppendDCSOptionLua(missionHQ.RealismAllowExternalViews, "externalViews");
            optionsLua += AppendDCSOptionLua(missionHQ.RealismBirdStrikes, "birdStrike");
            optionsLua += AppendDCSOptionLua(missionHQ.RealismRandomFailures, "randomFailures");

            return optionsLua;
        }

        private string AppendDCSOptionLua(DCSOption dcsOption, string luaKey, string valueTrue = "true", string valueFalse = "false")
        {
            switch (dcsOption)
            {
                // case DCSOption.Default:
                default:  return "";
                case DCSOption.ForceDisabled: return $"[\"{luaKey}\"] = {valueFalse},\n";
                case DCSOption.ForceEnabled: return $"[\"{luaKey}\"] = {valueTrue},\n";
            }
        }

        private string MakeCountryList(DCSMission missionHQ, Coalition coalition)
        {
            string countryTableLua = "";

            for (int i = 0; i < missionHQ.Countries[(int)coalition].Length; i++)
                countryTableLua += $"[{i + 1}] = {(int)missionHQ.Countries[(int)coalition][i]},\r\n";

            return countryTableLua;
        }

        private string MakeCoalitionTable(DCSMission missHQ, Coalition coalition)
        {
            string coalitionTableLua = "";

            for (int i = 0; i < missHQ.Countries[(int)coalition].Length; i++)
                coalitionTableLua +=
                    $"[{i + 1}] =\r\n" +
                    "{\r\n" +
                    $"[\"id\"] = {(int)missHQ.Countries[(int)coalition][i]},\r\n" +
                    ((i == 0) ? MakeUnitsLua(missHQ, coalition) + "\r\n" : "") + // All units belong to the first country in the coalition, others are empty
                    $"}}, -- end of [{i + 1}]\r\n";

            return coalitionTableLua;
        }

        private string MakeUnitsLua(DCSMission missHQ, Coalition coalition)
        {
            string allUnitsLua = HQTools.ReadIncludeLuaFile("Mission\\CoalitionUnits.lua");

            foreach (UnitCategory uc in Enum.GetValues(typeof(UnitCategory)))
                HQTools.ReplaceKey(ref allUnitsLua, uc.ToString(), MakeUnitsCategoryLua(missHQ, coalition, uc));

            return allUnitsLua;
        }

        private string MakeUnitsCategoryLua(DCSMission missHQ, Coalition coalition, UnitCategory categ)
        {
            string categoryUnitsLua = "";

            DCSMissionUnitGroup[] groups =
                (from g in missHQ.UnitGroups
                 where g.Coalition == coalition && g.Category == categ
                 select g).ToArray();

            int categIndex = 1;

            foreach (DCSMissionUnitGroup g in groups)
            {
                string groupLua = HQTools.ReadIncludeLuaFile($"Mission\\{g.LuaGroup}.lua");

                HQTools.ReplaceKey(ref groupLua, "Units", MakeUnitsUnitsLua(g, missHQ.SinglePlayer)); // Must be first, so full-group replacements come after unit-specific ones

                //if (g.Waypoints.Count > 0)
                //    HQTools.ReplaceKey(ref groupLua, "Waypoints", CreatePlayerWaypointsLua(g.Waypoints)); // Must be first after units

                HQTools.ReplaceKey(ref groupLua, "Name", g.Name);
                HQTools.ReplaceKey(ref groupLua, "GroupID", g.GroupID);
                HQTools.ReplaceKey(ref groupLua, "Frequency", g.RadioFrequency, "F2");
                HQTools.ReplaceKey(ref groupLua, "X", g.Coordinates.X);
                HQTools.ReplaceKey(ref groupLua, "Y", g.Coordinates.Y);
                HQTools.ReplaceKey(ref groupLua, "ObjectiveCenterX", missHQ.ObjectivesCenterPoint.X + HQTools.RandomDouble(-UNIT_RANDOM_WAYPOINT_VARIATION, UNIT_RANDOM_WAYPOINT_VARIATION));
                HQTools.ReplaceKey(ref groupLua, "ObjectiveCenterY", missHQ.ObjectivesCenterPoint.Y + HQTools.RandomDouble(-UNIT_RANDOM_WAYPOINT_VARIATION, UNIT_RANDOM_WAYPOINT_VARIATION));
                HQTools.ReplaceKey(ref groupLua, "Hidden", g.Hidden);
                foreach (DCSMissionUnitGroupCustomValueKey k in g.CustomValues.Keys)
                {
                    if (k.UnitIndex != -1) continue; // Replacement applies only to a single unit, don't apply it to the whole group
                    HQTools.ReplaceKey(ref groupLua, k.Key, g.CustomValues[k]);
                }
                HQTools.ReplaceKey(ref groupLua, "Index", categIndex); // Must be last, used by other values

                categoryUnitsLua += groupLua + "\r\n";
                categIndex++;
            }

            return categoryUnitsLua;
        }

        private string CreatePlayerWaypointsLua(DCSMissionWaypoint[] waypoints)
        {
            // FIXME: first and last WP
            // FIXME: EPLRS on first WP?

            string flightPlanLua = "";
            //string baseWPLuaFirst = HQTools.ReadIncludeLuaFile("Mission\\WaypointPlayerTakeoff.lua");
            //string baseWPLuaLast = HQTools.ReadIncludeLuaFile("Mission\\WaypointPlayerLanding.lua");
            string baseWPLua = HQTools.ReadIncludeLuaFile("Mission\\WaypointPlayer.lua");

            for (int i = 0; i < waypoints.Length; i++)
            {
                string wpLua = baseWPLua;
                //if (i == 0) continue; // First waypoint is included in GroupAircraftPlayer.lua
                //// FIXME: Remove - wpLua = baseWPLuaFirst;
                //else if (i == waypoints.Length - 1) wpLua = baseWPLuaLast;
                //else wpLua = baseWPLua;

                HQTools.ReplaceKey(ref wpLua, "Action", waypoints[i].WPAction);
                HQTools.ReplaceKey(ref wpLua, "Altitude", waypoints[i].AltitudeMultiplier);
                HQTools.ReplaceKey(ref wpLua, "AltitudeType", (waypoints[i].AltitudeMultiplier > 0) ? "BARO" : "RADIO");
                HQTools.ReplaceKey(ref wpLua, "AirdromeID", waypoints[i].AirdromeID);
                HQTools.ReplaceKey(ref wpLua, "Name", waypoints[i].Name);
                HQTools.ReplaceKey(ref wpLua, "Speed", waypoints[i].SpeedMultiplier);
                HQTools.ReplaceKey(ref wpLua, "Type", waypoints[i].WPType);
                HQTools.ReplaceKey(ref wpLua, "X", waypoints[i].Coordinates.X);
                HQTools.ReplaceKey(ref wpLua, "Y", waypoints[i].Coordinates.Y);
                HQTools.ReplaceKey(ref wpLua, "Index", i + 2); // Must be last, used by other values

                flightPlanLua += wpLua + "\n";
            }

            return flightPlanLua;
        }

        private string MakeUnitsUnitsLua(DCSMissionUnitGroup group, bool singlePlayer)
        {
            string unitsLua = "";

            for (int i = 0; i < group.UnitCount;i++)
            {
                string singleUnitLua = HQTools.ReadIncludeLuaFile($"Mission\\{group.LuaUnit}.lua");

                DCSSkillLevel skillLevel = group.UnitsSkill;
                if (group.Flags.Contains(UnitGroupFlag.FirstUnitIsClient) && (i == 0))
                    skillLevel = DCSSkillLevel.Client;

                HQTools.ReplaceKey(ref singleUnitLua, "Name", group.Name); // Must be replaced before "Index" because some unit names contain "$INDEX$"
                HQTools.ReplaceKey(ref singleUnitLua, "UnitID", UnitID);
                HQTools.ReplaceKey(ref singleUnitLua, "Unit", group.Units[i]);
                HQTools.ReplaceKey(ref singleUnitLua, "Heading", group.GetUnitHeading(i));
                HQTools.ReplaceKey(ref singleUnitLua, "Skill", skillLevel.ToString());
                HQTools.ReplaceKey(ref singleUnitLua, "X", group.GetUnitCoordinates(i).X);
                HQTools.ReplaceKey(ref singleUnitLua, "Y", group.GetUnitCoordinates(i).Y);
                foreach (DCSMissionUnitGroupCustomValueKey k in group.CustomValues.Keys)
                {
                    if (k.UnitIndex != i) continue; // Replacement does not target this unit, continue
                    HQTools.ReplaceKey(ref singleUnitLua, k.Key, group.CustomValues[k]);
                }

                HQTools.ReplaceKey(ref singleUnitLua, "Index", i + 1); // Must be last, used by other values

                unitsLua += singleUnitLua + "\r\n";
                UnitID++;
            }

            return unitsLua;
        }
    }
}

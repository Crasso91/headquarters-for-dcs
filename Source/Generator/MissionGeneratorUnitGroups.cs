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
using Headquarters4DCS.Mission;
using Headquarters4DCS.Template;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Headquarters4DCS.Generator
{
    public sealed class MissionGeneratorUnitGroups : IDisposable
    {
        private const string TANKER_TACAN = "40X"; // FIXME: load from file

        private readonly DefinitionLanguage Language;
        private readonly MissionGeneratorCallsign CSGenerator;

        private Dictionary<int, int> AirdromeParkingNumber = new Dictionary<int, int>();

        private List<int> UsedOnboardNumbers = new List<int>();
        private List<int> UsedFakeGroupNamesNumbers = new List<int>();

        ///// <summary>
        ///// The flight package's (player FG + escort AI FG) total "air-to-air" score. Used to know how many enemy aircraft should be spawned.
        ///// </summary>
        //private double TotalAAValue = 0;

        /// <summary>
        /// Unique ID of the next group to spawn. Only for "non-critical" groups.
        /// (objective "waypoint" groups have an ID of 1000*(objectiveIndex + 1), objective "center" group has an ID of 10000)
        /// </summary>
        private int GroupID;

        public MissionGeneratorUnitGroups(DefinitionLanguage language, MissionGeneratorCallsign csGenerator)
        {
            Language = language;
            GroupID = 1;
            CSGenerator = csGenerator;
            //TotalAAValue = 0;
        }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }

        //public void GeneratePlayerFlightGroups(HQMission mission, MissionTemplate template, DefinitionMissionObjective missionObjective)
        //{
        //    HQDebugLog.Instance.Log("Generating player flight groups...");

        //    foreach (MissionTemplatePlayerFlightGroup pfg in template.FlightGroupsPlayers)
        //    {
        //        DefinitionUnit aircraftDefinition = Library.GetDefinition<DefinitionUnit>(pfg.Aircraft);

        //        if ((aircraftDefinition == null) || !aircraftDefinition.AircraftPlayerControllable)
        //            throw new Exception($"Player aircraft \"{pfg.Aircraft}\" not found.");

        //        HQMissionUnitGroup uGroup = new HQMissionUnitGroup(
        //            "GroupAircraftPlayer", "UnitAircraft", aircraftDefinition.Category,
        //            GroupID, template.PlayerCoalition, mission.Airbases[1].Coordinates,
        //            Enumerable.Repeat(aircraftDefinition.ID, pfg.GroupSize).ToArray());

        //        SetupAircraftGroup(
        //            uGroup, mission, CallsignFamily.Aircraft, true,
        //            aircraftDefinition, missionObjective.MissionPayload, mission.Airbases[0].DCSID);

        //        if (pfg.AIWingmen)
        //        {
        //            // set group AI to default allied AI, and add a special flag so the first unit of the group will be a client
        //            uGroup.UnitsSkill = HQSkillToDCSSkill(template.AlliedSkill);
        //            uGroup.Flags.Add(UnitGroupFlag.FirstUnitIsClient);
        //        }
        //        else // No AI wingmen: make all units clients
        //            uGroup.UnitsSkill = DCSSkillLevel.Client;

        //        uGroup.CustomValues.Add("AirdromeID", HQTools.ValToString(mission.Airbases[0].DCSID));
        //        uGroup.CustomValues.Add("FinalWPIndex", HQTools.ValToString(mission.Waypoints.Count + 2));
        //        uGroup.CustomValues.Add("FinalX", HQTools.ValToString(mission.Airbases[1].Coordinates.X));
        //        uGroup.CustomValues.Add("FinalY", HQTools.ValToString(mission.Airbases[1].Coordinates.Y));
        //        uGroup.CustomValues.Add("FinalAirdromeID", HQTools.ValToString(mission.Airbases[1].DCSID));
        //        uGroup.CustomValues.Add("DCSTask", MGTools.GetDCSTaskNameString(missionObjective.DCSTask));
        //        uGroup.CustomValues.Add("DCSTaskTasks", MGTools.GetDCSTaskAdditionalTasksString(missionObjective.DCSTask, 2));

        //        uGroup.CustomValues.Add("TakeoffAltitude", "13"); // FIXME: fixed value in the Lua file
        //        uGroup.CustomValues.Add("TakeOffAltitudeType", "BARO"); // FIXME: fixed value in the Lua file
        //        uGroup.CustomValues.Add("TakeOffSpeed", "130.0"); // FIXME: fixed value in the Lua file

        //        switch (pfg.StartFrom)
        //        {
        //            default: // PlayerFlightGroupStartLocation.InAir
        //                uGroup.CustomValues.Add("TakeoffAltitude", HQTools.ValToString(20000 * HQTools.FEET_TO_METERS * HQTools.RandomDouble(0.9, 1.1))); // FIXME: altitude by aircraft
        //                uGroup.CustomValues.Add("TakeOffSpeed", HQTools.ValToString(300.0 * HQTools.KNOTS_TO_METERSPERSECOND)); // FIXME: speed by aircraft
        //                uGroup.CustomValues.Add("TakeOffType", "BARO");
        //                uGroup.CustomValues.Add("TakeOffAction", "Turning Point");
        //                uGroup.CustomValues.Add("TakeOffType", "Turning Point");
        //                break;
        //            case PlayerFlightGroupStartLocation.FromRunway:
        //                uGroup.CustomValues.Add("TakeOffAction", "From Runway");
        //                uGroup.CustomValues.Add("TakeOffType", "TakeOff");
        //                break;
        //            case PlayerFlightGroupStartLocation.FromParking:
        //                uGroup.CustomValues.Add("TakeOffAction", "From Parking Area");
        //                uGroup.CustomValues.Add("TakeOffType", "TakeOffParking");
        //                break;
        //            case PlayerFlightGroupStartLocation.FromParkingHot:
        //                uGroup.CustomValues.Add("TakeOffAction", "From Parking Area Hot");
        //                uGroup.CustomValues.Add("TakeOffType", "TakeOffParkingHot");
        //                break;
        //        }

        //        foreach (HQMissionWaypoint wp in mission.Waypoints)
        //            uGroup.Waypoints.Add(
        //                new HQMissionWaypoint(
        //                    wp.Coordinates, wp.Name, wp.AltitudeMultiplier * HQTools.RandomDouble(0.9, 1.1), HQTools.RandomDouble(0.9, 1.1)));

        //        mission.BriefingFlightPackage.Add(
        //        new HQMissionBriefingFlightGroup(
        //            uGroup.Name, uGroup.Units[0], DCSAircraftTask.CAS, // FIXME: tasking
        //            uGroup.UnitCount, uGroup.RadioFrequency)); // FIXME: complete

        //        mission.UnitGroups.Add(uGroup);

        //        GroupID++;
        //        TotalAAValue += aircraftDefinition.AircraftAirToAirRating[(missionObjective.MissionPayload == AircraftPayloadType.A2A) ? 0 : 1];
        //    }

        //    HQDebugLog.Instance.Log();
        //}

        public int AddPlayerFlightGroup(DCSMission mission, MissionTemplate template, MissionTemplatePlayerFlightGroup pfg/*, MissionTemplateLocation airbaseNode*/)
        {
            //if ((airbaseNode == null) || (airbaseNode.Definition.LocationType != TheaterLocationType.Airbase)) return -1; // No node, or not is not an airbase
            //DefinitionUnit aircraftDefinition = Library.Instance.GetDefinition<DefinitionUnit>(flightGroupTemplate.AircraftType);
            ////DefinitionTheaterNodeAirbase airbaseDefinition = HQLibrary.Instance.GetDefinition<DefinitionUnit>(flightGroupTemplate.AircraftType);
            //if (aircraftDefinition == null) return -1; // Aircraft does not exist

            //DCSMissionUnitGroup unitGroup = new DCSMissionUnitGroup(
            //    "GroupAircraftPlayer", "UnitAircraft", aircraftDefinition.Category,
            //    GroupID, (Coalition)airbaseNode.Coalition, airbaseNode.Definition.Position,
            //    Enumerable.Repeat(aircraftDefinition.ID, flightGroupTemplate.Count).ToArray());

            //SetupAircraftGroup(
            //    unitGroup, mission, CallsignFamily.Aircraft, true,
            //    aircraftDefinition, GetPayloadByPlayerGroupTask(flightGroupTemplate.Task), airbaseNode.Definition.Airbase.ID);

            //bool usePlayerInsteadOfClient = (template.GetPlayerCount() < 2) && !template.PreferencesForceClientInSP;

            //if (flightGroupTemplate.AIWingmen)
            //{
            //    // set group AI to default allied AI, and add a special flag so the first unit of the group will be a client
            //    unitGroup.UnitsSkill = HQSkillToDCSSkill(template.SituationAllySkillAir); // NEXTVERSION: allySkillAir or enemySkillAir according to airbase coalition
            //    unitGroup.Flags.Add(usePlayerInsteadOfClient ? UnitGroupFlag.FirstUnitIsPlayer : UnitGroupFlag.FirstUnitIsClient);
            //}
            //else // No AI wingmen: make all units clients
            //    unitGroup.UnitsSkill = usePlayerInsteadOfClient ? DCSSkillLevel.Player : DCSSkillLevel.Client;

            //unitGroup.CustomValues.Add("AirdromeID", HQTools.ValToString(airbaseNode.Definition.Airbase.ID));
            ////unitGroup.CustomValues.Add("FinalWPIndex", HQTools.ValToString(mission.Waypoints.Count + 2));
            //unitGroup.CustomValues.Add("FinalX", HQTools.ValToString(airbaseNode.Definition.Position.X));
            //unitGroup.CustomValues.Add("FinalY", HQTools.ValToString(airbaseNode.Definition.Position.Y));
            //unitGroup.CustomValues.Add("FinalAirdromeID", HQTools.ValToString(airbaseNode.Definition.Airbase.ID));
            ////unitGroup.CustomValues.Add("DCSTask", MGTools.GetDCSTaskNameString(missionObjective.DCSTask));
            ////unitGroup.CustomValues.Add("DCSTaskTasks", MGTools.GetDCSTaskAdditionalTasksString(missionObjective.DCSTask, 2));

            //unitGroup.CustomValues.Add("TakeoffAltitude", "13"); // FIXME: fixed value in the Lua file
            //unitGroup.CustomValues.Add("TakeOffAltitudeType", "BARO"); // FIXME: fixed value in the Lua file
            //unitGroup.CustomValues.Add("TakeOffSpeed", "130.0"); // FIXME: fixed value in the Lua file

            //switch (flightGroupTemplate.StartLocation)
            //{
            //    default: // PlayerFlightGroupStartLocation.InAir
            //        unitGroup.CustomValues.Add("TakeoffAltitude", HQTools.ValToString(20000 * HQTools.FEET_TO_METERS * HQTools.RandomDouble(0.9, 1.1))); // FIXME: altitude by aircraft
            //        unitGroup.CustomValues.Add("TakeOffSpeed", HQTools.ValToString(300.0 * HQTools.KNOTS_TO_METERSPERSECOND)); // FIXME: speed by aircraft
            //        unitGroup.CustomValues.Add("TakeOffType", "BARO");
            //        unitGroup.CustomValues.Add("TakeOffAction", "Turning Point");
            //        unitGroup.CustomValues.Add("TakeOffType", "Turning Point");
            //        break;
            //    case PlayerFlightGroupStartLocation.FromRunway:
            //        unitGroup.CustomValues.Add("TakeOffAction", "From Runway");
            //        unitGroup.CustomValues.Add("TakeOffType", "TakeOff");
            //        break;
            //    case PlayerFlightGroupStartLocation.FromParking:
            //        unitGroup.CustomValues.Add("TakeOffAction", "From Parking Area");
            //        unitGroup.CustomValues.Add("TakeOffType", "TakeOffParking");
            //        break;
            //    case PlayerFlightGroupStartLocation.FromParkingHot:
            //        unitGroup.CustomValues.Add("TakeOffAction", "From Parking Area Hot");
            //        unitGroup.CustomValues.Add("TakeOffType", "TakeOffParkingHot");
            //        break;
            //}

            //mission.BriefingFlightPackage.Add(
            //    new DCSMissionBriefingFlightGroup(
            //        unitGroup.Name, unitGroup.Units[0], DCSAircraftTask.CAS, airbaseNode.Definition.DisplayName, // FIXME: tasking
            //        unitGroup.UnitCount, unitGroup.RadioFrequency)); // FIXME: complete

            //mission.UnitGroups.Add(unitGroup);
            //GroupID++;

            //return GroupID - 1;
            return -1;
        }

        private AircraftPayloadType GetPayloadByPlayerGroupTask(DCSFlightGroupTask task)
        {
            switch (task)
            {
                case DCSFlightGroupTask.AntishipStrike:
                    return AircraftPayloadType.AntiShip;

                case DCSFlightGroupTask.CAP:
                case DCSFlightGroupTask.FighterSweep:
                case DCSFlightGroupTask.Intercept:
                    return AircraftPayloadType.A2A;

                case DCSFlightGroupTask.CAS:
                case DCSFlightGroupTask.GroundAttack:
                case DCSFlightGroupTask.PinpointStrike:
                case DCSFlightGroupTask.RunwayAttack:
                    return AircraftPayloadType.A2G;

                case DCSFlightGroupTask.SEAD:
                    return AircraftPayloadType.SEAD;

                case DCSFlightGroupTask.Escort:
                case DCSFlightGroupTask.Reconnaissance:
                case DCSFlightGroupTask.Transport:
                    return AircraftPayloadType.Default;
            }

            return AircraftPayloadType.Default;
        }

        public DCSMissionUnitGroup AddNodeFeatureGroup(
            DCSMission mission, MissionTemplate template,
            DefinitionFeatureUnitGroup unitGroupDefinition, Coordinates location)
        {
            DebugLog.Instance.Log("Adding unit group...");

            Coalition groupCoalition = unitGroupDefinition.Flags.Contains(MissionObjectiveUnitGroupFlags.Friendly) ? mission.CoalitionPlayer : mission.CoalitionEnemy;

            DCSMissionUnitGroup unitGroup = null;

            UnitFamily selectedFamily = HQTools.RandomFrom(unitGroupDefinition.Families);

            unitGroup =
                DCSMissionUnitGroup.FromCoalitionArmyAndUnitFamily(
                    unitGroupDefinition.LuaGroup, unitGroupDefinition.LuaUnit,
                    Library.Instance.GetDefinition<DefinitionCoalition>(groupCoalition == Coalition.Red ? template.ContextCoalitionRed : template.ContextCoalitionBlue),
                    template.ContextTimePeriod, selectedFamily, unitGroupDefinition.UnitCount.GetValue(),
                    GroupID, groupCoalition, location);


            if ((unitGroup == null) || (unitGroup.UnitCount == 0)) // TODO: is group critical or not? If not, simply output a warning
                throw new Exception($"Found no valid units to generate mission critical group of {unitGroup.Coalition} {unitGroup.Category}.");

            if (unitGroup.IsAircraft)
            {
                CallsignFamily csFamily = CallsignFamily.Aircraft;

                switch (selectedFamily)
                {
                    case UnitFamily.PlaneAWACS:
                        csFamily = CallsignFamily.AWACS;
                        break;
                    case UnitFamily.PlaneTankerBasket:
                    case UnitFamily.PlaneTankerBoom:
                        csFamily = CallsignFamily.Tanker;
                        break;
                }

                SetupAircraftGroup(unitGroup, mission, csFamily, unitGroupDefinition.Flags.Contains(MissionObjectiveUnitGroupFlags.Friendly));
            }

            // TODO: embedded air defense
            //if (grp.Flags.Contains(MissionObjectiveUnitGroupFlags.AllowAirDefense) && !grp.Flags.Contains(MissionObjectiveUnitGroupFlags.Friendly))
            //    unitGroup.AddAirDefenseUnits(Library, coalitions[(int)groupCoalition], template.TimePeriod, Library.Common.EnemyAirDefense[(int)HQTools.ResolveRandomAmount(template.EnemyAirDefense)]);

            mission.UnitGroups.Add(unitGroup);

            DebugLog.Instance.Log();

            GroupID++;
            return unitGroup; // GroupID - 1;
        }

        //public void GeneratePlayerFlightGroups(HQMission mission, MissionTemplate template, DefinitionMissionObjective missionObjective)
        //{
        //    HQDebugLog.Instance.Log("Generating player flight groups...");

        //    foreach (MissionTemplatePlayerFlightGroup pfg in template.FlightGroupsPlayers)
        //    {
        //        DefinitionUnit aircraftDefinition = Library.GetDefinition<DefinitionUnit>(pfg.Aircraft);

        //        if ((aircraftDefinition == null) || !aircraftDefinition.AircraftPlayerControllable)
        //            throw new Exception($"Player aircraft \"{pfg.Aircraft}\" not found.");

        //        HQMissionUnitGroup uGroup = new HQMissionUnitGroup(
        //            "GroupAircraftPlayer", "UnitAircraft", aircraftDefinition.Category,
        //            GroupID, template.PlayerCoalition, mission.Airbases[1].Coordinates,
        //            Enumerable.Repeat(aircraftDefinition.ID, pfg.GroupSize).ToArray());

        //        SetupAircraftGroup(
        //            uGroup, mission, CallsignFamily.Aircraft, true,
        //            aircraftDefinition, missionObjective.MissionPayload, mission.Airbases[0].DCSID);

        //        if (pfg.AIWingmen)
        //        {
        //            // set group AI to default allied AI, and add a special flag so the first unit of the group will be a client
        //            uGroup.UnitsSkill = HQSkillToDCSSkill(template.AlliedSkill);
        //            uGroup.Flags.Add(UnitGroupFlag.FirstUnitIsClient);
        //        }
        //        else // No AI wingmen: make all units clients
        //            uGroup.UnitsSkill = DCSSkillLevel.Client;

        //        uGroup.CustomValues.Add("AirdromeID", HQTools.ValToString(mission.Airbases[0].DCSID));
        //        uGroup.CustomValues.Add("FinalWPIndex", HQTools.ValToString(mission.Waypoints.Count + 2));
        //        uGroup.CustomValues.Add("FinalX", HQTools.ValToString(mission.Airbases[1].Coordinates.X));
        //        uGroup.CustomValues.Add("FinalY", HQTools.ValToString(mission.Airbases[1].Coordinates.Y));
        //        uGroup.CustomValues.Add("FinalAirdromeID", HQTools.ValToString(mission.Airbases[1].DCSID));
        //        uGroup.CustomValues.Add("DCSTask", MGTools.GetDCSTaskNameString(missionObjective.DCSTask));
        //        uGroup.CustomValues.Add("DCSTaskTasks", MGTools.GetDCSTaskAdditionalTasksString(missionObjective.DCSTask, 2));

        //        uGroup.CustomValues.Add("TakeoffAltitude", "13"); // FIXME: fixed value in the Lua file
        //        uGroup.CustomValues.Add("TakeOffAltitudeType", "BARO"); // FIXME: fixed value in the Lua file
        //        uGroup.CustomValues.Add("TakeOffSpeed", "130.0"); // FIXME: fixed value in the Lua file

        //        switch (pfg.StartFrom)
        //        {
        //            default: // PlayerFlightGroupStartLocation.InAir
        //                uGroup.CustomValues.Add("TakeoffAltitude", HQTools.ValToString(20000 * HQTools.FEET_TO_METERS * HQTools.RandomDouble(0.9, 1.1))); // FIXME: altitude by aircraft
        //                uGroup.CustomValues.Add("TakeOffSpeed", HQTools.ValToString(300.0 * HQTools.KNOTS_TO_METERSPERSECOND)); // FIXME: speed by aircraft
        //                uGroup.CustomValues.Add("TakeOffType", "BARO");
        //                uGroup.CustomValues.Add("TakeOffAction", "Turning Point");
        //                uGroup.CustomValues.Add("TakeOffType", "Turning Point");
        //                break;
        //            case PlayerFlightGroupStartLocation.FromRunway:
        //                uGroup.CustomValues.Add("TakeOffAction", "From Runway");
        //                uGroup.CustomValues.Add("TakeOffType", "TakeOff");
        //                break;
        //            case PlayerFlightGroupStartLocation.FromParking:
        //                uGroup.CustomValues.Add("TakeOffAction", "From Parking Area");
        //                uGroup.CustomValues.Add("TakeOffType", "TakeOffParking");
        //                break;
        //            case PlayerFlightGroupStartLocation.FromParkingHot:
        //                uGroup.CustomValues.Add("TakeOffAction", "From Parking Area Hot");
        //                uGroup.CustomValues.Add("TakeOffType", "TakeOffParkingHot");
        //                break;
        //        }

        //        foreach (HQMissionWaypoint wp in mission.Waypoints)
        //            uGroup.Waypoints.Add(
        //                new HQMissionWaypoint(
        //                    wp.Coordinates, wp.Name, wp.AltitudeMultiplier * HQTools.RandomDouble(0.9, 1.1), HQTools.RandomDouble(0.9, 1.1)));

        //        mission.BriefingFlightPackage.Add(
        //        new HQMissionBriefingFlightGroup(
        //            uGroup.Name, uGroup.Units[0], DCSAircraftTask.CAS, // FIXME: tasking
        //            uGroup.UnitCount, uGroup.RadioFrequency)); // FIXME: complete

        //        mission.UnitGroups.Add(uGroup);

        //        GroupID++;
        //        TotalAAValue += aircraftDefinition.AircraftAirToAirRating[(missionObjective.MissionPayload == AircraftPayloadType.A2A) ? 0 : 1];
        //    }

        //    HQDebugLog.Instance.Log();
        //}

        //public void GenerateObjectiveUnitGroupsAtEachObjective(HQMission mission, MissionTemplate template, DefinitionMissionObjective missionTask, DefinitionCoalition[] coalitions)
        //{
        //    int i;

        //    HQDebugLog.Instance.Log("Generating mission objective unit groups at each objective...");

        //    CommonSettingsEnemyAirDefense airDefense = Library.Common.EnemyAirDefense[(int)HQTools.ResolveRandomAmount(template.EnemyAirDefense)];

        //    for (i = 0; i < mission.Objectives.Count; i++)
        //    {
        //        DefinitionMissionObjectiveUnitGroup grp = missionTask.UnitGroupObjective;
        //        if (string.IsNullOrEmpty(grp.LuaGroup)) break; // No units

        //        HQMissionObjectiveLocation obj = mission.Objectives[i];

        //        Coalition groupCoalition = grp.Flags.Contains(MissionObjectiveUnitGroupFlags.Friendly) ? mission.CoalitionPlayer : mission.CoalitionEnemy;

        //        HQMissionUnitGroup uGroup = null;

        //        List<UnitFamily> availableFamilies = new List<UnitFamily>(grp.Families);
        //        while (availableFamilies.Count > 0)
        //        {
        //            UnitFamily selectedFamily = HQTools.RandomFrom(availableFamilies);

        //            uGroup =
        //                HQMissionUnitGroup.FromCoalitionArmyAndUnitFamily(
        //                    Library,
        //                    grp.LuaGroup, grp.LuaUnit, coalitions[(int)groupCoalition],
        //                    template.TimePeriod, selectedFamily, grp.UnitCount.GetValue(),
        //                    (i + 1) * 1000, groupCoalition, obj.Coordinates);

        //            if (uGroup.UnitCount > 0) break;

        //            availableFamilies.Remove(selectedFamily);
        //        }

        //        if ((uGroup == null) || (uGroup.UnitCount == 0))
        //            throw new Exception($"Found no valid units to generate mission critical group of {uGroup.Coalition} {uGroup.Category}.");

        //        if (grp.Flags.Contains(MissionObjectiveUnitGroupFlags.AllowAirDefense) && !grp.Flags.Contains(MissionObjectiveUnitGroupFlags.Friendly))
        //            uGroup.AddAirDefenseUnits(Library, coalitions[(int)groupCoalition], template.TimePeriod, Library.Common.EnemyAirDefense[(int)HQTools.ResolveRandomAmount(template.EnemyAirDefense)]);

        //        mission.UnitGroups.Add(uGroup);
        //    }

        //    HQDebugLog.Instance.Log();
        //}

        //public void GenerateObjectiveUnitGroupsAtCenter(HQMission mission, MissionTemplate template, MissionObjectiveDefinition missionTask, CoalitionDefinition[] coalitions)
        //{
        //    int i;

        //    HQDebugLog.Log("Generating common mission objective unit groups...");

        //    EnemyAirDefenseSettings airDefense = Library.Common.AirDefense[(int)HQTools.ResolveRandomAmount(template.EnemyAirDefense)];

        //    Coordinates location = (mission.ObjectiveCount > 0) ? mission.ObjectivesCenterPoint : mission.Airbases[0].Coordinates;

        //    for (i = 0; i < missionTask.UnitGroupOnlyOnce.Length; i++)
        //    {
        //        MissionObjectiveUnitGroup grp = missionTask.UnitGroupOnlyOnce[i];
        //        if (string.IsNullOrEmpty(grp.LuaGroup)) break; // No units

        //        Coalition groupCoalition = grp.Flags.Contains(MissionTaskUnitGroupFlags.Friendly) ? mission.CoalitionPlayer : mission.CoalitionEnemy;

        //        MissionHQUnitGroup uGroup = null;

        //        List<UnitFamily> availableFamilies = new List<UnitFamily>(grp.Families);
        //        while (availableFamilies.Count > 0)
        //        {
        //            UnitFamily selectedFamily = HQTools.RandomFrom(availableFamilies);

        //            uGroup =
        //                MissionHQUnitGroup.FromCoalitionArmyAndUnitFamily(
        //                    grp.LuaGroup, grp.LuaUnit, coalitions[(int)groupCoalition],
        //                    template.TimePeriod, selectedFamily, grp.UnitCount.GetValue(),
        //                    (i + 1) * 1000 + j, groupCoalition, location);

        //            if (uGroup.UnitCount > 0) break;

        //            availableFamilies.Remove(selectedFamily);
        //        }

        //        if ((uGroup == null) || (uGroup.UnitCount == 0))
        //            throw new Exception($"Found no valid units to generate mission critical group of {uGroup.Coalition} {uGroup.Category}.");

        //        if (grp.Flags.Contains(MissionTaskUnitGroupFlags.AllowAirDefense) && !grp.Flags.Contains(MissionTaskUnitGroupFlags.Friendly))
        //            uGroup.AddAirDefenseUnits(coalitions[(int)groupCoalition], template.TimePeriod, Library.Common.AirDefense[(int)HQTools.ResolveRandomAmount(template.EnemyAirDefense)]);

        //        mission.UnitGroups.Add(uGroup);
        //    }

        //    HQDebugLog.Log();
        //}

        //public void GenerateEnemyGroundAirDefense(
        //    HQMission mission, MissionTemplate template,
        //    DefinitionTheater theater, DefinitionMissionObjective missionTask, DefinitionCoalition[] coalitions)
        //{
        //    HQDebugLog.Instance.Log("Generating enemy ground air defense units...");

        //    AmountNR airDefenseLevel = HQTools.ResolveRandomAmount(template.EnemyAirDefense);

        //    HQDebugLog.Instance.Log(
        //        $"  Enemy air defense should be {template.EnemyAirDefense.ToString().ToUpperInvariant()}" +
        //        ((template.EnemyAirDefense == AmountNR.Random) ?
        //        $" (randomly selected level {airDefenseLevel.ToString().ToUpperInvariant()})" : ""));

        //    CommonSettingsEnemyAirDefense airDefense = Library.Common.EnemyAirDefense[(int)airDefenseLevel];

        //    foreach (AirDefenseRange adr in Enum.GetValues(typeof(AirDefenseRange)))
        //    {
        //        int adGroupCount = airDefense.InAreaGroupCount[(int)adr].GetValue();
        //        if (adGroupCount == 0) continue; // no unit groups for this air defense range

        //        CommonSettingsEnemyAirDefenseDistance distanceSettings = Library.Common.EnemyAirDefenseDistance[(int)adr];

        //        for (int i = 0; i < adGroupCount; i++)
        //        {
        //            // Select nodes (1) far enough from the player starting airbase and (2) between min and max distance from an objective
        //            DefinitionTheaterNode? selNode = theater.SelectNode(
        //                distanceSettings.NodeTypes, null,
        //                mission.Airbases[0].Coordinates, new MinMaxD(distanceSettings.MinDistanceFromTakeOffLocation, double.MaxValue),
        //                HQTools.RandomFrom(mission.Objectives).Coordinates, distanceSettings.DistanceFromObjective);

        //            if (!selNode.HasValue) // No nodes matching search criteria, don't spawn anything
        //            {
        //                HQDebugLog.Instance.Log("WARNING: failed to find a node to spawn enemy air defense.");
        //                continue;
        //            }

        //            HQMissionUnitGroup uGroup = HQMissionUnitGroup.FromCoalitionArmyAndUnitFamily(
        //                Library,
        //                "GroupVehicleIdle", "UnitVehicle",
        //                coalitions[(int)mission.CoalitionEnemy], template.TimePeriod,
        //                HQTools.RandomFrom(airDefense.InAreaFamilies[(int)adr]), airDefense.InAreaGroupSize[(int)adr].GetValue(),
        //                GroupID, mission.CoalitionEnemy, selNode.Value.Coordinates);

        //            uGroup.Name = GetGroupName(UnitFamily.VehicleSAMMedium);

        //            if (uGroup.UnitCount == 0) continue;

        //            mission.UnitGroups.Add(uGroup);
        //            GroupID++;
        //        }
        //    }

        //    HQDebugLog.Instance.Log();
        //}

        //public void GeneralEnemyCAP(HQMission mission, AmountNR enemyCombatAirPatrols, DefinitionTheater theater, DefinitionCoalition enemyCoalition)
        //{
        //    HQDebugLog.Instance.Log("Generating enemy combat air patrols...");

        //    // Select a random enemy CAP intensity if set to Random.
        //    enemyCombatAirPatrols = HQTools.ResolveRandomAmount(enemyCombatAirPatrols);

        //    // Multiply the total "air-to-air" score of friendly air groups by a multiplier according to the select "enemy air patrols" setting
        //    TotalAAValue = TotalAAValue * Library.Common.EnemyCAPMultiplier[(int)enemyCombatAirPatrols];

        //    HQDebugLog.Instance.Log("  Enemy CAP air-to-air power: " + TotalAAValue.ToString("F0"));

        //    if (TotalAAValue <= 0) return; // No AA points to spend, no units

        //    string[] availableFighterUnits = enemyCoalition.GetUnits(Library, mission.TimePeriod, UnitFamily.PlaneFighter, true, false);

        //    if (availableFighterUnits.Length == 0)
        //    {
        //        HQDebugLog.Instance.Log("  WARNING: No fighters or interceptors found in enemy army, could not generate enemy combat air patrols.");
        //        return;
        //    }

        //    while (TotalAAValue > 0)
        //    {
        //        List<string> groupUnits = new List<string>();

        //        string unit = HQTools.RandomFrom(availableFighterUnits);
        //        DefinitionUnit aircraftDefinition = Library.GetDefinition<DefinitionUnit>(unit);
        //        if (aircraftDefinition == null) { TotalAAValue--; continue; }
        //        int aaValueCost = Math.Max(1, aircraftDefinition.AircraftAirToAirRating[0]);

        //        do
        //        {
        //            groupUnits.Add(unit);
        //            TotalAAValue -= aaValueCost;

        //            if (TotalAAValue <= 0) break; // All "antiair points" have been expended, stop here
        //            if (groupUnits.Count >= HQTools.RandomFrom(2, 2, 3, 4, 4, 4)) break; // Group is full, stop here
        //        } while (true);

        //        if (groupUnits.Count == 0) { TotalAAValue--; continue; }

        //        // Select any nodes (aircraft can be spawned anywhere, even over water) located far enough from the
        //        // players starting airbase and neither too far or too near of the objective (see HQLibrary.Common.EnemyCAPDistance)
        //        DefinitionTheaterNode? selNode = theater.SelectNode(
        //            null, null,
        //            mission.Airbases[0].Coordinates, new MinMaxD(Library.Common.EnemyCAPMinDistanceFromStartLocation, double.MaxValue),
        //            HQTools.RandomFrom(mission.Objectives).Coordinates, Library.Common.EnemyCAPDistanceFromObjectives);

        //        if (!selNode.HasValue) { TotalAAValue--; continue; }

        //        HQMissionUnitGroup uGroup = new HQMissionUnitGroup(
        //            "GroupPlaneEnemyCAP", "UnitAircraft",
        //            UnitCategory.Plane, GroupID, mission.CoalitionEnemy, selNode.Value.Coordinates,
        //            groupUnits.ToArray());

        //        SetupAircraftGroup(uGroup, mission, CallsignFamily.Aircraft, false, aircraftDefinition, AircraftPayloadType.A2A);
        //        uGroup.CustomValues.Add("LateActivation", "false"); // FIXME: random chance
        //        uGroup.CustomValues.Add("ParkingID", "0"); // FIXME: should not be used, remove from Lua file

        //        mission.UnitGroups.Add(uGroup);
        //        GroupID++;
        //    }

        //    HQDebugLog.Instance.Log();
        //}

        /// <summary>
        /// Sets the various parameters and special values required for aircraft (fixed-wing and helicopters) unit groups.
        /// </summary>
        /// <param name="uGroup">The unit group to setup.</param>
        /// <param name="mission">The mission.</param>
        /// <param name="csFamily">The callsign family to use for this group.</param>
        /// <param name="isFriendly">Does this group belong to the player's coalition?</param>
        /// <param name="aircraftDefinition">The definition of the aircraft type. If none is provided (null), the method will try to get one from the unit type name.</param>
        /// <param name="payload">The type of payload to use.</param>
        /// <param name="airdromeID">The airdrome this aircraft group is linked to.</param>
        /// <returns>True if everything went right, false is something went wrong.</returns>
        private bool SetupAircraftGroup(DCSMissionUnitGroup uGroup, DCSMission mission, CallsignFamily csFamily, bool isFriendly, DefinitionUnit aircraftDefinition = null, AircraftPayloadType payload = AircraftPayloadType.Default, int airdromeID = 0)
        {
            if (uGroup.UnitCount == 0) return false;

            if (aircraftDefinition == null)
                aircraftDefinition = Library.Instance.GetDefinition<DefinitionUnit>(uGroup.Units[0]);

            if (aircraftDefinition == null) return false;

            MGCallsign cs = CSGenerator.GetCallsign(csFamily, isFriendly ? mission.CoalitionPlayer : mission.CoalitionEnemy);
            uGroup.Name = cs.GroupName;
            uGroup.CustomValues.Add("Callsign", cs.Lua);
            uGroup.CustomValues.Add("Payload", aircraftDefinition.GetPayloadLua(payload));
            uGroup.RadioFrequency = aircraftDefinition.CommsRadioFrequency;

            for (int i = 0; i < uGroup.UnitCount; i++)
            {
                uGroup.CustomValues.Add(new DCSMissionUnitGroupCustomValueKey("OnBoardNum", i), HQTools.ValToString(GetOnboardNumber()));
                if (airdromeID != -1)
                    uGroup.CustomValues.Add(new DCSMissionUnitGroupCustomValueKey("ParkingID", i), HQTools.ValToString(GetParkingNumber(airdromeID)));
            }

            return true;
        }

        //public void GenerateAIEscortFlightGroups(
        //    HQMission mission, MissionTemplate template,
        //    DefinitionCoalition[] coalitions,
        //    int count, UnitFamily family, string luaGroup, AircraftPayloadType payload, DCSAircraftTask task)
        //{
        //    HQDebugLog.Instance.Log($"Generating AI escort flight groups ({task.ToString()})...");

        //    int[] groupsCount = CreateGroupsFromAircraftCount(count);

        //    foreach (int gCount in groupsCount)
        //    {
        //        HQMissionUnitGroup uGroup = HQMissionUnitGroup.FromCoalitionArmyAndUnitFamily(
        //            Library,
        //            luaGroup, "unitAircraft",
        //            coalitions[(int)mission.CoalitionPlayer], mission.TimePeriod,
        //            family, gCount, GroupID, mission.CoalitionPlayer,
        //            mission.Airbases[0].Coordinates + Coordinates.CreateRandomInaccuracy(1000, 10000));

        //        if (uGroup.UnitCount == 0) continue; // No unit, proceed

        //        DefinitionUnit aircraftDefinition =
        //            (from d in Library.GetAllDefinitions<DefinitionUnit>() where d.ID == uGroup.Units[0] select d).FirstOrDefault();
        //        if (aircraftDefinition == null) continue; // Aircraft definition doesn't exist

        //        SetupAircraftGroup(
        //            uGroup, mission, CallsignFamily.Aircraft, true,
        //            aircraftDefinition, payload, mission.Airbases[0].DCSID);

        //        mission.BriefingFlightPackage.Add(new HQMissionBriefingFlightGroup(uGroup.Name, uGroup.Units[0], task, uGroup.UnitCount));

        //        GroupID++;
        //        TotalAAValue += aircraftDefinition.AircraftAirToAirRating[(payload == AircraftPayloadType.A2A) ? 0 : 1];
        //    }

        //    HQDebugLog.Instance.Log();
        //}

        // FIXME: write a more generic "add flight groups" option

        //public void GenerateAISupportFlightGroups(
        //    HQMission mission, MissionTemplate template,
        //    DefinitionCoalition[] coalitions,
        //    UnitFamily family, string luaGroup,
        //    CallsignFamily csFamily, DCSAircraftTask task)
        //{
        //    HQDebugLog.Instance.Log($"Generating AI support flight groups ({family.ToString()})...");

        //    // Set spawning position near the departure airbase
        //    Coordinates position = mission.Airbases[0].Coordinates + Coordinates.CreateRandomInaccuracy(1000, 5000);

        //    // If there are objectives, move the position to 15% to 30% of the distance between the starting position and the center of all objectives
        //    if (mission.Objectives.Count > 0)
        //        position = Coordinates.Lerp(position, mission.ObjectivesCenterPoint, HQTools.RandomDouble(0.15, 0.3));

        //    // Create a second waypoint 5 to 10 Km away from the original one to allow "racetrack" orbiting.
        //    Coordinates position2 = position + Coordinates.CreateRandomInaccuracy(5000, 10000);

        //    HQMissionUnitGroup uGroup = HQMissionUnitGroup.FromCoalitionArmyAndUnitFamily(
        //        Library,
        //        luaGroup, "UnitAircraft",
        //        coalitions[(int)mission.CoalitionPlayer], mission.TimePeriod,
        //        family, 1, GroupID, mission.CoalitionPlayer, position);

        //    if ((uGroup == null) || (uGroup.UnitCount == 0)) return; // No unit, proceed

        //    DefinitionUnit aircraftDefinition =
        //        (from d in Library.GetAllDefinitions<DefinitionUnit>() where d.DCSID == uGroup.Units[0] select d).FirstOrDefault();
        //    if (aircraftDefinition == null) // Aircraft definition doesn't exist, proceed
        //    { HQDebugLog.Instance.Log($"    WARNING: cannot find aircraft definition \"{uGroup.Units[0]}\""); return; }
        //    uGroup.CustomValues.Add("X2", HQTools.ValToString(position2.X));
        //    uGroup.CustomValues.Add("Y2", HQTools.ValToString(position2.Y));
        //    ////uGroup.RadioFrequency = coalitions[(int)template.PlayerCoalition].TankerFrequency;

        //    SetupAircraftGroup(uGroup, mission, csFamily, true, aircraftDefinition, AircraftPayloadType.Default, mission.Airbases[0].DCSID);

        //    mission.UnitGroups.Add(uGroup);
        //    GroupID++;

        //    string tacan = null;
        //    //if (task == DCSAircraftTask.Tanker) tacan = TANKER_TACAN; // FIXME

        //    mission.BriefingFlightPackage.Add(new HQMissionBriefingFlightGroup(uGroup.Name, uGroup.Units[0], task, 1, uGroup.RadioFrequency, tacan));

        //    HQDebugLog.Instance.Log();
        //}

        private int GetParkingNumber(int airdromeID)
        {
            if (!AirdromeParkingNumber.ContainsKey(airdromeID))
                AirdromeParkingNumber[airdromeID] = 1;
            else
                AirdromeParkingNumber[airdromeID]++;

            return AirdromeParkingNumber[airdromeID];
        }

        private int GetOnboardNumber()
        {
            int number;

            do
            {
                number = HQTools.RandomInt(111, 999);
            } while (UsedOnboardNumbers.Contains(number));

            return number;
        }

        private string GetGroupName(UnitFamily family)
        {
            int randomFakeGroupNameNumber;

            if (UsedFakeGroupNamesNumbers.Count > 250) UsedFakeGroupNamesNumbers.Clear();
            do { randomFakeGroupNameNumber = HQTools.RandomInt(1, 500); }
            while (UsedFakeGroupNamesNumbers.Contains(randomFakeGroupNameNumber));

            string nameTemplate;

            switch (family)
            {
                case UnitFamily.VehicleSAMLong:
                case UnitFamily.VehicleSAMMedium:
                case UnitFamily.VehicleSAMShort:
                case UnitFamily.VehicleSAMShortIR:
                case UnitFamily.VehicleAAA:
                    nameTemplate = Language.GetStringRandom("GroupNames", "AirDefense"); break;
                case UnitFamily.VehicleAPC:
                case UnitFamily.VehicleMBT:
                    nameTemplate = Language.GetStringRandom("GroupNames", "Armor"); break;
                case UnitFamily.VehicleArtillery:
                case UnitFamily.VehicleMissile:
                    nameTemplate = Language.GetStringRandom("GroupNames", "Artillery"); break;
                case UnitFamily.PlaneStrike:
                default:
                    nameTemplate = Language.GetStringRandom("GroupNames", "Default"); break;
                case UnitFamily.VehicleInfantry:
                case UnitFamily.VehicleInfantryMANPADS:
                    nameTemplate = Language.GetStringRandom("GroupNames", "Infantry"); break;
                case UnitFamily.VehicleCommand:
                case UnitFamily.VehicleTransport:
                    nameTemplate = Language.GetStringRandom("GroupNames", "Unarmed"); break;
            }

            nameTemplate = nameTemplate.Replace("$N$", HQTools.ValToString(randomFakeGroupNameNumber));
            nameTemplate = nameTemplate.Replace("$NTH$", Language.GetOrdinalAdjective(randomFakeGroupNameNumber));

            return nameTemplate;
        }

        private int[] CreateGroupsFromAircraftCount(int aircraftCount)
        {
            List<int> acGroups = new List<int>();

            while (aircraftCount > 0)
            {
                if (acGroups.Count == 0) acGroups.Add(0);
                if ((acGroups.Last() == 4) ||
                    ((acGroups.Last() == 3) && HQTools.RandomChance(4)) ||
                    ((acGroups.Last() == 2) && HQTools.RandomChance(3)))
                    acGroups.Add(1);
                else
                    acGroups[acGroups.Count - 1]++;

                aircraftCount--;
            }

            return acGroups.ToArray();
        }

        private DCSSkillLevel HQSkillToDCSSkill(HQSkillLevel aiSkill)
        {
            switch (aiSkill)
            {
                case HQSkillLevel.Average: return DCSSkillLevel.Average;
                case HQSkillLevel.Good: return DCSSkillLevel.Good;
                case HQSkillLevel.High: return DCSSkillLevel.High;
                case HQSkillLevel.Excellent: return DCSSkillLevel.Excellent;
            }

            return HQTools.RandomFrom(new DCSSkillLevel[] { DCSSkillLevel.Average, DCSSkillLevel.Good, DCSSkillLevel.High, DCSSkillLevel.Excellent });
        }
    }
}

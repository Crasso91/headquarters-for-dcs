///*
//==========================================================================
//This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
//Eagle Dynamics' DCS World flight simulator.

//HQ4DCS was created by Ambroise Garel (@akaAgar).
//You can find more information about the project on its GitHub page,
//https://akaAgar.github.io/headquarters-for-dcs

//HQ4DCS is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//HQ4DCS is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with HQ4DCS. If not, see https://www.gnu.org/licenses/
//==========================================================================
//*/

//using Headquarters4DCS.Enums;
//using Headquarters4DCS.Library;
//using Headquarters4DCS.Mission;
//using Headquarters4DCS.Template;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Headquarters4DCS.Generator
//{
//    /// <summary>
//    /// Generates an HQMission's flight plan (objective coordinates, waypoints...) from a mission template.
//    /// </summary>
//    public sealed class MissionGeneratorFlightPlan : IDisposable
//    {
//        /// <summary>
//        /// An array with some random count of extra waypoints to add before and after the objective waypoints.
//        /// </summary>
//        private static readonly int[] EXTRA_WAYPOINT_COUNT = new int[] { 1, 1, 2, 2, 2, 2, 3 };

//        /// <summary>
//        /// Random variation in waypoint altitude.
//        /// </summary>
//        private const double WAYPOINT_ALTITUDE_VARIATION = 0.2;

//        /// <summary>
//        /// The language definition to use.
//        /// </summary>
//        private readonly DefinitionLanguage Language;

//        /// <summary>
//        /// The callsign generator to use.
//        /// </summary>
//        private readonly MissionGeneratorCallsign CSGenerator;

//        /// <summary>
//        /// A list of mission objective names.
//        /// </summary>
//        private readonly List<string> AvailableObjectiveNames;

//        /// <summary>
//        /// Constructor.
//        /// </summary>
//        /// <param name="language">The language definition to use.</param>
//        /// <param name="csGenerator">The callsign generator to use.</param>
//        public MissionGeneratorFlightPlan(DefinitionLanguage language, MissionGeneratorCallsign csGenerator)
//        {
//            Language = language;
//            CSGenerator = csGenerator;

//            AvailableObjectiveNames = new List<string>(language.GetStringArray("Waypoints", "ObjectiveNames"));
//        }

//        /// <summary>
//        /// IDispose implementation.
//        /// </summary>
//        public void Dispose() { }

//        /// <summary>
//        /// Select locations for the various mission objectives. Must be called after takeoff airbase has been selected, as objectives should be spawned a certain distance from it.
//        /// </summary>
//        /// <param name="mission">The mission.</param>
//        /// <param name="template">The mission template.</param>
//        /// <param name="theater">The theater definition, to get node positions from.</param>
//        /// <param name="missionObjective">The mission objective definition, to know what kind of node types to look for.</param>
//        //public void GenerateObjectiveLocations(HQMission mission, MissionTemplate template, DefinitionTheater theater, DefinitionMissionObjective missionObjective)
//        //{
//        //    HQDebugLog.Instance.Log("Picking objective locations...");

//        //    CommonSettingsDistanceToObjective distanceToObjective = Library.Common.DistanceToObjective[(int)HQTools.ResolveRandomAmount(template.DistanceToObjectives)];

//        //    Coalition? wpNodeCoalition = null;
//        //    switch (template.ObjectivesLocation)
//        //    {
//        //        case TerritorySide.Enemy: wpNodeCoalition = mission.CoalitionEnemy; break;
//        //        case TerritorySide.Friendly: wpNodeCoalition = mission.CoalitionPlayer; break;
//        //    }

//        //    if (wpNodeCoalition.HasValue && mission.InvertTheaterCountries)
//        //        wpNodeCoalition = (wpNodeCoalition.Value == Coalition.Blue) ? Coalition.Red : Coalition.Blue;

//        //    for (int i = 0; i < template.NumberOfObjectives; i++)
//        //    {
//        //        DefinitionTheaterNode? node;

//        //        if (i == 0) // This is the first objective, measure distance from the starting location
//        //            node = theater.SelectNode(
//        //                missionObjective.WaypointNodeTypes, wpNodeCoalition,
//        //                mission.Airbases[0].Coordinates, distanceToObjective.DistanceFromStartLocation);
//        //        else // This is NOT the first objective, measure distance from the previous objective
//        //            node = theater.SelectNode(
//        //                missionObjective.WaypointNodeTypes, wpNodeCoalition,
//        //                mission.Objectives.Last().Coordinates, distanceToObjective.DistanceBetweenTargets);

//        //        if (!node.HasValue)
//        //            throw new Exception($"Failed to find a node to spawn objective #{i + 1}.");

//        //        // Select a random objective name
//        //        string objectiveName = Language.GetString("Waypoints", "Target").Replace("$N$", HQTools.ValToString(i + 1));
//        //        if (AvailableObjectiveNames.Count > 0)
//        //        {
//        //            objectiveName = HQTools.RandomFrom(AvailableObjectiveNames);
//        //            AvailableObjectiveNames.Remove(objectiveName);
//        //        }

//        //        mission.Objectives.Add(
//        //            new HQMissionObjectiveLocation(
//        //                node.Value.Coordinates,
//        //                objectiveName.ToUpperInvariant(),
//        //                missionObjective.WaypointAltitude,
//        //                0));
//        //    }

//        //    // Sort objectives by distance from the departure airbase
//        //    mission.Objectives.Sort(delegate (HQMissionObjectiveLocation obj1, HQMissionObjectiveLocation obj2)
//        //    { return (int)(obj2.Coordinates.GetSquaredDistanceFrom(mission.Airbases[0].Coordinates) - obj1.Coordinates.GetSquaredDistanceFrom(mission.Airbases[0].Coordinates)); });

//        //    HQDebugLog.Instance.Log();
//        //}

//        /// <summary>
//        /// Generate player flight plan waypoints. Must be called last, once airbase and objectives are set.
//        /// </summary>
//        /// <param name="mission">The mission.</param>
//        /// <param name="template">The mission template.</param>
//        /// <param name="theater">The theater definition.</param>
//        /// <param name="missionObjective">The mission objective</param>
//        public void GenerateWaypoints(HQMission mission, MissionTemplate template, DefinitionTheater theater, DefinitionMissionObjective missionObjective)
//        {
//            HQDebugLog.Instance.Log("Generating waypoints...");

//            if (mission.Objectives.Count == 0) return;

//            int i;

//            mission.Waypoints.Add(
//                new HQMissionWaypoint(
//                    mission.Airbases[0].Coordinates,
//                    Language.GetString("Waypoints", "TakeOff"),
//                    0.0, 0.0));

//            foreach (HQMissionObjectiveLocation obj in mission.Objectives)
//            {
//                mission.Waypoints.Add(
//                    new HQMissionWaypoint(
//                        obj.Coordinates + Coordinates.CreateRandomInaccuracy(missionObjective.WaypointOffset), // add a little offset if required by objective type, so WP is not directly on the objective
//                        obj.Name,
//                        missionObjective.WaypointAltitude));
//            }

//            // Add extra waypoints before the objectives
//            if (missionObjective.WaypointExtraBefore)
//            {
//                int wpBeforeCount = HQTools.RandomFrom(EXTRA_WAYPOINT_COUNT);
//                Coordinates firstObjectiveCoordinates = mission.Objectives.First().Coordinates;
//                double travelDistance = firstObjectiveCoordinates.GetDistanceFrom(mission.Airbases[0].Coordinates);
//                for (i = 0; i < wpBeforeCount; i++)
//                    mission.Waypoints.Insert(i + 1,
//                        new HQMissionWaypoint(
//                            Coordinates.Lerp(mission.Airbases[0].Coordinates, firstObjectiveCoordinates, (i + 1) / (double)(wpBeforeCount + 1)) +
//                            Coordinates.CreateRandomInaccuracy(travelDistance / 20.0, travelDistance / 10.0),
//                            Language.GetString("Waypoints", "Default").Replace("$N$", HQTools.ValToString(i + 2)), GetRandomAltitudeVariation()));
//            }

//            // Add extra waypoints after the objectives
//            if (missionObjective.WaypointExtraAfter)
//            {
//                int wpAfterCount = HQTools.RandomFrom(EXTRA_WAYPOINT_COUNT);
//                Coordinates lastObjectiveCoordinates = mission.Objectives.Last().Coordinates;
//                double returnDistance = lastObjectiveCoordinates.GetDistanceFrom(mission.Airbases[1].Coordinates);
//                for (i = 0; i < wpAfterCount; i++)
//                    mission.Waypoints.Add(
//                        new HQMissionWaypoint(
//                            Coordinates.Lerp(lastObjectiveCoordinates, mission.Airbases[1].Coordinates, (i + 1) / (double)(wpAfterCount + 1)) +
//                            Coordinates.CreateRandomInaccuracy(returnDistance / 20.0, returnDistance / 10.0),
//                            Language.GetString("Waypoints", "Default").Replace("$N$", HQTools.ValToString(mission.Waypoints.Count + 1)), GetRandomAltitudeVariation()));
//            }

//            mission.Waypoints.Add(new HQMissionWaypoint(mission.Airbases[1].Coordinates, Language.GetString("Waypoints", "Landing"), 0.0, 0.6));

//            HQDebugLog.Log();
//        }

//        /// <summary>
//        /// Returns a random altitude variation between 1 - WAYPOINT_ALTITUDE_VARIATION and 1 + WAYPOINT_ALTITUDE_VARIATION.
//        /// </summary>
//        /// <returns>A random altitude multiplier.</returns>
//        private double GetRandomAltitudeVariation()
//        { return HQTools.RandomDouble(1 - WAYPOINT_ALTITUDE_VARIATION, 1 + WAYPOINT_ALTITUDE_VARIATION); }

//        /// <summary>
//        /// Generate bullseyes for both coalitions. Must be called after departure airbase and 
//        /// </summary>
//        /// <param name="mission">The mission.</param>
//        //public void GenerateBullseye(HQMission mission)
//        //{
//        //    HQDebugLog.Instance.Log("Generating red/blue bullseyes...");

//        //    // Players' coalition's bullseye is 50% to 100% of the way between departure airbase and objective's center (unless there a no objectives, then center on the departure airbase)
//        //    // Plus a random variation of 5-20 Km
//        //    //mission.Bullseye[(int)mission.CoalitionPlayer] = Coordinates.Lerp(
//        //    //    mission.Airbases[0].Coordinates,
//        //    //    (mission.Objectives.Count == 0) ? mission.Airbases[0].Coordinates : mission.ObjectivesCenterPoint, HQTools.RandomDouble(0.5, 1.0)) +
//        //    //    Coordinates.CreateRandomInaccuracy(5000, 20000);

//        //    // Enemy coalition bullseye is 5-20 Km from the objective center (unless there are no objectives, then it's 5-20Km from player's departure airbase)
//        //    //mission.Bullseye[(int)mission.CoalitionEnemy] =
//        //    //    (mission.Objectives.Count == 0) ? mission.Airbases[0].Coordinates : mission.ObjectivesCenterPoint +
//        //    //    Coordinates.CreateRandomInaccuracy(5000, 20000);

//        //    HQDebugLog.Instance.Log($"    BLUE bullseye set at {mission.Bullseye[(int)Coalition.Blue].ToString("F0")}");
//        //    HQDebugLog.Instance.Log($"    RED bullseye set at {mission.Bullseye[(int)Coalition.Red].ToString("F0")}");

//        //    HQDebugLog.Instance.Log();
//        //}
//    }
//}

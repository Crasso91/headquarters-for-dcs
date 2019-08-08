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
using Headquarters4DCS.GeneratedMission;
using Headquarters4DCS.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Generator
{
    /// <summary>
    /// Mission generator. Turns a MissionTemplate into an HQMission.
    /// </summary>
    public sealed class MissionGenerator : IDisposable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MissionGenerator()
        {
            //CSGenerator = new CallsignGenerator();
        }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }

        public Mission Generate(MissionTemplate template)
        {
            int i;

            // Clear log, begin timing then create an instance of the HQ mission class
            Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();
            Mission mission = new Mission();
            DebugLog.Instance.Clear();
            DebugLog.Instance.Log($"STARTING MISSION GENERATION AT {DateTime.Now.ToLongTimeString()}...");
            DebugLog.Instance.Log(new string('=', DebugLog.Instance.GetLastMessage().Length));
            DebugLog.Instance.Log();

            try
            {
                using (MissionGeneratorTemplateChecker templateChecker = new MissionGeneratorTemplateChecker())
                { templateChecker.CheckTemplate(template); }

                // Pick definitions
                DefinitionCoalition[] coalitions = new DefinitionCoalition[2];
                coalitions[(int)Coalition.Blue] = Library.Instance.GetDefinition<DefinitionCoalition>(template.Settings.ContextCoalitionBlue);
                coalitions[(int)Coalition.Red] = Library.Instance.GetDefinition<DefinitionCoalition>(template.Settings.ContextCoalitionRed);

                DefinitionLanguage language = Library.Instance.GetDefinition<DefinitionLanguage>(template.Settings.PreferencesLanguage.ToLowerInvariant());
                DefinitionTheater theater = Library.Instance.GetDefinition<DefinitionTheater>(template.Theater);

                // Create a list of all available objective names
                List<string> waypointNames = language.GetStringArray("Mission", "Waypoint.ObjectiveNames").ToList();

                MissionGeneratorCallsign callsignGenerator = new MissionGeneratorCallsign(coalitions[(int)Coalition.Blue].NATOCallsigns, coalitions[(int)Coalition.Red].NATOCallsigns);
                MissionGeneratorUnitGroups unitGroupsGenerator = new MissionGeneratorUnitGroups(language, callsignGenerator);

                // Copy values from the template to the mission
                mission.Theater = template.Theater;
                mission.Language = template.Settings.PreferencesLanguage;
                mission.CoalitionPlayer = template.Settings.ContextPlayerCoalition;
                mission.SinglePlayer = (template.GetPlayerCount() < 2);
                mission.UseNATOCallsigns = coalitions[(int)template.Settings.ContextPlayerCoalition].NATOCallsigns;

                mission.Countries[(int)Coalition.Blue] = coalitions[(int)Coalition.Blue].Countries.ToArray();
                mission.Countries[(int)Coalition.Red] = coalitions[(int)Coalition.Red].Countries.Except(coalitions[(int)Coalition.Blue].Countries).ToArray();

                switch (template.Settings.BriefingUnits)
                {
                    case UnitSystem.ByCoalition:
                        mission.BriefingImperialUnits = (coalitions[(int)mission.CoalitionPlayer].UnitSystem == UnitSystem.Imperial); break;
                    case UnitSystem.Imperial: mission.BriefingImperialUnits = true; break;
                    case UnitSystem.Metric: mission.BriefingImperialUnits = false; break;
                }

                // Generate mission environment parameters (weather, time of day, date...)
                using (MissionGeneratorEnvironment environment = new MissionGeneratorEnvironment())
                {
                    environment.GenerateMissionDate(mission, template.Settings.ContextTimePeriod, template.Settings.EnvironmentSeason);
                    environment.GenerateMissionTime(mission, template.Settings.EnvironmentTimeOfDay, theater);
                    environment.GenerateWeather(mission, template.Settings.EnvironmentWeather, theater);
                    environment.GenerateWind(mission, template.Settings.EnvironmentWind, theater);
                }

                CreateFeatures(mission, template, unitGroupsGenerator, language, waypointNames, out Coordinates[] usedNodesCoordinates);
                List<string> usedPlayerAircraftTypeList = new List<string>();
                CreatePlayerFlightGroups(mission, template, unitGroupsGenerator, usedPlayerAircraftTypeList);
                mission.UsedPlayerAircraftTypes = (from string aircraftID in usedPlayerAircraftTypeList select aircraftID).Distinct().OrderBy(x => x).ToArray();

                mission.Bullseye = new Coordinates[2];
                for (i = 0; i < 2; i++)
                    mission.Bullseye[i] =
                        Coordinates.GetCenter(usedNodesCoordinates) +
                        Coordinates.CreateRandomInaccuracy(10000, 20000);

                mission.MapCenter = Coordinates.GetCenter(usedNodesCoordinates);


                /*
                // Copy scripts
                mission.ScriptsMission = missionObjective.ScriptMission.ToList();
                mission.ScriptsObjective = missionObjective.ScriptObjective.ToList();

                mission.RealismAllowExternalViews = template.RealismAllowExternalViews;
                mission.RealismBirdStrikes = template.RealismBirdStrikes;
                mission.RealismRandomFailures = template.RealismRandomFailures;

                // Create list of airports alignment from the theater definition
                mission.AirbasesCoalition.Clear();
                foreach (DefinitionTheaterAirbase ab in theater.Airbases)
                {
                    if (mission.AirbasesCoalition.ContainsKey(ab.DCSID)) continue;
                    mission.AirbasesCoalition.Add(ab.DCSID, template.InvertTheaterCountries ? (Coalition)(1 - (int)ab.Coalition) : ab.Coalition);
                }

                mission.OggFiles.Clear();
                mission.OggFiles.Add("radio0"); // FIXME: remove?
                mission.OggFiles.AddRange(missionObjective.MediaOgg);

                // Generate mission flight plan
                using (GeneratorFlightPlan flightPlan = new GeneratorFlightPlan(Library, language, csGenerator))
                {
                    flightPlan.SelectTakeoffAndLandingAirbases(mission, theater);
                    mission.MapCenter = mission.Airbases[0].Coordinates; // Center the map on the starting airdrome
                    flightPlan.GenerateObjectiveLocations(mission, template, theater, missionObjective);
                    flightPlan.GenerateWaypoints(mission, template, theater, missionObjective);
                    flightPlan.GenerateBullseye(mission);
                }

                // Generate units
                using (GeneratorUnitGroups unitGroups = new GeneratorUnitGroups(Library, language, csGenerator))
                {
                    unitGroups.GeneratePlayerFlightGroups(mission, template, missionObjective);

                    unitGroups.GenerateAIEscortFlightGroups(mission, template, coalitions, template.FlightGroupsAICAP, UnitFamily.PlaneFighter, "GroupPlaneEscortCAP", AircraftPayloadType.A2A, DCSAircraftTask.CAP);
                    unitGroups.GenerateAIEscortFlightGroups(mission, template, coalitions, template.FlightGroupsAISEAD, UnitFamily.PlaneSEAD, "GroupPlaneEscortSEAD", AircraftPayloadType.SEAD, DCSAircraftTask.SEAD);

                    if (template.FlightGroupsTanker)
                    {
                        unitGroups.GenerateAISupportFlightGroups(mission, template, coalitions, UnitFamily.PlaneTankerBasket, "GroupPlaneTanker", CallsignFamily.Tanker, DCSAircraftTask.Refueling);
                        unitGroups.GenerateAISupportFlightGroups(mission, template, coalitions, UnitFamily.PlaneTankerBoom, "GroupPlaneTanker", CallsignFamily.Tanker, DCSAircraftTask.Refueling);
                    }

                    if (template.FlightGroupsAWACS)
                        unitGroups.GenerateAISupportFlightGroups(mission, template, coalitions, UnitFamily.PlaneAWACS, "GroupPlaneAWACS", CallsignFamily.AWACS, DCSAircraftTask.AWACS);

                    unitGroups.GenerateObjectiveUnitGroupsAtEachObjective(mission, template, missionObjective, coalitions);
                    //unitGroups.GenerateObjectiveUnitGroupsAtCenter(mission, template, missionObjective, coalitions);

                    unitGroups.GenerateEnemyGroundAirDefense(mission, template, theater, missionObjective, coalitions);
                    unitGroups.GeneralEnemyCAP(mission, template.EnemyCombatAirPatrols, theater, coalitions[(int)mission.CoalitionEnemy]);
                }
                */

                using (MissionGeneratorBriefing briefing = new MissionGeneratorBriefing(language))
                {
                    briefing.GenerateMissionName(mission, template.Settings.BriefingName);
                    /*
                        briefing.GenerateMissionDescription(mission, template.BriefingDescription, missionObjective);
                        briefing.GenerateMissionTasks(mission, template, missionObjective);
                        briefing.GenerateMissionRemarks(mission, template, missionObjective);
                        */
                    briefing.GenerateRawTextBriefing(mission, template/*, missionObjective*/);
                    briefing.GenerateHTMLBriefing(mission, template/*, missionObjective*/);
                }

                stopwatch.Stop();
                DebugLog.Instance.Log();
                DebugLog.Instance.Log($"COMPLETED MISSION GENERATION AT {DateTime.Now.ToLongTimeString()} (TOOK {stopwatch.Elapsed.TotalMilliseconds.ToString("F0")} MILLISECONDS).");
                DebugLog.Instance.Log();
                mission.GenerationLog = DebugLog.Instance.GetFullLog();
            }
#if DEBUG
            catch (HQ4DCSException e)
#else
            catch (Exception e)
#endif
            {
                stopwatch.Stop();
                DebugLog.Instance.Log($"ERROR: {e.Message}");
                DebugLog.Instance.Log();
                DebugLog.Instance.Log($"MISSION GENERATION FAILED.");
                DebugLog.Instance.Log();

                MessageBox.Show(e.Message, "Mission generation failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mission.Dispose();
                mission = null;
            }

            //unitGroupsGenerator.Dispose();
            //callsignGenerator.Dispose();

            DebugLog.Instance.SaveToFileAndClear("MissionGeneration");
            return mission;
        }

        private void CreateFeatures(
            Mission mission, MissionTemplate template,
            MissionGeneratorUnitGroups unitGroupsGenerator, DefinitionLanguage language,
            List<string> waypointNames,
            out Coordinates[] usedNodesCoordinates)
        {
            int i, j;

            List<string> oggFilesList = new List<string>();
            List<MissionObjectiveLocation> objectivesList = new List<MissionObjectiveLocation>();
            //List<MissionScript>[] scriptsList = new List<MissionScript>[] { new List<MissionScript>(), new List<MissionScript>(), new List<MissionScript>() };
            List<MissionWaypoint> waypointsList = new List<MissionWaypoint>();

            List<Coordinates> usedNodesCoordinatesList = new List<Coordinates>();

            foreach (MissionTemplateLocation node in template.Locations.Values)
            {
                if (node.InUse) usedNodesCoordinatesList.Add(node.Definition.Position);

                DefinitionFeature[] features = node.GetFeaturesDefinitions();

                foreach (DefinitionFeature feature in features)
                {
                    List<string> usedNodesSpawnPoints = new List<string>();

                    oggFilesList.AddRange(feature.MediaOgg);

                    string featureWPName = "";

                    List<string> briefingMessagesReplacements = new List<string>();

                    for (i = 0; i < feature.UnitGroups.Length; i++)
                    {
                        int groupCount = feature.UnitGroups[i].GroupCount.GetValue();

                        for (j = 0; j < groupCount; j++)
                        {
                            DefinitionTheaterLocationSpawnPoint[] validSpawnPoints =
                                (from DefinitionTheaterLocationSpawnPoint s in
                                     node.Definition.SpawnPoints
                                 where feature.UnitGroups[i].SpawnPointTypes.Contains(s.PointType) &&
                                 !usedNodesSpawnPoints.Contains(s.UniqueID)
                                 select s).ToArray();

                            if (validSpawnPoints.Length == 0)
                            {
                                // No valid spawn points found. Throw an exception and abort mission generation if feature is an objective or required,
                                // else print a warning to the log and proceed the next feature.
                                if ((feature.Category == FeatureCategory.Objective) || (feature.FeatureFlags.Contains(FeatureFlag.Required)))
                                    throw new HQ4DCSException(
                                        $"Failed to find a spawn point of type {string.Join("/", feature.UnitGroups[i].SpawnPointTypes)} for feature {feature.DisplayName.ToUpperInvariant()} at {node.Definition.DisplayName.ToUpperInvariant()}.");

                                DebugLog.Instance.Log(
                                    $"Failed to find a spawn point of type {string.Join("/", feature.UnitGroups[i].SpawnPointTypes)} for feature {feature.DisplayName.ToUpperInvariant()} at {node.Definition.DisplayName.ToUpperInvariant()}.");

                                continue;
                            }

                            DefinitionTheaterLocationSpawnPoint spawnPoint = HQTools.RandomFrom(validSpawnPoints);

                            MissionUnitGroup newGroup = unitGroupsGenerator.AddNodeFeatureGroup(mission, template, feature.UnitGroups[i], spawnPoint.Position);
                            if (newGroup == null)
                                continue; // TODO: throw error if objective or required

                            if (!briefingMessagesReplacements.Contains("CALLSIGN"))
                                briefingMessagesReplacements.AddRange(new string[] { "CALLSIGN", newGroup.Name });
                            if (!briefingMessagesReplacements.Contains("FREQUENCY"))
                                briefingMessagesReplacements.AddRange(new string[] { "FREQUENCY", HQTools.ValToString(newGroup.RadioFrequency, "F1") });

                            usedNodesSpawnPoints.Add(spawnPoint.UniqueID);

                            // Feature is an objective and requires a waypoint
                            if ((feature.Category == FeatureCategory.Objective) && feature.WaypointEnabled)
                            {
                                featureWPName = HQTools.RandomFrom(waypointNames);
                                if (string.IsNullOrEmpty(featureWPName))
                                    featureWPName = $"WP{(waypointsList.Count + 1).ToString("00")}";
                                else
                                {
                                    waypointNames.Remove(featureWPName);
                                    featureWPName = featureWPName.ToUpperInvariant();
                                }

                                Coordinates wpPosition = spawnPoint.Position + Coordinates.CreateRandomInaccuracy(feature.WaypointInaccuracy);
                                objectivesList.Add(new MissionObjectiveLocation(spawnPoint.Position, featureWPName, feature.WaypointOnGround ? 0 : 1, 0));
                                waypointsList.Add(new MissionWaypoint(wpPosition, featureWPName));
                            }

                            for (int i = 0; i < E)
                        }
                    }

                    briefingMessagesReplacements.AddRange(new string[] { "OBJECTIVE", featureWPName });

                    if (!string.IsNullOrEmpty(feature.BriefingRemark))
                        mission.BriefingRemarks.Add(language.GetStringRandom("Briefing", $"Remark.{feature.BriefingRemark}", briefingMessagesReplacements.ToArray()));

                    if (!string.IsNullOrEmpty(feature.BriefingTask))
                        mission.BriefingTasks.Add(language.GetStringRandom("Briefing", $"Task.{feature.BriefingTask}", briefingMessagesReplacements.ToArray()));
                }
            }

            mission.Objectives = objectivesList.ToArray();
            mission.OggFiles = oggFilesList.Distinct().ToArray();
            //for (int i = 0; i < )
            //mission.Scripts = scriptsList.ToArray();
            mission.Waypoints = waypointsList.ToArray();

            usedNodesCoordinates = usedNodesCoordinatesList.ToArray();
        }

        private void CreatePlayerFlightGroups(
            Mission mission, MissionTemplate template,
            MissionGeneratorUnitGroups unitGroupsGenerator, List<string> usedPlayerAircraftTypeList)
        {
            foreach (MissionTemplateLocation node in template.Locations.Values)
            {
                // Only airbases can host player flight groups
                if (node.Definition.LocationType != TheaterLocationType.Airbase) continue;

                foreach (MissionTemplatePlayerFlightGroup pfg in node.PlayerFlightGroups)
                {
                    unitGroupsGenerator.AddPlayerFlightGroup(mission, template, pfg, node);
                    usedPlayerAircraftTypeList.Add(pfg.AircraftType);
                }
            }
        }
    }
}

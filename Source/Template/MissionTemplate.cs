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
using Headquarters4DCS.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Headquarters4DCS.Template
{
    /// <summary>
    /// An HQ4DCS mission template, which can be loaded or saved from/to a file, or used to generate a mission using the MissionGenerator class.
    /// </summary>
    public sealed class MissionTemplate : IDisposable
    {
        /// <summary>
        /// The description of the mission in the briefing.
        /// </summary>
        [Category("Briefing")]
        [DisplayName("Mission description"), Description("The description of the mission in the briefing. Leave empty to generate a random briefing.")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string BriefingDescription { get; set; }

        /// <summary>
        /// The name of the mission.
        /// </summary>
        [Category("Briefing")]
        [DisplayName("Mission name"), Description("The name of the mission. Leave empty to generate a random name.")]
        public string BriefingName { get; set; }

        /// <summary>
        /// Which unit system should be used in the mission briefing?
        /// </summary>
        [Category("Briefing")]
        [DisplayName("Unit system"), Description("Which unit system should be used in the mission briefing?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<UnitSystem>))]
        public UnitSystem BriefingUnits { get; set; }
        
        /// <summary>
        /// ID of the definition to use for the blue coalition.
        /// </summary>
        [Category("Context")]
        [DisplayName("Coalition, blue"), Description("Who belongs the blue coalition? This affects the type of units you will encounter.")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionCoalition>))]
        public string ContextCoalitionBlue { get; set; }

        /// <summary>
        /// ID of the definition to use for the red coalition.
        /// </summary>
        [Category("Context")]
        [DisplayName("Coalition, red"), Description("Who belongs the red coalition? This affects the type of units you will encounter.")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionCoalition>))]
        public string ContextCoalitionRed { get; set; }

        /// <summary>
        /// ID of the definition to use for the red coalition.
        /// </summary>
        [Category("Context")]
        [DisplayName("Countries coalition"), Description("Which coalitions do the countries (and the airfields on their territories) belong to?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<CountriesCoalition>))]
        public CountriesCoalition ContextCountriesCoalitions { get; set; }

        /// <summary>
        /// Which coalition do the players belong to?
        /// </summary>
        [Category("Context")]
        [DisplayName("Players coalition"), Description("Which coalition do(es) the player(s) belong to?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<Coalition>))]
        public Coalition ContextPlayerCoalition { get; set; }

        /// <summary>
        /// Theater on which the mission will take place.
        /// </summary>
        [Category("Context")]
        [DisplayName("Theater"), Description("On which DCS World theater will the mission take place?")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionTheater>))]
        public string ContextTheater { get; set; }

        /// <summary>
        /// Time period during which the mission take place.
        /// </summary>
        [Category("Context")]
        [DisplayName("Time period"), Description("When does the mission take place? This affects units allies and enemies will use.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<TimePeriod>))]
        public TimePeriod ContextTimePeriod { get; set; }

        /// <summary>
        /// Season during which the mission should take place.
        /// </summary>
        [Category("Environment")]
        [DisplayName("Season"), Description("During which season does the mission take place?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<Season>))]
        public Season EnvironmentSeason { get; set; }

        /// <summary>
        /// Starting time of the mission.
        /// </summary>
        [Category("Environment")]
        [DisplayName("Time of day"), Description("The starting time of the mission.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<TimeOfDay>))]
        public TimeOfDay EnvironmentTimeOfDay { get; set; }

        /// <summary>
        /// Weather be like during the mission.
        /// </summary>
        [Category("Environment")]
        [DisplayName("Weather"), Description("How will the weather be like during the mission?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<Weather>))]
        public Weather EnvironmentWeather { get; set; }

        /// <summary>
        /// How windy will the weather be during the mission.
        /// </summary>
        [Category("Environment")]
        [DisplayName("Wind"), Description("How windy will the weather be during the mission. If set to Auto, the wind speed will match the weather (e.g. always high wind in the middle of a thunderstorm).")]
        [TypeConverter(typeof(SplitEnumTypeConverter<Wind>))]
        public Wind EnvironmentWind { get; set; }

        /// <summary>
        /// How many objectives/targets will be present in the mission?
        /// </summary>
        [Category("Mission objective")]
        [DisplayName("Objective count"), Description("How many objectives/targets will be present in the mission?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<Count12345R>))]
        public Count12345R ObjectiveCount { get; set; }

        /// <summary>
        /// How far from the player(s) starting airbase will the objectives be?
        /// </summary>
        [Category("Mission objective")]
        [DisplayName("Objective distance"), Description("How far from the player(s) starting airbase will the objectives be?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<AmountR>))]
        public AmountR ObjectiveDistance { get; set; }

        /// <summary>
        /// The type of objective of this mission
        /// </summary>
        [Category("Mission objective")]
        [DisplayName("Objective type"), Description("The type of objective of this mission")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionObjective>))]
        public string ObjectiveType { get; set; }

        /// <summary>
        /// Player flight groups
        /// </summary>
        [Category("Mission package")]
        [DisplayName("Flight groups"), Description("All player controlled flight groups")]
        [TypeConverter(typeof(MissionTemplatePlayerFlightGroupConverter))]
        [Editor(typeof(DescriptionArrayEditor), typeof(UITypeEditor))]
        public MissionTemplatePlayerFlightGroup[] PlayerFlightGroups { get; set; }

        /// <summary>
        /// Should enemy units be visible on the F10 map?
        /// </summary>
        [Category("Preferences")]
        [DisplayName("Show enemy units on map"), Description("Should enemy units be visible on the F10 map?")]
        [TypeConverter(typeof(BooleanYesNoTypeConverter))]
        public bool PreferencesEnemiesOnF10Map { get; set; }

        /// <summary>
        /// If enabled, single-player missions will use client spots like multiplayer missions.
        /// </summary>
        [Category("Preferences")]
        [DisplayName("SP client slots"), Description("If enabled, single-player missions will use client spots, like multiplayer missions, so the player will be able to respawn whenever he/she wants.")]
        [TypeConverter(typeof(BooleanYesNoTypeConverter))]
        public bool PreferencesForceClientInSP { get; set; }

        /// <summary>
        /// ID of the language definition to use for the mission briefing and in-game messages.
        /// </summary>
        [Category("Preferences")]
        [DisplayName("Language"), Description("The language to use for the mission briefing and in-game messages.")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionLanguage>))]
        public string PreferencesLanguage { get; set; }

        /// <summary>
        /// Should enemy units be visible on the F10 map?
        /// </summary>
        [Category("Preferences")]
        [DisplayName("Extra waypoints"), Description("If enabled, extra navigation waypoints will be added between the takeoff/landing waypoints and the objective waypoints.")]
        [TypeConverter(typeof(BooleanYesNoTypeConverter))]
        public bool PreferencesExtraWaypoints { get; set; }

        /// <summary>
        /// Allow external views?
        /// </summary>
        [Category("Realism")]
        [DisplayName("Allow external views"), Description("Should players be allowed to use external views?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<DCSOption>))]
        public DCSOption RealismAllowExternalViews { get; set; }

        /// <summary>
        /// Bird strikes enabled?
        /// </summary>
        [Category("Realism")]
        [DisplayName("Bird strikes"), Description("Should random brid strikes happen when flying at low altitude?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<DCSOption>))]
        public DCSOption RealismBirdStrikes { get; set; }

        /// <summary>
        /// Random failures enabled?
        /// </summary>
        [Category("Realism")]
        [DisplayName("Random failures"), Description("Should random system failures happen?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<DCSOption>))]
        public DCSOption RealismRandomFailures { get; set; }

        [Category("Situation")]
        [DisplayName("Enemy air defense"), Description("Amount of enemy surface-to-air defense.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<AmountNR>))]
        public AmountNR SituationEnemyAirDefense { get; set; }

        [Category("Situation")]
        [DisplayName("Friendly air defense"), Description("Amount of friendly surface-to-air defense.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<AmountNR>))]
        public AmountNR SituationFriendlyAirDefense { get; set; }

        [Category("Situation")]
        [DisplayName("Enemy CAP"), Description("Amount of enemy combat air patrols.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<AmountNR>))]
        public AmountNR SituationEnemyCAP { get; set; }

        [Category("Situation")]
        [DisplayName("Friendly CAP"), Description("Amount of friendly combat air patrols.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<AmountNR>))]
        public AmountNR SituationFriendlyCAP { get; set; }

        /// <summary>
        /// Skill level of friendly AI aircraft.
        /// </summary>
        [Category("Skill level")]
        [DisplayName("Friendly aircraft AI"), Description("Skill level of friendly AI aircraft.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SkillFriendlyAir { get; set; }

        /// <summary>
        /// Skill level of friendly AI ground units.
        /// </summary>
        [Category("Skill level")]
        [DisplayName("Friendly ground AI"), Description("Skill level of friendly AI ground units.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SkillFriendlyGround { get; set; }

        /// <summary>
        /// Skill level of enemy AI aircraft.
        /// </summary>
        [Category("Skill level")]
        [DisplayName("Enemy aircraft AI"), Description("Skill level of enemy AI aircraft.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SkillEnemyAir { get; set; }

        /// <summary>
        /// Skill level of enemy AI ground units.
        /// </summary>
        [Category("Skill level")]
        [DisplayName("Enemy ground AI"), Description("Skill level of enemy AI ground units.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SkillEnemyGround { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MissionTemplate()
        {
            Clear(true);
        }

        /// <summary>
        /// Sets a new "clean" template. Resets all settings to their default.
        /// </summary>
        public void Clear() { Clear(true); }

        /// <summary>
        /// Sets a new "clean" template. Resets all settings to their default.
        /// </summary>
        /// <param name="addDefaultFlightGroup">Should a default flight group be added?</param>
        private void Clear(bool addDefaultFlightGroup)
        {
            BriefingDescription = "";
            BriefingName = "";
            BriefingUnits = UnitSystem.ByCoalition;

            ContextCoalitionBlue = Library.Instance.Common.DefaultCoalitionBlue;
            ContextCoalitionRed = Library.Instance.Common.DefaultCoalitionRed;
            ContextCountriesCoalitions = CountriesCoalition.Default;
            ContextPlayerCoalition = Coalition.Blue;
            ContextTheater = Library.Instance.Common.DefaultTheater;
            ContextTimePeriod = TimePeriod.Decade2000;

            EnvironmentSeason = Season.Random;
            EnvironmentTimeOfDay = TimeOfDay.RandomDaytime;
            EnvironmentWeather = Weather.Random;
            EnvironmentWind = Wind.Auto;

            ObjectiveCount = Count12345R.Random;
            ObjectiveDistance = AmountR.Random;
            ObjectiveType = Library.Instance.Common.DefaultObjective;

            PlayerFlightGroups = addDefaultFlightGroup ? new MissionTemplatePlayerFlightGroup[] { new MissionTemplatePlayerFlightGroup() } : new MissionTemplatePlayerFlightGroup[0];

            PreferencesEnemiesOnF10Map = false;
            PreferencesForceClientInSP = false;
            PreferencesLanguage = Library.Instance.Common.DefaultLanguage;
            PreferencesExtraWaypoints = true;

            RealismAllowExternalViews = DCSOption.Default;
            RealismBirdStrikes = DCSOption.Default;
            RealismRandomFailures = DCSOption.Default;

            SituationEnemyAirDefense = AmountNR.Random;
            SituationEnemyCAP = AmountNR.Random;
            SituationFriendlyAirDefense = AmountNR.Random;
            SituationFriendlyCAP = AmountNR.Random;

            SkillEnemyAir = HQSkillLevel.Random;
            SkillEnemyGround = HQSkillLevel.Random;
            SkillFriendlyAir = HQSkillLevel.Random;
            SkillFriendlyGround = HQSkillLevel.Random;
        }

        /// <summary>
        /// Loads the template from an HQT file.
        /// </summary>
        /// <param name="filePath">The path of the file to load from.</param>
        /// <returns>True if everything went right, false if an error happened.</returns>
        public bool LoadFromFile(string filePath)
        {
            Clear(false);

            using (INIFile ini = new INIFile(filePath))
            {
                BriefingDescription = ini.GetValue("Settings", "Briefing.Description", BriefingDescription);
                BriefingName = ini.GetValue("Settings", "Briefing.Name", BriefingName);
                BriefingUnits = ini.GetValue("Settings", "Briefing.Units", BriefingUnits);

                ContextCoalitionBlue = ini.GetValue("Settings", "Context.Coalition.Blue", ContextCoalitionBlue);
                ContextCoalitionRed = ini.GetValue("Settings", "Context.Coalition.Red", ContextCoalitionRed);
                ContextCountriesCoalitions = ini.GetValue("Settings", "Context.CountriesCoalitions", ContextCountriesCoalitions);
                ContextPlayerCoalition = ini.GetValue("Settings", "Context.PlayerCoalition", ContextPlayerCoalition);
                ContextTheater = ini.GetValue("Settings", "Context.Theater", ContextTheater);
                ContextTimePeriod = ini.GetValue("Settings", "Context.TimePeriod", ContextTimePeriod);

                EnvironmentSeason = ini.GetValue("Settings", "Environment.Season", EnvironmentSeason);
                EnvironmentTimeOfDay = ini.GetValue("Settings", "Environment.TimeOfDay", EnvironmentTimeOfDay);
                EnvironmentWeather = ini.GetValue("Settings", "Environment.Weather", EnvironmentWeather);
                EnvironmentWind = ini.GetValue("Settings", "Environment.Wind", EnvironmentWind);

                ObjectiveCount = ini.GetValue("Settings", "Objective.Count", ObjectiveCount);
                ObjectiveDistance = ini.GetValue("Settings", "Objective.Distance", ObjectiveDistance);
                ObjectiveType = ini.GetValue("Settings", "Objective.Type", ObjectiveType);

                List<MissionTemplatePlayerFlightGroup> playerFlightGroupsList = new List<MissionTemplatePlayerFlightGroup>();
                foreach (string k in ini.GetKeysInSection("FlightGroups"))
                    playerFlightGroupsList.Add(new MissionTemplatePlayerFlightGroup(ini, "FlightGroups", k));
                PlayerFlightGroups = playerFlightGroupsList.ToArray();

                PreferencesEnemiesOnF10Map = ini.GetValue("Settings", "Preferences.EnemiesOnF10Map", PreferencesEnemiesOnF10Map);
                PreferencesExtraWaypoints = ini.GetValue("Settings", "Preferences.ExtraWaypoints", PreferencesExtraWaypoints);
                PreferencesForceClientInSP = ini.GetValue("Settings", "Preferences.ForceClientInSP", PreferencesForceClientInSP);
                PreferencesLanguage = ini.GetValue("Settings", "Preferences.Language", PreferencesLanguage);

                RealismAllowExternalViews = ini.GetValue("Settings", "Realism.AllowExternalViews", RealismAllowExternalViews);
                RealismBirdStrikes = ini.GetValue("Settings", "Realism.BirdStrikes", RealismBirdStrikes);
                RealismRandomFailures = ini.GetValue("Settings", "Realism.RandomFailures", RealismRandomFailures);

                SituationEnemyAirDefense = ini.GetValue("Settings", "Situation.Enemy.AirDefense", SituationEnemyAirDefense);
                SituationEnemyCAP = ini.GetValue("Settings", "Situation.Enemy.CAP", SituationEnemyCAP);
                SituationFriendlyAirDefense = ini.GetValue("Settings", "Situation.Friendly.AirDefense", SituationFriendlyAirDefense);
                SituationFriendlyCAP = ini.GetValue("Settings", "Situation.Friendly.CAP", SituationFriendlyCAP);

                SkillEnemyAir = ini.GetValue("Settings", "Skill.Enemy.Air", SkillEnemyAir);
                SkillEnemyGround = ini.GetValue("Settings", "Skill.Enemy.Ground", SkillEnemyGround);
                SkillFriendlyAir = ini.GetValue("Settings", "Skill.Friendly.Air", SkillFriendlyAir);
                SkillFriendlyGround = ini.GetValue("Settings", "Skill.Friendly.Ground", SkillFriendlyGround);
            }

            return true;
        }

        /// <summary>
        /// Save the template to an HQT file.
        /// </summary>
        /// <param name="filePath">The path of the file to save to.</param>
        /// <returns>True if everything went right, false if an error happened.</returns>
        public bool SaveToFile(string filePath)
        {
            using (INIFile ini = new INIFile())
            {
                ini.SetValue("Settings", "Briefing.Description", BriefingDescription);
                ini.SetValue("Settings", "Briefing.Name", BriefingName);
                ini.SetValue("Settings", "Briefing.Units", BriefingUnits);

                ini.SetValue("Settings", "Context.Coalition.Blue", ContextCoalitionBlue);
                ini.SetValue("Settings", "Context.Coalition.Red", ContextCoalitionRed);
                ini.SetValue("Settings", "Context.CountriesCoalitions", ContextCountriesCoalitions);
                ini.SetValue("Settings", "Context.PlayerCoalition", ContextPlayerCoalition);
                ini.SetValue("Settings", "Context.Theater", ContextTheater);
                ini.SetValue("Settings", "Context.TimePeriod", ContextTimePeriod);

                ini.SetValue("Settings", "Environment.Season", EnvironmentSeason);
                ini.SetValue("Settings", "Environment.TimeOfDay", EnvironmentTimeOfDay);
                ini.SetValue("Settings", "Environment.Weather", EnvironmentWeather);
                ini.SetValue("Settings", "Environment.Wind", EnvironmentWind);

                ini.SetValue("Settings", "Objective.Count", ObjectiveCount);
                ini.SetValue("Settings", "Objective.Distance", ObjectiveDistance);
                ini.SetValue("Settings", "Objective.Type", ObjectiveType);

                ini.SetValue("Settings", "Preferences.EnemiesOnF10Map", PreferencesEnemiesOnF10Map);
                ini.SetValue("Settings", "Preferences.ExtraWaypoints", PreferencesExtraWaypoints);
                ini.SetValue("Settings", "Preferences.ForceClientInSP", PreferencesForceClientInSP);
                ini.SetValue("Settings", "Preferences.Language", PreferencesLanguage);

                ini.SetValue("Settings", "Realism.AllowExternalViews", RealismAllowExternalViews);
                ini.SetValue("Settings", "Realism.BirdStrikes", RealismBirdStrikes);
                ini.SetValue("Settings", "Realism.RandomFailures", RealismRandomFailures);

                ini.SetValue("Settings", "Situation.Enemy.AirDefense", SituationEnemyAirDefense);
                ini.SetValue("Settings", "Situation.Enemy.CAP", SituationEnemyCAP);
                ini.SetValue("Settings", "Situation.Friendly.AirDefense", SituationFriendlyAirDefense);
                ini.SetValue("Settings", "Situation.Friendly.CAP", SituationFriendlyCAP);

                ini.SetValue("Settings", "Skill.Enemy.Air", SkillEnemyAir);
                ini.SetValue("Settings", "Skill.Enemy.Ground", SkillEnemyGround);
                ini.SetValue("Settings", "Skill.Friendly.Air", SkillFriendlyAir);
                ini.SetValue("Settings", "Skill.Friendly.Ground", SkillFriendlyGround);

                for (int i = 0; i < PlayerFlightGroups.Length; i++)
                    PlayerFlightGroups[i].SaveToFile(ini, "FlightGroups", $"FG{i.ToString("000")}");

                ini.SaveToFile(filePath);
            }

            return true;
        }

        /// <summary>
        /// Returns the total number of client-controlled aircraft (not flight groups) in this template.
        /// </summary>
        /// <returns>Number of players</returns>
        public int GetPlayerCount()
        {
            int playerCount = 0;

            foreach (MissionTemplatePlayerFlightGroup pfg in PlayerFlightGroups)
                playerCount += pfg.GetPlayerCount();

            return playerCount;
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { }
    }
}

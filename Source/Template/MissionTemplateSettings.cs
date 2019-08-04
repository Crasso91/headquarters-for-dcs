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

using Headquarters4DCS.Library;
using Headquarters4DCS.TypeConverters;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Headquarters4DCS.Template
{
    public sealed class MissionTemplateSettings : IDisposable
    {
        [LocalizedCategory("Briefing")]
        [LocalizedDisplayName("BriefingDescription"), LocalizedDescription("BriefingDescription")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string BriefingDescription { get; set; }

        [LocalizedCategory("Briefing")]
        [LocalizedDisplayName("BriefingName"), LocalizedDescription("BriefingName")]
        public string BriefingName { get; set; }

        [LocalizedCategory("Briefing")]
        [LocalizedDisplayName("BriefingUnits"), LocalizedDescription("BriefingUnits")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<SpeedAndDistanceUnit>))]
        public SpeedAndDistanceUnit BriefingUnits { get; set; }
        
        [LocalizedCategory("Context")]
        [LocalizedDisplayName("ContextCoalitionBlue"), LocalizedDescription("ContextCoalitionBlue")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionCoalition>))]
        public string ContextCoalitionBlue { get; set; }

        [LocalizedCategory("Context")]
        [LocalizedDisplayName("ContextCoalitionRed"), LocalizedDescription("ContextCoalitionRed")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionCoalition>))]
        public string ContextCoalitionRed { get; set; }

        [LocalizedCategory("Context")]
        [LocalizedDisplayName("ContextPlayerCoalition"), LocalizedDescription("ContextPlayerCoalition")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<Coalition>))]
        public Coalition ContextPlayerCoalition { get; set; }

        [LocalizedCategory("Context")]
        [LocalizedDisplayName("ContextTimePeriod"), LocalizedDescription("ContextTimePeriod")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<TimePeriod>))]
        public TimePeriod ContextTimePeriod { get; set; }

        [LocalizedCategory("Environment")]
        [LocalizedDisplayName("EnvironmentSeason"), LocalizedDescription("EnvironmentSeason")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<Season>))]
        public Season EnvironmentSeason { get; set; }

        [LocalizedCategory("Environment")]
        [LocalizedDisplayName("EnvironmentTimeOfDay"), LocalizedDescription("EnvironmentTimeOfDay")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<TimeOfDay>))]
        public TimeOfDay EnvironmentTimeOfDay { get; set; }

        [LocalizedCategory("Environment")]
        [LocalizedDisplayName("EnvironmentWeather"), LocalizedDescription("EnvironmentWeather")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<Weather>))]
        public Weather EnvironmentWeather { get; set; }

        [LocalizedCategory("Environment")]
        [LocalizedDisplayName("EnvironmentWind"), LocalizedDescription("EnvironmentWind")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<Wind>))]
        public Wind EnvironmentWind { get; set; }

        [LocalizedCategory("Preferences")]
        [LocalizedDisplayName("PreferencesEnemiesOnF10Map"), LocalizedDescription("PreferencesEnemiesOnF10Map")]
        [TypeConverter(typeof(LocalizedBooleanTypeConverter))]
        public bool PreferencesEnemiesOnF10Map { get; set; }

        [LocalizedCategory("Preferences")]
        [LocalizedDisplayName("PreferencesForceClientInSP"), LocalizedDescription("PreferencesForceClientInSP")]
        [TypeConverter(typeof(LocalizedBooleanTypeConverter))]
        public bool PreferencesForceClientInSP { get; set; }

        [LocalizedCategory("Preferences")]
        [LocalizedDisplayName("PreferencesLanguage"), LocalizedDescription("PreferencesLanguage")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionLanguage>))]        
        public string PreferencesLanguage { get; set; }

        [LocalizedCategory("Preferences")]
        [LocalizedDisplayName("PreferencesPilotGender"), LocalizedDescription("PreferencesPilotGender")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<Gender>))]
        public Gender PreferencesPilotGender { get; set; }

        /// <summary>
        /// Allow external views?
        /// </summary>
        [LocalizedCategory("Realism")]
        [LocalizedDisplayName("RealismAllowExternalViews"), LocalizedDescription("RealismAllowExternalViews")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<DCSOption>))]
        public DCSOption RealismAllowExternalViews { get; set; }

        /// <summary>
        /// Bird strikes enabled?
        /// </summary>
        [LocalizedCategory("Realism")]
        [LocalizedDisplayName("RealismBirdStrikes"), LocalizedDescription("RealismBirdStrikes")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<DCSOption>))]
        public DCSOption RealismBirdStrikes { get; set; }

        /// <summary>
        /// Random failures enabled?
        /// </summary>
        [LocalizedCategory("Realism")]
        [LocalizedDisplayName("RealismRandomFailures"), LocalizedDescription("RealismRandomFailures")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<DCSOption>))]
        public DCSOption RealismRandomFailures { get; set; }

        [LocalizedCategory("Situation")]
        [LocalizedDisplayName("SituationAllySkillAir"), LocalizedDescription("SituationAllySkillAir")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SituationAllySkillAir { get; set; }

        [LocalizedCategory("Situation")]
        [LocalizedDisplayName("SituationAllySkillGround"), LocalizedDescription("SituationAllySkillGround")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SituationAllySkillGround { get; set; }

        [LocalizedCategory("Situation")]
        [LocalizedDisplayName("SituationEnemySkillAir"), LocalizedDescription("SituationEnemySkillAir")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SituationEnemySkillAir { get; set; }

        [LocalizedCategory("Situation")]
        [LocalizedDisplayName("SituationEnemySkillGround"), LocalizedDescription("SituationEnemySkillGround")]
        [TypeConverter(typeof(LocalizedEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SituationEnemySkillGround { get; set; }

        public MissionTemplateSettings() { Clear(); }
        public void Dispose() { }

        public void Clear()
        {
            BriefingDescription = "";
            BriefingName = "";
            BriefingUnits = SpeedAndDistanceUnit.ByCoalition;

            ContextCoalitionBlue = "USA"; // TODO: load from file
            ContextCoalitionRed = "Russia"; // TODO: load from file
            ContextPlayerCoalition = Coalition.Blue;
            ContextTimePeriod = TimePeriod.Decade2000;

            EnvironmentSeason = Season.Random;
            EnvironmentTimeOfDay = TimeOfDay.RandomDaytime;
            EnvironmentWeather = Weather.Random;
            EnvironmentWind = Wind.Auto;

            PreferencesEnemiesOnF10Map = false;
            PreferencesForceClientInSP = false;
            PreferencesLanguage = "English";
            PreferencesPilotGender = Gender.Male;

            SituationAllySkillAir = HQSkillLevel.Random;
            SituationAllySkillGround = HQSkillLevel.Random;
            SituationEnemySkillAir = HQSkillLevel.Random;
            SituationEnemySkillGround = HQSkillLevel.Random;
        }

        public void LoadFromFile(INIFile ini)
        {
            Clear();

            BriefingDescription = ini.GetValue("Settings", "Briefing.Description", BriefingDescription);
            BriefingName = ini.GetValue("Settings", "Briefing.Name", BriefingName);
            BriefingUnits = ini.GetValue("Settings", "Briefing.Units", BriefingUnits);

            ContextCoalitionBlue = ini.GetValue("Settings", "Context.Coalition.Blue", ContextCoalitionBlue);
            ContextCoalitionRed = ini.GetValue("Settings", "Context.Coalition.Red", ContextCoalitionRed);
            ContextPlayerCoalition = ini.GetValue("Settings", "Context.PlayerCoalition", ContextPlayerCoalition);
            ContextTimePeriod = ini.GetValue("Settings", "Context.TimePeriod", ContextTimePeriod);

            EnvironmentSeason = ini.GetValue("Settings", "Environment.Season", EnvironmentSeason);
            EnvironmentTimeOfDay = ini.GetValue("Settings", "Environment.TimeOfDay", EnvironmentTimeOfDay);
            EnvironmentWeather = ini.GetValue("Settings", "Environment.Weather", EnvironmentWeather);
            EnvironmentWind = ini.GetValue("Settings", "Environment.Wind", EnvironmentWind);

            PreferencesEnemiesOnF10Map = ini.GetValue("Settings", "Preferences.EnemiesOnF10Map", PreferencesEnemiesOnF10Map);
            PreferencesForceClientInSP = ini.GetValue("Settings", "Preferences.ForceClientInSP", PreferencesForceClientInSP);
            PreferencesLanguage = ini.GetValue("Settings", "Preferences.Language", PreferencesLanguage);
            PreferencesPilotGender = ini.GetValue("Settings", "Preferences.PilotGender", PreferencesPilotGender);

            SituationAllySkillAir = ini.GetValue("Settings", "Situation.Skill.Ally.Air", SituationAllySkillAir);
            SituationAllySkillGround = ini.GetValue("Settings", "Situation.Skill.Ally.Ground", SituationAllySkillGround);
            SituationEnemySkillAir = ini.GetValue("Settings", "Situation.Skill.Enemy.Air", SituationEnemySkillAir);
            SituationEnemySkillGround = ini.GetValue("Settings", "Situation.Skill.Enemy.Ground", SituationEnemySkillGround);
        }

        public void SaveToFile(INIFile ini)
        {
            ini.SetValue("Settings", "Briefing.Description", BriefingDescription);
            ini.SetValue("Settings", "Briefing.Name", BriefingName);
            ini.SetValue("Settings", "Briefing.Units", BriefingUnits);

            ini.SetValue("Settings", "Context.Coalition.Blue", ContextCoalitionBlue);
            ini.SetValue("Settings", "Context.Coalition.Red", ContextCoalitionRed);
            ini.SetValue("Settings", "Context.PlayerCoalition", ContextPlayerCoalition);
            ini.SetValue("Settings", "Context.TimePeriod", ContextTimePeriod);

            ini.SetValue("Settings", "Environment.Season", EnvironmentSeason);
            ini.SetValue("Settings", "Environment.TimeOfDay", EnvironmentTimeOfDay);
            ini.SetValue("Settings", "Environment.Weather", EnvironmentWeather);
            ini.SetValue("Settings", "Environment.Wind", EnvironmentWind);

            ini.SetValue("Settings", "Preferences.EnemiesOnF10Map", PreferencesEnemiesOnF10Map);
            ini.SetValue("Settings", "Preferences.ForceClientInSP", PreferencesForceClientInSP);
            ini.SetValue("Settings", "Preferences.Language", PreferencesLanguage);
            ini.SetValue("Settings", "Preferences.PilotGender", PreferencesPilotGender);

            ini.SetValue("Settings", "Situation.Skill.Ally.Air", SituationAllySkillAir);
            ini.SetValue("Settings", "Situation.Skill.Ally.Ground", SituationAllySkillGround);
            ini.SetValue("Settings", "Situation.Skill.Enemy.Air", SituationEnemySkillAir);
            ini.SetValue("Settings", "Situation.Skill.Enemy.Ground", SituationEnemySkillGround);
        }
    }
}

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
    /// <summary>
    /// Global settings for a mission template.
    /// </summary>
    public sealed class MissionTemplateSettings : IDisposable
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
        [DisplayName("Coalition, blue"), Description("Who belongs to the BLUE coalition? This affects the type of units you will encounter.")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionCoalition>))]
        public string ContextCoalitionBlue { get; set; }

        /// <summary>
        /// ID of the definition to use for the red coalition.
        /// </summary>
        [Category("Context")]
        [DisplayName("Coalition, red"), Description("Who belongs to the RED coalition? This affects the type of units you will encounter.")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionCoalition>))]
        public string ContextCoalitionRed { get; set; }

        /// <summary>
        /// Which coalition do the players belong to?
        /// </summary>
        [Category("Context")]
        [DisplayName("Players coalition"), Description("Which coalition do(es) the player(s) belong to?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<Coalition>))]
        public Coalition ContextPlayerCoalition { get; set; }

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
        /// Should the voice radio messages by the player pilots use a male or a female voice?
        /// </summary>
        [Category("Preferences")]
        [DisplayName("Pilot gender"), Description("Should the voice radio messages by the player pilot(s) use a male or a female voice?")]
        [TypeConverter(typeof(SplitEnumTypeConverter<Gender>))]
        public Gender PreferencesPilotGender { get; set; }

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

        /// <summary>
        /// Skill level of friendly AI aircraft.
        /// </summary>
        [Category("Situation")]
        [DisplayName("Skill level, friendly aircraft"), Description("Skill level of friendly AI aircraft.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SituationAllySkillAir { get; set; }

        /// <summary>
        /// Skill level of friendly AI ground units.
        /// </summary>
        [Category("Situation")]
        [DisplayName("Skill level, friendly ground units"), Description("Skill level of friendly AI ground units.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SituationAllySkillGround { get; set; }

        /// <summary>
        /// Skill level of enemy AI aircraft.
        /// </summary>
        [Category("Situation")]
        [DisplayName("Skill level, enemy aircraft"), Description("Skill level of enemy AI aircraft.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SituationEnemySkillAir { get; set; }

        /// <summary>
        /// Skill level of enemy AI ground units.
        /// </summary>
        [Category("Situation")]
        [DisplayName("Skill level, enemy ground units"), Description("Skill level of enemy AI ground units.")]
        [TypeConverter(typeof(SplitEnumTypeConverter<HQSkillLevel>))]
        public HQSkillLevel SituationEnemySkillGround { get; set; }

        public MissionTemplateSettings() { Clear(); }
        public void Dispose() { }

        public void Clear()
        {
            BriefingDescription = "";
            BriefingName = "";
            BriefingUnits = UnitSystem.ByCoalition;

            ContextCoalitionBlue = HQLibrary.Instance.Common.DefaultCoalitionBlue;
            ContextCoalitionRed = HQLibrary.Instance.Common.DefaultCoalitionRed;
            ContextPlayerCoalition = Coalition.Blue;
            ContextTimePeriod = TimePeriod.Decade2000;

            EnvironmentSeason = Season.Random;
            EnvironmentTimeOfDay = TimeOfDay.RandomDaytime;
            EnvironmentWeather = Weather.Random;
            EnvironmentWind = Wind.Auto;

            PreferencesEnemiesOnF10Map = false;
            PreferencesForceClientInSP = false;
            PreferencesLanguage = HQLibrary.Instance.Common.DefaultLanguage;
            PreferencesPilotGender = Gender.Male;

            RealismAllowExternalViews = DCSOption.Default;
            RealismBirdStrikes = DCSOption.Default;
            RealismRandomFailures = DCSOption.Default;

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

            RealismAllowExternalViews = ini.GetValue("Settings", "Realism.AllowExternalViews", RealismAllowExternalViews);
            RealismBirdStrikes = ini.GetValue("Settings", "Realism.BirdStrikes", RealismBirdStrikes);
            RealismRandomFailures = ini.GetValue("Settings", "Realism.RandomFailures", RealismRandomFailures);

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

            ini.SetValue("Settings", "Realism.AllowExternalViews", RealismAllowExternalViews);
            ini.SetValue("Settings", "Realism.BirdStrikes", RealismBirdStrikes);
            ini.SetValue("Settings", "Realism.RandomFailures", RealismRandomFailures);

            ini.SetValue("Settings", "Situation.Skill.Ally.Air", SituationAllySkillAir);
            ini.SetValue("Settings", "Situation.Skill.Ally.Ground", SituationAllySkillGround);
            ini.SetValue("Settings", "Situation.Skill.Enemy.Air", SituationEnemySkillAir);
            ini.SetValue("Settings", "Situation.Skill.Enemy.Ground", SituationEnemySkillGround);
        }
    }
}

using Headquarters4DCS.Enums;
using Headquarters4DCS.Library;
using Headquarters4DCS.TypeConverters;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Headquarters4DCS.Template
{
    public sealed class HQTemplateSettings : IDisposable
    {
        [Category("Briefing"), DisplayName("Mission name")]
        public string BriefingName { get; set; }

        [Category("Briefing"), DisplayName("Mission description")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string BriefingDescription { get; set; }

        [Category("Briefing"), DisplayName("Language")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionLanguage>))]
        public string BriefingLanguage { get; set; }

        [Category("Context"), DisplayName("Coalition, blue")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionCoalition>))]
        public string ContextCoalitionBlue { get; set; }

        [Category("Context"), DisplayName("Coalition, red")]
        [TypeConverter(typeof(DefinitionsStringConverter<DefinitionCoalition>))]
        public string ContextCoalitionRed { get; set; }

        [Category("Context"), DisplayName("Player coalition")]
        public Coalition ContextPlayerCoalition { get; set; }

        [Category("Context"), DisplayName("Time period")]
        public TimePeriod ContextTimePeriod { get; set; }

        [Category("Environment"), DisplayName("Season")]
        public Season EnvironmentSeason { get; set; }

        [Category("Environment"), DisplayName("Time of day")]
        public TimeOfDay EnvironmentTimeOfDay { get; set; }

        [Category("Environment"), DisplayName("Weather")]
        public Weather EnvironmentWeather { get; set; }

        [Category("Environment"), DisplayName("Wind")]
        public Wind EnvironmentWind { get; set; }

        [Category("Situation"), DisplayName("Skill, allies")]
        public HQSkillLevel SituationAllySkill { get; set; }

        [Category("Situation"), DisplayName("Skill, enemies")]
        public HQSkillLevel SituationEnemySkill { get; set; }

        public HQTemplateSettings() { Clear(); }
        public void Dispose() { }

        public void Clear()
        {
            BriefingName = "";
            BriefingDescription = "";
            BriefingLanguage = "English";

            ContextCoalitionBlue = "USA"; // TODO: load from file
            ContextCoalitionRed = "Russia"; // TODO: load from file
            ContextPlayerCoalition = Coalition.Blue;
            ContextTimePeriod = TimePeriod.Decade2000;

            EnvironmentSeason = Season.Random;
            EnvironmentTimeOfDay = TimeOfDay.RandomDaytime;
            EnvironmentWeather = Weather.Random;
            EnvironmentWind = Wind.Auto;

            SituationAllySkill = HQSkillLevel.Random;
            SituationEnemySkill = HQSkillLevel.Random;
        }

        public void LoadFromFile(INIFile ini)
        {
            Clear();

            BriefingName = ini.GetValue("Settings", "Briefing.Name", BriefingName);
            BriefingDescription = ini.GetValue("Settings", "Briefing.Description", BriefingDescription);
            BriefingLanguage = ini.GetValue("Settings", "Briefing.Language", BriefingLanguage);

            ContextCoalitionBlue = ini.GetValue("Settings", "Context.Coalition.Blue", ContextCoalitionBlue);
            ContextCoalitionRed = ini.GetValue("Settings", "Context.Coalition.Red", ContextCoalitionRed);
            ContextPlayerCoalition = ini.GetValue("Settings", "Context.PlayerCoalition", ContextPlayerCoalition);
            ContextTimePeriod = ini.GetValue("Settings", "Context.TimePeriod", ContextTimePeriod);

            EnvironmentSeason = ini.GetValue("Settings", "Environment.Season", EnvironmentSeason);
            EnvironmentTimeOfDay = ini.GetValue("Settings", "Environment.TimeOfDay", EnvironmentTimeOfDay);
            EnvironmentWeather = ini.GetValue("Settings", "Environment.Weather", EnvironmentWeather);
            EnvironmentWind = ini.GetValue("Settings", "Environment.Wind", EnvironmentWind);

            SituationAllySkill = ini.GetValue("Settings", "Situation.Skill.Ally", SituationAllySkill);
            SituationEnemySkill = ini.GetValue("Settings", "Situation.Skill.Enemy", SituationEnemySkill);
        }

        public void SaveToFile(INIFile ini)
        {
            ini.SetValue("Settings", "Briefing.Name", BriefingName);
            ini.SetValue("Settings", "Briefing.Description", BriefingDescription);
            ini.SetValue("Settings", "Briefing.Language", BriefingDescription);

            ini.SetValue("Settings", "Context.Coalition.Blue", ContextCoalitionBlue);
            ini.SetValue("Settings", "Context.Coalition.Red", ContextCoalitionRed);
            ini.SetValue("Settings", "Context.PlayerCoalition", ContextPlayerCoalition);
            ini.SetValue("Settings", "Context.TimePeriod", BriefingLanguage);

            ini.SetValue("Settings", "Environment.Season", EnvironmentSeason);
            ini.SetValue("Settings", "Environment.TimeOfDay", EnvironmentTimeOfDay);
            ini.SetValue("Settings", "Environment.Weather", EnvironmentWeather);
            ini.SetValue("Settings", "Environment.Wind", EnvironmentWind);

            ini.SetValue("Settings", "Situation.Skill.Ally", SituationAllySkill);
            ini.SetValue("Settings", "Situation.Skill.Enemy", SituationEnemySkill);
        }
    }
}

using Headquarters4DCS.Enums;
using Headquarters4DCS.Library;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using Headquarters4DCS.TypeConverters;

namespace Headquarters4DCS.Template
{
    public sealed class HQTemplateNodeAirbase : HQTemplateNode
    {
        private DefinitionTheaterNodeAirbase AirbaseDefinition { get { return (DefinitionTheaterNodeAirbase)NodeDefinition; } }

        [Category("Information"), DisplayName("(Name)")]
        public string Name { get { return AirbaseDefinition.Name; } }

        //[Category("Information"), DisplayName("Country")]
        //public Country Country { get { return AirbaseDefinition.Country; } }

        //[Category("Information"), DisplayName("Military airbase")]
        //public bool MilitaryAirbase { get { return AirbaseDefinition.IsMilitary; } }

        //[Category("Information"), DisplayName("TACAN")]
        //public string TACAN { get { return AirbaseDefinition.TACAN; } }

        //[Category("Information"), DisplayName("ILS")]
        //public string ILS { get { return AirbaseDefinition.ILS; } }

        //[Category("Information"), DisplayName("ATC")]
        //public string ATC { get { return HQTools.ValToString(AirbaseDefinition.ATC, "F1"); } }

        [Category("Settings"), DisplayName("Coalition")]
        public Coalition Coalition { get; set; } = Coalition.Blue;

        [Category("Settings"), DisplayName("Features")]
        [TypeConverter(typeof(ArrayTypeConverter<string>)), TheaterNodeType(TheaterNodeType.Airbase)]
        [Editor(typeof(UITypeEditorNodeFeatures), typeof(UITypeEditor))]
        public string[] Features { get; set; } = new string[0];

        [Category("Settings"), DisplayName("Player flight groups")]
        [TypeConverter(typeof(ArrayTypeConverter<HQTemplatePlayerFlightGroup>))]
        [Editor(typeof(UITypeEditorPlayerFlightGroups), typeof(UITypeEditor))]
        public HQTemplatePlayerFlightGroup[] PlayerFlightGroups { get; set; } = new HQTemplatePlayerFlightGroup[0];

        public HQTemplateNodeAirbase(DefinitionTheaterNode nodeDefinition) : base(nodeDefinition) { }

        protected override void Clear()
        {
            Coalition = AirbaseDefinition.Coalition;
            Features = new string[0];
            PlayerFlightGroups = new HQTemplatePlayerFlightGroup[0];
        }

        public override void LoadFromFile(INIFile ini)
        {
            Clear();

            Coalition = ini.GetValue(INISection, "Coalition", Coalition);

            PlayerFlightGroups = new HQTemplatePlayerFlightGroup[Math.Max(0, ini.GetValue<int>(INISection, "FlightGroupsCount"))];
            for (int i = 0; i < PlayerFlightGroups.Length; i++)
                PlayerFlightGroups[i] = new HQTemplatePlayerFlightGroup(ini, INISection, i);
        }

        public override void SaveToFile(INIFile ini)
        {
            if (Coalition != AirbaseDefinition.Coalition) ini.SetValue(INISection, "Coalition", Coalition);

            if (PlayerFlightGroups.Length > 0)
            {
                ini.SetValue(INISection, "FlightGroupsCount", PlayerFlightGroups.Length);

                for (int i = 0; i < PlayerFlightGroups.Length; i++)
                    PlayerFlightGroups[i].SaveToFile(ini, INISection, i);
            }
        }
    }
}

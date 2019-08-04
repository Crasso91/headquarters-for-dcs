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
using System;
using System.ComponentModel;
using System.Drawing.Design;
using Headquarters4DCS.TypeConverters;

namespace Headquarters4DCS.Template
{
    public sealed class MissionTemplateNodeAirbase : MissionTemplateNode
    {
        [Browsable(false)]
        public DefinitionTheaterNodeAirbase DefinitionAirbase { get { return (DefinitionTheaterNodeAirbase)base.Definition; } }

        [Category("Information"), DisplayName("(Name)")]
        public string Name { get { return DefinitionAirbase.Name; } }

        [Category("Information"), DisplayName("Country")]
        public Country Country { get { return DefinitionAirbase.Country; } }

        [Category("Information"), DisplayName("Military airbase")]
        public bool MilitaryAirbase { get { return DefinitionAirbase.IsMilitary; } }

        [Category("Information"), DisplayName("TACAN")]
        public string TACAN { get { return DefinitionAirbase.TACAN; } }

        [Category("Information"), DisplayName("ILS")]
        public string ILS { get { return DefinitionAirbase.ILS; } }

        [Category("Information"), DisplayName("ATC")]
        public string ATC { get { return HQTools.ValToString(DefinitionAirbase.ATC[0], "F1"); } }

        [Category("Settings"), DisplayName("Coalition")]
        public CoalitionNeutral Coalition { get; set; } = CoalitionNeutral.Blue;

        //[Category("Settings"), DisplayName("Features")]
        //[TypeConverter(typeof(ArrayTypeConverter<string>)), TheaterNodeType(TheaterNodeType.Airbase)]
        //[Editor(typeof(UITypeEditorNodeFeatures), typeof(UITypeEditor))]
        //public string[] Features { get; set; } = new string[0];

        //[Category("Settings"), DisplayName("Player flight groups")]
        //[TypeConverter(typeof(ArrayTypeConverter<HQTemplatePlayerFlightGroup>))]
        //[Editor(typeof(UITypeEditorPlayerFlightGroups), typeof(UITypeEditor))]
        //public HQTemplatePlayerFlightGroup[] PlayerFlightGroups { get; set; } = new HQTemplatePlayerFlightGroup[0];

        public MissionTemplateNodeAirbase(DefinitionTheaterNode nodeDefinition) : base(nodeDefinition) { }

        protected override void Clear()
        {
            base.Clear();
            Coalition = DefinitionAirbase.Coalition;
        }

        public override void LoadFromFile(INIFile ini)
        {
            base.LoadFromFile(ini);
            Coalition = ini.GetValue(INISection, "Coalition", Coalition);
        }

        public override void SaveToFile(INIFile ini)
        {
            base.SaveToFile(ini);
            if (Coalition != DefinitionAirbase.Coalition) ini.SetValue(INISection, "Coalition", Coalition);
        }
    }
}

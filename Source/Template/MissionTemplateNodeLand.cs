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
using System.ComponentModel;
using System.Drawing.Design;

namespace Headquarters4DCS.Template
{
    public sealed class MissionTemplateNodeLand : MissionTemplateNode
    {
        private DefinitionTheaterNodeLand LocationDefinition { get { return (DefinitionTheaterNodeLand)Definition; } }

        [Category("Information"), DisplayName("(Name)")]
        public string Name { get { return LocationDefinition.Name; } }

        //[Category("Settings"), DisplayName("Features")]
        //[TypeConverter(typeof(ArrayTypeConverter<string>)), TheaterNodeType(TheaterNodeType.Land)]
        //[Editor(typeof(UITypeEditorNodeFeatures), typeof(UITypeEditor))]
        //public string[] Features { get; set; } = new string[0];

        public MissionTemplateNodeLand(DefinitionTheaterNode nodeDefinition) : base(nodeDefinition) { }
    }
}

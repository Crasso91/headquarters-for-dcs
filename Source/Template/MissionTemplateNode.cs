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
using System.Linq;

namespace Headquarters4DCS.Template
{
    public abstract class MissionTemplateNode
    {
        protected string INISection { get { return $"node_{Definition.ID.ToLowerInvariant()}"; } }

        public readonly DefinitionTheaterNode Definition;

        public virtual bool InUse { get { return (Features.Length > 0) || (PlayerFlightGroups.Length > 0); } }

        public string[] Features { get; set; } = new string[0];
        public MissionTemplatePlayerFlightGroup[] PlayerFlightGroups { get; set; } = new MissionTemplatePlayerFlightGroup[0];

        public MissionTemplateNode(DefinitionTheaterNode nodeDefinition)
        {
            Definition = nodeDefinition;
            Clear();
        }

        protected virtual void Clear()
        {
            Features = new string[0];
            PlayerFlightGroups = new MissionTemplatePlayerFlightGroup[0];
        }

        public virtual void LoadFromFile(INIFile ini)
        {
            Clear();

            Features = ini.GetValueArray<string>(INISection, "Features", '|');

            PlayerFlightGroups = new MissionTemplatePlayerFlightGroup[Math.Max(0, ini.GetValue<int>(INISection, "FlightGroupsCount"))];
            for (int i = 0; i < PlayerFlightGroups.Length; i++)
                PlayerFlightGroups[i] = new MissionTemplatePlayerFlightGroup(ini, INISection, i);
        }

        public virtual void SaveToFile(INIFile ini)
        {
            if (Features.Length > 0)
                ini.SetValueArray(INISection, "Features", Features, '|');

            if (PlayerFlightGroups.Length > 0)
            {
                ini.SetValue(INISection, "FlightGroupsCount", PlayerFlightGroups.Length);

                for (int i = 0; i < PlayerFlightGroups.Length; i++)
                    PlayerFlightGroups[i].SaveToFile(ini, INISection, i);
            }
        }

        public DefinitionFeature[] GetFeaturesDefinitions()
        {
            return
                (from f in
                     (from string fID in Features
                      select HQLibrary.Instance.GetDefinition<DefinitionFeature>(fID))
                 where f != null select f).OrderBy(x => x).ToArray();
        }

        public virtual string GetInformationString()
        {
            string infoString = "";

            infoString += string.Join(", ", (from DefinitionFeature featureDef in GetFeaturesDefinitions() select featureDef.DisplayName).ToArray());

            return infoString;
        }
    }
}

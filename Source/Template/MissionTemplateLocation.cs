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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Headquarters4DCS.Template
{
    /// <summary>
    /// A mission template location. Stores all mission features, player flight groups... assigned to this location.
    /// </summary>
    public sealed class MissionTemplateLocation
    {
        /// <summary>
        /// The ini section of the HQT file to load from/save to.
        /// </summary>
        private string INISection { get { return $"location_{Definition.ID.ToLowerInvariant()}"; } }

        /// <summary>
        /// Definition of this location in the library.
        /// </summary>
        public readonly DefinitionTheaterLocation Definition;

        /// <summary>
        /// Coalition this location belongs to (if applicable).
        /// </summary>
        public CoalitionNeutral Coalition { get; set; } = CoalitionNeutral.Neutral;

        /// <summary>
        /// Is this location in use (are there mission features or flight groups located here)?
        /// </summary>
        public bool InUse { get { return (Features.Count > 0) || (PlayerFlightGroups.Count > 0); } }

        /// <summary>
        /// Mission features assigned to this location.
        /// </summary>
        public List<string> Features { get; set; } = new List<string>();

        /// <summary>
        /// Player flight groups starting on this location.
        /// </summary>
        public List<MissionTemplatePlayerFlightGroup> PlayerFlightGroups { get; set; } = new List<MissionTemplatePlayerFlightGroup>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locationDefinition">The definition of the location in the library's theater definition.</param>
        public MissionTemplateLocation(DefinitionTheaterLocation locationDefinition)
        {
            Definition = locationDefinition;
            Clear();
        }

        /// <summary>
        /// Resets this location to default settings, remove all flight groups and features.
        /// </summary>
        private void Clear()
        {
            Coalition = Definition.Coalition;
            Features.Clear();
            PlayerFlightGroups.Clear();
        }

        /// <summary>
        /// Loads this location from an HQT ini file.
        /// </summary>
        /// <param name="ini">Ini file to load from.</param>
        public void LoadFromFile(INIFile ini)
        {
            Clear();

            Features.AddRange(ini.GetValueArray<string>(INISection, "Features"));

            int pfgCount = Math.Max(0, ini.GetValue<int>(INISection, "PlayerFlightGroupsCount"));
            for (int i = 0; i < pfgCount; i++)
                PlayerFlightGroups.Add(new MissionTemplatePlayerFlightGroup(ini, INISection, i));
        }

        /// <summary>
        /// Saves this location to an HQT ini file.
        /// </summary>
        /// <param name="ini">Ini file to save to.</param>
        public void SaveToFile(INIFile ini)
        {
            if (Features.Count > 0)
                ini.SetValueArray(INISection, "Features", Features.ToArray());

            if (PlayerFlightGroups.Count > 0)
            {
                ini.SetValue(INISection, "PlayerFlightGroupsCount", PlayerFlightGroups.Count);

                for (int i = 0; i < PlayerFlightGroups.Count; i++)
                    PlayerFlightGroups[i].SaveToFile(ini, INISection, i);
            }
        }

        /// <summary>
        /// Returns the definitions of all mission features used by this location.
        /// </summary>
        /// <returns>An array of DefinitionFeature</returns>
        public DefinitionFeature[] GetFeaturesDefinitions()
        {
            return
                (from f in
                     (from string fID in Features
                      select Library.Instance.GetDefinition<DefinitionFeature>(fID))
                 where f != null select f).OrderBy(x => x).ToArray();
        }
    }
}

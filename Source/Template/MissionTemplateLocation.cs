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
        public bool InUse { get { return (Features.Length > 0) || (PlayerFlightGroups.Length > 0); } }

        /// <summary>
        /// Mission features assigned to this location.
        /// </summary>
        public string[] Features { get; set; } = new string[0];

        /// <summary>
        /// Player flight groups starting on this location.
        /// </summary>
        public MissionTemplatePlayerFlightGroup[] PlayerFlightGroups { get; set; } = new MissionTemplatePlayerFlightGroup[0];

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
            Features = new string[0];
            PlayerFlightGroups = new MissionTemplatePlayerFlightGroup[0];
        }

        /// <summary>
        /// Loads this location from an HQT ini file.
        /// </summary>
        /// <param name="ini">Ini file to load from.</param>
        public void LoadFromFile(INIFile ini)
        {
            Clear();

            Features = ini.GetValueArray<string>(INISection, "Features");

            PlayerFlightGroups = new MissionTemplatePlayerFlightGroup[Math.Max(0, ini.GetValue<int>(INISection, "FlightGroupsCount"))];
            for (int i = 0; i < PlayerFlightGroups.Length; i++)
                PlayerFlightGroups[i] = new MissionTemplatePlayerFlightGroup(ini, INISection, i);
        }

        /// <summary>
        /// Saves this location to an HQT ini file.
        /// </summary>
        /// <param name="ini">Ini file to save to.</param>
        public void SaveToFile(INIFile ini)
        {
            if (Features.Length > 0)
                ini.SetValueArray(INISection, "Features", Features);

            if (PlayerFlightGroups.Length > 0)
            {
                ini.SetValue(INISection, "FlightGroupsCount", PlayerFlightGroups.Length);

                for (int i = 0; i < PlayerFlightGroups.Length; i++)
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
                      select HQLibrary.Instance.GetDefinition<DefinitionFeature>(fID))
                 where f != null select f).OrderBy(x => x).ToArray();
        }
    }
}

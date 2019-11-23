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

using System;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Common HQ4DCS settings (default units types, etc.) loaded from Library/Settings.ini.
    /// </summary>
    public sealed class LibraryCommonSettings : IDisposable
    {
        /// <summary>
        /// Default blue coalition ID.
        /// </summary>
        public string DefaultCoalitionBlue { get; private set; }

        /// <summary>
        /// Default red coalition ID.
        /// </summary>
        public string DefaultCoalitionRed { get; private set; }

        /// <summary>
        /// Default language ID.
        /// </summary>
        public string DefaultLanguage { get; private set; }

        /// <summary>
        /// Default objective.
        /// </summary>
        public string DefaultObjective { get; private set; }

        /// <summary>
        /// Default player aircraft ID.
        /// </summary>
        public string DefaultPlayerAircraft { get; private set; }

        /// <summary>
        /// Default player flight group start location.
        /// </summary>
        public PlayerFlightGroupStartLocation DefaultPlayerFlightGroupStartLocation { get; private set; }

        /// <summary>
        /// Default theater ID.
        /// </summary>
        public string DefaultTheater { get; private set; }

        /// <summary>
        /// Shared OGG files automatically included in all missions.
        /// </summary>
        public string[] SharedOggFiles { get; private set; }

        /// <summary>
        /// Settings for various amount of air defense intensity.
        /// </summary>
        public LibraryCommonSettingsEnemyAirDefense[] AirDefense { get; private set; }

        /// <summary>
        /// Settings for min/max distance of short, medium and long range air defense units.
        /// </summary>
        public LibraryCommonSettingsEnemyAirDefenseDistance[] AirDefenseDistance { get; private set; }

        /// <summary>
        /// Min/max distance for various "distance to target" options.
        /// </summary>
        public LibraryCommonSettingsDistanceToObjective[] DistanceToObjective { get; private set; }

        /// <summary>
        /// How far should enemy combat air patrols be spawned from the objective and the players' initial location?
        /// </summary>
        public LibraryCommonSettingsEnemyAirDefenseDistance EnemyCAPDistance { get; private set; }

        /// <summary>
        /// How many times stronger/weaker should the enemy air force be, relative to the allied air force, for various settings of Enemy CAP
        /// </summary>
        public double[] EnemyCAPMultiplier { get; private set; }

        /// <summary>
        /// Constructor.
        /// Loads all data from Library\Settings.ini
        /// </summary>
        public LibraryCommonSettings()
        {
            int i;

            using (INIFile ini = new INIFile($"{HQTools.PATH_LIBRARY}Settings.ini"))
            {
                // Default values
                DefaultCoalitionBlue = ini.GetValue("Defaults", "Coalition.Blue", "USA");
                DefaultCoalitionRed = ini.GetValue("Defaults", "Coalition.Red", "Russia");
                DefaultLanguage = ini.GetValue("Defaults", "Language", "English");
                DefaultObjective = ini.GetValue("Defaults", "Objective", "CAP");
                DefaultPlayerAircraft = ini.GetValue("Defaults", "Aircraft.Player", "Su-25T");
                DefaultPlayerFlightGroupStartLocation = ini.GetValue("Defaults", "PlayerFlightGroup.StartLocation", PlayerFlightGroupStartLocation.FromParking);
                DefaultTheater = ini.GetValue("Defaults", "Theater", "Caucasus");

                // Common media files
                SharedOggFiles = ini.GetValueArray<string>("Shared", "OggFiles");

                AirDefense = new LibraryCommonSettingsEnemyAirDefense[HQTools.EnumCount<AmountNR>() - 1]; // -1 because we don't need "Random"
                for (i = 0; i < AirDefense.Length; i++)
                    AirDefense[i] = new LibraryCommonSettingsEnemyAirDefense(ini, "EnemyAirDefense", ((AmountNR)i).ToString());

                AirDefenseDistance = new LibraryCommonSettingsEnemyAirDefenseDistance[HQTools.EnumCount<AirDefenseRange>()];
                for (i = 0; i < AirDefenseDistance.Length; i++)
                    AirDefenseDistance[i] = new LibraryCommonSettingsEnemyAirDefenseDistance(ini, "EnemyAirDefenseDistance", ((AirDefenseRange)i).ToString());

                DistanceToObjective = new LibraryCommonSettingsDistanceToObjective[HQTools.EnumCount<AmountR>() - 1]; // -1 because we don't need "Random"
                for (i = 0; i < DistanceToObjective.Length; i++)
                    DistanceToObjective[i] = new LibraryCommonSettingsDistanceToObjective(ini, "DistanceToObjective", ((AmountR)i).ToString());

                EnemyCAPDistance = new LibraryCommonSettingsEnemyAirDefenseDistance(ini, "EnemyCombatAirPatrols", null);

                EnemyCAPMultiplier = new double[HQTools.EnumCount<AmountNR>() - 1]; // -1 because we don't need "Random"
                for (i = 0; i < EnemyCAPMultiplier.Length; i++)
                    EnemyCAPMultiplier[i] = Math.Max(0, ini.GetValue<int>("EnemyCombatAirPatrols", $"Multiplier.{((AmountNR)i).ToString()}")) / 100.0;
            }
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { }
    }
}


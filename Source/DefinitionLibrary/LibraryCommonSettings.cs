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
        /// Default player aircraft ID.
        /// </summary>
        public string DefaultPlayerAircraft { get; private set; }

        /// <summary>
        /// Default player flight group tasking.
        /// </summary>
        public PlayerFlightGroupTask DefaultPlayerFlightGroupTask { get; private set; }

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
        /// Constructor.
        /// Loads all data from Library\Settings.ini
        /// </summary>
        public LibraryCommonSettings()
        {
            using (INIFile ini = new INIFile($"{HQTools.PATH_LIBRARY}Settings.ini"))
            {
                DefaultCoalitionBlue = ini.GetValue("Defaults", "Coalition.Blue", "USA");
                DefaultCoalitionRed = ini.GetValue("Defaults", "Coalition.Red", "Russia");
                DefaultPlayerAircraft = ini.GetValue("Defaults", "Aircraft.Player", "Su-25T");
                DefaultPlayerFlightGroupTask = ini.GetValue("Defaults", "PlayerFlightGroup.Task", PlayerFlightGroupTask.CAP);
                DefaultPlayerFlightGroupStartLocation = ini.GetValue("Defaults", "PlayerFlightGroup.StartLocation", PlayerFlightGroupStartLocation.FromParking);
                DefaultLanguage = ini.GetValue("Defaults", "Language", "English");
                DefaultTheater = ini.GetValue("Defaults", "Theater", "Caucasus");

                SharedOggFiles = ini.GetValueArray<string>("Shared", "OggFiles");
            }
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { }
    }
}


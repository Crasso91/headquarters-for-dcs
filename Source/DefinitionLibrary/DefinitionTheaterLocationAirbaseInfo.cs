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

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Information about an airbase.
    /// </summary>
    public struct DefinitionTheaterLocationAirbaseInfo
    {
        /// <summary>
        /// The name of the airdrome.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Array of valid ATC frequencies.
        /// </summary>
        public readonly float[] ATC;

        /// <summary>
        /// ILS frequency (null or empty if none).
        /// </summary>
        public readonly string[] ILS;

        /// <summary>
        /// TACAN frequency (null or empty if none).
        /// </summary>
        public readonly string[] TACAN;

        /// <summary>
        /// Internal ID of this airdrome in DCS World.
        /// </summary>
        public readonly int ID;

        /// <summary>
        /// Number of parking spots on this airdrome.
        /// </summary>
        public readonly int ParkingSpots;

        /// <summary>
        /// A list of runway orientations.
        /// </summary>
        public readonly int[] Runways;

        /// <summary>
        /// Is this airdrome a military airdrome?
        /// </summary>
        public readonly bool IsMilitary;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ini">The .ini file to load airbase data from.</param>
        public DefinitionTheaterLocationAirbaseInfo(INIFile ini)
        {
            ATC = ini.GetValueArray<float>("Airbase", "ATC");
            ID = ini.GetValue<int>("Airbase", "ID");
            ILS = ini.GetValueArray<string>("Airbase", "ILS");
            IsMilitary = ini.GetValue<bool>("Airbase", "Military");
            Name = ini.GetValue<string>("Airbase", "Name");
            ParkingSpots = ini.GetValue<int>("Airbase", "ParkingSpots");
            Runways = ini.GetValueArray<int>("Airbase", "Runways");
            TACAN = ini.GetValueArray<string>("Airbase", "TACAN");
        }
    }
}

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
    public struct DefinitionTheaterAirbase
    {
        /// <summary>
        /// The name of the airbase.
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
        /// Internal ID of this airbase in DCS World.
        /// </summary>
        public readonly int ID;

        /// <summary>
        /// Number of parking spots on this airbase.
        /// </summary>
        public readonly int ParkingSpots;

        /// <summary>
        /// A list of runway orientations.
        /// </summary>
        public readonly int[] Runways;

        /// <summary>
        /// Is this airbase a military airbase?
        /// </summary>
        public readonly bool IsMilitary;

        /// <summary>
        /// In which country is this airbase located?
        /// </summary>
        public readonly DCSCountry Country;

        /// <summary>
        /// Which coalition this airbase belongs to?
        /// </summary>
        public readonly Coalition Coalition;

        /// <summary>
        /// Map X,Y coordinates of this airbase
        /// </summary>
        public readonly Coordinates Coordinates;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ini">The .ini file to load airbase data from.</param>
        /// <param name="airbaseKey">The top-level key (airbase unique ID)</param>
        public DefinitionTheaterAirbase(INIFile ini, string airbaseKey)
        {
            ATC = ini.GetValueArray<float>("Airbase", $"{airbaseKey}.ATC");
            ID = ini.GetValue<int>("Airbase", $"{airbaseKey}.ID");
            ILS = ini.GetValueArray<string>("Airbase", $"{airbaseKey}.ILS");
            Coordinates = ini.GetValue<Coordinates>("Airbase", $"{airbaseKey}.Coordinates");
            IsMilitary = ini.GetValue<bool>("Airbase", $"{airbaseKey}.Military");
            Name = ini.GetValue<string>("Airbase", $"{airbaseKey}.Name");
            ParkingSpots = ini.GetValue<int>("Airbase", $"{airbaseKey}.ParkingSpots");
            Runways = ini.GetValueArray<int>("Airbase", $"{airbaseKey}.Runways");
            TACAN = ini.GetValueArray<string>("Airbase", $"{airbaseKey}.TACAN");
            Country = ini.GetValue<DCSCountry>("Airbase", $"{airbaseKey}.Country");
            Coalition = ini.GetValue<Coalition>("Airbase", $"{airbaseKey}.Coalition");
        }
    }
}

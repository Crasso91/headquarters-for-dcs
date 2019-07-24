///*
//==========================================================================
//This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
//Eagle Dynamics' DCS World flight simulator.

//HQ4DCS was created by Ambroise Garel (@akaAgar).
//You can find more information about the project on its GitHub page,
//https://akaAgar.github.io/headquarters-for-dcs

//HQ4DCS is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//HQ4DCS is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with HQ4DCS. If not, see https://www.gnu.org/licenses/
//==========================================================================
//*/

//using Headquarters4DCS.Enums;

//namespace Headquarters4DCS.Library
//{
//    /// <summary>
//    /// Information about an airdrome.
//    /// </summary>
//    public struct DefinitionTheaterAirbase
//    {
//        /// <summary>
//        /// The name of the airdrome.
//        /// </summary>
//        public readonly string Name;

//        /// <summary>
//        /// Array of valid ATC frequencies.
//        /// </summary>
//        public readonly float ATC;

//        /// <summary>
//        /// ILS frequency (null or empty if none).
//        /// </summary>
//        public readonly string ILS;

//        /// <summary>
//        /// TACAN frequency (null or empty if none).
//        /// </summary>
//        public readonly string TACAN;

//        /// <summary>
//        /// Which coalition this airdrome belong to?
//        /// </summary>
//        public readonly Coalition Coalition;

//        /// <summary>
//        /// Coordinates of the airdrome (center of the runway) on the map.
//        /// </summary>
//        public readonly Coordinates Coordinates;

//        /// <summary>
//        /// Internal ID of this airdrome in DCS World.
//        /// </summary>
//        public readonly int DCSID;

//        /// <summary>
//        /// Number of parking spots on this airdrome.
//        /// </summary>
//        public readonly int ParkingSpots;

//        /// <summary>
//        /// A list of runway orientations.
//        /// </summary>
//        public readonly int[] Runways;

//        /// <summary>
//        /// Is this airdrome a military airdrome?
//        /// </summary>
//        public readonly bool IsMilitary;

//        /// <summary>
//        /// Is this airdrome near water? (closer than +/- 15 nm)
//        /// </summary>
//        public readonly bool IsNearWater;

//        /// <summary>
//        /// Constructor. Loads data from the theater ini file.
//        /// </summary>
//        /// <param name="ini">The ini file to load from</param>
//        /// <param name="key">Top level ini key to use.</param>
//        public DefinitionTheaterAirbase(INIFile ini, string key)
//        {
//            ATC = ini.GetValue<float>("Airbases", key + ".ATC");
//            Coalition = ini.GetValue<Coalition>("Airbases", key + ".Coalition");
//            Coordinates = ini.GetValue<Coordinates>("Airbases", key + ".Coordinates");
//            DCSID = ini.GetValue<int>("Airbases", key + ".DCSID");
//            ILS = ini.GetValue<string>("Airbases", key + ".ILS");
//            IsMilitary = ini.GetValue<bool>("Airbases", key + ".Military");
//            IsNearWater = ini.GetValue<bool>("Airbases", key + ".NearWater");
//            Name = ini.GetValue<string>("Airbases", key + ".Name");
//            ParkingSpots = ini.GetValue<int>("Airbases", key + ".ParkingSpots");
//            Runways = ini.GetValueArray<int>("Airbases", key + ".Runways");
//            TACAN = ini.GetValue<string>("Airbases", key + ".TACAN");
//        }

//        /// <summary>
//        /// ToString() override. Returns essential information about the airdrome (name and ID) formatted in a string.
//        /// </summary>
//        /// <returns>A string.</returns>
//        public override string ToString() { return $"{Name} (ID: {DCSID})"; }
//    }
//}

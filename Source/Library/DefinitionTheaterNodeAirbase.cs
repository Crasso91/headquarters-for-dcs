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

using Headquarters4DCS.Enums;

namespace Headquarters4DCS.Library
{
    /// <summary>
    /// Information about an airdrome.
    /// </summary>
    public sealed class DefinitionTheaterNodeAirbase : DefinitionTheaterNode
    {
        /// <summary>
        /// The name of the airdrome.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Array of valid ATC frequencies.
        /// </summary>
        public float ATC { get; private set; }

        /// <summary>
        /// ILS frequency (null or empty if none).
        /// </summary>
        public string ILS { get; private set; }

        /// <summary>
        /// TACAN frequency (null or empty if none).
        /// </summary>
        public string TACAN { get; private set; }

        /// <summary>
        /// Which coalition this airdrome belong to?
        /// </summary>
        public Coalition Coalition { get; private set; }

        /// <summary>
        /// Country in which this airbase is located.
        /// </summary>
        public Country Country { get; private set; }

        /// <summary>
        /// Coordinates of the airdrome (center of the runway) on the map.
        /// </summary>
        public Coordinates Coordinates { get; private set; }

        /// <summary>
        /// Internal ID of this airdrome in DCS World.
        /// </summary>
        public int DCSID { get; private set; }

        /// <summary>
        /// Number of parking spots on this airdrome.
        /// </summary>
        public int ParkingSpots { get; private set; }

        /// <summary>
        /// A list of runway orientations.
        /// </summary>
        public int[] Runways { get; private set; }

        /// <summary>
        /// Is this airdrome a military airdrome?
        /// </summary>
        public bool IsMilitary { get; private set; }

        public DefinitionTheaterNodeAirbase(string id, string iniFilePath) : base(id, iniFilePath) { }

        protected override void LoadNodeData(INIFile ini)
        {
            ATC = ini.GetValue<float>("Node", "ATC");
            Coalition = ini.GetValue<Coalition>("Node", "Coalition");
            Country = ini.GetValue<Country>("Node", "Country");
            Coordinates = ini.GetValue<Coordinates>("Node", "Coordinates");
            DCSID = ini.GetValue<int>("Node", "DCSID");
            ILS = ini.GetValue<string>("Node", "ILS");
            IsMilitary = ini.GetValue<bool>("Node", "Military");
            Name = ini.GetValue<string>("Node", "Name");
            ParkingSpots = ini.GetValue<int>("Node", "ParkingSpots");
            Runways = ini.GetValueArray<int>("Node", "Runways");
            TACAN = ini.GetValue<string>("Node", "TACAN");
        }

        ///// <summary>
        ///// Constructor. Loads data from the theater ini file.
        ///// </summary>
        ///// <param name="ini">The ini file to load from</param>
        //public DefinitionTheaterNodeAirbase(string id, string iniFilePath)
        //{
        //    ID = id;
        //    using (INIFile ini = new INIFile(iniFilePath))
        //    {
        //        ATC = ini.GetValue<float>("Airbase", "ATC");
        //        Coalition = ini.GetValue<Coalition>("Airbase", "Coalition");
        //        Coordinates = ini.GetValue<Coordinates>("Airbase", "Coordinates");
        //        DCSID = ini.GetValue<int>("Airbase", "DCSID");
        //        ILS = ini.GetValue<string>("Airbase", "ILS");
        //        IsMilitary = ini.GetValue<bool>("Airbase", "Military");
        //        IsNearWater = ini.GetValue<bool>("Airbase", "NearWater");
        //        MapPosition = ini.GetValue<Coordinates>("Airbase", "MapPosition");
        //        Name = ini.GetValue<string>("Airbase", "Name");
        //        ParkingSpots = ini.GetValue<int>("Airbase", "ParkingSpots");
        //        Runways = ini.GetValueArray<int>("Airbase", "Runways");
        //        TACAN = ini.GetValue<string>("Airbase", "TACAN");
        //    }
        //}

        ///// <summary>
        ///// ToString() override. Returns essential information about the airdrome (name and ID) formatted in a string.
        ///// </summary>
        ///// <returns>A string.</returns>
        //public override string ToString() { return $"{Name} (ID: {DCSID})"; }
    }
}

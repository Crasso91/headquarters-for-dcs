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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// The definition of a DCS World theater.
    /// </summary>
    public sealed class DefinitionTheater : Definition
    {
        /// <summary>
        /// How many times should SelectNodesInRadius() expand its search radius when no nodes are found?
        /// </summary>
        private const int MAX_RADIUS_SEARCH_ITERATIONS = 32;

        /// <summary>
        /// The public ID of the theater in DCS World.
        /// </summary>
        public string DCSID { get; private set; }

        /// <summary>
        /// The default coordinates of the map center.
        /// </summary>
        public Coordinates DefaultMapCenter { get; private set; }

        /// <summary>
        /// Required DCS World modules.
        /// </summary>
        public string[] RequiredModules { get; private set; }

        /// <summary>
        /// Magnetic declination from true north.
        /// </summary>
        public float MagneticDeclination { get; private set; }

        /// <summary>
        /// Sunrise and sunset time (in minutes) for each month (January is 0, December is 11)
        /// </summary>
        public MinMaxI[] DayTime { get; private set; }

        /// <summary>
        /// Min and max temperature (in degrees Celsius) for each month (January is 0, December is 11)
        /// </summary>
        public MinMaxI[] Temperature { get; private set; }

        /// <summary>
        /// Weather parameters for all "weather quality" settings. 11 values, from clear (0) to storm (10).
        /// </summary>
        public DefinitionTheaterWeather[] Weather { get; private set; }

        /// <summary>
        /// Wind parameters for all "wind speed" settings. 11 values, from clear (0) to storm (10).
        /// </summary>
        public DefinitionTheaterWind[] Wind { get; private set; }

        /// <summary>
        /// All airdromes in this theater.
        /// </summary>
        public DefinitionTheaterAirbase[] Airbases { get; private set; }

        /// <summary>
        /// All spawn points in this theater.
        /// </summary>
        public DefinitionTheaterSpawnPoint[] SpawnPoints { get; private set; }

        /// <summary>
        /// Loads data required by this definition.
        /// </summary>
        /// <param name="path">Path to definition file or directory.</param>
        /// <returns>True is successful, false if an error happened.</returns>
        protected override bool OnLoad(string path)
        {
            int i;

            // No common settings? No map? No theater.
            //if (!File.Exists(path + "CommonSettings.ini") || !File.Exists(path + "GUIMap.jpg"))
            //    return false;

            // Load common settings
            using (INIFile ini = new INIFile(path))
            {
                // [Theater] section
                DCSID = ini.GetValue<string>("Theater", "DCSID");
                DefaultMapCenter = ini.GetValue<Coordinates>("Theater", "DefaultMapCenter");
                RequiredModules = ini.GetValueArray<string>("Theater", "RequiredModules");
                MagneticDeclination = ini.GetValue<float>("Theater", "MagneticDeclination");

                // [Daytime] section
                DayTime = new MinMaxI[12];
                for (i = 0; i < 12; i++)
                    DayTime[i] = ini.GetValue<MinMaxI>("Daytime", ((Month)i).ToString());

                // [Temperature] section
                Temperature = new MinMaxI[12];
                for (i = 0; i < 12; i++)
                    Temperature[i] = ini.GetValue<MinMaxI>("Temperature", ((Month)i).ToString());

                // [Weather] section
                Weather = new DefinitionTheaterWeather[HQTools.EnumCount<Weather>() - 1]; // -1 because we don't want "Random"
                for (i = 0; i < Weather.Length; i++)
                    Weather[i] = new DefinitionTheaterWeather(ini, ((Weather)i).ToString());

                // [Wind] section
                Wind = new DefinitionTheaterWind[HQTools.EnumCount<Wind>() - 1]; // -1 because we don't want "Auto"
                for (i = 0; i < Wind.Length; i++)
                    Wind[i] = new DefinitionTheaterWind(ini, ((Wind)i).ToString());

                // [Airbases] section
                Airbases = new DefinitionTheaterAirbase[ini.GetTopLevelKeysInSection("Airbases").Length];
                i = 0;
                foreach (string k in ini.GetTopLevelKeysInSection("Airbases"))
                { Airbases[i] = new DefinitionTheaterAirbase(ini, k); i++; }

                // [SpawnPoints] section
                SpawnPoints = new DefinitionTheaterSpawnPoint[ini.GetKeysInSection("SpawnPoints").Length];
                i = 0;
                foreach (string k in ini.GetKeysInSection("SpawnPoints"))
                { SpawnPoints[i] = new DefinitionTheaterSpawnPoint(ini, k); i++; }
            }

            //// Location files
            //// ------------------
            //Locations = new Dictionary<string, DefinitionTheaterLocation>(StringComparer.InvariantCultureIgnoreCase);
            //foreach (string f in Directory.GetFiles(path, "Location_*.ini"))
            //{
            //    string k = DefinitionTheaterLocation.GetLocationIDFromINIFileName(f);
            //    if (string.IsNullOrEmpty(k) || Locations.ContainsKey(k)) continue;
            //    Locations.Add(k, new DefinitionTheaterLocation(f));
            //}

            return true;
        }

        public DefinitionTheaterSpawnPoint? GetRandomSpawnPoint(
            TheaterLocationSpawnPointType[] validTypes = null, DCSCountry[] validCountries = null,
            MinMaxD? distanceFrom = null, Coordinates? distanceOrigin = null,
            IEnumerable<string> invalidIDs = null)
        {
            IEnumerable<DefinitionTheaterSpawnPoint> validSP = (from DefinitionTheaterSpawnPoint s in SpawnPoints select s);

            if (invalidIDs != null)
                validSP = (from DefinitionTheaterSpawnPoint s in validSP where !invalidIDs.Contains(s.UniqueID) select s);

            if (validTypes != null)
                validSP = (from DefinitionTheaterSpawnPoint s in validSP where validTypes.Contains(s.PointType) select s);

            if (validCountries != null)
                validSP = (from DefinitionTheaterSpawnPoint s in validSP where validCountries.Contains(s.Country) select s);

            if (distanceFrom.HasValue && distanceOrigin.HasValue)
            {
                if (validSP.Count() == 0) return null;

                MinMaxD searchRange = distanceFrom.Value;

                IEnumerable<DefinitionTheaterSpawnPoint> validSPInRange = (from DefinitionTheaterSpawnPoint s in validSP select s);

                do
                {
                    validSPInRange = (from DefinitionTheaterSpawnPoint s in validSP where searchRange.Contains(distanceOrigin.Value.GetDistanceFrom(s.Position)) select s);
                    searchRange = new MinMaxD(searchRange.Min * 0.9, searchRange.Max * 1.1);
                } while (validSPInRange.Count() == 0);

                validSP = (from DefinitionTheaterSpawnPoint s in validSPInRange select s);
            }

            if (validSP.Count() == 0) return null;
            return HQTools.RandomFrom(validSP.ToArray());
        }
    }
}

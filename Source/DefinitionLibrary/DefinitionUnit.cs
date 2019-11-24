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

using System.Collections.Generic;
using System.Linq;

namespace Headquarters4DCS.DefinitionLibrary
{
    public sealed class DefinitionUnit : Definition
    {
        /// <summary>
        /// Maximum number of pylons per aircraft.
        /// </summary>
        private const int MAX_PYLONS = 24;

        /// <summary>
        /// Default radio frequency when none is provided.
        /// </summary>
        private const float DEFAULT_RADIO_FREQUENCY = 127.5f;

        /// <summary>
        /// Default plane cruise speed for (in m/s).
        /// </summary>
        private const double DEFAULT_CRUISE_SPEED_PLANE = 130;

        /// <summary>
        /// Default helicopter cruise speed (in m/s).
        /// </summary>
        private const double DEFAULT_CRUISE_SPEED_HELICOPTER = 50;

        /// <summary>
        /// Default plane cruise altitude (in meters).
        /// </summary>
        private const double DEFAULT_CRUISE_ALTITUDE_PLANE = 6000;

        /// <summary>
        /// Default helicopter cruise altitude (in meters).
        /// </summary>
        private const double DEFAULT_CRUISE_ALTITUDE_HELICOPTER = 350;

        /// <summary>
        /// The public ID of this unit in DCS World.
        /// </summary>
        public string DCSID { get; private set; } = "";

        /// <summary>
        /// The category this unit belongs to (vehicle, helicopter, plane, static or ship). Not loaded from the file but generated from Families.
        /// </summary>
        public UnitCategory Category { get; private set; } = UnitCategory.Vehicle;

        /// <summary>
        /// The unit families this unit belongs to (HelicopterTransport, PlaneInterceptor, ShipFrigate, StaticStructureProduction, VehicleArtillery...)
        /// </summary>
        public UnitFamily[] Families { get; private set; } = new UnitFamily[] { UnitFamily.VehicleTransport };

        /// <summary>
        /// Min/max decade during which this unit is/was used. Can be overriden in coalition definitions.
        /// </summary>
        public TimePeriod[] InService { get; private set; } = new TimePeriod[] { TimePeriod.Decade1940, TimePeriod.Decade2010 };

        /// <summary>
        /// Extra Lua code for this unit.
        /// </summary>
        public string ExtraLua { get; private set; } = "";

        /// <summary>
        /// Default radio frequency for this unit.
        /// </summary>
        public float CommsRadioFrequency { get; private set; } = DEFAULT_RADIO_FREQUENCY;

        /// <summary>
        /// TACAN channel. For tankers and carriers.
        /// </summary>
        public int CommsTACANChannel { get; private set; } = 0;

        /// <summary>
        /// TACAN channel mode (X or Y). For tankers and carriers.
        /// </summary>
        public string CommsTACANChannelMode { get; private set; } = "X";

        /// <summary>
        /// TACAN callsign. For tankers and carriers.
        /// </summary>
        public string CommsTACANCallsign { get; private set; } = "TRK";

        /// <summary>
        /// ILS channel. For carriers.
        /// </summary>
        public int CommsILSChannel { get; private set; } = 0;

        /// <summary>
        /// Default DCS World task for this aircraft.
        /// </summary>
        public DCSFlightGroupTask AircraftDefaultTask { get; private set; } = DCSFlightGroupTask.CAP;

        /// <summary>
        /// Air-to-air power rating of this aircraft. Value #0 is "when carrying an A2A payload", value #1 is "when carrying an A2G payload".
        /// </summary>
        public int[] AircraftAirToAirRating { get; private set; } = new int[] { 0, 0 };

        /// <summary>
        /// Default cruise altitude for this aircraft (in meters, converter from the .ini value in feet).
        /// </summary>
        public double AircraftCruiseAltitude { get; private set; } = DEFAULT_CRUISE_ALTITUDE_PLANE;

        /// <summary>
        /// Default cruise speed for this aircraft (in meters/second, converter from the .ini value in knots).
        /// </summary>
        public double AircraftCruiseSpeed { get; private set; } = DEFAULT_CRUISE_SPEED_PLANE;

        /// <summary>
        /// Type of carrier ship types this aircraft can take off from.
        /// </summary>
        public CarrierGroupShipType[] AircraftCarrierShipType { get; private set; } = new CarrierGroupShipType[0];

        /// <summary>
        /// Is this aircraft player controllable?
        /// </summary>
        public bool AircraftPlayerControllable { get; private set; } = false;

        /// <summary>
        /// Names of modules required to fly this aircraft. Multiple names can be provided because some aircraft appear in multiple modules (like Flaming Cliff planes)
        /// </summary>
        public string[] AircraftPlayerControllableModules { get; private set; } = new string[0];

        /// <summary>
        /// Default common payload (mostly fuel, gun, chaff and flares). Will be copy-pasted in the flight group Lua table in the mission Lua file.
        /// </summary>
        public string AircraftPayloadCommon { get; private set; } = "";

        /// <summary>
        /// An array of valid payload types for this aircraft.
        /// </summary>
        public AircraftPayloadType[] AircraftAvailablePayloads { get; private set; } = new AircraftPayloadType[0];

        /// <summary>
        /// An array of pylons CLSID for the various payloads available to this aircraft.
        /// </summary>
        public string[,] AircraftPayloadPylons { get; private set; } = new string[HQTools.EnumCount<AircraftPayloadType>(), MAX_PYLONS];

        // TODO
        // public Country[][] LiveriesPriority { get; private set; } = new Country[2][];
        // public string[][] Liveries { get; private set; } = new string[HQTools.EnumCount<Country>()][];

        /// <summary>
        /// Loads data required by this definition.
        /// </summary>
        /// <param name="ini">The ini file to load from.</param>
        /// <returns>True is successful, false if an error happened.</returns>
        protected override bool OnLoad(INIFile ini)
        {
            // --------------
            // [Unit] section
            // --------------
            ID = ini.GetValue<string>("Unit", "ID");
            if (string.IsNullOrEmpty(ID)) return false;

            DCSID = ini.GetValue<string>("Unit", "DCSID");
            if (string.IsNullOrEmpty(DCSID)) DCSID = ID;

            DisplayName = ini.GetValue<string>("Unit", "DisplayName");

            Families = ini.GetValueArray<UnitFamily>("Unit", "Families"); if (Families.Length == 0) return false;

            Category = HQTools.GetUnitCategoryFromUnitFamily(Families[0]);
            // All families must belong to the same category. If that's not the case, remove all "wrong" families.
            Families = (from UnitFamily f in Families where f.ToString().StartsWith(Category.ToString()) select f).ToArray();
            if (Families.Length == 0) return false;

            InService = HQTools.ParseEnumString<TimePeriod>(ini.GetValue<string>("Unit", "InService"), '-', "Decade");
            if (InService.Length < 2) return false;
            InService = InService.Take(2).OrderBy(x => x).ToArray();

            ExtraLua = ini.GetValue<string>("Unit", "ExtraLua");

            // ---------------
            // [Comms] section
            // ---------------
            CommsRadioFrequency = ini.GetValue<float>("Comms", "RadioFrequency");
            CommsRadioFrequency = (CommsRadioFrequency <= 0) ? DEFAULT_RADIO_FREQUENCY : CommsRadioFrequency;

            CommsTACANChannel = HQTools.Clamp(ini.GetValue<int>("Comms", "TACAN.Channel"), 0, 99);
            CommsTACANChannelMode = ini.GetValue<string>("Comms", "TACAN.ChannelMode").ToUpperInvariant();
            CommsTACANChannelMode = (CommsTACANChannelMode == "Y") ? "Y" : "X";
            CommsTACANCallsign = ini.GetValue<string>("Comms", "TACAN.Callsign").ToUpperInvariant();

            CommsILSChannel = HQTools.Clamp(ini.GetValue<int>("Comms", "ILS.Channel"), 0, 99);

            // ----------------------------------------------------
            // [Aircraft] section (only for planes and helicopters)
            // ----------------------------------------------------
            if ((Category == UnitCategory.Helicopter) || (Category == UnitCategory.Plane))
            {
                AircraftDefaultTask = ini.GetValue<DCSFlightGroupTask>("Aircraft", "DefaultTask");
                AircraftAirToAirRating[0] = ini.GetValue<int>("Aircraft", "AirToAirRating.A2APayload");
                AircraftAirToAirRating[1] = ini.GetValue<int>("Aircraft", "AirToAirRating.A2GPayload");
                AircraftCruiseAltitude = ini.GetValue<int>("Aircraft", "CruiseAltitude") * HQTools.FEET_TO_METERS;
                if (AircraftCruiseAltitude <= 0) AircraftCruiseAltitude = Category == UnitCategory.Helicopter ? 350 : 6000;
                AircraftCruiseSpeed = ini.GetValue<int>("Aircraft", "CruiseSpeed") * HQTools.KNOTS_TO_METERSPERSECOND;
                if (AircraftCruiseSpeed <= 0) AircraftCruiseSpeed = Category == UnitCategory.Helicopter ? 50 : 130;
                AircraftPlayerControllable = ini.GetValue<bool>("Aircraft", "PlayerControllable");
                AircraftCarrierShipType = ini.GetValueArray<CarrierGroupShipType>("Aircraft", "CarrierShipType");

                // -----------------------------------------------------------
                // [AircraftPayload] section (only for planes and helicopters)
                // -----------------------------------------------------------
                int i, j;

                AircraftPayloadCommon = ini.GetValue<string>("AircraftPayload", "Common");

                List<AircraftPayloadType> payloadsList = new List<AircraftPayloadType>();

                for (i = 0; i < AircraftPayloadPylons.GetLength(0); i++)
                    for (j = 0; j < AircraftPayloadPylons.GetLength(1); j++)
                    {
                        AircraftPayloadPylons[i, j] = ini.GetValue<string>("AircraftPayload",
                            $"Pylons.{((AircraftPayloadType)i).ToString()}.Pylon{HQTools.ValToString(j + 1, "00")}");

                        // Each payload with at least one pylon not empty is a valid payload
                        if (!payloadsList.Contains((AircraftPayloadType)i) && !string.IsNullOrEmpty(AircraftPayloadPylons[i, j]))
                            payloadsList.Add((AircraftPayloadType)i);
                    }

                AircraftAvailablePayloads = payloadsList.ToArray();
            }

            // ------------------
            // [Liveries] section
            // ------------------
            // NEXT VERSION
            //LiveriesPriority[0] = ini.GetValueArray<Country>("Liveries", "Priority.Real");
            //LiveriesPriority[1] = ini.GetValueArray<Country>("Liveries", "Priority.Fictional");

            //Liveries = new string[HQTools.EnumCount<Country>()][];
            //for (int i = 0; i < Liveries.Length; i++)
            //    Liveries[i] = ini.GetValueArray<string>("Liveries", $"Country.{(Country)i}", '|');

            return true;
        }

        /// <summary>
        /// Returns a proper loadout for this aircraft.
        /// </summary>
        /// <param name="payloadType"></param>
        /// <returns></returns>
        public string GetPayloadLua(AircraftPayloadType payloadType)
        {
            if ((Category != UnitCategory.Helicopter) && (Category != UnitCategory.Plane)) return "{ }";
            if (AircraftAvailablePayloads.Length == 0) return "{ }"; // No payloads

            string payloadLua = "[\"pylons\"] = {\n";

            // No payload for this configuration, use the default payload instead
            if (!AircraftAvailablePayloads.Contains(payloadType)) payloadType = AircraftPayloadType.Default;

            for (int i = 0; i < MAX_PYLONS; i++)
            {
                string p = AircraftPayloadPylons[(int)payloadType, i].Trim();
                if (string.IsNullOrEmpty(p)) continue; // No payload on this pylon

                payloadLua +=
                    $"[{HQTools.ValToString(i + 1)}] = {{ [\"CLSID\"] = \"{p}\" }},\n";
            }

            payloadLua += "},\n";

            payloadLua += AircraftPayloadCommon.Replace(",", ",\n");

            // TODO: payload upgrades by decade

            return "{\n" + payloadLua + "\n}";
        }

        /// <summary>
        /// Returns a livery for this aircraft matching the countries of its coalition.
        /// </summary>
        /// <param name="coalitionCountries">The countries in the coalition this aircraft belongs to.</param>
        /// <returns>The name of the livery</returns>
        public string GetLiveryName(DCSCountry[] coalitionCountries)
        {
            // TODO

            return "default";
        }
    }
}

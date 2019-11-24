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

using System.Linq;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// The definition of a coalition, to be selected in the mission template.
    /// </summary>
    public sealed class DefinitionCoalition : Definition
    {
        ///// <summary>
        ///// The ID of the coalition's name in the language .ini file.
        ///// </summary>
        //public string Name { get; private set; } = "";

        /// <summary>
        /// Min/max time period during which this coalition can be used.
        /// </summary>
        public TimePeriod[] MinMaxTimePeriod { get; private set; } = new TimePeriod[] { TimePeriod.Decade1940, TimePeriod.Decade2010 };

        /// <summary>
        /// Unit system to use in briefings.
        /// </summary>
        public UnitSystem UnitSystem { get; private set; }

        /// <summary>
        /// The name of the DCS countries belonging to this coalition. During the mission, all units will belong to the first country in the list (the "primary" country). Others are just for show in the briefing and to know which liveries should be used.
        /// </summary>
        public DCSCountry[] Countries { get; private set; } = new DCSCountry[0];

        /// <summary>
        /// A list of units in use by this coalition.
        /// </summary>
        public string[] Units { get; private set; } = new string[0];

        /// <summary>
        /// A list of units this coalition should keep using even after their default "in service" timespan (e.g. MiG-21 for North Korea or F-14 for Iran)
        /// </summary>
        public string[] LegacyUnits { get; private set; } = new string[0];

        /// <summary>
        /// Should this coalition use NATO callsigns?
        /// </summary>
        public bool NATOCallsigns { get; private set; } = true;

        /// <summary>
        /// An array of the names of required DCS World modules.
        /// </summary>
        public string[] RequiredModules { get; private set; } = new string[0];

        /// <summary>
        /// Loads data required by this definition.
        /// </summary>
        /// <param name="ini">The ini file to load from.</param>
        /// <returns>True is successful, false if an error happened.</returns>
        protected override bool OnLoad(INIFile ini)
        {
            // -------------------
            // [Coalition] section
            // -------------------
            DisplayName = ini.GetValue<string>("Coalition", "DisplayName");

            MinMaxI timePeriodInteger = ini.GetValue<MinMaxI>("Coalition", "TimePeriod");
            MinMaxTimePeriod = new TimePeriod[] { (TimePeriod)timePeriodInteger.Min, (TimePeriod)timePeriodInteger.Max };

            NATOCallsigns = ini.GetValue<bool>("Coalition", "NATOCallsigns");
            UnitSystem = ini.GetValue<UnitSystem>("Coalition", "UnitSystem");
            if (UnitSystem == UnitSystem.ByCoalition) UnitSystem = UnitSystem.Metric;
            RequiredModules = ini.GetValueArray<string>("Coalition", "RequiredModules");
            Countries = ini.GetValueArray<DCSCountry>("Coalition", "Countries").Distinct().ToArray();
            if (Countries.Length == 0) return false; // No countries, bad coalition

            // ---------------
            // [Units] section
            // ---------------
            Units = ini.GetAllValuesInSectionAsStringArray("Units");

            // ---------------------
            // [LegacyUnits] section
            // ---------------------
            LegacyUnits = ini.GetAllValuesInSectionAsStringArray("LegacyUnits");

            return true;
        }

        /// <summary>
        /// Returns all IDs of units definitions of the desired families and time period, available to this coalition.
        /// </summary>
        /// <param name="library">The library from which to get unit data.</param>
        /// <param name="timePeriod">The time period during which units must be available.</param>
        /// <param name="family">The family the unit must belong to. If empty, all units for the time period will be returned.</param>
        /// <param name="extendSearchToUnitsOfOtherFamilies">If true and no unit of the matching family is found, extend search to "nearby" families (PlaneInterceptor for PlaneFlighter, etc.)</param>
        /// <param name="returnDefaultIfNoneFound">If true and no unit is found, return default unit. If false and no unit is found, return an empty array.</param>
        /// <returns>An array of DCS unit IDs.</returns>
        public string[] GetUnits(TimePeriod timePeriod, UnitFamily family, bool extendSearchToUnitsOfOtherFamilies, bool returnDefaultIfNoneFound)
        {
            // If extendSearchToUnitsOfOtherFamilies if true, get an array of families to search, by order of priority
            // else create an array with family as the only value.
            UnitFamily[] validFamilies = extendSearchToUnitsOfOtherFamilies ?
                GetExtendedFamiliesArray(family) : new UnitFamily[] { family };

            string[] validUnits = new string[0];

            // Search for units if all valid families, break as soon as at least one unit is found.
            for (int i = 0; i < validFamilies.Length; i++)
            {
                validUnits =
                    (from u in (from uID in Units select Library.Instance.GetDefinition<DefinitionUnit>(uID))
                     where u != null && u.Families.Contains(validFamilies[i]) &&
                     timePeriod >= u.InService[0] &&
                     (LegacyUnits.Contains(u.ID) || (timePeriod <= u.InService[1]))
                     select u.DCSID).Distinct().ToArray();

                if (validUnits.Length > 0) break;
            }

            // If no unit was found and returnDefaultIfNoneFound is true, return the default unit.
            if (returnDefaultIfNoneFound && (validUnits.Length == 0))
                // TODO: return new string[] { library.Common.DefautUnitForFamily[(int)family] };
                return new string[0];

            return validUnits;
        }

        /// <summary>
        /// Returns an array of replacement unit families to use if no unit was found in the required family.
        /// The first element of the array is always the initial family, as the array is meant to be searched from the first to the last index, stopping when at least one unit is found.
        /// </summary>
        /// <param name="family">The family in which no units might be found.</param>
        /// <returns>An array of UnitFamily.</returns>
        private UnitFamily[] GetExtendedFamiliesArray(UnitFamily family)
        {
            switch (family)
            {
                case UnitFamily.HelicopterRecon:
                    return new UnitFamily[] { UnitFamily.HelicopterRecon, UnitFamily.HelicopterAttack, UnitFamily.HelicopterUtility };
                case UnitFamily.HelicopterTransport:
                    return new UnitFamily[] { UnitFamily.HelicopterTransport, UnitFamily.HelicopterUtility };
                case UnitFamily.HelicopterUtility:
                    return new UnitFamily[] { UnitFamily.HelicopterUtility, UnitFamily.HelicopterTransport };

                case UnitFamily.PlaneAntiShip:
                    return new UnitFamily[] { UnitFamily.PlaneAntiShip, UnitFamily.PlaneStrike, UnitFamily.PlaneAttack, UnitFamily.PlaneSEAD };
                case UnitFamily.PlaneAttack:
                    return new UnitFamily[] { UnitFamily.PlaneAttack, UnitFamily.PlaneStrike, UnitFamily.PlaneSEAD };
                case UnitFamily.PlaneFighter:
                    return new UnitFamily[] { UnitFamily.PlaneFighter, UnitFamily.PlaneInterceptor };
                case UnitFamily.PlaneRecon:
                    return new UnitFamily[] { UnitFamily.PlaneRecon, UnitFamily.PlaneFighter, UnitFamily.PlaneInterceptor, UnitFamily.PlaneAttack, UnitFamily.PlaneStrike, UnitFamily.PlaneSEAD };
                case UnitFamily.PlaneSEAD:
                    return new UnitFamily[] { UnitFamily.PlaneSEAD, UnitFamily.PlaneStrike, UnitFamily.PlaneAttack };
                case UnitFamily.PlaneStrike:
                    return new UnitFamily[] { UnitFamily.PlaneStrike, UnitFamily.PlaneSEAD, UnitFamily.PlaneAttack };

                case UnitFamily.ShipAssault:
                    return new UnitFamily[] { UnitFamily.ShipAssault, UnitFamily.ShipFrigate };
                case UnitFamily.ShipCarrier:
                    return new UnitFamily[] { UnitFamily.ShipCarrier, UnitFamily.ShipCruiser };

                case UnitFamily.VehicleCommand:
                    return new UnitFamily[] { UnitFamily.VehicleCommand, UnitFamily.VehicleTransport };
                case UnitFamily.VehicleMissile:
                    return new UnitFamily[] { UnitFamily.VehicleMissile, UnitFamily.VehicleArtillery };
                case UnitFamily.VehicleSAMLong:
                    return new UnitFamily[] { UnitFamily.VehicleSAMLong, UnitFamily.VehicleSAMMedium, UnitFamily.VehicleSAMShort, UnitFamily.VehicleSAMShortIR };
                case UnitFamily.VehicleSAMMedium:
                    return new UnitFamily[] { UnitFamily.VehicleSAMMedium, UnitFamily.VehicleSAMShort, UnitFamily.VehicleSAMShortIR };
                case UnitFamily.VehicleSAMShort:
                    return new UnitFamily[] { UnitFamily.VehicleSAMShort, UnitFamily.VehicleSAMShortIR };
            }

            return new UnitFamily[] { family };
        }
    }
}

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

namespace Headquarters4DCS.Enums
{
    // A list of DCS World countries can be found here: https://wiki.hoggitworld.com/view/DCS_enum_country

    /// <summary>
    /// A country, for use in coalition definitions. Must match values used by DCS World.
    /// </summary>
    public enum Country
    {
        Russia = 0,
        Ukraine = 1,
        USA = 2,
        Turkey = 3,
        UK = 4,
        France = 5,
        Germany = 6,
        Aggressors = 7,
        Canada = 8,
        Spain = 9,
        TheNetherlands = 10,
        Belgium = 11,
        Norway = 12,
        Denmark = 13,
        // no number 14,
        Israel = 15,
        Georgia = 16,
        Insurgents = 17,
        Abkhazia = 18,
        SouthOsetia = 19,
        Italy = 20,
        Australia = 21,
        Switzerland = 22,
        Austria = 23,
        Belarus = 24,
        Bulgaria = 25,
        CzechRepublic = 26,
        China = 27,
        Croatia = 28,
        Egypt = 29,
        Finland = 30,
        Greece = 31,
        Hungary = 32,
        India = 33,
        Iran = 34,
        Iraq = 35,
        Japan = 36,
        Kazakhstan = 37,
        NorthKorea = 38,
        Pakistan = 39,
        Poland = 40,
        Romania = 41,
        SaudiArabia = 42,
        Serbia = 43,
        Slovakia = 44,
        SouthKorea = 45,
        Sweden = 46,
        Syria = 47,

        // Added around DCS 1.5.4
        Yemen = 48,
        Vietnam = 49,
        Venezuela = 50,
        Tunisia = 51,
        Thailand = 52,
        Sudan = 53,
        Philippines = 54,
        Morocco = 55,
        Mexico = 56,
        Malaysia = 57,
        Libya = 58,
        Jordan = 59,
        Indonesia = 60,
        Honduras = 61,
        Ethiopia = 62,
        Chile = 63,
        Brazil = 64,
        Bahrain = 65,

        // Added in DCS 2.1
        ThirdReich = 66,
        Yugoslavia = 67,
        USSR = 68,
        ItalianSocialRepublic = 69,

        // Added in DCS 2.5
        Algeria = 70,
        Kuwait = 71,
        Qatar = 72,
        Oman = 73,
        UnitedArabEmirates = 74,

        // Added around DCS 2.5.4
        SouthAfrica = 75,
        Cuba = 76
    }
}

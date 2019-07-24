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

namespace Headquarters4DCS.Library
{
    /// <summary>
    /// Stores information about wind conditions in a theater definition.
    /// </summary>
    public struct DefinitionTheaterWind
    {
        /// <summary>
        /// Min/max wind speed (in meters/second).
        /// </summary>
        public readonly MinMaxI Wind;

        /// <summary>
        /// Min/max turbulence (in meters/second).
        /// </summary>
        public readonly MinMaxI Turbulence;

        /// <summary>
        /// Constructor. Loads data from a theater definition .ini file.
        /// </summary>
        /// <param name="ini">The .ini file to load from.</param>
        /// <param name="key">The value key.</param>
        public DefinitionTheaterWind(INIFile ini, string key)
        {
            Wind = ini.GetValue<MinMaxI>("Wind", key + ".Wind");
            Turbulence = ini.GetValue<MinMaxI>("Wind", key + ".Turbulence");
        }
    }
}

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
    /// Stores information about weather conditions in a theater definition.
    /// </summary>
    public struct DefinitionTheaterWeather
    {
        /// <summary>
        /// Min,max base cloud height (in meters).
        /// </summary>
        public readonly MinMaxI CloudsBase;

        /// <summary>
        /// Min,max cloud density (0-10).
        /// </summary>
        public readonly MinMaxI CloudsDensity;

        /// <summary>
        /// Precipitation type. Be aware that DCS only allow some values is cloud density if high enough. Invalid values will be ignored by DCS.
        /// </summary>
        public readonly Precipitation[] CloudsPrecipitation;

        /// <summary>
        /// Min,max base cloud thickness in meters
        /// </summary>
        public readonly MinMaxI CloudsThickness;

        /// <summary>
        /// Are dust storm enabled. If multiple values can be provided, a random value will be selected(e.g. "true,true,false" gives 66% chance of duststorm).
        /// </summary>
        public readonly bool[] DustEnabled;

        /// <summary>
        /// Min,max dust storm density, if enabled (0-10).
        /// </summary>
        public readonly MinMaxI DustDensity;

        /// <summary>
        /// Is fog enabled. If multiple values can be provided, a random value will be selected(e.g. true,true,false gives 66% chance of fog).
        /// </summary>
        public readonly bool[] FogEnabled;

        /// <summary>
        /// Min,max fog thickness.
        /// </summary>
        public readonly MinMaxI FogThickness;

        /// <summary>
        /// Min,max fog visibility in meters.
        /// </summary>
        public readonly MinMaxI FogVisibility;

        /// <summary>
        /// Min,max atmospheric pressure at mean sea level.
        /// </summary>
        public readonly MinMaxI QNH;

        /// <summary>
        /// Min,max turbulence in meters/second.
        /// </summary>
        public readonly MinMaxI Turbulence;

        /// <summary>
        /// Min,max visibility in meters.
        /// </summary>
        public readonly MinMaxI Visibility;

        /// <summary>
        /// Constructor. Loads data from a theater definition .ini file.
        /// </summary>
        /// <param name="ini">The .ini file to load from.</param>
        /// <param name="key">The value key.</param>
        public DefinitionTheaterWeather(INIFile ini, string key)
        {
            CloudsBase = ini.GetValue<MinMaxI>("Weather", key + ".Clouds.Base");
            CloudsDensity = ini.GetValue<MinMaxI>("Weather", key + ".Clouds.Density");
            CloudsPrecipitation = ini.GetValueArray<Precipitation>("Weather", key + ".Clouds.Precipitation");
            if (CloudsPrecipitation.Length == 0) CloudsPrecipitation = new Precipitation[] { Precipitation.None };
            CloudsThickness = ini.GetValue<MinMaxI>("Weather", key + ".Clouds.Thickness");

            DustEnabled = ini.GetValueArray<bool>("Weather", key + ".Dust.Enabled");
            if (DustEnabled.Length == 0) DustEnabled = new bool[] { false };
            DustDensity = ini.GetValue<MinMaxI>("Weather", key + ".Dust.Density");

            FogEnabled = ini.GetValueArray<bool>("Weather", key + ".Fog.Enabled");
            if (FogEnabled.Length == 0) DustEnabled = new bool[] { false };
            FogThickness = ini.GetValue<MinMaxI>("Weather", key + ".Fog.Thickness");
            FogVisibility = ini.GetValue<MinMaxI>("Weather", key + ".Fog.Visibility");

            QNH = ini.GetValue<MinMaxI>("Weather", key + ".QNH");
            Turbulence = ini.GetValue<MinMaxI>("Weather", key + ".Turbulence");
            Visibility = ini.GetValue<MinMaxI>("Weather", key + ".Visibility");
        }
    }
}

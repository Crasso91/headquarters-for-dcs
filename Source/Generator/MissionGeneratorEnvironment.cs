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

using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Mission;
using System;

namespace Headquarters4DCS.Generator
{
    /// <summary>
    /// Generates an HQMission's environment parameters (weather, date, time of the day) from a mission template.
    /// </summary>
    public sealed class MissionGeneratorEnvironment : IDisposable
    {
        /// <summary>
        /// The number of days in each month.
        /// </summary>
        private static readonly int[] DAYS_PER_MONTH = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        private static readonly Weather[] RANDOM_WEATHER = new Weather[]
        {
            Weather.Clear, Weather.Clear,
            Weather.LightClouds, Weather.LightClouds, Weather.LightClouds,
            Weather.SomeClouds, Weather.SomeClouds,
            Weather.Overcast, Weather.Precipitation, Weather.Storm
        };

        /// <summary>
        /// FIXME: description
        /// </summary>
        private static readonly int[] WIND_ALTITUDE = new int[] { 0, 2000, 8000 };

        /// <summary>
        /// Constructor.
        /// </summary>
        public MissionGeneratorEnvironment() { }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Picks a date (day, month and year) for the mission.
        /// </summary>
        /// <param name="mission">The mission.</param>
        /// <param name="timePeriod">The time period (decade) during which the mission is supposed to take place.</param>
        /// <param name="season">The season during which the mission is supposed to take place.</param>
        public void GenerateMissionDate(DCSMission mission, TimePeriod timePeriod, Season season)
        {
            DebugLog.Instance.Log("Generating mission date...");

            DebugLog.Instance.Log($"  Mission should take place during the {timePeriod.ToString().Substring(6)}s");
            switch (timePeriod)
            {
                case TimePeriod.Decade1940: mission.DateYear = HQTools.RandomInt(1940, 1950); break;
                case TimePeriod.Decade1950: mission.DateYear = HQTools.RandomInt(1950, 1960); break;
                case TimePeriod.Decade1960: mission.DateYear = HQTools.RandomInt(1960, 1970); break;
                case TimePeriod.Decade1970: mission.DateYear = HQTools.RandomInt(1970, 1980); break;
                case TimePeriod.Decade1980: mission.DateYear = HQTools.RandomInt(1980, 1990); break;
                case TimePeriod.Decade1990: mission.DateYear = HQTools.RandomInt(1990, 2000); break;
                default: mission.DateYear = HQTools.RandomInt(2000, 2010); break; // default is TimePeriod.Decade2000
                case TimePeriod.Decade2010: mission.DateYear = HQTools.RandomInt(2010, 2020); break;
            }
            DebugLog.Instance.Log($"    Year set to {mission.DateYear}.");

            DebugLog.Instance.Log($"  Mission should take place during the {season.ToString().ToUpperInvariant()} season");
            Month[] months = new Month[0];
            switch (season)
            {
                case Season.Spring: months = new Month[] { Month.March, Month.April, Month.May, Month.June }; break;
                case Season.Summer: months = new Month[] { Month.June, Month.July, Month.August, Month.September }; break;
                case Season.Fall: months = new Month[] { Month.September, Month.October, Month.November, Month.December }; break;
                case Season.Winter: months = new Month[] { Month.December, Month.January, Month.February, Month.March }; break;
                default: break; // default is Season.Random
            }

            if (months.Length == 0) // Season.Random
            {
                mission.DateMonth = (Month)HQTools.RandomInt(12);
                mission.DateDay = HQTools.RandomMinMax(1, GetDaysPerMonth(mission.DateMonth, mission.DateYear));
            }
            else
            {
                int mID = HQTools.RandomInt(4);
                mission.DateMonth = months[mID];
                switch (mID)
                {
                    case 0: mission.DateDay = HQTools.RandomMinMax(21, GetDaysPerMonth(mission.DateMonth, mission.DateYear)); break; // First month of the season
                    case 3: mission.DateDay = HQTools.RandomMinMax(1, 20); break; // Last month of the season
                    default: mission.DateDay = HQTools.RandomMinMax(1, GetDaysPerMonth(mission.DateMonth, mission.DateYear)); break;
                }
            }

            DebugLog.Instance.Log($"    Month set to {mission.DateMonth}.");
            DebugLog.Instance.Log($"    Day set to {mission.DateDay}.");

            DebugLog.Instance.Log();
        }

        /// <summary>
        /// Picks a starting time for the mission.
        /// Must be called after mission date has been set because sunrise/sunset time changes every month.
        /// </summary>
        /// <param name="mission">The mission.</param>
        /// <param name="timeOfDay">The preferred time of day (noon, dawn, twilight, night, random...)</param>
        /// <param name="theater">Theater definition from which to get sunrise/sunset time.</param>
        public void GenerateMissionTime(DCSMission mission, TimeOfDay timeOfDay, DefinitionTheater theater)
        {
            DebugLog.Instance.Log("Generating mission starting time...");

            double fullTime;

            DebugLog.Instance.Log($"  Mission should start at {timeOfDay.ToString().ToUpperInvariant()}");

            switch (timeOfDay)
            {
                default: // aka TimeOfDay.Random;
                    mission.TimeHour = HQTools.RandomInt(0, 24);
                    mission.TimeMinute = HQTools.RandomInt(0, 4) * 15;
                    return;

                case TimeOfDay.RandomDaytime:
                    fullTime = HQTools.RandomInt(theater.DayTime[(int)mission.DateMonth].Min, theater.DayTime[(int)mission.DateMonth].Max - 60);
                    break;

                case TimeOfDay.Dawn:
                    fullTime = HQTools.RandomInt(theater.DayTime[(int)mission.DateMonth].Min, theater.DayTime[(int)mission.DateMonth].Min + 120);
                    break;

                case TimeOfDay.Noon:
                    fullTime = HQTools.RandomInt(
                        (theater.DayTime[(int)mission.DateMonth].Min + theater.DayTime[(int)mission.DateMonth].Max) / 2 - 90,
                        (theater.DayTime[(int)mission.DateMonth].Min + theater.DayTime[(int)mission.DateMonth].Max) / 2 + 90);
                    break;

                case TimeOfDay.Twilight:
                    fullTime = HQTools.RandomInt(theater.DayTime[(int)mission.DateMonth].Max - 120, theater.DayTime[(int)mission.DateMonth].Max + 30);
                    break;

                case TimeOfDay.Night:
                    fullTime = HQTools.RandomInt(0, theater.DayTime[(int)mission.DateMonth].Min - 120);
                    break;
            }

            mission.TimeHour = HQTools.Clamp((int)Math.Floor(fullTime / 60), 0, 23);
            mission.TimeMinute = HQTools.Clamp((int)Math.Floor((fullTime - mission.TimeHour * 60) / 15) * 15, 0, 45);

            DebugLog.Instance.Log($"    Starting time set to {mission.TimeHour.ToString("00")}:{mission.TimeMinute.ToString("00")}");
            DebugLog.Instance.Log();
        }

        /// <summary>
        /// Generates weather settings (precipitation, cloud coverage, temperature...) for the mission.
        /// Must be called after mission date has been set because min/max temperature changes every month.
        /// </summary>
        /// <param name="mission">The mission.</param>
        /// <param name="weather">The preferred type of weather (clear, cloudy, storm...).</param>
        /// <param name="theater">Theater definition from which to get weather info for this part of the world.</param>
        public void GenerateWeather(DCSMission mission, Weather weather, DefinitionTheater theater)
        {
            DebugLog.Instance.Log("Generating weather conditions...");

            mission.WeatherTemperature = theater.Temperature[(int)mission.DateMonth].GetValue();

            DebugLog.Instance.Log($"  Weather should be {weather.ToString().ToUpperInvariant()}");
            mission.WeatherLevel = (weather == Weather.Random) ? HQTools.RandomFrom(RANDOM_WEATHER) : weather;

            DebugLog.Instance.Log($"    Weather quality set to {mission.WeatherLevel}");

            // Clouds and precipitations
            mission.WeatherCloudBase = theater.Weather[(int)mission.WeatherLevel].CloudsBase.GetValue();
            mission.WeatherCloudsDensity = theater.Weather[(int)mission.WeatherLevel].CloudsDensity.GetValue();
            mission.WeatherCloudsPrecipitation = HQTools.RandomFrom(theater.Weather[(int)mission.WeatherLevel].CloudsPrecipitation);
            mission.WeatherCloudsThickness = theater.Weather[(int)mission.WeatherLevel].CloudsThickness.GetValue();

            // Dust
            mission.WeatherDustEnabled = HQTools.RandomFrom(theater.Weather[(int)mission.WeatherLevel].DustEnabled);
            mission.WeatherDustDensity = mission.WeatherDustEnabled ? theater.Weather[(int)mission.WeatherLevel].DustDensity.GetValue() : 0;

            // Fog
            mission.WeatherFogEnabled = HQTools.RandomFrom(theater.Weather[(int)mission.WeatherLevel].FogEnabled);
            mission.WeatherFogThickness = mission.WeatherFogEnabled ? theater.Weather[(int)mission.WeatherLevel].FogThickness.GetValue() : 0;
            mission.WeatherFogVisibility = mission.WeatherFogEnabled ? theater.Weather[(int)mission.WeatherLevel].FogVisibility.GetValue() : 0;

            // Pressure, turbulence and visiblity
            mission.WeatherQNH = theater.Weather[(int)mission.WeatherLevel].QNH.GetValue();
            mission.WeatherTurbulence = theater.Weather[(int)mission.WeatherLevel].Turbulence.GetValue();
            mission.WeatherVisibility = theater.Weather[(int)mission.WeatherLevel].Visibility.GetValue();

            DebugLog.Instance.Log($"    Cloud base altitude set to {mission.WeatherCloudBase} m");
            DebugLog.Instance.Log($"    Cloud density set to {mission.WeatherCloudBase}");
            DebugLog.Instance.Log($"    Precipitation set to {mission.WeatherCloudsPrecipitation}");
            DebugLog.Instance.Log($"    Cloud thickness set to {mission.WeatherCloudsThickness} m");

            DebugLog.Instance.Log($"    Dust set to {mission.WeatherDustEnabled}");
            DebugLog.Instance.Log($"    Dust density set to {mission.WeatherDustDensity}");

            DebugLog.Instance.Log($"    Fog set to {mission.WeatherFogEnabled}");
            DebugLog.Instance.Log($"    Fog thickness set to {mission.WeatherFogThickness}");
            DebugLog.Instance.Log($"    Fog visibility set to {mission.WeatherFogVisibility} m");

            DebugLog.Instance.Log($"    QNH set to {mission.WeatherQNH}");
            DebugLog.Instance.Log($"    Turbulence set to {mission.WeatherTurbulence} m/s");
            DebugLog.Instance.Log($"    Visibility set to {mission.WeatherVisibility} m");

            DebugLog.Instance.Log();
        }

        /// <summary>
        /// Generates wind settings for the mission. Must be called once mission weather level has been set, as weather is used for auto wind.
        /// </summary>
        /// <param name="mission">The mission.</param>
        /// <param name="wind">The preferred wind speed.</param>
        /// <param name="theater">Theater definition from which to get wind info for this part of the world.</param>
        public void GenerateWind(DCSMission mission, Wind wind, DefinitionTheater theater)
        {
            DebugLog.Instance.Log("Generating wind...");

            DebugLog.Instance.Log($"  Wind speed should be {wind.ToString().ToUpperInvariant()}");

            // If auto, speed depends on weather, so we never end up with no wind in a storm
            mission.WindLevel = (wind == Wind.Auto) ?
                (Wind)(HQTools.Clamp((int)mission.WeatherLevel + HQTools.RandomMinMax(-1, 1), 0, (int)Wind.StrongGale))
                : wind;

            DebugLog.Instance.Log($"    Wind speed level set to {mission.WindLevel}");

            for (int i = 0; i < 3; i++)
            {
                mission.WeatherWindSpeed[i] = Math.Max(0, theater.Wind[(int)mission.WindLevel].Wind.GetValue());
                mission.WeatherWindDirection[i] = (mission.WeatherWindSpeed[i] > 0) ? HQTools.RandomInt(0, 360) : 0;
                DebugLog.Instance.Log($"    Wind speed at {WIND_ALTITUDE[i]} meters set to {mission.WeatherWindSpeed[i]} m/s, direction of {mission.WeatherWindDirection[i]}");
            }

            // Turbulence = max(weatherTurbulence, windTurbulence)
            mission.WeatherTurbulence = Math.Max(mission.WeatherTurbulence, theater.Wind[(int)mission.WindLevel].Turbulence.GetValue());
            DebugLog.Instance.Log($"    Turbulence updated to {mission.WeatherTurbulence} m/s");

            DebugLog.Instance.Log();
        }

        /// <summary>
        /// Returns the number of days in a month.
        /// </summary>
        /// <param name="month">The month of the year.</param>
        /// <param name="year">The year. Used to know if it's a leap year.</param>
        /// <returns>The number of days in the month.</returns>
        private int GetDaysPerMonth(Month month, int year)
        {
            // Not february, return
            if (month != Month.February) return DAYS_PER_MONTH[(int)month];

            bool leapYear = false;
            if ((year % 400) == 0) leapYear = true;
            else if ((year % 100) == 0) leapYear = false;
            else if ((year % 4) == 0) leapYear = true;

            return leapYear ? 29 : 28;
        }
    }
}

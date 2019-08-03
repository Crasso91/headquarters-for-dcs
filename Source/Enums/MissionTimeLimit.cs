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

namespace Headquarters4DCS
{
    /// <summary>
    /// A time limit setting for the mission.
    /// </summary>
    public enum MissionTimeLimit
    {
        None = -1,

        TimeLimit15Minutes = 15,
        TimeLimit30Minutes = 30,
        TimeLimit60Minutes = 60,
        TimeLimit90Minutes = 90,
        TimeLimit120Minutes = 120,
        TimeLimit180Minutes = 180,
        TimeLimit240Minutes = 240,
    }
}

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

namespace Headquarters4DCS.Generator
{
    /// <summary>
    /// Stores information about an aircraft group callsign
    /// </summary>
    public struct MGCallsign
    {
        public readonly string GroupName;
        public readonly string UnitName;
        //public readonly string OnboardNum;
        public readonly string Lua;

        public MGCallsign(string groupName, string unitName/*, string onboardNum*/, string lua)
        {
            GroupName = groupName;
            UnitName = unitName;
            //OnboardNum = onboardNum;
            Lua = lua;
        }
    }
}

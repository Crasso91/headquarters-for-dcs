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
    /// Where should a script be included in the mission script Lua file?
    /// </summary>
    public enum ObjectiveScriptScope
    {
        /// <summary>
        /// Include script at the end of the file so it is ran on mission start.
        /// </summary>
        Global,

        /// <summary>
        /// Include script in the hq.eventHandler:onEvent function, so it is ran when an event happens.
        /// </summary>
        Event,

        /// <summary>
        /// Include script in the hq.everySecond() function, so it is ran every second.
        /// </summary>
        Timer
    }
}

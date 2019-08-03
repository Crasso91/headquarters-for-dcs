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
    /// The DCS World task assigned to a flight group. Must match names used by DCS World.
    /// </summary>
    public enum DCSAircraftTask
    {
        Nothing,

        AFAC,
        AntishipStrike,
        AWACS,
        CAS,
        CAP,
        Escort,
        FighterSweep,
        //GAI,
        GroundAttack,
        Intercept,
        PinpointStrike,
        Reconnaissance,
        Refueling,
        RunwayAttack,
        SEAD,
        Transport,


        /*
        None,

        AntiShip,
        AWACS,
        Bombing,
        CAP,
        CAS,
        CSAR,
        Escort,
        Intercept,
        Interdiction, // does not exist in DCS, only to display in the briefing, converted to CAS in-game
        Recon,
        SEAD,
        Strike,
        Tanker,
        Transport,

        // Escort tasks must be last in the enum, so they appear after "primary mission groups" in the briefing
        // (flight groups are sorted by task)
        CAPEscort, // does not exist in DCS, only to display in the briefing, converted to CAP in-game
        SEADEscort // does not exist in DCS, only to display in the briefing, converted to SEAD in-game
        */
    }
}

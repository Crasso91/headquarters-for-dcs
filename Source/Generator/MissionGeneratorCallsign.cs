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

using System;
using System.Collections.Generic;

namespace Headquarters4DCS.Generator
{
    /// <summary>
    /// Generates unique callsigns for the aircraft in the mission.
    /// </summary>
    public sealed class MissionGeneratorCallsign : IDisposable
    {
        private static readonly int NATO_CALLSIGN_COUNT = HQTools.EnumCount<CallsignFamily>();

        private static readonly string[][] NATO_CALLSIGN_NAMES = new string[][]
        {
            new string[] { "Enfield","Springfield","Uzi","Colt","Dodge","Ford","Chevy","Pontiac" },
            new string[] { "Argus","Boxer","Charlie","Diamond","Echo","Fire","Giant","Hector","Hitman" },
            new string[] { "Overlord","Magic","Wizard","Focus", "Darkstar" },
            new string[] { "Axeman","Darknight","Warrior","Pointer","Eyeball","Moonbeam","Whiplash","Finger","Pinpoint","Ferret","Shaba","Playboy","Hammer","Jaguar","Deathstar","Anvil","Firefly","Mantis","Badger" },
            new string[] { "Texaco","Arco","Shell" },
        };

        private readonly bool[] CoalitionUsesNATOCallsigns = new bool[2];

        private readonly int[][] NATOCallsigns = new int[NATO_CALLSIGN_COUNT][];
        private readonly List<string> RussianCallsigns = new List<string>();

        public MissionGeneratorCallsign(bool blueNATOCallsigns, bool redNATOCallsigns)
        {
            CoalitionUsesNATOCallsigns[(int)Coalition.Blue] = blueNATOCallsigns;
            CoalitionUsesNATOCallsigns[(int)Coalition.Red] = redNATOCallsigns;

            for (int i = 0; i < NATO_CALLSIGN_NAMES.Length; i++)
                NATOCallsigns[i] = new int[NATO_CALLSIGN_NAMES[i].Length];

            Clear();
        }

        public void Dispose() { }

        public void Clear()
        {
            int i, j;

            for (i = 0; i < NATO_CALLSIGN_COUNT; i++)
                for (j = 0; j < NATO_CALLSIGN_NAMES[i].Length; j++)
                    NATOCallsigns[i][j] = 0;

            RussianCallsigns.Clear();
        }

        /// <summary>
        /// Returns an unique callsign for an aircraft group.
        /// </summary>
        /// <param name="csFamily">The type of aircraft (AWACS, tanker, fighter...)</param>
        /// <param name="natoCallsign">Should this callsign be in the NATO format (true) or the Russian format (false)</param>
        /// <returns></returns>
        public MGCallsign GetCallsign(CallsignFamily csFamily, Coalition coalition)
        {
            if (CoalitionUsesNATOCallsigns[(int)coalition])
                return GetNATOCallsign(csFamily);

            return GetRussianCallsign();
        }

        /// <summary>
        /// Returns an unique callsign in the NATO format (Callsign Number Number)
        /// </summary>
        /// <param name="csFamily">The type of aircraft (AWACS, tanker, fighter...)</param>
        /// <returns>The callsign</returns>
        private MGCallsign GetNATOCallsign(CallsignFamily csFamily)
        {
            int callsignIndex;

            do
            {
                callsignIndex = HQTools.RandomInt(NATO_CALLSIGN_NAMES[(int)csFamily].Length);
            } while (NATOCallsigns[(int)csFamily][callsignIndex] >= 9);

            NATOCallsigns[(int)csFamily][callsignIndex]++;

            string groupName =
                NATO_CALLSIGN_NAMES[(int)csFamily][callsignIndex] + " " +
                HQTools.ValToString(NATOCallsigns[(int)csFamily][callsignIndex]);

            string unitName = groupName + " $INDEX$";
            //string onboardNum = HQTools.ValToString((callsignIndex % 9) + 1) +
            //    NATOCallsigns[(int)csFamily][callsignIndex] + "$INDEX$";
            string lua =
                $"{{ [1]= {HQTools.ValToString(callsignIndex + 1)}, " +
                $"[2]={HQTools.ValToString(NATOCallsigns[(int)csFamily][callsignIndex])}, " +
                "[3]=$INDEX$, " +
                $"[\"name\"] = \"{unitName.Replace(" ", "")}\", }}";

            return new MGCallsign(groupName, unitName/*, onboardNum*/, lua);
        }

        /// <summary>
        /// Returns an unique callsign in the russian format (3-digits)
        /// </summary>
        /// <returns>The callsign</returns>
        private MGCallsign GetRussianCallsign()
        {
            int[] fgNumber = new int[2];
            string fgName = "";

            do
            {
                fgNumber[0] = HQTools.RandomMinMax(1, 9);
                fgNumber[1] = HQTools.RandomMinMax(0, 9);

                fgName = HQTools.ValToString(fgNumber[0]) + HQTools.ValToString(fgNumber[1]);
            } while (RussianCallsigns.Contains(fgName));

            RussianCallsigns.Add(fgName);

            string unitName = fgName + "$INDEX$";

            return new MGCallsign(fgName + "0", unitName/*, unitName*/, unitName);
        }
    }
}

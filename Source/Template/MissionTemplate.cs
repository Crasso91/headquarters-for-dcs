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

using Headquarters4DCS.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Headquarters4DCS.Template
{
    public sealed class MissionTemplate : IDisposable
    {
        /// <summary>
        /// Theater in which the mission takes place.
        /// </summary>
        public string Theater { get; private set; } = HQLibrary.Instance.Common.DefaultTheater;

        public readonly MissionTemplateSettings Settings = null;

        public Dictionary<string, MissionTemplateNode> Nodes = new Dictionary<string, MissionTemplateNode>();

        public MissionTemplate()
        {
            Settings = new MissionTemplateSettings();

            Clear(HQLibrary.Instance.Common.DefaultTheater);
        }

        public void Clear(string theaterID)
        {
            Theater = HQLibrary.Instance.DefinitionExists<DefinitionTheater>(theaterID) ? theaterID : HQLibrary.Instance.Common.DefaultTheater;
            DefinitionTheater theaterDefinition = HQLibrary.Instance.GetDefinition<DefinitionTheater>(Theater);

            Nodes.Clear();

            foreach (DefinitionTheaterNode n in theaterDefinition.Nodes.Values)
            {
                if (n is DefinitionTheaterNodeAirbase)
                    Nodes.Add(n.ID, new MissionTemplateNodeAirbase(n));
                //else if (n is DefinitionTheaterNodeCarrierLocation)
                //    Nodes.Add(n.ID, new HQTemplateNodeCarrierGroup(n));
                else if (n is DefinitionTheaterNodeLand)
                    Nodes.Add(n.ID, new MissionTemplateNodeLand(n));
            }
        }

        public void InvertAirbasesCoalition()
        {
            string[] keys = Nodes.Keys.ToArray();

            foreach (string k in keys)
            {
                if (!(Nodes[k] is MissionTemplateNodeAirbase)) continue;
                ((MissionTemplateNodeAirbase)Nodes[k]).Coalition = 1 - ((MissionTemplateNodeAirbase)Nodes[k]).Coalition;
            }
        }

        public bool LoadFromFile(string filePath)
        {
            using (INIFile ini = new INIFile(filePath))
            {
                string theater = ini.GetValue<string>("Settings", "Theater");
                Clear(theater);

                DefinitionTheater theaterDefinition = HQLibrary.Instance.GetDefinition<DefinitionTheater>(Theater);

                foreach (string k in Nodes.Keys)
                {
                    Nodes[k].LoadFromFile(ini);
                }
            }

            return true;
        }

        public bool SaveToFile(string filePath)
        {
            using (INIFile ini = new INIFile())
            {
                ini.SetValue("Settings", "Theater", Theater);
                Settings.SaveToFile(ini);

                foreach (string k in Nodes.Keys)
                    Nodes[k].SaveToFile(ini);

                ini.SaveToFile(filePath);
            }

            return true;
        }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose() { }

        public int GetPlayerCount()
        {
            int playerCount = 0;

            foreach (MissionTemplateNode node in Nodes.Values)
            {
                foreach (MissionTemplatePlayerFlightGroup fg in node.PlayerFlightGroups)
                    playerCount += fg.AIWingmen ? 1 : fg.Count;
            }

            return playerCount;
        }
    }
}

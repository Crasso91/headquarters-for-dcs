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

using Headquarters4DCS.Mission;
using System;

namespace Headquarters4DCS.Miz
{
    /// <summary>
    /// Creates the "Warehouses" entry in the MIZ file.
    /// </summary>
    public sealed class MizExporterLuaWarehouse : IDisposable
    {
        /// <summary>
        /// The warehouse Lua template, loaded from Include\Lua
        /// </summary>
        private readonly string WarehouseLua;

        /// <summary>
        /// The Lua template for an airport warehouse, loaded from Include\Lua\Warehouses
        /// </summary>
        private readonly string WarehouseAirportLua;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MizExporterLuaWarehouse()
        {
            WarehouseLua = HQTools.ReadIncludeLuaFile("Warehouses.lua");
            WarehouseAirportLua = HQTools.ReadIncludeLuaFile("Warehouses\\Airport.lua");
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Generates the content of the Lua file.
        /// </summary>
        /// <param name="missHQ">An HQ4DCS mission.</param>
        /// <returns>The contents of the Lua file.</returns>
        public string MakeLua(DCSMission missHQ)
        {
            string airportsLua = "";

            foreach (int id in missHQ.AirbasesCoalition.Keys)
            {
                string apLua = WarehouseAirportLua;
                HQTools.ReplaceKey(ref apLua, "Index", id);
                HQTools.ReplaceKey(ref apLua, "Coalition", missHQ.AirbasesCoalition[id].ToString().ToUpperInvariant());
                airportsLua += apLua + "\n";
            }

            string lua = WarehouseLua;
            HQTools.ReplaceKey(ref lua, "Airports", airportsLua);

            return lua;
        }
    }
}

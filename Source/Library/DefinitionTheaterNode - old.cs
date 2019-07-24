///*
//==========================================================================
//This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
//Eagle Dynamics' DCS World flight simulator.

//HQ4DCS was created by Ambroise Garel (@akaAgar).
//You can find more information about the project on its GitHub page,
//https://akaAgar.github.io/headquarters-for-dcs

//HQ4DCS is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//HQ4DCS is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with HQ4DCS. If not, see https://www.gnu.org/licenses/
//==========================================================================
//*/

//using Headquarters4DCS.Enums;
//using System;
//using System.Globalization;

//namespace Headquarters4DCS.Library
//{
//    /// <summary>
//    /// Information about a node (location where units can be spawned).
//    /// </summary>
//    public struct DefinitionTheaterNode
//    {
//        /// <summary>
//        /// Coordinates of the node.
//        /// </summary>
//        public readonly Coordinates Coordinates;

//        /// <summary>
//        /// The type of node. Determines which kind of units should be spawned here.
//        /// </summary>
//        public readonly TheaterNodeType NodeType;

//        /// <summary>
//        /// On which coalition's territory is this node located?
//        /// </summary>
//        public readonly Coalition Side;

//        /// <summary>
//        /// Is this node valid?
//        /// </summary>
//        public readonly bool IsValid;

//        /// <summary>
//        /// The unique ID of this node.
//        /// </summary>
//        public readonly int UniqueID;

//        /// <summary>
//        /// Constructor. Loads data from the theater ini file.
//        /// </summary>
//        /// <param name="ini">The ini file to load from</param>
//        /// <param name="key">Top level ini key to use.</param>
//        /// <param name="id">The unique ID of this node.</param>
//        public DefinitionTheaterNode(INIFile ini, string key, int id) : this()
//        {
//            string[] vals = ini.GetValueArray<string>("Nodes", key);
//            IsValid = true;
//            UniqueID = id;

//            if (vals.Length < 4) { IsValid = false; return; }

//            try
//            {
//                Coordinates = new Coordinates(Convert.ToDouble(vals[0], NumberFormatInfo.InvariantInfo), Convert.ToDouble(vals[1], NumberFormatInfo.InvariantInfo));
//                NodeType = (TheaterNodeType)Enum.Parse(typeof(TheaterNodeType), vals[2], true);
//                Side = (Coalition)Enum.Parse(typeof(Coalition), vals[3], true);
//            }
//            catch (Exception)
//            {
//                IsValid = false;
//            }
//        }
//    }
//}

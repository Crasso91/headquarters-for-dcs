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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Headquarters4DCS.Library
{
    /// <summary>
    /// The definition of a DCS World theater.
    /// </summary>
    public sealed class DefinitionTheater : Definition
    {
        /// <summary>
        /// How many times should SelectNodesInRadius() expand its search radius when no nodes are found?
        /// </summary>
        private const int MAX_RADIUS_SEARCH_ITERATIONS = 32;

        /// <summary>
        /// The public ID of the theater in DCS World.
        /// </summary>
        public string DCSID { get; private set; }

        /// <summary>
        /// The default coordinates of the map center.
        /// </summary>
        public Coordinates DefaultMapCenter { get; private set; }

        /// <summary>
        /// Required DCS World modules.
        /// </summary>
        public string[] RequiredModules { get; private set; }

        /// <summary>
        /// Magnetic declination from true north.
        /// </summary>
        public float MagneticDeclination { get; private set; }

        /// <summary>
        /// Sunrise and sunset time (in minutes) for each month (January is 0, December is 11)
        /// </summary>
        public MinMaxI[] DayTime { get; private set; }

        /// <summary>
        /// Min and max temperature (in degrees Celsius) for each month (January is 0, December is 11)
        /// </summary>
        public MinMaxI[] Temperature { get; private set; }

        /// <summary>
        /// Weather parameters for all "weather quality" settings. 11 values, from clear (0) to storm (10).
        /// </summary>
        public DefinitionTheaterWeather[] Weather { get; private set; }

        /// <summary>
        /// Wind parameters for all "wind speed" settings. 11 values, from clear (0) to storm (10).
        /// </summary>
        public DefinitionTheaterWind[] Wind { get; private set; }

        /// <summary>
        /// All airdromes in this theater.
        /// </summary>
        //public Dictionary<string, DefinitionTheaterNodeAirbase> Airbases { get; private set; }

        //public Dictionary<string, DefinitionTheaterCarrierLocation> CarrierLocations { get; private set; }

        public Dictionary<string, DefinitionTheaterNode> Nodes { get; private set; }

        /// <summary>
        /// All locations of this theater where targets can be spawned.
        /// </summary>
        public Dictionary<string, DefinitionTheaterNodeRegion> Locations { get; private set; }

        /// <summary>
        /// Navigation path for the carrier group in this theater.
        /// </summary>
        public Coordinates[] CarrierWaypoints { get; private set; }

        /// <summary>
        /// Nodes (points where to spawn units) for this theater.
        /// </summary>
        //public DefinitionTheaterNode[] Nodes { get; private set; }

        private List<int> ExcludedNodeIDs = new List<int>();

        /// <summary>
        /// Loads data required by this definition.
        /// </summary>
        /// <param name="path">Path to definition file or directory.</param>
        /// <returns>True is successful, false if an error happened.</returns>
        protected override bool OnLoad(string path)
        {
            int i;

            using (INIFile ini = new INIFile(path + "Theater.ini"))
            {
                if (!File.Exists(path + "Map.jpg")) return false;

                // -----------------
                // [Theater] section
                // -----------------
                DCSID = ini.GetValue<string>("Theater", "DCSID");
                DefaultMapCenter = ini.GetValue<Coordinates>("Theater", "DefaultMapCenter");
                RequiredModules = ini.GetValueArray<string>("Theater", "RequiredModules");
                MagneticDeclination = ini.GetValue<float>("Theater", "MagneticDeclination");

                // -----------------
                // [Daytime] section
                // -----------------
                DayTime = new MinMaxI[12];
                for (i = 0; i < 12; i++)
                    DayTime[i] = ini.GetValue<MinMaxI>("Daytime", ((Month)i).ToString());

                // ---------------------
                // [Temperature] section
                // ---------------------
                Temperature = new MinMaxI[12];
                for (i = 0; i < 12; i++)
                    Temperature[i] = ini.GetValue<MinMaxI>("Temperature", ((Month)i).ToString());

                // -----------------
                // [Weather] section
                // -----------------
                Weather = new DefinitionTheaterWeather[HQTools.EnumCount<Weather>() - 1]; // -1 because we don't want "Random"
                for (i = 0; i < Weather.Length; i++)
                    Weather[i] = new DefinitionTheaterWeather(ini, ((Weather)i).ToString());

                // --------------
                // [Wind] section
                // --------------
                Wind = new DefinitionTheaterWind[HQTools.EnumCount<Wind>() - 1]; // -1 because we don't want "Auto"
                for (i = 0; i < Wind.Length; i++)
                    Wind[i] = new DefinitionTheaterWind(ini, ((Wind)i).ToString());

                // ------------------
                // [Airbases] section
                // ------------------
                Nodes = new Dictionary<string, DefinitionTheaterNode>(StringComparer.InvariantCultureIgnoreCase);
                foreach (string f in Directory.GetFiles(path + "Nodes", "*.ini")) // FIXME: what if directory doesn't exist
                {
                    string k = Path.GetFileNameWithoutExtension(f);
                    if (string.IsNullOrEmpty(k) || Nodes.ContainsKey(k)) continue;

                    if (k.ToLowerInvariant().StartsWith("airbase_"))
                        Nodes.Add(k, new DefinitionTheaterNodeAirbase(k, f));
                    else if (k.ToLowerInvariant().StartsWith("carriergroup_"))
                        Nodes.Add(k, new DefinitionTheaterNodeCarrierGroup(k, f));
                    else if (k.ToLowerInvariant().StartsWith("region_"))
                        Nodes.Add(k, new DefinitionTheaterNodeRegion(k, f));
                }

                //foreach (string f in Directory.GetFiles(path + "CarrierLocations", "*.ini")) // FIXME: what if directory doesn't exist
                //{
                //    string k = Path.GetFileNameWithoutExtension(f);
                //    if (string.IsNullOrEmpty(k) || Nodes.ContainsKey(k)) continue;
                //    Nodes.Add(k, new Def(k, f));
                //}

                // ------------------
                // [Targets] section
                // ------------------
                //Locations = new Dictionary<string, DefinitionTheaterNodeLocation>(StringComparer.InvariantCultureIgnoreCase);
                //foreach (string f in Directory.GetFiles(path + "Locations", "*.ini")) // FIXME: what if directory doesn't exist
                //{
                //    string k = Path.GetFileNameWithoutExtension(f);
                //    if (string.IsNullOrEmpty(k) || Locations.ContainsKey(k)) continue;
                //    Locations.Add(k, new DefinitionTheaterNodeLocation(k, f));
                //}

                // -------------------------------
                // [CarrierGroupWaypoints] section
                // -------------------------------
                List<Coordinates> carrierWPList = new List<Coordinates>();
                string[] carrierPathKeys = ini.GetKeysInSection("CarrierGroupWaypoints");
                for (i = 0; i < carrierPathKeys.Length; i++)
                    carrierWPList.Add(ini.GetValue<Coordinates>("CarrierGroupWaypoints", carrierPathKeys[i]));
                CarrierWaypoints = carrierWPList.ToArray();

                // ---------------
                // [Nodes] section
                // ---------------
                //List<DefinitionTheaterNode> nodesList = new List<DefinitionTheaterNode>();
                //string[] nodeKeys = ini.GetKeysInSection("Nodes");
                //for (i = 0; i < nodeKeys.Length; i++)
                //    nodesList.Add(new DefinitionTheaterNode(ini, nodeKeys[i], i));
                //Nodes = (from n in nodesList where n.IsValid select n).ToArray();
            }

            return true;
        }

        /// <summary>
        /// Returns a random airdome.
        /// </summary>
        /// <param name="requiredCoalition">If not null, airdrome must belong to this coalition.</param>
        /// <param name="mustBeMilitary">If true, airdrome must be military.</param>
        /// <param name="mustBeNearWater">If true, airdrome must be near water.</param>
        /// <returns>An airdrome</returns>
        //public DefinitionTheaterNodeAirbase GetRandomAirdrome(Coalition? requiredCoalition = null, bool mustBeMilitary = false, bool mustBeNearWater = false)
        //{
        //    // No airdrome? Return an empty airdrome structure.
        //    if (Airbases.Count == 0) return new DefinitionTheaterNodeAirbase();

        //    DefinitionTheaterNodeAirbase[] validAirdromes =
        //        (from DefinitionTheaterNodeAirbase ad in Airbases
        //         where
        //         (!requiredCoalition.HasValue || (ad.Coalition == requiredCoalition.Value)) &&
        //         (!mustBeMilitary || ad.IsMilitary) &&
        //         (!mustBeNearWater || ad.IsNearWater)
        //         select ad).ToArray();

        //    // No matching airdrome found? Ignore preferences and return a random airdrome.
        //    if (validAirdromes.Length == 0) validAirdromes = Airbases.Values.ToArray();

        //    return HQTools.RandomFrom(validAirdromes);
        //}

        /// <summary>
        /// Clears the list of already used nodes.
        /// </summary>
        public void ClearExcludedNodes()
        { ExcludedNodeIDs.Clear(); }

        /// <summary>
        /// Select a random node matching several criteria and marks it 
        /// </summary>
        /// <param name="nodeTypes">If not null, node must be of one of these types. If null, node can be of any type.</param>
        /// <param name="coalition">If not null, node must belong to this coalition. If null, node can belong to any coalition.</param>
        /// <param name="location1">If neither location1 nor minMaxDistance1 are null, node must be between minMaxDistance1.Min and minMaxDistance1.Max meters of location1.</param>
        /// <param name="minMaxDistance1">If neither location1 nor minMaxDistance1 are null, node must be between minMaxDistance1.Min and minMaxDistance1.Max meters of location1.</param>
        /// <param name="location2">If neither location2 nor minMaxDistance2 are null, node must be between minMaxDistance2.Min and minMaxDistance2.Max meters of location2.</param>
        /// <param name="minMaxDistance2">If neither location2 nor minMaxDistance2 are null, node must be between minMaxDistance2.Min and minMaxDistance2.Max meters of location2.</param>
        /// <returns>The node, or null if no node was found.</returns>
        //public DefinitionTheaterNode? SelectNode(
        //    TheaterNodeType[] nodeTypes = null, Coalition? coalition = null,
        //    Coordinates? location1 = null, MinMaxD? minMaxDistance1 = null,
        //    Coordinates? location2 = null, MinMaxD? minMaxDistance2 = null)
        //{
        //    // Remove all already used nodes from the list.
        //    IEnumerable<DefinitionTheaterNode> validNodes = (from DefinitionTheaterNode n in Nodes where !ExcludedNodeIDs.Contains(n.UniqueID) select n);

        //    // If a "node type" selection criteria is provided, select only nodes of the proper types
        //    if ((nodeTypes != null) && (nodeTypes.Length > 0))
        //        validNodes = (from DefinitionTheaterNode n in validNodes where nodeTypes.Contains(n.NodeType) select n);

        //    // If a "coalition" selection criteria is provided, select only nodes located in the required coalitions's countries
        //    if (coalition.HasValue)
        //        validNodes = (from DefinitionTheaterNode n in validNodes where n.Side == coalition.Value select n);

        //    // If both location1 and minMaxDist1 are provided, select only nodes between min and max distance of location1
        //    if (location1.HasValue && minMaxDistance1.HasValue)
        //        validNodes = SelectNodesInRadius(validNodes, location1.Value, minMaxDistance1.Value);

        //    // If both location2 and minMaxDist2 are provided, select only nodes between min and max distance of location1
        //    if (location2.HasValue && minMaxDistance2.HasValue)
        //        validNodes = SelectNodesInRadius(validNodes, location2.Value, minMaxDistance2.Value);

        //    // No nodes match selection criteria? Return null.
        //    if (validNodes.Count() == 0) return null;

        //    // Pick a random matching node, add its ID to the list of already used nodes, then return the node.
        //    DefinitionTheaterNode selectedNode = HQTools.RandomFrom(validNodes.ToArray());
        //    ExcludedNodeIDs.Add(selectedNode.UniqueID);
        //    return selectedNode;
        //}

        /// <summary>
        /// Select nodes in a search radius. While no node is found and the max number of iterations has not been reached, expand search radius.
        /// </summary>
        /// <param name="nodes">A enumeration of nodes to search.</param>
        /// <param name="center">The center of the search area.</param>
        /// <param name="distance">Required min and max distance from the center of the search area.</param>
        /// <returns>Nodes found in thes search radius</returns>
        //private IEnumerable<DefinitionTheaterNode> SelectNodesInRadius(IEnumerable<DefinitionTheaterNode> nodes, Coordinates center, MinMaxD distance)
        //{
        //    if (nodes.Count() == 0) return nodes;

        //    IEnumerable<DefinitionTheaterNode> nodesInRadius = null;

        //    for (int i = 0; i < MAX_RADIUS_SEARCH_ITERATIONS; i++)
        //    {
        //        nodesInRadius = (from DefinitionTheaterNode n in nodes
        //                         where distance.Contains(n.Coordinates.GetDistanceFrom(center))
        //                         select n);

        //        if (nodesInRadius.Count() > 0) break;

        //        distance = new MinMaxD(distance.Min * 0.9, distance.Max * 1.1);
        //    }

        //    return nodesInRadius;
        //}
    }
}

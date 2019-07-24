using Headquarters4DCS.Enums;
using System;

namespace Headquarters4DCS.Library
{
    public struct NodeSpawnPoint
    {
        public readonly bool IsValid;
        public readonly Coordinates Position;
        public readonly NodeSpawnPointType PointType;
        public readonly string UniqueID;

        public NodeSpawnPoint(INIFile ini, string section, string key) : this()
        {
            string[] vals = ini.GetValueArray<string>("Nodes", key);
            IsValid = true;
            UniqueID = key;

            if (vals.Length < 3) { IsValid = false; return; }

            try
            {
                Position = new Coordinates(HQTools.StringToDouble(vals[0]), HQTools.StringToDouble(vals[1]));
                PointType = (NodeSpawnPointType)Enum.Parse(typeof(NodeSpawnPointType), vals[2], true);
            }
            catch (Exception)
            {
                IsValid = false;
            }
        }
    }
}

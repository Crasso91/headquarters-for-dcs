using System.Collections.Generic;

namespace Headquarters4DCS.Library
{
    public abstract class DefinitionTheaterNode
    {
        protected abstract bool UseSpawnPoints { get; }

        public string ID { get; private set; }
        public NodeSpawnPoint[] SpawnPoints { get; private set; } = new NodeSpawnPoint[0];

        /// <summary>
        /// Position on the HQ4DCS map.
        /// </summary>
        public Coordinates MapPosition { get; private set; }

        public DefinitionTheaterNode(string id, string iniFilePath)
        {
            ID = id;

            using (INIFile ini = new INIFile(iniFilePath))
            {
                MapPosition = ini.GetValue<Coordinates>("Node", "MapPosition");
                LoadNodeData(ini);
                LoadSpawnPoints(ini);
            }
        }

        private void LoadSpawnPoints(INIFile ini)
        {
            if (UseSpawnPoints)
            {
                string[] spawnPointsKeys = ini.GetKeysInSection("SpawnPoints");

                List<NodeSpawnPoint> spawnPointList = new List<NodeSpawnPoint>();
                foreach (string k in spawnPointsKeys)
                {
                    NodeSpawnPoint sp = new NodeSpawnPoint(ini, "SpawnPoints", k);
                    if (!sp.IsValid) continue;
                    spawnPointList.Add(sp);
                }
                SpawnPoints = spawnPointList.ToArray();
            }
        }

        protected abstract void LoadNodeData(INIFile ini);
    }
}

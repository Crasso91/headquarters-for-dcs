namespace Headquarters4DCS.Library
{
    public abstract class DefinitionTheaterNode
    {
        public readonly string ID;

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
            }
        }

        protected abstract void LoadNodeData(INIFile ini);
    }
}

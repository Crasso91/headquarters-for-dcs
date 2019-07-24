namespace Headquarters4DCS.Library
{
    public sealed class DefinitionTheaterNodeRegion : DefinitionTheaterNode
    {
        protected override bool UseSpawnPoints { get { return true; } }

        public string Name { get; private set; }

        public DefinitionTheaterNodeRegion(string id, string iniFilePath) : base(id, iniFilePath) { }

        protected override void LoadNodeData(INIFile ini)
        {
            Name = ini.GetValue<string>("Node", "Name");
        }
    }
}

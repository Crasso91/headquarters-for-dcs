namespace Headquarters4DCS.Library
{
    public sealed class DefinitionTheaterNodeLocation : DefinitionTheaterNode
    {
        public string Name { get; private set; }

        public DefinitionTheaterNodeLocation(string id, string iniFilePath) : base(id, iniFilePath) { }

        protected override void LoadNodeData(INIFile ini)
        {
            Name = ini.GetValue<string>("Node", "Name");
        }
    }
}

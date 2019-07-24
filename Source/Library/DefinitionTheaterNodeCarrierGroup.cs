namespace Headquarters4DCS.Library
{
    public sealed class DefinitionTheaterNodeCarrierGroup : DefinitionTheaterNode
    {
        protected override bool UseSpawnPoints { get { return false; } }

        public DefinitionTheaterNodeCarrierGroup(string id, string iniFilePath) : base(id, iniFilePath) { }

        protected override void LoadNodeData(INIFile ini)
        {

        }
    }
}

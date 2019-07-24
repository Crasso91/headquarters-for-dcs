using Headquarters4DCS.Library;
using System;
using System.ComponentModel;

namespace Headquarters4DCS.Template
{
    public abstract class HQTemplateNode
    {
        protected readonly DefinitionTheaterNode NodeDefinition;

        [Browsable(false)]
        public string ID { get { return NodeDefinition.ID; } }

        protected string INISection { get { return $"node_{ID.ToLowerInvariant()}"; } }

        public HQTemplateNode(DefinitionTheaterNode nodeDefinition)
        {
            NodeDefinition = nodeDefinition;
            Clear();
        }

        protected abstract void Clear();
        public abstract void LoadFromFile(INIFile ini);
        public abstract void SaveToFile(INIFile ini);
    }
}

using Headquarters4DCS.Enums;
using Headquarters4DCS.Library;
using Headquarters4DCS.TypeConverters;
using System.ComponentModel;
using System.Drawing.Design;

namespace Headquarters4DCS.Template
{
    public sealed class HQTemplateNodeLocation : HQTemplateNode
    {
        private DefinitionTheaterNodeLocation LocationDefinition { get { return (DefinitionTheaterNodeLocation)NodeDefinition; } }

        [Category("Information"), DisplayName("(Name)")]
        public string Name { get { return LocationDefinition.Name; } }

        [Category("Settings"), DisplayName("Features")]
        [TypeConverter(typeof(ArrayTypeConverter<string>)), TheaterNodeType(TheaterNodeType.Land)]
        [Editor(typeof(UITypeEditorNodeFeatures), typeof(UITypeEditor))]
        public string[] Features { get; set; } = new string[0];

        public HQTemplateNodeLocation(DefinitionTheaterNode nodeDefinition) : base(nodeDefinition) { }

        protected override void Clear()
        {
            Features = new string[0];
        }

        public override void LoadFromFile(INIFile ini)
        {
            Clear();

            Features = ini.GetValueArray<string>(INISection, "Features");
        }

        public override void SaveToFile(INIFile ini)
        {
            if (Features.Length > 0) ini.SetValueArray<string>(INISection, "Features", Features);
        }
    }
}

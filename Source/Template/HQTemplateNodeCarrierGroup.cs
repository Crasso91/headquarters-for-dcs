//using System.ComponentModel;
//using System.Drawing.Design;
//using Headquarters4DCS.Library;
//using Headquarters4DCS.TypeConverters;

//namespace Headquarters4DCS.Template
//{
//    public sealed class HQTemplateNodeCarrierGroup : HQTemplateNode
//    {
//        private DefinitionTheaterNodeCarrierLocation CarrierDefinition { get { return (DefinitionTheaterNodeCarrierLocation)NodeDefinition; } }

//        [TypeConverter(typeof(ArrayTypeConverter<string>))]
//        [Editor(typeof(DefinitionListTypeEditor), typeof(UITypeEditor))]
//        public string[] Features { get; set; } = new string[0];

//        [TypeConverter(typeof(ArrayTypeConverter<HQTemplatePlayerFlightGroup>))]
//        [Editor(typeof(UITypeEditorPlayerFlightGroups), typeof(UITypeEditor))]
//        public HQTemplatePlayerFlightGroup[] PlayerFlightGroups { get; set; } = new HQTemplatePlayerFlightGroup[0];

//        public HQTemplateNodeCarrierGroup(DefinitionTheaterNode nodeDefinition) : base(nodeDefinition) { }

//        protected override void Clear()
//        {
//            PlayerFlightGroups = new HQTemplatePlayerFlightGroup[0];
//        }

//        public override void LoadFromFile(INIFile ini)
//        {
//            Clear();
//        }

//        public override void SaveToFile(INIFile ini)
//        {
//        }
//    }
//}

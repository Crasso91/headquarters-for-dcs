using Headquarters4DCS.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Headquarters4DCS.TypeConverters
{
    public sealed class UITypeEditorNodeFeatures : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (!(value is string[])) return value;

            string[] valueArr = (string[])value;

            if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService svc)
            {
                using (UIEditorFormNodeFeatures form = new UIEditorFormNodeFeatures())
                {
                    form.NodeTypes = ((TheaterNodeTypeAttribute)context.PropertyDescriptor.Attributes[typeof(TheaterNodeTypeAttribute)]).NodeTypes;
                    form.Values = valueArr;
                    if (svc.ShowDialog(form) == DialogResult.OK)
                        value = form.Values;
                }
            }

            return value;
        }
    }
}

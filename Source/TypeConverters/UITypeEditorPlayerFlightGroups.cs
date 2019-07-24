using Headquarters4DCS.Forms;
using Headquarters4DCS.Template;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Headquarters4DCS.TypeConverters
{
    public sealed class UITypeEditorPlayerFlightGroups : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        { return UITypeEditorEditStyle.Modal; }

        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (!(value is HQTemplatePlayerFlightGroup[])) return value;

            HQTemplatePlayerFlightGroup[] valueArr = (HQTemplatePlayerFlightGroup[])value;

            if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService svc)
            {
                using (FormPlayerFlightGroups form = new FormPlayerFlightGroups())
                {
                    form.Values = valueArr;
                    if (svc.ShowDialog(form) == DialogResult.OK)
                        value = form.Values;
                }
            }

            return value;
        }
    }
}

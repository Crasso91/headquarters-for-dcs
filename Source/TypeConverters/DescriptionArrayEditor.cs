using System;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Headquarters4DCS.TypeConverters
{
    public sealed class DescriptionArrayEditor : ArrayEditor
    {
        public DescriptionArrayEditor(Type type) : base(type) { }

        protected override CollectionForm CreateCollectionForm()
        {
            CollectionForm form = base.CreateCollectionForm();
            form.Shown += delegate { ShowDescription(form); };
            return form;
        }

        private static void ShowDescription(Control control)
        {
            if (control is PropertyGrid grid)
            {
                grid.HelpVisible = true;
                grid.PropertySort = PropertySort.Alphabetical;
                grid.ToolbarVisible = false;
            }

            foreach (Control child in control.Controls)
                ShowDescription(child);
        }
    }
}

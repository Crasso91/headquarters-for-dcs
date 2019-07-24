/*
==========================================================================
This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
Eagle Dynamics' DCS World flight simulator.

HQ4DCS was created by Ambroise Garel (@akaAgar).
You can find more information about the project on its GitHub page,
https://akaAgar.github.io/headquarters-for-dcs

HQ4DCS is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

HQ4DCS is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with HQ4DCS. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

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

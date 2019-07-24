using Headquarters4DCS.Enums;
using Headquarters4DCS.Library;
using Headquarters4DCS.Template;
using Headquarters4DCS.TypeConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class FormPlayerFlightGroups : Form
    {
        public HQTemplatePlayerFlightGroup[] Values = new HQTemplatePlayerFlightGroup[0];

        public FormPlayerFlightGroups()
        {
            InitializeComponent();
        }

        private void FormPlayerFlightGroups_Load(object sender, EventArgs e)
        {
            AddGroupToolStripButton.Image = UITools.GetImageFromResource("Icons.add.png");
            RemoveGroupToolStripButton.Image = UITools.GetImageFromResource("Icons.delete.png");

            ((DataGridViewComboBoxColumn)FlightGroupsDataGridView.Columns[0]).Items.AddRange((from DefinitionUnit u in HQLibrary.Instance.GetAllDefinitions<DefinitionUnit>() where u.AircraftPlayerControllable select u.ID).ToArray());
            ((DataGridViewComboBoxColumn)FlightGroupsDataGridView.Columns[1]).Items.AddRange("1", "2", "3", "4");
            ((DataGridViewComboBoxColumn)FlightGroupsDataGridView.Columns[2]).Items.AddRange(Enum.GetNames(typeof(PlayerFlightGroupTask)));
            ((DataGridViewComboBoxColumn)FlightGroupsDataGridView.Columns[4]).Items.AddRange(Enum.GetNames(typeof(PlayerFlightGroupStartLocation)));

            foreach (HQTemplatePlayerFlightGroup v in Values)
                FlightGroupsDataGridView.Rows.Add(v.AircraftType, v.Count.ToString(), v.Task.ToString(), v.AIWingmen, v.StartLocation.ToString());
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender == OkButton)
            {
                List<HQTemplatePlayerFlightGroup> newValues = new List<HQTemplatePlayerFlightGroup>();

                foreach (DataGridViewRow r in FlightGroupsDataGridView.Rows)
                    newValues.Add(
                        new HQTemplatePlayerFlightGroup(
                            r.Cells[0].Value.ToString(),
                            HQTools.StringToInt(r.Cells[1].Value.ToString()),
                            (PlayerFlightGroupTask)Enum.Parse(typeof(PlayerFlightGroupTask), r.Cells[2].Value.ToString(), true),
                            HQTools.StringToBool(r.Cells[3].Value.ToString()),
                            (PlayerFlightGroupStartLocation)Enum.Parse(typeof(PlayerFlightGroupStartLocation), r.Cells[4].Value.ToString(), true)
                            ));

                Values = newValues.ToArray();
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.Cancel;

            Close();
        }

        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            if (sender == AddGroupToolStripButton)
                FlightGroupsDataGridView.Rows.Add(
                    HQTemplate.DEFAULT_AIRCRAFT,
                    "1",
                    HQTemplatePlayerFlightGroup.DEFAULT_TASK.ToString(),
                    false,
                    HQTemplatePlayerFlightGroup.DEFAULT_START_LOCATION.ToString());
            else if (sender == RemoveGroupToolStripButton)
            {
                if (FlightGroupsDataGridView.SelectedRows.Count == 0) return;
                FlightGroupsDataGridView.Rows.Remove(FlightGroupsDataGridView.SelectedRows[0]);
            }
        }
    }
}

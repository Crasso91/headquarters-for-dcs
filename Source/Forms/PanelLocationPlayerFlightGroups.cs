using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Template;
using Headquarters4DCS.TypeConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class PanelLocationPlayerFlightGroups : Form
    {
        private readonly MissionTemplateLocation EditedNode;

        public PanelLocationPlayerFlightGroups(MissionTemplateLocation editedNode)
        {
            InitializeComponent();
            EditedNode = editedNode; // TODO: clone
        }

        private void FormPlayerFlightGroups_Load(object sender, EventArgs e)
        {
            AddGroupToolStripButton.Image = GUITools.GetImageFromResource("Icons.add.png");
            RemoveGroupToolStripButton.Image = GUITools.GetImageFromResource("Icons.delete.png");

            ((DataGridViewComboBoxColumn)FlightGroupsDataGridView.Columns[0]).Items.AddRange((from DefinitionUnit u in Library.Instance.GetAllDefinitions<DefinitionUnit>() where u.AircraftPlayerControllable select u.ID).ToArray());
            ((DataGridViewComboBoxColumn)FlightGroupsDataGridView.Columns[1]).Items.AddRange("1", "2", "3", "4");
            ((DataGridViewComboBoxColumn)FlightGroupsDataGridView.Columns[2]).Items.AddRange(Enum.GetNames(typeof(PlayerFlightGroupTask)));
            ((DataGridViewComboBoxColumn)FlightGroupsDataGridView.Columns[4]).Items.AddRange(Enum.GetNames(typeof(PlayerFlightGroupStartLocation)));

            foreach (MissionTemplatePlayerFlightGroup v in EditedNode.PlayerFlightGroups)
                FlightGroupsDataGridView.Rows.Add(v.AircraftType, v.Count.ToString(), v.Task.ToString(), v.AIWingmen, v.StartLocation.ToString());
        }

        public MissionTemplatePlayerFlightGroup[] GetFlightGroups()
        {
            List<MissionTemplatePlayerFlightGroup> flightGroups = new List<MissionTemplatePlayerFlightGroup>();

            foreach (DataGridViewRow r in FlightGroupsDataGridView.Rows)
                flightGroups.Add(
                    new MissionTemplatePlayerFlightGroup(
                        r.Cells[0].Value.ToString(),
                        HQTools.StringToInt(r.Cells[1].Value.ToString()),
                        (PlayerFlightGroupTask)Enum.Parse(typeof(PlayerFlightGroupTask), r.Cells[2].Value.ToString(), true),
                        HQTools.StringToBool(r.Cells[3].Value.ToString()),
                        (PlayerFlightGroupStartLocation)Enum.Parse(typeof(PlayerFlightGroupStartLocation), r.Cells[4].Value.ToString(), true)));

            return flightGroups.ToArray();
        }

        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            if (sender == AddGroupToolStripButton)
                FlightGroupsDataGridView.Rows.Add(
                    Library.Instance.Common.DefaultPlayerAircraft,
                    "1",
                    Library.Instance.Common.DefaultPlayerFlightGroupTask.ToString(),
                    false,
                    Library.Instance.Common.DefaultPlayerFlightGroupStartLocation.ToString());
            else if (sender == RemoveGroupToolStripButton)
            {
                if (FlightGroupsDataGridView.SelectedRows.Count == 0) return;
                FlightGroupsDataGridView.Rows.Remove(FlightGroupsDataGridView.SelectedRows[0]);
            }
        }
    }
}

using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Template;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class PanelMainFormSidePanel : Form
    {
        private readonly FormMain MainForm = null;
        private MissionTemplate Template { get { return MainForm.Template; } }
        private string SelectedLocationID { get { return MainForm.SelectedLocationID; } }

        private DefinitionTheater Theater { get { return Library.Instance.GetDefinition<DefinitionTheater>(Template.Theater); } }

        private readonly SidePanelLocationContextMenu LocationsContextMenu = null;

        private bool DisableTheaterUpdates = false;

        public PanelMainFormSidePanel(FormMain mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
            LocationsContextMenu = new SidePanelLocationContextMenu(MainForm, LocationsTreeView, SidePanelImageList);
        }

        private void Event_Form_Load(object sender, EventArgs e)
        {
            TemplateSettingsPropertyGrid.SelectedObject = Template.Settings;

            foreach (string s in GUITools.GetAllResourceKeys("LocationsIcons."))
                SidePanelImageList.Images.Add(s.Substring("LocationsIcons.".Length).Replace(".png", ""), GUITools.GetImageFromResource(s));

            CollapseAllLocationsToolStripButton.Image = GUITools.GetImageFromResource("Icons.collapseAll.png");
            ExpandAllLocationsToolStripButton.Image = GUITools.GetImageFromResource("Icons.expandAll.png");
        }

        public void UpdateTheater(TheaterUpdateType updateType)
        {
            if (DisableTheaterUpdates) return;

            TemplateSettingsPropertyGrid.Refresh();

            if (updateType == TheaterUpdateType.Full)
            {
                LocationsTreeView.Nodes.Clear();
                foreach (DefinitionTheaterLocation location in Theater.Locations.Values)
                {
                    TreeNode locationTreeNode = new TreeNode(location.DisplayName)
                    {
                        Name = location.ID,
                        Tag = location,
                    };
                    LocationsTreeView.Nodes.Add(locationTreeNode);
                }

                LocationsTreeView.Sort();
            }

            if (Theater == null) return;

            foreach (TreeNode n in LocationsTreeView.Nodes)
            {
                if ((updateType == TheaterUpdateType.SelectedLocation) && (n.Name != SelectedLocationID)) continue;

                n.Nodes.Clear();
                if (!Template.Locations.ContainsKey(n.Name)) continue;

                if (n.NodeFont != null) { n.NodeFont.Dispose(); n.NodeFont = null; }
                n.Text = Template.Locations[n.Name].Definition.DisplayName;
                    n.ImageKey = "location";

                if (Template.Locations[n.Name].Definition.LocationType == TheaterLocationType.Airbase)
                {
                    n.ImageKey = "airbase";
                    n.Text += $" ({Template.Locations[n.Name].Coalition.ToString().ToUpperInvariant()})";
                }

                foreach (DefinitionFeature feature in Template.Locations[n.Name].GetFeaturesDefinitions())
                {
                    TreeNode featureNode = n.Nodes.Add(feature.GetDisplayNameWithCategory());
                    featureNode.Tag = feature.ID;
                    featureNode.ImageKey = $"feature{feature.Category}";
                    featureNode.SelectedImageKey = $"feature{feature.Category}";
                    featureNode.ToolTipText = "Mission feature - Right-click for options";
                }
                // TODO: sort

                for (int i = 0; i < Template.Locations[n.Name].PlayerFlightGroups.Count; i++)
                {
                    TreeNode playerFlightGroupsNode = n.Nodes.Add(Template.Locations[n.Name].PlayerFlightGroups[i].ToString());
                    playerFlightGroupsNode.Tag = i;
                    playerFlightGroupsNode.ImageKey = $"playerFlightGroup";
                    playerFlightGroupsNode.SelectedImageKey = $"playerFlightGroup";
                    playerFlightGroupsNode.ToolTipText = "Player flight group - Right-click for options";
                }

                n.SelectedImageKey = n.ImageKey;

                if (n.Nodes.Count > 0)
                    n.ToolTipText = "Double-click to expand/collapse, right-click for options";
                else
                    n.ToolTipText = "Right-click for options";
            }

            if (SelectedLocationID == null)
                LocationsTreeView.SelectedNode = null;
            else if (LocationsTreeView.Nodes.ContainsKey(SelectedLocationID))
            {
                LocationsTreeView.SelectedNode = LocationsTreeView.Nodes[SelectedLocationID];
                LocationsTreeView.Nodes[SelectedLocationID].EnsureVisible();
                LocationsTreeView.Nodes[SelectedLocationID].ExpandAll();
                MainTabControl.SelectedTab = LocationsTabPage;
                LocationsTreeView.Focus();
            }
        }

        private void Event_LocationsTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if ((e.Button != MouseButtons.Right) || (e.Node == null)) return;
            LocationsTreeView.SelectedNode = e.Node;
            LocationsContextMenu.Show(e.Location);
        }

        private void Event_LocationsToolStripButtons_Click(object sender, EventArgs e)
        {
            if (sender == CollapseAllLocationsToolStripButton)
                LocationsTreeView.CollapseAll();
            else if (sender == ExpandAllLocationsToolStripButton)
                LocationsTreeView.ExpandAll();
        }

        private void Event_LocationsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
                MainForm.SelectedLocationID = null;
            else
                MainForm.SelectedLocationID = GUITools.GetTopLevelNode(e.Node).Name;

            DisableTheaterUpdates = true;
            MainForm.UpdateTheater(TheaterUpdateType.SelectedLocation);
            DisableTheaterUpdates = false;
        }

        private void TemplateSettingsPropertyGrid_Click(object sender, EventArgs e)
        {

        }
    }
}

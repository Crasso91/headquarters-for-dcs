using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Template;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class PanelMainFormSidePanel : Form
    {
        private readonly FormMain MainForm = null;
        private MissionTemplate Template { get { return MainForm.Template; } }
        private string SelectedNodeID { get { return MainForm.SelectedLocationID; } }

        private DefinitionTheater Theater { get { return Library.Instance.GetDefinition<DefinitionTheater>(Template.Theater); } }

        private bool DisableAutoSelect = false;

        public PanelMainFormSidePanel(FormMain mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;

            TemplateSettingsPropertyGrid.MouseEnter += new EventHandler(SidePanelMouseEnter);
        }

        private void FormLoad(object sender, EventArgs e)
        {
            TemplateSettingsPropertyGrid.SelectedObject = Template.Settings;

            SidePanelimageList.Images.Add("airbase", GUITools.GetImageFromResource("MapIcons.airbase.png"));
            SidePanelimageList.Images.Add("airbase_blue", GUITools.GetImageFromResource("MapIcons.airbase_blue.png"));
            SidePanelimageList.Images.Add("airbase_red", GUITools.GetImageFromResource("MapIcons.airbase_red.png"));
            SidePanelimageList.Images.Add("location", GUITools.GetImageFromResource("MapIcons.location.png"));

            RefreshTheaterValues();
        }

        public void UpdateTheater(bool fullUpdate)
        {
            NodesTreeView.Nodes.Clear();
            DefinitionTheater theater = Theater;
            if (theater == null) return;

            foreach (DefinitionTheaterLocation n in theater.Locations.Values)
                NodesTreeView.Nodes.Add(n.ID, n.DisplayName);

            NodesTreeView.Sort();
        }

        public void RefreshTheaterValues()
        {
            DefinitionTheater theater = Theater;
            if (theater == null) return;

            TemplateSettingsPropertyGrid.Refresh();

            foreach (TreeNode n in NodesTreeView.Nodes)
            {
                if (!Template.Locations.ContainsKey(n.Name)) continue;

                if (n.NodeFont != null) { n.NodeFont.Dispose(); n.NodeFont = null; }
                n.NodeFont = new Font(NodesTreeView.Font, Template.Locations[n.Name].InUse ? FontStyle.Bold : FontStyle.Regular);

                switch (Template.Locations[n.Name].Definition.LocationType)
                {
                    case TheaterLocationType.Airbase:
                        switch (Template.Locations[n.Name].Coalition)
                        {
                            default: n.ImageKey = "airbase"; break;
                            case CoalitionNeutral.Blue: n.ImageKey = "airbase_blue"; break;
                            case CoalitionNeutral.Red: n.ImageKey = "airbase_red"; break;
                        }
                        break;
                    default:
                        n.ImageKey = "location";
                        break;
                }

                n.SelectedImageKey = n.ImageKey;
            }
        }

        private void NodesTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right) NodesTreeViewNodeMouseDoubleClick(sender, e);
        }

        private void NodesTreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null) return;
            if (!Template.Locations.ContainsKey(e.Node.Name)) return;
            NodesTreeView.SelectedNode = e.Node;

            using (FormLocationEditor nodeEditorForm = new FormLocationEditor(Template.Locations[e.Node.Name]))
            {
                if (nodeEditorForm.ShowDialog() == DialogResult.OK)
                {
                    Template.Locations[e.Node.Name] = nodeEditorForm.EditedLocation;
                    RefreshTheaterValues();
                }
            }
        }

        public void SidePanelMouseEnter(object sender, EventArgs e)
        {
            if (MainTabControl.SelectedTab == MissionTabPage)
                MainForm.SetStatusBarMessage("This panel allows you to change global mission settings.");
            else if (MainTabControl.SelectedTab == LocationsTabPage)
                MainForm.SetStatusBarMessage("Left-click a location to select it. Double-click or right-click a location to edit it.");
            else
                MainForm.SetStatusBarMessage("");
        }

        public void UpdateSelectedLocation()
        {
            DisableAutoSelect = true;
            if (MainForm.SelectedLocationID == null)
                NodesTreeView.SelectedNode = null;
            else if (NodesTreeView.Nodes.ContainsKey(MainForm.SelectedLocationID))
            {
                NodesTreeView.SelectedNode = NodesTreeView.Nodes[MainForm.SelectedLocationID];
                NodesTreeView.Nodes[MainForm.SelectedLocationID].EnsureVisible();
                MainTabControl.SelectedTab = LocationsTabPage;
            }
            DisableAutoSelect = false;
        }

        private void NodesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (DisableAutoSelect) return;

            if (e.Node == null)
                MainForm.UpdateSelectedLocation(null);
            else
                MainForm.UpdateSelectedLocation(e.Node.Name);
        }
    }
}

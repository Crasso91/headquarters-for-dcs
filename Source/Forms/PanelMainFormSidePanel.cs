using Headquarters4DCS.Library;
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
        private string SelectedNodeID { get { return MainForm.SelectedNodeID; } }

        private DefinitionTheater Theater { get { return HQLibrary.Instance.GetDefinition<DefinitionTheater>(Template.Theater); } }

        public PanelMainFormSidePanel(FormMain mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;

            TemplateSettingsPropertyGrid.MouseEnter += new EventHandler(SidePanelMouseEnter);
        }

        private void FormLoad(object sender, EventArgs e)
        {
            TemplateSettingsPropertyGrid.SelectedObject = Template.Settings;
            //TemplateSettingsPropertyGrid.Font = new Font(TemplateSettingsPropertyGrid.Font.FontFamily, TemplateSettingsPropertyGrid.Font.Size * 1.25f);

            SidePanelimageList.Images.Add("airbase", GUITools.GetImageFromResource("MapIcons.airbase.png"));
            SidePanelimageList.Images.Add("airbase_blue", GUITools.GetImageFromResource("MapIcons.airbase_blue.png"));
            SidePanelimageList.Images.Add("airbase_red", GUITools.GetImageFromResource("MapIcons.airbase_red.png"));
        }

        public void UpdateTheater(bool fullUpdate)
        {
            NodesTreeView.Nodes.Clear();
            DefinitionTheater theater = Theater;
            if (theater == null) return;

            foreach (DefinitionTheaterNode n in theater.Nodes.Values)
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
                if (!Template.Nodes.ContainsKey(n.Name)) continue;

                if (n.NodeFont != null) { n.NodeFont.Dispose(); n.NodeFont = null; }
                n.NodeFont = new Font(NodesTreeView.Font, Template.Nodes[n.Name].InUse ? FontStyle.Bold : FontStyle.Regular);

                if (Template.Nodes[n.Name] is MissionTemplateNodeAirbase airbaseNode)
                {
                    switch (airbaseNode.Coalition)
                    {
                        default: n.ImageKey = "airbase"; break;
                        case CoalitionNeutral.Blue: n.ImageKey = "airbase_blue"; break;
                        case CoalitionNeutral.Red: n.ImageKey = "airbase_red"; break;
                    }
                }

                n.SelectedImageKey = n.ImageKey;
            }
        }

        public void UpdateLanguage()
        {
            SettingsTabPage.Text = GUITools.Language.GetString("UserInterface", "Misc.MissionSettings");
            NodesTabPage.Text = GUITools.Language.GetString("UserInterface", "Misc.Nodes");
            TemplateSettingsPropertyGrid.SelectedObject = Template.Settings;
            RefreshTheaterValues();
            NodesTreeView.Sort();
        }

        private void NodesTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right) NodesTreeViewNodeMouseDoubleClick(sender, e);
        }

        private void NodesTreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null) return;
            if (!Template.Nodes.ContainsKey(e.Node.Name)) return;
            NodesTreeView.SelectedNode = e.Node;

            using (FormNodeEditor nodeEditorForm = new FormNodeEditor(Template.Nodes[e.Node.Name]))
            {
                if (nodeEditorForm.ShowDialog() == DialogResult.OK)
                {
                    Template.Nodes[e.Node.Name] = nodeEditorForm.EditedNode;
                    //UpdateMap();
                    //UpdateStatusBar();
                    RefreshTheaterValues();
                }
            }
        }

        public void SidePanelMouseEnter(object sender, EventArgs e)
        {
            MainForm.SetStatusBarMessage(MainTabControl.SelectedTab.Text);
        }

        public void UpdateSelectedNode()
        {
            if (MainForm.SelectedNodeID == null)
            {
                NodesTreeView.SelectedNode = null;
                return;
            }

            if (!NodesTreeView.Nodes.ContainsKey(MainForm.SelectedNodeID)) return;

            NodesTreeView.SelectedNode = NodesTreeView.Nodes[MainForm.SelectedNodeID];
            NodesTreeView.Nodes[MainForm.SelectedNodeID].EnsureVisible();
            MainTabControl.SelectedTab = NodesTabPage;
        }
    }
}

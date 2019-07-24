using Headquarters4DCS.Library;
using Headquarters4DCS.Template;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class FormMain : Form
    {
        private readonly HQ4DCS HQ = null;

        private HQTemplate Mission = null;

        private readonly FormMapDrawer MapDrawer = null;

        private string LastSaveFilePath = null;

        private string SelectedNodeID
        {
            get
            {
                string selectedNodeID = null;
                if ((SettingsPropertyGrid.SelectedObject != null) &&
                    (SettingsPropertyGrid.SelectedObject is HQTemplateNode node))
                    selectedNodeID = node.ID;
                return selectedNodeID;
            }
        }

        public FormMain(HQ4DCS hq)
        {
            InitializeComponent();

            HQ = hq;
            Mission = new HQTemplate(HQLibrary.Instance);
            SettingsPropertyGrid.SelectedObject = Mission.Settings;

            MapDrawer = new FormMapDrawer(HQLibrary.Instance, Mission);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadIcons();

            UpdateMap();
            UpdateFormTitle();

            foreach (string s in HQLibrary.Instance.GetAllDefinitionIDs<DefinitionTheater>())
                MenuFileNew.DropDownItems.Add(s);

            //SettingsPropertyGrid.Font = new Font(SettingsPropertyGrid.Font.FontFamily, SettingsPropertyGrid.Font.Size * 1.2f);
        }

        private void LoadIcons()
        {
            Icon = UITools.GetIconFromResource("Icon.ico");

            MenuFileNew.Image = UITools.GetImageFromResource("Icons.new.png");
            MenuFileOpen.Image = UITools.GetImageFromResource("Icons.load.png");
            MenuFileSave.Image = UITools.GetImageFromResource("Icons.save.png");
            MenuFileSaveAs.Image = UITools.GetImageFromResource("Icons.save_as.png");
            MenuFileGenerate.Image = UITools.GetImageFromResource("Icons.generate.png");
            MenuFileExit.Image = UITools.GetImageFromResource("Icons.exit.png");

            ToolStripButtonFileNew.Image = MenuFileNew.Image;
            ToolStripButtonFileOpen.Image = MenuFileOpen.Image;
            ToolStripButtonFileSave.Image = MenuFileSave.Image;
            ToolStripButtonFileSaveAs.Image = MenuFileSaveAs.Image;
            ToolStripButtonFileGenerate.Image = MenuFileGenerate.Image;

            ToolStripButtonZoomIn.Image = UITools.GetImageFromResource("Icons.zoom_in.png");
            ToolStripButtonZoomOut.Image = UITools.GetImageFromResource("Icons.zoom_out.png");
        }

        private void UpdateMap()
        {
            MapDrawer.UpdateImage(SelectedNodeID);
            MapPictureBox.Image = MapDrawer.Image;
        }

        private void UpdateFormTitle()
        {
            Text =
                $"Headquarters {HQ4DCS.HQ4DCS_VERSION_STRING} for DCS World {HQ4DCS.DCSWORLD_TARGETED_VERSION} - " +
                (string.IsNullOrEmpty(LastSaveFilePath) ? "New mission" : Path.GetFileName(LastSaveFilePath));
        }

        private void MenuClick(object sender, EventArgs e)
        {
            string senderName = ((ToolStripItem)sender).Name;

            switch (senderName)
            {
                case "MenuFileOpen":
                case "ToolStripButtonFileOpen":
                    string fileToLoad = UITools.ShowOpenFileDialog("hqt", HQTools.PATH_TEMPLATES, "HQT files");
                    if (fileToLoad == null) return;
                    Mission.LoadFromFile(fileToLoad);
                    LastSaveFilePath = fileToLoad;
                    UpdateMap();
                    UpdateFormTitle();
                    return;
                case "MenuFileSave":
                case "ToolStripButtonFileSave":
                    if (string.IsNullOrEmpty(LastSaveFilePath)) { MenuClick(MenuFileSaveAs, e); return; }
                    return;
                case "MenuFileSaveAs":
                case "ToolStripButtonFileSaveAs":
                    string fileToSave = UITools.ShowSaveFileDialog(
                        "hqt", HQTools.PATH_TEMPLATES,
                        string.IsNullOrEmpty(LastSaveFilePath) ? "NewMission.hqt" : Path.GetFileName(LastSaveFilePath),
                        "HQT files");
                    if (fileToSave == null) return;
                    Mission.SaveToFile(fileToSave);
                    LastSaveFilePath = fileToSave;
                    UpdateFormTitle();
                    return;
                case "MenuFileExit":
                    Close();
                    return;
                case "MenuFileGenerate":
                case "ToolStripButtonFileGenerate":
                    using (FormMissionOutput formOutput = new FormMissionOutput()) { formOutput.ShowDialog(); }
                    return;
                case "MenuEditInvertAirbasesCoalition":
                    Mission.InvertAirbasesCoalition();
                    UpdateMap();
                    SettingsPropertyGrid.Refresh();
                    return;
                case "ToolStripButtonZoomIn":
                    MapDrawer.Zoom++;
                    UpdateMap();
                    return;
                case "ToolStripButtonZoomOut":
                    MapDrawer.Zoom--;
                    UpdateMap();
                    return;
            }
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (SelectedNodeID == null) return;
                    SettingsPropertyGrid.SelectedObject = Mission.Settings;
                    UpdateMap(); return;
                case Keys.Add:
                    MapDrawer.Zoom++; UpdateMap(); return;
                case Keys.Subtract:
                    MapDrawer.Zoom--; UpdateMap(); return;
            }
        }

        private void MapPictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            DefinitionTheaterNode node = GetNodeOnMapPosition(e.X, e.Y);

            if (node == null)
                SettingsPropertyGrid.SelectedObject = Mission.Settings;
            else
                SettingsPropertyGrid.SelectedObject = Mission.Nodes[node.ID];

            UpdateMap();
        }

        private void AddContextMenuItem(string action, string parameter, string text)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text) { Name = action, Tag = parameter };
            MapContextMenuStrip.Items.Add(item);
        }

        private DefinitionTheaterNode GetNodeOnMapPosition(int x, int y, int radius = 16)
        {
            DefinitionTheater theater = HQLibrary.Instance.GetDefinition<DefinitionTheater>("Caucasus");
            Coordinates position = new Coordinates(x, y) / MapDrawer.ZoomMultiplier;

            DefinitionTheaterNode[] nodes = (from DefinitionTheaterNode n in theater.Nodes.Values where n.MapPosition.GetDistanceFrom(position) < radius select n).ToArray();
            if (nodes.Length == 0) return null;

            for (int i = 0; i < nodes.Length; i++)
            {
                // Iterate through nearby objects when clicking multiple times
                if ((nodes[i].ID == SelectedNodeID) && (i < nodes.Length - 1)) return nodes[i + 1];
            }

            return nodes[0];
        }

        private void SettingsPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateMap();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO
        }

        private void MenuFileNewDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null) return;
            // TODO: confirmation message box

            Mission.Clear(e.ClickedItem.Text);
            UpdateMap();
            UpdateFormTitle();
        }
    }
}

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

        public MissionTemplate Template { get; private set; } = null;

        //private readonly GUIMapDrawer MapDrawer = null;

        private string LastSaveFilePath = null;

        public string SelectedNodeID { get; private set; } = null;

        private readonly PanelMainFormMap MapPanel = null;
        private readonly PanelMainFormSidePanel SidePanel = null;


        public FormMain(HQ4DCS hq)
        {
            InitializeComponent();
            SetStatusBarMessage("");

            HQ = hq;
            Template = new MissionTemplate();

            MapPanel = new PanelMainFormMap(this);
            SidePanel = new PanelMainFormSidePanel(this);
            GUITools.SetupFormForPanel(MapPanel, MainSplitContainer.Panel2);
            GUITools.SetupFormForPanel(SidePanel, MainSplitContainer.Panel1);

            LoadIcons();
        }

        public void SetStatusBarMessage(string message)
        {
            StatusStripLabelInfo.Text = message;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            foreach (string s in HQLibrary.Instance.GetAllDefinitionIDs<DefinitionTheater>())
            {
                MenuFileNew.DropDownItems.Add(s);
                ToolStripButtonFileNew.DropDownItems.Add(s);
            }

            foreach (string s in HQLibrary.Instance.GetAllDefinitionIDs<DefinitionLanguage>())
            {
                ToolStripMenuItem languageItem = new ToolStripMenuItem(s) { Name = s };
                MenuLanguage.DropDownItems.Add(languageItem);
            }

            MapPanel.UpdateTheater(true);
            SidePanel.UpdateTheater(true);

            UpdateLanguage();
            UpdateFormTitle();
            UpdateTheater(true);
        }

        private void UpdateLanguage()
        {
            foreach (ToolStripMenuItem tsmi in MenuLanguage.DropDownItems)
                tsmi.Checked = (tsmi.Name == GUITools.Language.ID);

            MenuFile.Text = GUITools.Language.GetString("UserInterface", "Menu.File");
            MenuFileNew.Text = GUITools.Language.GetString("UserInterface", "Menu.File.New");
            MenuFileOpen.Text = GUITools.Language.GetString("UserInterface", "Menu.File.Open");
            MenuFileSave.Text = GUITools.Language.GetString("UserInterface", "Menu.File.Save");
            MenuFileSaveAs.Text = GUITools.Language.GetString("UserInterface", "Menu.File.SaveAs");
            MenuFileGenerate.Text = GUITools.Language.GetString("UserInterface", "Menu.File.Generate");
            MenuFileExit.Text = GUITools.Language.GetString("UserInterface", "Menu.File.Exit");

            MenuEdit.Text = GUITools.Language.GetString("UserInterface", "Menu.Edit");
            MenuEditInvertAirbasesCoalition.Text = GUITools.Language.GetString("UserInterface", "Menu.Edit.InvertAirbaseCoalitions");

            MenuLanguage.Text = GUITools.Language.GetString("UserInterface", "Menu.Language");

            MenuHelp.Text = GUITools.Language.GetString("UserInterface", "Menu.Help");
            MenuHelpWebsite.Text = GUITools.Language.GetString("UserInterface", "Menu.Help.Website");
            MenuHelpAbout.Text = GUITools.Language.GetString("UserInterface", "Menu.Help.About");

            ToolStripButtonFileNew.Text = MenuFileNew.Text.Replace("&", "");
            ToolStripButtonFileOpen.Text = MenuFileOpen.Text.Replace("&", "");
            ToolStripButtonFileSave.Text = MenuFileSave.Text.Replace("&", "");
            ToolStripButtonFileSaveAs.Text = MenuFileSaveAs.Text.Replace("&", "");
            ToolStripButtonFileGenerate.Text = MenuFileGenerate.Text.Replace("&", "");
            ToolStripButtonZoomIn.Text = GUITools.Language.GetString("UserInterface", "Misc.ZoomIn");
            ToolStripButtonZoomOut.Text = GUITools.Language.GetString("UserInterface", "Misc.ZoomOut");

            SidePanel.UpdateLanguage();
        }

        private void LoadIcons()
        {
            Icon = GUITools.GetIconFromResource("Icon.ico");

            MenuFileNew.Image = GUITools.GetImageFromResource("Icons.new.png");
            MenuFileOpen.Image = GUITools.GetImageFromResource("Icons.load.png");
            MenuFileSave.Image = GUITools.GetImageFromResource("Icons.save.png");
            MenuFileSaveAs.Image = GUITools.GetImageFromResource("Icons.save_as.png");
            MenuFileGenerate.Image = GUITools.GetImageFromResource("Icons.generate.png");
            MenuFileExit.Image = GUITools.GetImageFromResource("Icons.exit.png");

            MenuEditInvertAirbasesCoalition.Image = GUITools.GetImageFromResource("Icons.switchSides.png");

            ToolStripButtonFileNew.Image = MenuFileNew.Image;
            ToolStripButtonFileOpen.Image = MenuFileOpen.Image;
            ToolStripButtonFileSave.Image = MenuFileSave.Image;
            ToolStripButtonFileSaveAs.Image = MenuFileSaveAs.Image;
            ToolStripButtonFileGenerate.Image = MenuFileGenerate.Image;

            ToolStripButtonZoomIn.Image = GUITools.GetImageFromResource("Icons.zoom_in.png");
            ToolStripButtonZoomOut.Image = GUITools.GetImageFromResource("Icons.zoom_out.png");
        }

        public void UpdateSelectedNode(string newSelectedNode, bool updateSidePanelSelectedNode)
        {
            SelectedNodeID = newSelectedNode;
            UpdateTheater(false);
            if (updateSidePanelSelectedNode) SidePanel.UpdateSelectedNode();
        }

        private void UpdateFormTitle()
        {
            Text =
                $"Headquarters {HQ4DCS.HQ4DCS_VERSION_STRING} for DCS World {HQ4DCS.DCSWORLD_TARGETED_VERSION} - " +
                (string.IsNullOrEmpty(LastSaveFilePath) ? GUITools.Language.GetString("UserInterface", "Misc.NewMission") : Path.GetFileName(LastSaveFilePath));
        }

        private void MenuClick(object sender, EventArgs e)
        {
            string senderName = ((ToolStripItem)sender).Name;

            switch (senderName)
            {
                case "MenuFileOpen":
                case "ToolStripButtonFileOpen":
                    string fileToLoad = GUITools.ShowOpenFileDialog("hqt", HQTools.PATH_TEMPLATES, "HQ4DCS mission templates");
                    if (fileToLoad == null) return;
                    Template.LoadFromFile(fileToLoad);
                    LastSaveFilePath = fileToLoad;
                    SelectedNodeID = null;
                    UpdateTheater(true);
                    UpdateFormTitle();
                    return;
                case "MenuFileSave":
                case "ToolStripButtonFileSave":
                    if (string.IsNullOrEmpty(LastSaveFilePath)) { MenuClick(MenuFileSaveAs, e); return; }
                    Template.SaveToFile(LastSaveFilePath);
                    return;
                case "MenuFileSaveAs":
                case "ToolStripButtonFileSaveAs":
                    string fileToSave = GUITools.ShowSaveFileDialog(
                        "hqt", HQTools.PATH_TEMPLATES,
                        string.IsNullOrEmpty(LastSaveFilePath) ? "NewMission.hqt" : Path.GetFileName(LastSaveFilePath),
                        "HQ4DCS mission templates");
                    if (fileToSave == null) return;
                    Template.SaveToFile(fileToSave);
                    LastSaveFilePath = fileToSave;
                    UpdateFormTitle();
                    return;
                case "MenuFileExit":
                    Close();
                    return;
                case "MenuFileGenerate":
                case "ToolStripButtonFileGenerate":
                    using (FormMissionOutput formOutput = new FormMissionOutput(Template)) { formOutput.ShowDialog(); }
                    return;
                case "MenuEditInvertAirbasesCoalition":
                    Template.InvertAirbasesCoalition();
                    UpdateTheater(false);
                    //SettingsPropertyGrid.Refresh();
                    return;
                    //case "ToolStripButtonZoomIn":
                    //    MapDrawer.Zoom++;
                    //    UpdateMap();
                    //    return;
                    //case "ToolStripButtonZoomOut":
                    //    MapDrawer.Zoom--;
                    //    UpdateMap();
                    //    return;
            }
        }

        private void UpdateTheater(bool fullUpdate)
        {
            MapPanel.UpdateTheater(fullUpdate);
            SidePanel.UpdateTheater(fullUpdate);
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (SelectedNodeID == null) return;
                    SelectedNodeID = null;
                    //UpdateMap();
                    //UpdateStatusBar();
                    UpdateTheater(false);
                    return;
                    //case Keys.Add:
                    //    MapDrawer.Zoom++; UpdateMap(); return;
                    //case Keys.Subtract:
                    //    MapDrawer.Zoom--; UpdateMap(); return;
            }
        }

        //private void MapPictureBoxMouseDown(object sender, MouseEventArgs e)
        //{
        //    if ((e.Button != MouseButtons.Left) && (e.Button != MouseButtons.Right)) return;

        //    SelectedNodeID = GetNodeIDOnMapPosition(e.X, e.Y, (e.Button == MouseButtons.Left));
        //    UpdateMap();
        //    UpdateStatusBar();

        //    if ((e.Button == MouseButtons.Right) && (SelectedNodeID != null))
        //    {
        //        using (FormNodeEditor nodeEditorForm = new FormNodeEditor(Template.Nodes[SelectedNodeID], Language))
        //        {
        //            if (nodeEditorForm.ShowDialog() == DialogResult.OK)
        //            {
        //                Template.Nodes[SelectedNodeID] = nodeEditorForm.EditedNode;
        //                UpdateMap();
        //                UpdateStatusBar();
        //            }
        //        }
        //    }
        //}

        private void AddContextMenuItem(string action, string parameter, string text)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text) { Name = action, Tag = parameter };
            MapContextMenuStrip.Items.Add(item);
        }

        

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
#if !DEBUG
            e.Cancel =
                (MessageBox.Show(
                    "Are you sure you want to close HQ4DCS?\r\nUnsaved changes will be lost.",
                    "Exit?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes);
#endif
        }

        private void MenuFileNewDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null) return;

#if !DEBUG
            if (MessageBox.Show(
                "Are you sure you want to create a new mission?\r\nUnsaved changes will be lost.",
                "New mission?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
#endif

            Template.Clear(e.ClickedItem.Name);
            UpdateTheater(true);
            UpdateFormTitle();
        }

        private void MenuLanguageDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null) return;
            string langID = e.ClickedItem.Name;
            if ((!HQLibrary.Instance.DefinitionExists<DefinitionLanguage>(langID)) || (langID == GUITools.Language.ID)) return;

            GUITools.Language = HQLibrary.Instance.GetDefinition<DefinitionLanguage>(langID);
            UpdateLanguage();
        }

        private void MainSplitContainer_Panel1_MouseEnter(object sender, EventArgs e)
        {
            SidePanel.SidePanelMouseEnter(sender, e);
        }
    }
}

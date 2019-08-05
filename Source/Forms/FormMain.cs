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

using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.DevTools;
using Headquarters4DCS.Template;
using System;
using System.IO;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    /// <summary>
    /// Main form of the application.
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Mission template.
        /// </summary>
        public MissionTemplate Template { get; private set; } = null;

        /// <summary>
        /// File path to the last saved mission template. Null is mission wasn't saved yet.
        /// </summary>
        private string LastSaveFilePath = null;

        /// <summary>
        /// Selected theater location. Null if none.
        /// </summary>
        public string SelectedLocationID { get; private set; } = null;

        /// <summary>
        /// Child form, right panel (map).
        /// </summary>
        private readonly PanelMainFormMap MapPanel = null;

        /// <summary>
        /// Child form, left panel (mission settings and list of locations).
        /// </summary>
        private readonly PanelMainFormSidePanel SidePanel = null;

#if !DEBUG
        /// <summary>
        /// Was the "this is a development tool" warning already displayed?
        /// </summary>
        private bool DevToolWarningAlreadyShown = false;
#endif

        /// <summary>
        /// Constructor.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            SetStatusBarMessage("");

            Template = new MissionTemplate();

            MapPanel = new PanelMainFormMap(this);
            SidePanel = new PanelMainFormSidePanel(this);
            GUITools.SetupFormForPanel(MapPanel, MainSplitContainer.Panel2);
            GUITools.SetupFormForPanel(SidePanel, MainSplitContainer.Panel1);

            LoadIcons();
        }

        /// <summary>
        /// Changes the tooltip message in the bottom status bar.
        /// </summary>
        /// <param name="message">Message to display.</param>
        public void SetStatusBarMessage(string message)
        {
            StatusStripLabelInfo.Text = message;
        }

        /// <summary>
        /// Form load event.
        /// </summary>
        /// <param name="sender">The form.</param>
        /// <param name="e">Event arguments.</param>
        private void Event_FormLoad(object sender, EventArgs e)
        {
            // Adds all available theaters add sub items to the "new mission" button and menu item
            foreach (DefinitionTheater theater in Library.Instance.GetAllDefinitions<DefinitionTheater>())
            {
                ToolStripMenuItem theaterMenuItem = new ToolStripMenuItem(theater.DisplayName) { Name = theater.ID };
                MenuFileNew.DropDownItems.Add(theaterMenuItem);

                ToolStripButton theaterButton = new ToolStripButton(theater.DisplayName) { Name = theater.ID };
                ToolStripButtonFileNew.DropDownItems.Add(theaterButton);
            }

            MapPanel.UpdateTheater(true);
            SidePanel.UpdateTheater(true);

            UpdateFormTitle();
            UpdateTheater(true);
        }

        /// <summary>
        /// Event raised when a key is pressed.
        /// </summary>
        /// <param name="sender">This form.</param>
        /// <param name="e">Key event arguments.</param>
        private void Event_FormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: // Escape: unselect selected location if any
                    if (SelectedLocationID == null) return;
                    SelectedLocationID = null;
                    UpdateTheater(false);
                    return;
            }
        }

        /// <summary>
        /// Event raised when the form is closing.
        /// </summary>
        /// <param name="sender">This form.</param>
        /// <param name="e">Form closing arguments.</param>
        private void Event_MainFormClosing(object sender, FormClosingEventArgs e)
        {
            // If not a debug build, show a confirmation message before closing.
#if !DEBUG
            e.Cancel =
                (MessageBox.Show(
                    "Are you sure you want to close HQ4DCS?\r\nUnsaved changes will be lost.",
                    "Exit?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes);
#endif
        }

        /// <summary>
        /// Event raised when a theater dropitem is selected from the "New mission" menu item or toolstripbutton.
        /// </summary>
        /// <param name="sender">The "New mission" menu item or toolstrip button.</param>
        /// <param name="e">ToolStripItem clicked arguments.</param>
        private void Event_MenuFileNewDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null) return;

#if !DEBUG
            if (MessageBox.Show(
                "Are you sure you want to create a new mission?\r\nUnsaved changes will be lost.",
                "New mission?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
#endif

            LastSaveFilePath = null;
            Template.Clear(e.ClickedItem.Name);
            UpdateTheater(true);
            UpdateFormTitle();
        }

        /// <summary>
        /// Event raised when the mouse enters the left panel of MainSplitContainer.
        /// Manually raises the SidePanel.MouseEnter events, because it won't be raised properly if the mission setting tab is selected
        /// (seems to be a bug with PropertyGrid).
        /// </summary>
        /// <param name="sender">The left panel of MainSplitContainer.</param>
        /// <param name="e">Event arguments.</param>
        private void Event_MainSplitContainerPanel1MouseEnter(object sender, EventArgs e)
        {
            SidePanel.SidePanelMouseEnter(sender, e);
        }

        /// <summary>
        /// Event raised when any menu or toolbar item is clicked, apart from the "new mission" theater subitems.
        /// (These are handled by Event_MenuFileNewDropDownItemClicked)
        /// </summary>
        /// <param name="sender">Sender control.</param>
        /// <param name="e">Event arguments.</param>
        private void Event_MenuClick(object sender, EventArgs e)
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
                    SelectedLocationID = null;
                    UpdateTheater(true);
                    UpdateFormTitle();
                    return;
                case "MenuFileSave":
                case "ToolStripButtonFileSave":
                    if (string.IsNullOrEmpty(LastSaveFilePath)) { Event_MenuClick(MenuFileSaveAs, e); return; }
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
                case "MenuMissionGenerate":
                case "ToolStripButtonMissionGenerate":
                    using (FormMissionOutput formOutput = new FormMissionOutput(Template)) { formOutput.ShowDialog(); }
                    return;
                case "MenuMissionInvertAirbasesCoalition":
                    Template.InvertCoalitions();
                    UpdateTheater(false);
                    return;
                case "MenuDevelopmentMizToIni":
                    ShowDevToolWarningMessage();
                    using (FormMizToINI miz2IniForm = new FormMizToINI()) { miz2IniForm.ShowDialog(); }
                    return;
                case "MenuDevelopmentRadioMessageGenerator":
                    ShowDevToolWarningMessage();
                    using (FormRadioMessageGenerator rmgForm = new FormRadioMessageGenerator()) { rmgForm.ShowDialog(); }
                    return;
            }
        }

        /// <summary>
        /// Display a "warning: this is a development tool" message box the first time the method is called.
        /// </summary>
        private void ShowDevToolWarningMessage()
        {
#if !DEBUG
            if (DevToolWarningAlreadyShown) return;
            DevToolWarningAlreadyShown = true;

            MessageBox.Show(
                "The tool you're about to use is intended for HQ4DCS developers and modders. If you just want to create missions and do not intend to add new theaters, missions and the like to the HQ4DCS library, you shouldn't need it.",
                "Development tools", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
        }

        /// <summary>
        /// Load all icons from the embedded ressources. Called once when forms is created.
        /// </summary>
        private void LoadIcons()
        {
            Icon = GUITools.GetIconFromResource("Icon.ico");

            MenuFileNew.Image = GUITools.GetImageFromResource("Icons.new.png");
            MenuFileOpen.Image = GUITools.GetImageFromResource("Icons.load.png");
            MenuFileSave.Image = GUITools.GetImageFromResource("Icons.save.png");
            MenuFileSaveAs.Image = GUITools.GetImageFromResource("Icons.save_as.png");
            MenuFileExit.Image = GUITools.GetImageFromResource("Icons.exit.png");

            MenuMissionInvertAirbasesCoalition.Image = GUITools.GetImageFromResource("Icons.switchSides.png");
            MenuMissionGenerate.Image = GUITools.GetImageFromResource("Icons.generate.png");

            ToolStripButtonFileNew.Image = MenuFileNew.Image;
            ToolStripButtonFileOpen.Image = MenuFileOpen.Image;
            ToolStripButtonFileSave.Image = MenuFileSave.Image;
            ToolStripButtonFileSaveAs.Image = MenuFileSaveAs.Image;
            ToolStripButtonMissionGenerate.Image = MenuMissionGenerate.Image;

            ToolStripButtonZoomIn.Image = GUITools.GetImageFromResource("Icons.zoom_in.png");
            ToolStripButtonZoomOut.Image = GUITools.GetImageFromResource("Icons.zoom_out.png");
        }

        /// <summary>
        /// Updates the selected location in the map and side panel.
        /// </summary>
        /// <param name="newSelectedID"></param>
        public void UpdateSelectedLocation(string newSelectedID)
        {
            SelectedLocationID = newSelectedID;
            UpdateTheater(false);

            MapPanel.UpdateSelectedLocation();
            SidePanel.UpdateSelectedLocation();
        }

        /// <summary>
        /// Updates the title of the form. Called when a mission is created, loaded or saved, to change the displayed file name.
        /// </summary>
        private void UpdateFormTitle()
        {
            Text =
                $"Headquarters {HQ4DCS.HQ4DCS_VERSION_STRING} for DCS World {HQ4DCS.DCSWORLD_TARGETED_VERSION} - " +
                (string.IsNullOrEmpty(LastSaveFilePath) ? "New mission" : Path.GetFileName(LastSaveFilePath));
        }

        /// <summary>
        /// Updates the theater display (icons, etc.) on both the map and the side panel sub forms.
        /// </summary>
        /// <param name="fullUpdate">Should a full update (reload the background image on the map, etc.) be performed? Only when changing mission theater.</param>
        private void UpdateTheater(bool fullUpdate)
        {
            MapPanel.UpdateTheater(fullUpdate);
            SidePanel.UpdateTheater(fullUpdate);
        }
    }
}

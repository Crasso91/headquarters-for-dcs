using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Template;
using System;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class FormLocationEditor : Form
    {
        public readonly MissionTemplateLocation EditedLocation;

        private readonly PanelLocationPropertyGrid PanelProperties = null;
        private readonly PanelLocationEditorFeatures PanelFeatures = null;
        private readonly PanelLocationPlayerFlightGroups PanelPlayerFlightGroups = null;

        public FormLocationEditor(MissionTemplateLocation editedLocation)
        {
            InitializeComponent();

            EditedLocation = editedLocation;
            Text = editedLocation.Definition.DisplayName;

            if (editedLocation.Definition.LocationType == TheaterLocationType.Airbase)
            {
                PanelProperties = new PanelLocationPropertyGrid(editedLocation);
                SetupTabPage(PanelProperties, "Properties");
            }

            PanelFeatures = new PanelLocationEditorFeatures(editedLocation);
            SetupTabPage(PanelFeatures, "Features");

            if (editedLocation.Definition.LocationType == TheaterLocationType.Airbase)
            {
                PanelPlayerFlightGroups = new PanelLocationPlayerFlightGroups(editedLocation);
                SetupTabPage(PanelPlayerFlightGroups, "Player flight groups");
            }
        }

        private void SetupTabPage(Form panelForm, string title)
        {
            TabPage tabPage = new TabPage(title);
            GUITools.SetupFormForPanel(panelForm, tabPage);
            MainTabControl.TabPages.Add(tabPage);
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender == OkButton)
            {
                EditedLocation.Features = PanelFeatures.SelectedFeatures;
                if (PanelPlayerFlightGroups != null)
                    EditedLocation.PlayerFlightGroups = PanelPlayerFlightGroups.GetFlightGroups();
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.Cancel;

            Close();
        }

        private void FormNodeEditor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: Close(); return;
            }
        }
    }
}

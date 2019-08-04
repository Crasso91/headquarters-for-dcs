using Headquarters4DCS.Library;
using Headquarters4DCS.Template;
using System;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class FormNodeEditor : Form
    {
        public readonly MissionTemplateNode EditedNode;

        private readonly PanelNodePropertyGrid PanelProperties = null;
        private readonly PanelNodeEditorFeatures PanelFeatures = null;
        private readonly PanelNodePlayerFlightGroups PanelPlayerFlightGroups = null;

        public FormNodeEditor(MissionTemplateNode editedNode)
        {
            InitializeComponent();

            EditedNode = editedNode; // TODO: clone
            Text = editedNode.Definition.DisplayName;

            if (editedNode is MissionTemplateNodeAirbase)
            {
                PanelProperties = new PanelNodePropertyGrid(editedNode);
                SetupTabPage(PanelProperties, "Properties");
            }

            PanelFeatures = new PanelNodeEditorFeatures(editedNode);
            SetupTabPage(PanelFeatures, "Features");

            if (editedNode is MissionTemplateNodeAirbase)
            {
                PanelPlayerFlightGroups = new PanelNodePlayerFlightGroups(editedNode);
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
                EditedNode.Features = PanelFeatures.SelectedFeatures;
                // TODO: save player flight groups
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

        private void FormNodeEditor_Load(object sender, EventArgs e)
        {

        }
    }
}

using Headquarters4DCS.Template;
using System;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class PanelNodePropertyGrid : Form
    {
        private readonly MissionTemplateNode EditedNode = null;

        public PanelNodePropertyGrid(MissionTemplateNode editedNode)
        {
            InitializeComponent();
            EditedNode = editedNode;
            NodePropertyGrid.SelectedObject = EditedNode;
        }

        private void PanelNodePropertyGrid_Load(object sender, EventArgs e)
        {

        }
    }
}

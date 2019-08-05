using Headquarters4DCS.Template;
using System;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class PanelLocationPropertyGrid : Form
    {
        private readonly MissionTemplateLocation EditedNode = null;

        public PanelLocationPropertyGrid(MissionTemplateLocation editedNode)
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

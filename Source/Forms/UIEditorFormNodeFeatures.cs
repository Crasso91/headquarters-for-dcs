using Headquarters4DCS.Enums;
using Headquarters4DCS.Library;
using Headquarters4DCS.TypeConverters;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class UIEditorFormNodeFeatures : Form
    {
        public TheaterNodeType[] NodeTypes = new TheaterNodeType[0];
        public string[] Values = new string[0];

        public UIEditorFormNodeFeatures()
        {
            InitializeComponent();
        }

        private void UIEditorMultipleDefinitions_Load(object sender, EventArgs e)
        {
            AvailableFeaturesTreeView.Nodes.Clear();

            DefinitionNodeFeature[] validNodes =
                (from DefinitionNodeFeature n in HQLibrary.Instance.GetAllDefinitions<DefinitionNodeFeature>()
                 where n.ValidNodeTypes.Intersect(NodeTypes).Count() > 0 select n).ToArray();

            foreach (DefinitionNodeFeature n in validNodes)
            {
                TreeNode node = new TreeNode(n.DisplayName) { Tag = n.ID };
                AvailableFeaturesTreeView.Nodes.Add(node);
            }

            AvailableFeaturesTreeView.Sort();

            SelectedFeaturesTreeView.Nodes.Clear();
            foreach (string s in Values)
            {
                DefinitionNodeFeature nodeFeature = HQLibrary.Instance.GetDefinition<DefinitionNodeFeature>(s);
                if (nodeFeature == null) continue;
                if (nodeFeature.ValidNodeTypes.Intersect(NodeTypes).Count() == 0) continue;

                TreeNode node = new TreeNode(nodeFeature.DisplayName) { Tag = nodeFeature.ID };
                SelectedFeaturesTreeView.Nodes.Add(node);
            }
            SelectedFeaturesTreeView.Sort();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender == OkButton)
            {
                Values = (from TreeNode n in SelectedFeaturesTreeView.Nodes select n.Tag.ToString()).ToArray();
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.Cancel;

            Close();
        }

        private void AvailableFeaturesTreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null) return;
            TreeNode node = new TreeNode(e.Node.Text) { Tag = e.Node.Tag.ToString() };
            SelectedFeaturesTreeView.Nodes.Add(node);
        }

        private void SelectedFeaturesTreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null) return;
            e.Node.Remove();
        }
    }
}

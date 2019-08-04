using Headquarters4DCS.Library;
using Headquarters4DCS.Template;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class PanelNodeEditorFeatures : Form
    {
        public string[] SelectedFeatures
        {
            get { return (from TreeNode n in SelectedFeaturesTreeView.Nodes select n.Tag.ToString()).ToArray(); }
        }

        private readonly MissionTemplateNode EditedNode;

        public PanelNodeEditorFeatures(MissionTemplateNode editedNode)
        {
            InitializeComponent();
            EditedNode = editedNode;
        }

        private void PanelNodeEditorFeatures_Load(object sender, EventArgs e)
        {
            AvailableFeaturesTreeView.Nodes.Clear();

            DefinitionFeature[] validNodes =
                (from DefinitionFeature n in HQLibrary.Instance.GetAllDefinitions<DefinitionFeature>()
                 where n.ValidNodeTypes.Contains(EditedNode.Definition.NodeType) select n).ToArray();

            foreach (DefinitionFeature feature in validNodes)
            {
                string categoryKey = feature.Category.ToString();

                if (!AvailableFeaturesTreeView.Nodes.ContainsKey(categoryKey))
                    AvailableFeaturesTreeView.Nodes.Add(categoryKey, categoryKey);

                TreeNode node = new TreeNode(feature.Category.ToString() + " - " + feature.DisplayName) { Tag = feature.ID, ToolTipText = feature.Description };
                AvailableFeaturesTreeView.Nodes[categoryKey].Nodes.Add(node);
            }
            AvailableFeaturesTreeView.Sort();

            SelectedFeaturesTreeView.Nodes.Clear();
            foreach (string s in EditedNode.Features)
            {
                DefinitionFeature feature = HQLibrary.Instance.GetDefinition<DefinitionFeature>(s);
                if (feature == null) continue;
                if (!feature.ValidNodeTypes.Contains(EditedNode.Definition.NodeType)) continue;

                TreeNode node = new TreeNode(feature.Category.ToString() + " - " + feature.DisplayName) { Tag = feature.ID, ToolTipText = feature.Description };
                SelectedFeaturesTreeView.Nodes.Add(node);
            }
            SelectedFeaturesTreeView.Sort();
        }

        private void AvailableFeaturesTreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if ((e.Node == null) || (e.Node.Level == 0)) return;
            TreeNode node = new TreeNode(e.Node.Text) { Tag = e.Node.Tag.ToString(), ToolTipText = e.Node.ToolTipText };
            SelectedFeaturesTreeView.Nodes.Add(node);
        }

        private void SelectedFeaturesTreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null) return;
            e.Node.Remove();
        }
    }
}

using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Template;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class PanelLocationEditorFeatures : Form
    {
        public string[] SelectedFeatures
        {
            get { return (from TreeNode n in SelectedFeaturesTreeView.Nodes select n.Tag.ToString()).ToArray(); }
        }

        private readonly MissionTemplateLocation EditedNode;

        public PanelLocationEditorFeatures(MissionTemplateLocation editedNode)
        {
            InitializeComponent();
            EditedNode = editedNode;
        }

        private void PanelNodeEditorFeatures_Load(object sender, EventArgs e)
        {
            AvailableFeaturesTreeView.Nodes.Clear();

            DefinitionFeature[] validNodes =
                (from DefinitionFeature n in Library.Instance.GetAllDefinitions<DefinitionFeature>()
                 where n.FeatureLocationTypes.Contains(EditedNode.Definition.LocationType) select n).ToArray();

            foreach (DefinitionFeature feature in validNodes)
            {
                string categoryKey = feature.FeatureCategory.ToString();

                if (!AvailableFeaturesTreeView.Nodes.ContainsKey(categoryKey))
                    AvailableFeaturesTreeView.Nodes.Add(categoryKey, categoryKey);

                TreeNode node = new TreeNode(feature.FeatureCategory.ToString() + " - " + feature.DisplayName) { Tag = feature.ID, ToolTipText = feature.DisplayDescription };
                AvailableFeaturesTreeView.Nodes[categoryKey].Nodes.Add(node);
            }
            AvailableFeaturesTreeView.Sort();

            SelectedFeaturesTreeView.Nodes.Clear();
            foreach (string s in EditedNode.Features)
            {
                DefinitionFeature feature = Library.Instance.GetDefinition<DefinitionFeature>(s);
                if (feature == null) continue;
                if (!feature.FeatureLocationTypes.Contains(EditedNode.Definition.LocationType)) continue;

                TreeNode node = new TreeNode(feature.FeatureCategory.ToString() + " - " + feature.DisplayName) { Tag = feature.ID, ToolTipText = feature.DisplayDescription };
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

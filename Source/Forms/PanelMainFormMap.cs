using Cyotek.Windows.Forms;
using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Template;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class PanelMainFormMap : Form
    {
        private const int ICON_CLICK_RADIUS = 16;

        private readonly FormMain MainForm = null;
        private MissionTemplate Template { get { return MainForm.Template; } }
        private string SelectedNodeID { get { return MainForm.SelectedLocationID; } }

        private Point MapMouseDownLocation = Point.Empty;

        public PanelMainFormMap(FormMain mainForm)
        {
            InitializeComponent();

            MainForm = mainForm;
        }

        private void PanelMainFormMap_Load(object sender, EventArgs e)
        {
            MapIconsImageList.Images.Clear();
            MapIconsImageList.Images.Add("airbase", GUITools.GetImageFromResource("MapIcons.airbase.png"));
            MapIconsImageList.Images.Add("airbase_blue", GUITools.GetImageFromResource("MapIcons.airbase_blue.png"));
            MapIconsImageList.Images.Add("airbase_red", GUITools.GetImageFromResource("MapIcons.airbase_red.png"));
            MapIconsImageList.Images.Add("location", GUITools.GetImageFromResource("MapIcons.location.png"));
            MapIconsImageList.Images.Add("selected", GUITools.GetImageFromResource("MapIcons.selected.png"));
        }

        public void UpdateTheater(bool fullUpdate)
        {
            if (fullUpdate)
            {
                DefinitionTheater theater = Library.Instance.GetDefinition<DefinitionTheater>(Template.Theater);

                MapImageBox.Image = Image.FromFile(HQTools.PATH_LIBRARY + $"Theaters/{Template.Theater}/GUIMap.jpg");
                MapImageBox.BackColor = theater.MapBackgroundColor;

                MapImageBox.IconsLocation.Clear();
                foreach (DefinitionTheaterLocation n in theater.Locations.Values)
                    MapImageBox.IconsLocation.Add(n.ID, new ImageBoxOverlayIcon("airbase", (int)n.MapPosition.X, (int)n.MapPosition.Y));
                MapImageBox.IconsLocation.Add("", new ImageBoxOverlayIcon("selected", -512, -512));
            }

            if ((SelectedNodeID != null) && Template.Locations.ContainsKey(SelectedNodeID))
                MapImageBox.IconsLocation[""].Location = Template.Locations[SelectedNodeID].Definition.MapPosition.ToPoint();
            else
                MapImageBox.IconsLocation[""].Location = new Point(-512, -512);

            foreach (string n in MapImageBox.IconsLocation.Keys)
            {
                if (!Template.Locations.ContainsKey(n)) continue;

                if (Template.Locations[n].Definition.LocationType == TheaterLocationType.Airbase)
                {
                    if (Template.Locations[n].Coalition == CoalitionNeutral.Blue)
                        MapImageBox.IconsLocation[n].IconKey = "airbase_blue";
                    else if (Template.Locations[n].Coalition == CoalitionNeutral.Red)
                        MapImageBox.IconsLocation[n].IconKey = "airbase_red";
                    else
                        MapImageBox.IconsLocation[n].IconKey = "airbase";
                }
                else
                        MapImageBox.IconsLocation[n].IconKey = "location";
            }

            MapImageBox.Refresh();
        }

        private string GetNodeIDOnMapPosition(int x, int y, bool proceedToNext)
        {
            DefinitionTheater theater = Library.Instance.GetDefinition<DefinitionTheater>(Template.Theater); // TODO: what if theater doesn't exist?
            Coordinates position = new Coordinates(x - MapImageBox.AutoScrollPosition.X, y - MapImageBox.AutoScrollPosition.Y) / (MapImageBox.Zoom / 100.0);

            DefinitionTheaterLocation[] nodes = (from DefinitionTheaterLocation n in theater.Locations.Values where n.MapPosition.GetDistanceFrom(position) < ICON_CLICK_RADIUS select n).ToArray();
            if (nodes.Length == 0) return null;

            // If not iterating through nearby nodes and the currently selected node is within click reach, stop looking for another and return it.
            if (!proceedToNext && !string.IsNullOrEmpty(SelectedNodeID) && nodes.Contains(Template.Locations[SelectedNodeID].Definition))
                return SelectedNodeID;

            for (int i = 0; i < nodes.Length; i++)
            {
                // Iterate through nearby objects when clicking multiple times
                if ((SelectedNodeID != null) && (nodes[i].ID == Template.Locations[SelectedNodeID].Definition.ID) && (i < nodes.Length - 1)) return nodes[i + 1].ID;
            }

            return nodes[0].ID;
        }

        public void UpdateSelectedLocation()
        {
            // TODO
        }

        private void MapImageBoxMouseDown(object sender, MouseEventArgs e)
        {
            MapMouseDownLocation = e.Location;
        }

        private void MapImageBoxMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
                MessageBox.Show(e.Location.ToString());


            if ((e.Button != MouseButtons.Left) && (e.Button != MouseButtons.Right)) return;
            if (e.Location != MapMouseDownLocation) return;

            string newSelectedNode = GetNodeIDOnMapPosition(e.X, e.Y, (e.Button == MouseButtons.Left));
            MainForm.UpdateSelectedLocation(newSelectedNode);

            if ((e.Button == MouseButtons.Right) && (newSelectedNode != null))
            {
                using (FormLocationEditor locationEditorForm = new FormLocationEditor(Template.Locations[newSelectedNode]))
                {
                    if (locationEditorForm.ShowDialog() == DialogResult.OK)
                    {
                        Template.Locations[newSelectedNode] = locationEditorForm.EditedLocation;
                        MainForm.UpdateSelectedLocation(newSelectedNode);
                    }
                }
            }
        }

        private void MapImageBoxMouseEnter(object sender, EventArgs e)
        {
            MainForm.SetStatusBarMessage("Drag the map to move it, use the mouse wheel to zoom. Left-click a location to select, right-click a location to edit.");
        }

        private void MapImageBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            // TODO: fix this: double-clicking should open the location editor
            MapMouseDownLocation = e.Location;
            if (e.Button != MouseButtons.Left) return;
            MapImageBoxMouseUp(sender, new MouseEventArgs(MouseButtons.Right, 1, e.X, e.Y, e.Delta));
        }
    }
}

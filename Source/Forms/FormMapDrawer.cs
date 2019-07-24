using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Headquarters4DCS.Enums;
using Headquarters4DCS.Library;
using Headquarters4DCS.Template;

namespace Headquarters4DCS.Forms
{
    public sealed class FormMapDrawer : IDisposable
    {
        private static readonly float[] ZOOM_LEVELS = new float[] { 0.25f, 0.5f, 0.75f, 1.0f, 1.25f, 1.5f, 1.75f, 2.0f };

        private const int ICON_SIZE = 24;
        private const int ICON_SIZE_HALF = ICON_SIZE / 2;

        private readonly HQLibrary Library = null;
        private readonly HQTemplate Mission = null;

        public Image Image { get; private set; } = null;

        public int Zoom { get { return _Zoom; } set { _Zoom = HQTools.Clamp(value, 0, ZOOM_LEVELS.Length - 1); } }
        private int _Zoom = 3;

        public float ZoomMultiplier { get { return ZOOM_LEVELS[_Zoom]; } }

        private readonly Dictionary<string, Image> Icons = new Dictionary<string, Image>();

        public FormMapDrawer(HQLibrary library, HQTemplate mission)
        {
            Library = library;
            Mission = mission;

            LoadIcon("airbase");
            LoadIcon("airbase_blue");
            LoadIcon("airbase_red");
            //LoadIcon("airbase_takeoff");
            LoadIcon("favourite");
            LoadIcon("carrier");
            LoadIcon("carrier_blue");
            LoadIcon("carrier_red");
            LoadIcon("location");
            LoadIcon("target");
            LoadIcon("selected");
        }

        private void LoadIcon(string iconName)
        {
            // TODO: add empty file if missing
            //Icons.Add(iconName, Image.FromFile(HQTools.PATH_MEDIA + iconName + ".png"));

            Image image = UITools.GetImageFromResource($"MapIcons.{iconName}.png");
            // TODO: if (image == null) create new image

            Icons.Add(iconName, image);
        }

        public void Dispose()
        {
            DestroyImage();
        }

        public void UpdateImage(string selectedNodeID)
        {
            DestroyImage();

            DefinitionTheater theater = Library.GetDefinition<DefinitionTheater>("Caucasus");
            string icon = "";

            Image srcImage = Image.FromFile(HQTools.PATH_LIBRARY + "Theaters/Caucasus/Map.png");
            Image = new Bitmap((int)(srcImage.Width * ZoomMultiplier), (int)(srcImage.Height * ZoomMultiplier));

            using (Graphics g = Graphics.FromImage(Image))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.DrawImage(srcImage, new Rectangle(0, 0, Image.Width, Image.Height), new Rectangle(0, 0, srcImage.Width, srcImage.Height), GraphicsUnit.Pixel);

                foreach (DefinitionTheaterNode n in theater.Nodes.Values)
                {
                    if (n is DefinitionTheaterNodeAirbase ab)
                    {
                        icon = "airbase";
                        HQTemplateNodeAirbase missionAB = (Mission.Nodes.ContainsKey(n.ID) && (Mission.Nodes[n.ID] is HQTemplateNodeAirbase)) ? (HQTemplateNodeAirbase)Mission.Nodes[n.ID] : null;

                        Coalition abCoalition = (missionAB != null) ? missionAB.Coalition : ab.Coalition;

                        if (abCoalition == Coalition.Blue) icon = "airbase_blue";
                        else icon = "airbase_red";

                        DrawIcon(g, icon, n.MapPosition);

                        if (missionAB != null)
                        {
                            //if (missionAB.PrimaryAirdrome)
                            if (missionAB.PlayerFlightGroups.Length > 0)
                            {
                                //DrawIcon(g, "airbase_takeoff", n.MapPosition);
                                //DrawIcon(g, "airbase_landing", n.MapPosition);
                                DrawIcon(g, "favourite", n.MapPosition);
                            }
                        }
                    }
                    //else if (n is DefinitionTheaterNodeCarrierLocation cg)
                    //{
                    //    icon = "carrier";
                    //    HQTemplateNodeCarrierGroup missionCG = (Mission.Nodes.ContainsKey(n.ID) && (Mission.Nodes[n.ID] is HQTemplateNodeCarrierGroup)) ? (HQTemplateNodeCarrierGroup)Mission.Nodes[n.ID] : null;
                    //    if (missionCG.PlayerFlightGroups.Length > 0) icon = (Mission.Settings.ContextPlayerCoalition == Coalition.Red) ? "carrier_red" : "carrier_blue";

                    //    DrawIcon(g, icon, n.MapPosition);
                    //}
                    else if (n is DefinitionTheaterNodeLocation loc)
                    {
                        icon = "location";
                        HQTemplateNodeLocation missionLoc = (Mission.Nodes.ContainsKey(n.ID) && (Mission.Nodes[n.ID] is HQTemplateNodeLocation)) ? (HQTemplateNodeLocation)Mission.Nodes[n.ID] : null;
                        if (missionLoc != null)
                        {
                            if ((from DefinitionNodeFeature fDef in
                                 (from string fID in missionLoc.Features
                                  where HQLibrary.Instance.DefinitionExists<DefinitionNodeFeature>(fID)
                                  select HQLibrary.Instance.GetDefinition<DefinitionNodeFeature>(fID))
                                 where fDef.Flags.Contains(NodeFeatureFlags.Objective)
                                 select fDef).Count() > 0)
                                icon = "target";
                        }

                        DrawIcon(g, icon, loc.MapPosition);
                    }

                    if (selectedNodeID == n.ID) DrawIcon(g, "selected", n.MapPosition, 14);
                }
            }

            srcImage.Dispose();
        }

        private void DrawIcon(Graphics g, string icon, Coordinates position, int offset = ICON_SIZE_HALF)
        {
            g.DrawImageUnscaled(Icons[icon], (position * ZoomMultiplier - new Coordinates(offset, offset)).ToPoint());
        }

        private void DestroyImage()
        {
            if (Image == null) return;

            Image.Dispose();
            Image = null;
        }
    }
}

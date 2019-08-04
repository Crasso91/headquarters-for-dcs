//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Headquarters4DCS.Library;
//using Headquarters4DCS.Template;

//namespace Headquarters4DCS.Forms
//{
//    public sealed class GUIMapDrawer : IDisposable
//    {
//        //private static readonly float[] ZOOM_LEVELS = new float[] { 0.25f, 0.5f, 0.75f, 1.0f, 1.25f, 1.5f, 1.75f, 2.0f };

//        public const int ICON_CLICK_RADIUS = 16;

//        private const int ICON_SIZE = 24;
//        private const int ICON_SIZE_HALF = ICON_SIZE / 2;

//        private readonly MissionTemplate Mission = null;

//        public Image Image { get; private set; } = null;

//        //public int Zoom { get { return _Zoom; } set { _Zoom = HQTools.Clamp(value, 0, ZOOM_LEVELS.Length - 1); } }
//        //private int _Zoom = 3;

//        //public float ZoomMultiplier { get { return ZOOM_LEVELS[_Zoom]; } }

//        private readonly Dictionary<string, Image> Icons = new Dictionary<string, Image>();

//        public GUIMapDrawer(MissionTemplate mission)
//        {
//            Mission = mission;

//            // TODO: get list of all icon names from resources manifest
//            LoadIcon("airbase");
//            LoadIcon("airbase_blue");
//            LoadIcon("airbase_red");
//            LoadIcon("airbase_aircraft");
//            LoadIcon("favourite");
//            LoadIcon("carrier");
//            LoadIcon("carrier_blue");
//            LoadIcon("carrier_red");
//            LoadIcon("location");
//            LoadIcon("location_red");
//            LoadIcon("location_blue");
//            LoadIcon("location_both");
//            LoadIcon("location_ally_blue");
//            LoadIcon("location_ally_red");
//            LoadIcon("location_enemy_blue");
//            LoadIcon("location_enemy_red");
//            LoadIcon("location_support_blue");
//            LoadIcon("location_support_red");
//            LoadIcon("location_target");
//            LoadIcon("selected");
//        }

//        private void LoadIcon(string iconName)
//        {
//            Image image = GUITools.GetImageFromResource($"MapIcons.{iconName}.png");
//            // TODO: if (image == null) create new image

//            Icons.Add(iconName, image);
//        }

//        public void Dispose()
//        {
//            DestroyImage();
//        }

//        public void UpdateImage(string selectedNodeID /*, TODO: bool declutter*/)
//        {
//            DestroyImage();

//            DefinitionTheater theater = HQLibrary.Instance.GetDefinition<DefinitionTheater>(Mission.Theater);
//            string icon = "";

//            // TODO: what if image is null?
//            //Image srcImage = Image.FromFile(HQTools.PATH_LIBRARY + $"Theaters/{Mission.Theater}/Map.jpg");
//            //Image = new Bitmap((int)(srcImage.Width * ZoomMultiplier), (int)(srcImage.Height * ZoomMultiplier));
//            Image = Image.FromFile(HQTools.PATH_LIBRARY + $"Theaters/{Mission.Theater}/Map.jpg");

//            return;

//            using (Graphics g = Graphics.FromImage(Image))
//            {
//                g.InterpolationMode = InterpolationMode.NearestNeighbor;
//                g.CompositingQuality = CompositingQuality.HighSpeed;
//                //g.DrawImage(srcImage, new Rectangle(0, 0, Image.Width, Image.Height), new Rectangle(0, 0, srcImage.Width, srcImage.Height), GraphicsUnit.Pixel);

//                foreach (DefinitionTheaterNode n in theater.Nodes.Values)
//                {
//                    if (n is DefinitionTheaterNodeAirbase ab)
//                    {
//                        icon = "airbase";
//                        MissionTemplateNodeAirbase missionAB = (Mission.Nodes.ContainsKey(n.ID) && (Mission.Nodes[n.ID] is MissionTemplateNodeAirbase)) ? (MissionTemplateNodeAirbase)Mission.Nodes[n.ID] : null;

//                        CoalitionNeutral abCoalition = (missionAB != null) ? missionAB.Coalition : ab.Coalition;

//                        if (abCoalition == CoalitionNeutral.Blue) icon = "airbase_blue";
//                        else if (abCoalition == CoalitionNeutral.Red) icon = "airbase_red";
//                        else icon = "airbase";

//                        DrawIcon(g, icon, n.MapPosition);

//                        if (missionAB != null)
//                        {
//                            if (missionAB.PlayerFlightGroups.Length > 0)
//                                DrawIcon(g, "airbase_aircraft", n.MapPosition);
//                        }
//                    }
//                    //else if (n is DefinitionTheaterNodeCarrierLocation cg)
//                    //{
//                    //    icon = "carrier";
//                    //    HQTemplateNodeCarrierGroup missionCG = (Mission.Nodes.ContainsKey(n.ID) && (Mission.Nodes[n.ID] is HQTemplateNodeCarrierGroup)) ? (HQTemplateNodeCarrierGroup)Mission.Nodes[n.ID] : null;
//                    //    if (missionCG.PlayerFlightGroups.Length > 0) icon = (Mission.Settings.ContextPlayerCoalition == Coalition.Red) ? "carrier_red" : "carrier_blue";

//                    //    DrawIcon(g, icon, n.MapPosition);
//                    //}
//                    else if (n is DefinitionTheaterNodeLand loc)
//                    {
//                        MissionTemplateNodeLand missionLoc = (Mission.Nodes.ContainsKey(n.ID) && (Mission.Nodes[n.ID] is MissionTemplateNodeLand)) ? (MissionTemplateNodeLand)Mission.Nodes[n.ID] : null;

//                        DrawIcon(g, "location", loc.MapPosition);

//                        if (missionLoc != null)
//                        {
//                            DefinitionFeature[] features = (from string fID in missionLoc.Features
//                                                            where HQLibrary.Instance.DefinitionExists<DefinitionFeature>(fID)
//                                                            select HQLibrary.Instance.GetDefinition<DefinitionFeature>(fID)).ToArray();

//                            if ((from DefinitionFeature fDef in features where fDef.Category == FeatureCategory.Objective select fDef).Count() > 0)
//                                DrawIcon(g, "location_target", loc.MapPosition);

//                            if ((from DefinitionFeature fDef in features where fDef.Category == FeatureCategory.FriendlyTroops select fDef).Count() > 0)
//                                DrawIcon(g, $"location_ally_{Mission.Settings.ContextPlayerCoalition.ToString().ToLowerInvariant()}", loc.MapPosition);

//                            if ((from DefinitionFeature fDef in features where fDef.Category == FeatureCategory.Support select fDef).Count() > 0)
//                                DrawIcon(g, $"location_support_{Mission.Settings.ContextPlayerCoalition.ToString().ToLowerInvariant()}", loc.MapPosition);
//                        }

//                    }

//                    if (selectedNodeID == n.ID) DrawIcon(g, "selected", n.MapPosition, 14);
//                }
//            }

//            //srcImage.Dispose();
//        }

//        private void DrawIcon(Graphics g, string icon, Coordinates position, int offset = ICON_SIZE_HALF)
//        {
//            g.DrawImageUnscaled(Icons[icon], (position - new Coordinates(offset, offset)).ToPoint());
//        }

//        private void DestroyImage()
//        {
//            if (Image == null) return;

//            Image.Dispose();
//            Image = null;
//        }
//    }
//}

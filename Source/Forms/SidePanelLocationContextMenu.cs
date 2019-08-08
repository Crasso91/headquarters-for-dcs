/*
==========================================================================
This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
Eagle Dynamics' DCS World flight simulator.

HQ4DCS was created by Ambroise Garel (@akaAgar).
You can find more information about the project on its GitHub page,
https://akaAgar.github.io/headquarters-for-dcs

HQ4DCS is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

HQ4DCS is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with HQ4DCS. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Template;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public sealed class SidePanelLocationContextMenu : IDisposable
    {
        private readonly ContextMenuStrip LocationContextMenuStrip;

        private readonly FormMain MainForm;
        private readonly TreeView LocationsTreeView;
        private readonly MissionTemplate Template;
        private readonly ImageList SidePanelImageList;

        public SidePanelLocationContextMenu(
            FormMain mainForm,
            TreeView locationsTreeView,
            ImageList sidePanelImageList)
        {
            LocationContextMenuStrip = new ContextMenuStrip();

            MainForm = mainForm;
            Template = mainForm.Template;
            LocationsTreeView = locationsTreeView;
            SidePanelImageList = sidePanelImageList;
        }

        public void Dispose() { }

        private MissionTemplateLocation GetTemplateLocationFromTreeNode(TreeNode node)
        {
            if (node == null) return null;
            string definitionID = (node.Level > 0) ? node.Parent.Name : node.Name;
            if (!Template.Locations.ContainsKey(definitionID)) return null;
            return Template.Locations[definitionID];
        }

        public void Show(Point menuLocation)
        {
            LocationContextMenuStrip.Items.Clear();

            MissionTemplateLocation location = GetTemplateLocationFromTreeNode(LocationsTreeView.SelectedNode);
            if (location == null) return;

            if (LocationsTreeView.SelectedNode.Level == 0)
            {
                DefinitionFeature[] availableFeatures =
                    (from DefinitionFeature f in Library.Instance.GetAllDefinitions<DefinitionFeature>()
                     where f.FeatureLocationTypes.Contains(location.Definition.LocationType)
                     select f).ToArray();

                ToolStripDropDownItem featureParentMenuItem = AddItemToLocationContextMenuStrip(null, "Add mission feature");
                foreach (DefinitionFeature feature in availableFeatures)
                {
                    if (!featureParentMenuItem.DropDownItems.ContainsKey(feature.Category.ToString()))
                        AddItemToLocationContextMenuStrip(featureParentMenuItem, feature.Category.ToString()).Name = feature.Category.ToString();

                    AddItemToLocationContextMenuStrip(
                        (ToolStripDropDownItem)featureParentMenuItem.DropDownItems[feature.Category.ToString()],
                        feature.DisplayName, "AddFeature", feature.ID, $"feature{feature.Category}");

                    // TODO: sort items
                }

                if (location.Definition.LocationType == TheaterLocationType.Airbase)
                {
                    AddItemToLocationContextMenuStrip(null, "Add player flight group", null).Name = "AddPlayerFlightGroup";

                    ToolStripMenuItem coalitionParentMenuItem = AddItemToLocationContextMenuStrip(null, "Airbase coalition");
                    foreach (CoalitionNeutral coalitionID in (CoalitionNeutral[])Enum.GetValues(typeof(CoalitionNeutral)))
                        AddItemToLocationContextMenuStrip(coalitionParentMenuItem, coalitionID.ToString(), "Coalition", coalitionID, null, location.Coalition == coalitionID);
                }

            }
            else
            {
                if (LocationsTreeView.SelectedNode.Tag is int playerFlightGroupIndex)
                    CreatePlayerFlightGroupMenuItems(location, playerFlightGroupIndex);
                else if (LocationsTreeView.SelectedNode.Tag is string)
                    AddItemToLocationContextMenuStrip(null, "Remove feature", "RemoveFeature");
            }

            LocationContextMenuStrip.Show(LocationsTreeView, menuLocation);
        }

        private void CreatePlayerFlightGroupMenuItems(MissionTemplateLocation location, int playerFlightGroupIndex)
        {
            if ((location == null) || (playerFlightGroupIndex < 0) || (playerFlightGroupIndex >= location.PlayerFlightGroups.Count)) return;

            MissionTemplatePlayerFlightGroup playerFG = location.PlayerFlightGroups[playerFlightGroupIndex];

            // Aircraft type
            ToolStripMenuItem pfgAircraftParentMenuItem = AddItemToLocationContextMenuStrip(null, "Aircraft type");
            foreach (DefinitionUnit unit in from DefinitionUnit u in Library.Instance.GetAllDefinitions<DefinitionUnit>() where u.AircraftPlayerControllable select u)
                AddItemToLocationContextMenuStrip(pfgAircraftParentMenuItem, unit.DisplayName, "PlayerFlightGroupAircraft", unit.ID, null, unit.ID == playerFG.AircraftType);

            // Flight group size
            ToolStripMenuItem pfgCountParentMenuItem = AddItemToLocationContextMenuStrip(null, "Flight group size");
            for (int i = 1; i <= MissionTemplatePlayerFlightGroup.MAX_AIRCRAFT_COUNT; i++)
                AddItemToLocationContextMenuStrip(pfgCountParentMenuItem, HQTools.ValToString(i), "PlayerFlightGroupSize", i, null, i == playerFG.Count);

            // Flight group tasking
            ToolStripMenuItem pfgTaskParentMenuItem = AddItemToLocationContextMenuStrip(null, "Task");
            foreach (PlayerFlightGroupTask task in (PlayerFlightGroupTask[])Enum.GetValues(typeof(PlayerFlightGroupTask)))
                AddItemToLocationContextMenuStrip(pfgTaskParentMenuItem, GUITools.SplitEnumCamelCase(task), "PlayerFlightGroupTask", task, null, playerFG.Task == task);

            // Flight group start location
            ToolStripMenuItem pfgStartLocationParentMenuItem = AddItemToLocationContextMenuStrip(null, "Start location");
            foreach (PlayerFlightGroupStartLocation startLocation in (PlayerFlightGroupStartLocation[])Enum.GetValues(typeof(PlayerFlightGroupStartLocation)))
                AddItemToLocationContextMenuStrip(pfgStartLocationParentMenuItem, GUITools.SplitEnumCamelCase(startLocation), "PlayerFlightGroupStartLocation", startLocation, null, playerFG.StartLocation == startLocation);

            // AI wingmen on/off
            ToolStripMenuItem pfgAIWingmenParentMenuItem = AddItemToLocationContextMenuStrip(null, "Use AI wingmen");
            AddItemToLocationContextMenuStrip(pfgAIWingmenParentMenuItem, "Yes", "PlayerFlightGroupAIWingmen", true, null, playerFG.AIWingmen);
            AddItemToLocationContextMenuStrip(pfgAIWingmenParentMenuItem, "No", "PlayerFlightGroupAIWingmen", false, null, !playerFG.AIWingmen);

            AddItemToLocationContextMenuStrip(null, "Remove player flight group", null).Name = "RemovePlayerFlightGroup";
        }

        private void Event_ContextMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == null) return;
            ToolStripMenuItem tsiSender = (ToolStripMenuItem)sender;
            MissionTemplateLocation location = GetTemplateLocationFromTreeNode(LocationsTreeView.SelectedNode);
            if (location == null) return;
            if (tsiSender.HasDropDownItems) return;

            switch (tsiSender.Name)
            {
                case "AddFeature":
                    Template.Locations[location.Definition.ID].Features.Add((string)tsiSender.Tag);
                    break;
                case "AddPlayerFlightGroup":
                    Template.Locations[location.Definition.ID].PlayerFlightGroups.Add(new MissionTemplatePlayerFlightGroup());
                    break;
                case "Coalition":
                    Template.Locations[location.Definition.ID].Coalition = (CoalitionNeutral)tsiSender.Tag;
                    break;
                case "PlayerFlightGroupAircraft":
                    Template.Locations[location.Definition.ID].PlayerFlightGroups[(int)LocationsTreeView.SelectedNode.Tag].AircraftType = (string)tsiSender.Tag;
                    break;
                case "PlayerFlightGroupSize":
                    Template.Locations[location.Definition.ID].PlayerFlightGroups[(int)LocationsTreeView.SelectedNode.Tag].Count = (int)tsiSender.Tag;
                    break;
                case "PlayerFlightGroupTask":
                    Template.Locations[location.Definition.ID].PlayerFlightGroups[(int)LocationsTreeView.SelectedNode.Tag].Task = (PlayerFlightGroupTask)tsiSender.Tag;
                    break;
                case "PlayerFlightGroupStartLocation":
                    Template.Locations[location.Definition.ID].PlayerFlightGroups[(int)LocationsTreeView.SelectedNode.Tag].StartLocation = (PlayerFlightGroupStartLocation)tsiSender.Tag;
                    break;
                case "PlayerFlightGroupAIWingmen":
                    Template.Locations[location.Definition.ID].PlayerFlightGroups[(int)LocationsTreeView.SelectedNode.Tag].AIWingmen = (bool)tsiSender.Tag;
                    break;
                case "RemoveFeature":
                    Template.Locations[location.Definition.ID].Features.Remove((string)tsiSender.Tag);
                    break;
                case "RemovePlayerFlightGroup":
                    Template.Locations[location.Definition.ID].PlayerFlightGroups.RemoveAt((int)tsiSender.Tag);
                    break;
                default:
                    return; // No changes made, return so MainForm.UpdateTheater() isn't called for nothing
            }

            MainForm.UpdateTheater(TheaterUpdateType.SelectedLocation);
        }

        private ToolStripMenuItem AddItemToLocationContextMenuStrip(
            ToolStripDropDownItem parent,
            string text, string name = null, object tag = null,
            string icon = null, bool isChecked = false)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem(text) { Checked = isChecked, Name = name, Tag = tag };
            menuItem.Click += Event_ContextMenuItem_Click;
            if (!string.IsNullOrEmpty(icon))
                menuItem.Image = SidePanelImageList.Images[icon];

            if (parent == null)
                LocationContextMenuStrip.Items.Add(menuItem);
            else
                parent.DropDownItems.Add(menuItem);

            return menuItem;
        }
    }
}

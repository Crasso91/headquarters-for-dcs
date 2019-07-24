///*
//==========================================================================
//This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
//Eagle Dynamics' DCS World flight simulator.

//HQ4DCS was created by Ambroise Garel (@akaAgar).
//You can find more information about the project on its GitHub page,
//https://akaAgar.github.io/headquarters-for-dcs

//HQ4DCS is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//HQ4DCS is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with HQ4DCS. If not, see https://www.gnu.org/licenses/
//==========================================================================
//*/

//using Headquarters4DCS.Forms;
//using System;
//using System.ComponentModel.Design;
//using System.Drawing;
//using System.Windows.Forms;

//// Based on code by Marc Gravell: https://stackoverflow.com/questions/199897/turn-on-description-panel-in-the-standard-collectioneditor

//namespace Headquarters4DCS.TypeConverters
//{
//    /// <summary>
//    /// The custom collection editor to display when editing the player flight groups in the mission template.
//    /// </summary>
//    public sealed class PlayerFlightGroupsCollectionEditor : CollectionEditor
//    {
//        /// <summary>
//        /// Constructor.
//        /// </summary>
//        /// <param name="type">The type of the collection for this editor to edit.</param>
//        public PlayerFlightGroupsCollectionEditor(Type type) : base(type) { }

//        /// <summary>
//        /// Creates the editor form. Your standard editor form, with a custom (localized) title
//        /// and a propertygrid with ToolBar disabled and Help enabled.
//        /// </summary>
//        /// <returns>The form.</returns>
//        protected override CollectionForm CreateCollectionForm()
//        {
//            CollectionForm form = base.CreateCollectionForm();

//            form.Text = UILanguage.StaticInstance.GetString("UserInterface", "Forms.PlayerFlightGroups.Title");
//            form.Shown += delegate { SearchForPropertyGrid(form); };
//            form.Size = new Size(720, 480);
//            return form;
//        }

//        /// <summary>
//        /// Looks for the proprerty grid to set its parameters.
//        /// </summary>
//        /// <param name="control">The control to search for a property grid.
//        /// If the control is not a property grid, recursively call the function to search its children.</param>
//        private void SearchForPropertyGrid(Control control)
//        {
//            if (control is PropertyGrid pGrid)
//            {
//                pGrid.HelpVisible = true;
//                pGrid.ToolbarVisible = false;
//                pGrid.PropertySort = PropertySort.Alphabetical;
//                return;
//            }

//            foreach (Control child in control.Controls)
//                SearchForPropertyGrid(child);
//        }
//    }
//}

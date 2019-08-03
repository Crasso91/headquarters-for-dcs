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

using Headquarters4DCS.Forms;
using System.ComponentModel;

namespace Headquarters4DCS.TypeConverters
{
    public sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string LanguageID;
        private readonly bool Encased;

        public LocalizedDisplayNameAttribute(string languageID, bool encased = false)
        {
            LanguageID = languageID;
            Encased = encased;
        }

        public override string DisplayName
        {
            get
            {
                string displayName = GUITools.Language.GetString("Properties", $"Name.{LanguageID}");
                if (Encased) displayName = $"({displayName})";
                return displayName;
            }
        }
    }
}

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

using System;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// Abstract parent class for all HQ library definition classes. The Load() method loads data from an .ini file.
    /// </summary>
    public abstract class Definition : IDisposable
    {
        /// <summary>
        /// The unique ID of this definition.
        /// </summary>
        public string ID { get; protected set; }

        /// <summary>
        /// The name to display for this definition.
        /// </summary>
        public string DisplayName { get; protected set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Definition() { }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public virtual void Dispose() { }

        /// <summary>
        /// Loads definition from an .ini file.
        /// </summary>
        /// <param name="id">Unique ID of the definition. Generated from the .ini file name, can be overriden by the definition.</param>
        /// <param name="iniPath">Path to definition .ini file.</param>
        /// <returns>True is successful, false if an error happened.</returns>
        public bool Load(string id, string iniPath)
        {
            ID = id.Trim();
            INIFile ini = new INIFile(iniPath);
            bool result = OnLoad(ini);
            ini.Dispose();
            if (string.IsNullOrEmpty(ID)) ID = id.Trim();
            if (ID.Contains(",")) return false; // Commas are not allowed as they're used as separators
            DisplayName = string.IsNullOrEmpty(DisplayName) ? ID : DisplayName;
            return result;
        }

        /// <summary>
        /// Loads data required by this definition.
        /// </summary>
        /// <param name="ini">INI file.</param>
        /// <returns>True is successful, false if an error happened.</returns>
        protected abstract bool OnLoad(INIFile ini);
    }
}

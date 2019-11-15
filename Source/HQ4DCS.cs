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
using Headquarters4DCS.Forms;
using Headquarters4DCS.Generator;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Headquarters4DCS
{
    /// <summary>
    /// The main application class.
    /// </summary>
    public sealed class HQ4DCS : IDisposable
    {
        /// <summary>
        /// Static entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (HQ4DCS hq = new HQ4DCS()) { }

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        /// A string represenation of HQ4DCS's current version.
        /// </summary>
        public static string HQ4DCS_VERSION_STRING = "Open Beta 2 PROTOTYPE";

        /// <summary>
        /// Targeted DCS World version. Only for information purposes, generated missions should work with other versions.
        /// </summary>
        public const string DCSWORLD_TARGETED_VERSION = "2.5.4.30386";

        /// <summary>
        /// The main application form.
        /// </summary>
        private readonly FormMain Form;

        public readonly MissionGenerator Generator;

        /// <summary>
        /// Constructor. Creates all required sub-classes and starts the application.
        /// </summary>
        public HQ4DCS()
        {
            if (!Library.Instance.LoadAll())
            {
                // If failed to load the definitions library, abort and exit.
                return;
            }

            Generator = new MissionGenerator();

            using (Form = new FormMain(this))
            {
                Application.Run(Form); // Show the main form
            }
        }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose()
        {
            if (Library.Instance != null) Library.Instance.Dispose();
        }
    }
}

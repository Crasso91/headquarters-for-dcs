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
using Headquarters4DCS.Library;
//using Headquarters4DCS.Template;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Headquarters4DCS
{
    /*
     * HOW DOES HQ4DCS WORK?
     * 
     * HQ4DCS has been designed to be as simple as possible. Its basic principle is as follows:
     * 
     * 1. Populate the library (Headquarters4DCS.Library.HQLibrary) with data from the .ini files found in the Library directory.
     *    This data will be used to know what common parameters should be used, such as how far each type of enemy air defense should
     *    be spawned from the player's original position, but also what options should be offered to the player in the mission template
     *    menu (theaters, coalitions...).
     * 2. Let the user create a mission template (Headquarters4DCS.Template.MissionTemplate) from the WinForms interface, or load one from
     *    a template (.hqt file – basically just a renamed .ini file)
     * 3. When the user clicks the "generate mission" button, create an instance of the mission generator
     *    (Headquarters4DCS.Generator.MissionGenerator) which will convert the mission template into an HQMission
     *    (Headquarters4DCS.Mission.HQMission). The HQMission contains all information about the mission: briefing, unit groups, etc.
     * 4. When the user decides to export the HQMission to a DCS World .miz file, create an instance of the MIZExporter class
     *    (Headquarters4DCS.MIZExport.MIZExporter) which will create a .miz file from the HQMission using the Lua scripts
     *    in the Include directory.
    */


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
        public static string HQ4DCS_VERSION_STRING { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }

        /// <summary>
        /// Targeted DCS World version. Only for information purposes, generated missions should work with other versions.
        /// </summary>
        public const string DCSWORLD_TARGETED_VERSION = "2.5.4.30386";

        /// <summary>
        /// The main application form.
        /// </summary>
        private readonly FormMain Form;

        /// <summary>
        /// The mission template.
        /// </summary>
        //public readonly MissionTemplate Template;

        /// <summary>
        /// Settings and presets loaded from the INI subdirectory.
        /// </summary>
        //public readonly HQLibrary Library;

        /// <summary>
        /// Constructor. Creates all required sub-classes and starts the application.
        /// </summary>
        public HQ4DCS()
        {
            // Load library from INI files
            //Library = new HQLibrary();
            if (!HQLibrary.Instance.LoadAll()) return; // If failed to load library, abort and exit.

            // Create the mission template
            //Template = new MissionTemplate(Library);

            // Show the main form
            Form = new FormMain(this);
            Application.Run(Form);
        }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose()
        {
            //if (Template != null) Template.Dispose();
            //if (Library != null) Library.Dispose();
        }
    }
}

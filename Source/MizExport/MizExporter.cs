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

using Headquarters4DCS.GeneratedMission;
using Headquarters4DCS.DefinitionLibrary;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Headquarters4DCS.MizExport
{
    /// <summary>
    /// Exports HQ4DCS missions to DCS World .miz files.
    /// </summary>
    public sealed class MizExporter : IDisposable
    {
        /// <summary>
        /// Dictionary of files to save into .miz file.
        /// </summary>
        private readonly Dictionary<string, byte[]> Entries = new Dictionary<string, byte[]>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public MizExporter() { }

        /// <summary>
        /// Adds an entry to the .miz file.
        /// </summary>
        /// <param name="entryPath">The path to the entry in the .miz file.</param>
        /// <param name="text">The (UTF-8) text of the entry.</param>
        /// <returns>True if everything went well, false if an error happened.</returns>
        private bool AddMIZEntry(string entryPath, string text)
        {
            DebugLog.Instance.Log($"Adding text file {entryPath} to the .miz file");

            if (string.IsNullOrEmpty(text)) text = "";
            else text = text.Replace("\r\n", "\n"); // Convert CRLF end of lines to LF, CRLF can cause problems

#if DEBUG
            // In debug builds, write all text entries to the DebugOutput path for easier debugging/comparisons
            string debugDumpFileName = entryPath.Replace('\\', '/').Replace('/', '_');
            if (Path.GetExtension(entryPath).Length == 0) debugDumpFileName += ".lua";
            HQTools.CreateDirectoryIfMissing(HQTools.PATH_DEBUG);
            File.WriteAllText(HQTools.PATH_DEBUG + debugDumpFileName, text);
#endif

            return AddMIZEntry(entryPath, Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// Adds an entry to the .miz file.
        /// </summary>
        /// <param name="entryPath">The path to the entry in the .miz file.</param>
        /// <param name="bytes">The bytes of the entry.</param>
        /// <returns>True if everything went well, false if an error happened.</returns>
        private bool AddMIZEntry(string entryPath, byte[] bytes)
        {
            DebugLog.Instance.Log($"Adding binary file {entryPath} to the .miz file");

            if (Entries.ContainsKey(entryPath.Replace('\\', '/'))) return false;
            Entries.Add(entryPath, bytes);
            return true;
        }

        /// <summary>
        /// Saves the mission to a DCS World .miz file.
        /// </summary>
        /// <param name="missHQ">The HQ4DCS mission to use.</param>
        /// <param name="filePath">The path to the .miz file to save to.</param>
        /// <returns>True if everything went well, false if an error happened.</returns>
        public bool CreateMizFile(Mission missHQ, string filePath)
        {
            if (missHQ == null) return false;
            if (string.IsNullOrEmpty(filePath)) return false;

            bool exportedSucessfully = true;

            DefinitionLanguage language = Library.Instance.GetDefinition<DefinitionLanguage>(missHQ.Language);
            if (language == null) return false;

            Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();
            Entries.Clear();
            DebugLog.Instance.Clear();
            DebugLog.Instance.Log($"Starting .miz export (to file {filePath}) at {DateTime.Now.ToLongTimeString()}...");
            DebugLog.Instance.Log();

            // Add entries to the .miz file
            try
            {
                // HQ4DCS stuff, not required by DCS World
                AddMIZEntry("Credits.txt", $"Generated with HQ4DCS version {HQ4DCS.HQ4DCS_VERSION_STRING} ({HQTools.WEBSITE_URL})");
                AddMIZEntry("hq4dcs/Briefing.html", missHQ.BriefingHTML); // HTML briefing
                AddMIZEntry("hq4dcs/GenerationLog.txt", missHQ.GenerationLog); // Mission generator log

                // "Mission" Lua file
                using (MizExporterLuaMission luaMission = new MizExporterLuaMission())
                { AddMIZEntry("mission", luaMission.MakeLua(missHQ)); }

                // "Options" Lua file
                using (MizExporterLuaOptions luaOptions = new MizExporterLuaOptions())
                { AddMIZEntry("options", luaOptions.MakeLua(missHQ)); }

                // "Warehouses" Lua file
                using (MizExporterLuaWarehouse luaWarehouses = new MizExporterLuaWarehouse())
                { AddMIZEntry("warehouses", luaWarehouses.MakeLua(missHQ)); }

                // New in DCS World 2.5.5: a "theatre" file containing nothing but the name of the theater.
                // Doesn't seem to do anything, but add it anyway just to be safe.
                using (MizExporterLuaMission luaMission = new MizExporterLuaMission()) { AddMIZEntry("theatre", missHQ.Theater); }

                // Mission script Lua
                using (MizExporterLuaScript luaScript = new MizExporterLuaScript(language))
                { AddMIZEntry("l10n/DEFAULT/script.lua", luaScript.MakeLua(missHQ)); }

                // "Dictionary" Lua file
                using (MizExporterLuaDictionary luaDictionary = new MizExporterLuaDictionary())
                { AddMIZEntry("l10n/DEFAULT/dictionary", luaDictionary.MakeLua()); }

                // "MapResource" Lua file
                using (MizExporterLuaMapResource luaMapResource = new MizExporterLuaMapResource())
                { AddMIZEntry("l10n/DEFAULT/mapResource", luaMapResource.MakeLua(missHQ)); }

                // Add all audio (.ogg) files
                using (MizExporterMediaAudio mediaAudio = new MizExporterMediaAudio())
                {
                    Dictionary<string, byte[]> audioFiles = mediaAudio.GetMediaFiles(missHQ);
                    foreach (string k in audioFiles.Keys)
                        AddMIZEntry(k, audioFiles[k]);
                }

                // Add all images files but kneeboard pages (handled differently)
                using (MizExporterMediaImages mediaImages = new MizExporterMediaImages())
                {
                    Dictionary<string, byte[]> imageFiles = mediaImages.MakeImages(missHQ);
                    foreach (string k in imageFiles.Keys)
                        AddMIZEntry(k, imageFiles[k]);
                }

                // Add kneeboard briefing pages
                using (MizExporterMediaKneeboard mediaKneeboardImages = new MizExporterMediaKneeboard())
                {
                    Dictionary<string, byte[]> imageFiles = mediaKneeboardImages.MakeKneeboardImages(missHQ);
                    foreach (string k in imageFiles.Keys)
                        AddMIZEntry(k, imageFiles[k]);
                }

                // Save everything to a .miz file (just a .zip file with a different extension)
                using (FileStream fS = new FileStream(filePath, FileMode.Create))
                {
                    using (ZipOutputStream zS = new ZipOutputStream(fS))
                    {
                        foreach (string k in Entries.Keys)
                        {
                            zS.PutNextEntry(new ZipEntry(k));
                            zS.Write(Entries[k], 0, Entries[k].Length);
                        }
                        zS.Finish();
                    }

                    fS.Close();
                }

                stopwatch.Stop();
                DebugLog.Instance.Log();
                DebugLog.Instance.Log($"Completed .miz export at {DateTime.Now.ToLongTimeString()} (took {stopwatch.Elapsed.TotalMilliseconds.ToString("F0")} milliseconds).");
            }
#if DEBUG
            catch (Exception e)
#else
            catch (HQ4DCSException e)
#endif
            {
                stopwatch.Stop();
                DebugLog.Instance.Log($"ERROR: {e}");
                DebugLog.Instance.Log();
                DebugLog.Instance.Log($"Export to .miz FAILED.");

                MessageBox.Show(e.Message, "Export to .miz failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                exportedSucessfully = false;
            }

            // Save log to the Logs directory
            DebugLog.Instance.SaveToFileAndClear("ExportToMIZ");

            return exportedSucessfully;
        }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }
    }
}

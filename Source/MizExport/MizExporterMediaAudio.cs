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
using System;
using System.Collections.Generic;
using System.IO;

namespace Headquarters4DCS.MizExport
{
    /// <summary>
    /// Handles the inclusion of all .ogg files into the .miz file.
    /// </summary>
    public sealed class MizExporterMediaAudio : IDisposable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MizExporterMediaAudio() { }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Reads the bytes of all .ogg files required for a mission.
        /// </summary>
        /// <param name="missHQ">An HQ4DCS mission.</param>
        /// <returns>A dictionary of entries to include the .miz file. Key is the entry name, value is an array holding the bytes of the entry.</returns>
        public Dictionary<string, byte[]> GetMediaFiles(Mission missHQ)
        {
            DebugLog.Instance.Log($"Adding audio files...");

            Dictionary<string, byte[]> audioFiles = new Dictionary<string, byte[]>();

            foreach (string f in missHQ.OggFiles)
            {
                string oggFilePath = HQTools.PATH_INCLUDE + "Ogg\\" + f + ".ogg";
                if (!File.Exists(oggFilePath))
                {
                    DebugLog.Instance.Log($"WARNING: Failed to load .ogg file Include\\Ogg\\{f}.ogg");
                    continue; // File doesn't exist, continue
                }

                audioFiles.Add($"l10n/DEFAULT/{f.ToLowerInvariant()}.ogg", File.ReadAllBytes(oggFilePath));
            }

            return audioFiles;
        }
    }
}

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
using Headquarters4DCS.GeneratedMission;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Headquarters4DCS.MizExport
{
    /// <summary>
    /// Creates the custom kneeboard images for the mission.
    /// </summary>
    public sealed class MizExporterMediaKneeboard : IDisposable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MizExporterMediaKneeboard() { }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Creates all custom kneeboard images and returns them as a dictionary of byte arrays.
        /// </summary>
        /// <param name="mission">An HQ4DCS mission.</param>
        /// <returns>A dictionary of entries to include the .miz file. Key is the entry name, value is an array holding the bytes of the entry.</returns>
        public Dictionary<string, byte[]> MakeKneeboardImages(Mission mission)
        {
            Dictionary<string, byte[]> kneeboardImages = new Dictionary<string, byte[]>();

            DebugLog.Instance.Log($"Adding kneeboard images...");

            Image kneeboardImage = null;

            using (HTMLExporter htmlExporter = new HTMLExporter())
            { kneeboardImage = htmlExporter.ExportToImage(mission.BriefingHTML); }
            if (kneeboardImage == null) return kneeboardImages; // Failed to generate an image, abort

            // TODO: format is wrong - should be 768x1024
            // TODO: briefing should be split in multiple pages to make sure even long briefings are readable
            foreach (string acType in mission.UsedPlayerAircraftTypes)
                kneeboardImages.Add($"KNEEBOARD/{acType}/IMAGES/01.png", HQTools.ImageToBytes(kneeboardImage, ImageFormat.Png));

            return kneeboardImages;
        }
    }
}

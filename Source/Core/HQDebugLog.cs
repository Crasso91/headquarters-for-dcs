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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Headquarters4DCS
{
    /// <summary>
    /// A static class used to log notes, warnings and errors to text files in the Logs subdirectory.
    /// </summary>
    public sealed class HQDebugLog
    {
        /// <summary>
        /// HQDebugLog singleton.
        /// </summary>
        public static HQDebugLog Instance
        {
            get
            {
                if (_Instance == null) _Instance = new HQDebugLog();
                return _Instance;
            }
        }

        /// <summary>
        /// HQDebugLog private singleton object.
        /// </summary>
        private static HQDebugLog _Instance = null;

        /// <summary>
        /// Maximum number of log files stored in the "Logs" subdirectory. If there are more, the oldest ones are deleted.
        /// </summary>
        private const int MAX_LOG_FILES = 20;

        /// <summary>
        /// The lines in the log.
        /// </summary>
        private List<string> LogLines = new List<string>();

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The message to log. Leave empty to print an empty line.</param>
        public void Log(string message = "")
        {
            LogLines.Add(message);
        }

        /// <summary>
        /// Saves the content of the log to a file and clears the log.
        /// </summary>
        /// <param name="fileSuffix">A suffix to append to the date/time in the filename.</param>
        /// <returns>True if file was written successfully, false if something went wrong.</returns>
        public bool SaveToFileAndClear(string fileSuffix = null)
        {
            DeleteOldLogFiles();

            if (!HQTools.CreateDirectoryIfMissing(HQTools.PATH_LOGS)) { Clear(); return false; }

            string filePath = HQTools.PATH_LOGS + string.Format("{0:yyyy-MM-dd (hh-mm-ss.fff)}", DateTime.Now);
            if (!string.IsNullOrEmpty(fileSuffix)) filePath += " " + fileSuffix;
            filePath += ".txt";

            try
            {
                File.WriteAllLines(filePath, LogLines.ToArray(), Encoding.UTF8);
                Clear();
                return true;
            }
            catch (Exception) // Failed to save log file (no write access to the Logs directory?)
            {
                Clear();
                return false;
            }
        }

        /// <summary>
        /// If there's more than MAX_LOG_FILES in the Logs directory, delete the oldest files.
        /// </summary>
        private void DeleteOldLogFiles()
        {
            if (!Directory.Exists(HQTools.PATH_LOGS)) return;

            // Sort by name because files are named according to date and time of generation.
            string[] logFiles = Directory.GetFiles(HQTools.PATH_LOGS, "*.txt").OrderBy(x => x).ToArray();
            if (logFiles.Length <= MAX_LOG_FILES) return;

            Array.Resize(ref logFiles, logFiles.Length - MAX_LOG_FILES - 1);

            foreach (string f in logFiles)
            {
                try { File.Delete(f); }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Clears all logged messages.
        /// </summary>
        public void Clear()
        {
            LogLines.Clear();
        }

        /// <summary>
        /// Returns the whole log as a single string.
        /// </summary>
        /// <returns>All messages of the log joined in a single string.</returns>
        public string GetFullLog()
        {
            return string.Join("\r\n", LogLines);
        }

        /// <summary>
        /// Returns the last line (message) of the log.
        /// </summary>
        /// <returns>The last line, or an empty string if the log is empty.</returns>
        public string GetLastMessage()
        {
            if (LogLines.Count == 0) return "";
            return LogLines.Last();
        }
    }
}

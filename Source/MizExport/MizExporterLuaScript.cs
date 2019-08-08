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
using Headquarters4DCS.GeneratedMission;
using System;
using System.Text.RegularExpressions;

namespace Headquarters4DCS.MizExport
{
    /// <summary>
    /// Creates the "l10n/DEFAULT/script.lua" entry in the MIZ file.
    /// </summary>
    public sealed class MizExporterLuaScript : IDisposable
    {
        /// <summary>
        /// The language definition to use for the script.
        /// </summary>
        private readonly DefinitionLanguage Language;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MizExporterLuaScript(DefinitionLanguage language) { Language = language; }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Generates the content of the Lua file.
        /// </summary>
        /// <param name="missHQ">An HQ4DCS mission.</param>
        /// <returns>The contents of the Lua file.</returns>
        public string MakeLua(Mission missHQ)
        {
            //int i, j;

            string lua = HQTools.ReadIncludeLuaFile("Script.lua");

            // Add the debug script to scripts generated with the debug build.
#if DEBUG
            lua += HQTools.ReadIncludeLuaFile("Script\\DebugMenu.lua") + "\n\n";
#endif

            //for (i = 0; i < HQTools.MISSION_SCRIPT_SCOPE_COUNT; i++)
            //{
            //    string scriptReplacement = "";

            //    scriptReplacement += missHQ.ScriptsMission[i] + "\n";
            //    for (j = 0; j < missHQ.Objectives.Count; j++)
            //        scriptReplacement += missHQ.ScriptsObjective[i].Replace("$ID$", HQTools.ValToString(j + 1)) + "\n";

            //    HQTools.ReplaceKey(ref lua, $"Script{((MissionScriptScope)i).ToString()}", scriptReplacement);
            //}

            HQTools.ReplaceKey(ref lua, "UnitNames", CreateUnitNamesTable(missHQ.UseNATOCallsigns));
            HQTools.ReplaceKey(ref lua, "ObjectiveNames", CreateObjectiveNamesTable(missHQ));
            HQTools.ReplaceKey(ref lua, "PlayerCoalition", missHQ.CoalitionPlayer.ToString().ToUpperInvariant());
            HQTools.ReplaceKey(ref lua, "EnemyCoalition", missHQ.CoalitionEnemy.ToString().ToUpperInvariant());
            HQTools.ReplaceKey(ref lua, "ObjectiveCount", missHQ.Objectives.Length);

            DoLocalizationReplacements(ref lua);

            return lua;
        }

        /// <summary>
        /// Returns a Lua table with the localized names of all DCS World units.
        /// </summary>
        /// <param name="useNATOcallsigns">Should units be named according to their NATO callsigns when available (e.g. SA-10, not S-300)?</param>
        /// <returns>A string containing a Lua table with all units names, using the DCS World unit name as key.</returns>
        private string CreateUnitNamesTable(bool useNATOcallsigns)
        {
            string namesTable = $"hq.unitNames =\n{{\n";
            foreach (string u in Language.GetAllLocalizedUnitTypes())
                namesTable += $"[\"{u}\"] = \"{Language.GetUnitName(u, useNATOcallsigns)}\",\n";
            namesTable += "}";

            return namesTable;
        }

        /// <summary>
        /// Copy all included scripts into the mission Lua script.
        /// </summary>
        /// <param name="lua">The mission Lua script.</param>
        /// <param name="missHQ">A HQ4DCS mission.</param>
        private void CopyMissionLuaScripts(ref string lua, Mission missHQ)
        {
            string[] fullScriptCode = new string[HQTools.EnumCount<MissionScriptScope>()];
            for (int i = 0; i < fullScriptCode.Length; i++) fullScriptCode[i] = "";

            foreach (MissionScript script in missHQ.Scripts)
            {
                string scriptLua = HQTools.ReadIncludeLuaFile($"Script\\{script.LuaFile}");

                fullScriptCode[(int)script.ScriptScope] += scriptLua + "\n\n";
            }

            // Copy scripts in the mission Lua script.
            HQTools.ReplaceKey(ref lua, "ScriptGlobal", fullScriptCode[(int)MissionScriptScope.Global]);
            HQTools.ReplaceKey(ref lua, "ScriptEvent", fullScriptCode[(int)MissionScriptScope.Event]);
            HQTools.ReplaceKey(ref lua, "ScriptTimer", fullScriptCode[(int)MissionScriptScope.Timer]);
        }

        /// <summary>
        /// Replaces all strings encaded in pound signs (£) by a localized string found in the language definition.
        /// Format is £SECTION/KEY£
        /// </summary>
        /// <param name="lua">The mission Lua script.</param>
        private void DoLocalizationReplacements(ref string lua)
        {
            Regex rgx = new Regex("£.*£");
            foreach (Match m in rgx.Matches(lua))
            {
                string localizedString = "";

                string value = m.Value.Trim('£', ' ', '\t');
                localizedString = Language.GetStringRandom("InGame", value);
                if (string.IsNullOrEmpty(localizedString)) localizedString = value;

                // Remove all pound signs from replacements to make sure we don't get into an endless regex match loop
                localizedString = localizedString.Replace("£", "");

                lua = rgx.Replace(lua, localizedString, 1);
            }
        }

        /// <summary>
        /// Returns a Lua string table storing with the names of all objectives.
        /// </summary>
        /// <param name="missHQ">A HQ4DCS mission.</param>
        /// <returns>A Lua table in a string</returns>
        private string CreateObjectiveNamesTable(Mission missHQ)
        {
            string objectiveNames = "{ ";

            for (int i = 0; i < missHQ.Objectives.Length; i++)
                objectiveNames += $"\"{missHQ.Objectives[i].Name}\"" + ((i == missHQ.Objectives.Length - 1) ? "" : ", ");

            objectiveNames += " }\n";

            return objectiveNames;
        }
    }
}

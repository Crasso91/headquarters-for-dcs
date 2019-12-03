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
using Headquarters4DCS.Mission;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Headquarters4DCS.Miz
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
        /// <param name="mission">An HQ4DCS mission.</param>
        /// <returns>The contents of the Lua file.</returns>
        public string MakeLua(DCSMission mission)
        {
            string lua = HQTools.ReadIncludeLuaFile("Script.lua");

            GenerateCommonScript(ref lua, mission);
            CopyMissionLuaScripts(ref lua, mission);

#if DEBUG
            // Add the debug script to scripts generated with the debug build.
            lua += HQTools.ReadIncludeLuaFile("Script\\DebugMenu.lua") + "\n\n";
#endif
            MakeCommonReplacements(ref lua, mission);
            DoLocalizationReplacements(ref lua);

            return lua;
        }

        /// <summary>
        /// Read values from the mission and make replacements in the Lua file
        /// </summary>
        /// <param name="lua">Lua string</param>
        /// <param name="mission">HQ4DCS mission to use</param>
        private void MakeCommonReplacements(ref string lua, DCSMission mission)
        {
            HQTools.ReplaceKey(ref lua, "UnitNames", CreateUnitNamesTable(mission.UseNATOCallsigns));
            HQTools.ReplaceKey(ref lua, "ObjectiveNames", CreateObjectiveNamesTable(mission));
            HQTools.ReplaceKey(ref lua, "PlayerCoalition", mission.CoalitionPlayer.ToString().ToUpperInvariant());
            HQTools.ReplaceKey(ref lua, "EnemyCoalition", mission.CoalitionEnemy.ToString().ToUpperInvariant());
            HQTools.ReplaceKey(ref lua, "ObjectiveCount", mission.Objectives.Length);
        }

        /// <summary>
        /// Generate the script common to all missions at the beginning of the Lua code.
        /// </summary>
        /// <param name="lua">Mission Lua script.</param>
        /// <param name="mission">HQ4DCS mission</param>
        private void GenerateCommonScript(ref string lua, DCSMission mission)
        {
            string scriptLua = "";
            scriptLua += $"hq.objectiveCount = {mission.Objectives.Length}\n";
            scriptLua += $"hq.objectiveLeft = {mission.Objectives.Length}\n";
            scriptLua += $"hq.objectiveNames = {{ \"{string.Join("\", \"", (from DCSMissionObjectiveLocation o in mission.Objectives select o.Name.ToUpperInvariant()))}\" }}\n";
            scriptLua += $"hq.objectiveStatus = {{ {string.Join(", ", Enumerable.Repeat("false", mission.Objectives.Length))} }}\n";

            HQTools.ReplaceKey(ref lua, "ScriptCommon", scriptLua);
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
        /// <param name="mission">A HQ4DCS mission.</param>
        private void CopyMissionLuaScripts(ref string lua, DCSMission mission)
        {
            DefinitionObjective objectiveDef = Library.Instance.GetDefinition<DefinitionObjective>(mission.ObjectiveDefinition);
            if (objectiveDef == null) return;

            int i;

            // For each script scope (global, event and timer)...
            foreach (ObjectiveScriptScope scope in HQTools.EnumValues<ObjectiveScriptScope>())
            {
                string scriptLua = "";

                // ...add once every script to include once...
                foreach (string scriptFile in objectiveDef.IncludeLua[(int)ObjectiveScriptRepetition.Once, (int)scope])
                    scriptLua += HQTools.ReadIncludeLuaFile($"Script\\{scriptFile}");

                // ...and add each time for each objective the scripts to include once for each objective
                for (i = 0; i < mission.Objectives.Length; i++)
                {
                    string objectiveScriptLua = "";

                    foreach (string scriptFile in objectiveDef.IncludeLua[(int)ObjectiveScriptRepetition.Each, (int)scope])
                        objectiveScriptLua += HQTools.ReadIncludeLuaFile($"Script\\{scriptFile}");

                    HQTools.ReplaceKey(ref objectiveScriptLua, "ObjectiveID", 1 + i);
                    HQTools.ReplaceKey(ref objectiveScriptLua, "GroupID", 1001 + i);

                    scriptLua += objectiveScriptLua + "\n";
                }

                HQTools.ReplaceKey(ref lua, $"Script{scope.ToString()}", scriptLua);
            }
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
        private string CreateObjectiveNamesTable(DCSMission missHQ)
        {
            string objectiveNames = "{ ";

            for (int i = 0; i < missHQ.Objectives.Length; i++)
                objectiveNames += $"\"{missHQ.Objectives[i].Name}\"" + ((i == missHQ.Objectives.Length - 1) ? "" : ", ");

            objectiveNames += " }\n";

            return objectiveNames;
        }
    }
}

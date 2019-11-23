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
using Headquarters4DCS.Template;
using System;
using System.IO;
using System.Linq;

namespace Headquarters4DCS.Generator
{
    /// <summary>
    /// Generates an HQMission's name, raw-text and HTML briefings.
    /// </summary>
    public sealed class MissionGeneratorBriefing : IDisposable
    {
        /// <summary>
        /// The maximum of random parts to use in the mission name.
        /// </summary>
        private const int MAX_MISSION_NAME_PARTS = 9;

        /// <summary>
        /// The language definition to use.
        /// </summary>
        private readonly DefinitionLanguage Language;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="language">The language definition to use.</param>
        public MissionGeneratorBriefing(DefinitionLanguage language) { Language = language; }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Generate a random mission name if none is provided in the template, or returns the provided name if there is one.
        /// </summary>
        /// <param name="mission">The mission.</param>
        /// <param name="template">The provided mission name in the template.</param>
        public void GenerateMissionName(DCSMission mission, string templateMissionName)
        {
            DebugLog.Instance.Log("Generating mission name...");

            if (templateMissionName == null) templateMissionName = "";

            // If a custom mission name was provided, use it...
            if (!string.IsNullOrEmpty(templateMissionName.Trim()))
            {
                mission.BriefingName = templateMissionName.Trim();
                return;
            }

            // ...else generate a random name.
            // First get a random template then replace parts 1 to MAX_MISSION_NAME_PARTS ($PART1$, $PART2$, $PART3$...) by random parts.
            string name = HQTools.RandomFrom(Language.GetStringArray("Mission", "Name.Template"));
            for (int i = 1; i <= MAX_MISSION_NAME_PARTS; i++)
                name = name.Replace(
                    $"$P{i.ToString()}$",
                    HQTools.RandomFrom(Language.GetStringArray("Mission", $"Name.Part{i.ToString()}")));

            mission.BriefingName = name;

            DebugLog.Instance.Log("");
        }

        /// <summary>
        /// Generates a random mission description if none is provided in the template, or returns the provided name if there is one.
        /// </summary>
        /// <param name="mission">The mission.</param>
        /// <param name="templateMissionDescription">The provided description in the template.</param>
        /// <param name="missionObjective">The mission objective definition.</param>
        //public void GenerateMissionDescription(HQMission mission, string templateMissionDescription, DefinitionMissionObjective missionObjective)
        //{
        //    HQDebugLog.Instance.Log("Generating mission briefing description...");

        //    if (templateMissionDescription == null) templateMissionDescription = "";

        //    // If a custom mission description was provided, use it...
        //    if (!string.IsNullOrEmpty(templateMissionDescription.Trim()))
        //    {
        //        mission.BriefingDescription = templateMissionDescription.Trim();
        //        return;
        //    }

        //    // ...else generate a random description
        //    mission.BriefingDescription = Language.GetStringRandom("BriefingMission", $"Description.{missionObjective.BriefingDescription}");

        //    HQDebugLog.Instance.Log("");
        //}

        /// <summary>
        /// Generates the list of tasks for the mission.
        /// </summary>
        /// <param name="mission"></param>
        /// <param name="template"></param>
        /// <param name="missionTask"></param>
        //public void GenerateMissionTasks(HQMission mission, MissionTemplate template, DefinitionMissionObjective missionTask)
        //{
        //    HQDebugLog.Instance.Log("Generating mission briefing objectives...");

        //    mission.BriefingTasks.Clear();

        //    mission.BriefingTasks.Add(Language.GetStringRandom("BriefingCommon", $"Task.TakeOff").Replace("$AIRBASE$", mission.Airbases[0].Name));

        //    foreach (HQMissionObjectiveLocation o in mission.Objectives)
        //        mission.BriefingTasks.Add(Language.GetStringRandom("BriefingMission", $"Task.{missionTask.BriefingTask}").Replace("$NAME$", o.Name));

        //    mission.BriefingTasks.Add(Language.GetStringRandom("BriefingCommon", $"Task.Land").Replace("$AIRBASE$", mission.Airbases[1].Name));

        //    HQDebugLog.Instance.Log("");
        //}

        //public void GenerateMissionRemarks(HQMission mission, MissionTemplate template, DefinitionMissionObjective missionTask)
        //{
        //    HQDebugLog.Instance.Log("Generating mission briefing remarks...");

        //    mission.BriefingRemarks.Clear();

        //    foreach (string s in missionTask.BriefingRemarks)
        //        mission.BriefingRemarks.Add(Language.GetStringRandom("BriefingMission", $"Remark.{s}"));

        //    HQDebugLog.Instance.Log("");
        //}

        public void GenerateRawTextBriefing(DCSMission mission, MissionTemplate template)
        {
            DebugLog.Instance.Log("Generating raw text MIZ briefing...");

            string text = "";

            if (template.GetPlayerCount() == 1)
                text += $"{Language.GetString("Briefing", "Subtitle.SinglePlayer")}\n\n";
            else
                text += $"{Language.GetString("Briefing", "Subtitle.PvE").Replace("$PLAYERS$", HQTools.ValToString(template.GetPlayerCount()))}\n\n";

            text += mission.BriefingDescription + "\n\n";

            // Tasks
            text += $"{Language.GetString("Briefing", "Section.Tasks").ToUpperInvariant()}{Language.Semicolon}\n";
            foreach (string t in mission.BriefingTasks) text += $"- {t}\n";
            if (mission.BriefingTasks.Count == 0) text += $"- {Language.GetString("Briefing", "Misc.None")}\n";
            text += "\n";

            // Remarks
            text += $"{Language.GetString("Briefing", "Section.Remarks").ToUpperInvariant()}{Language.Semicolon}\n";
            if (mission.BriefingImperialUnits)
                text += $"- {Language.GetString("Briefing", "Remark.TotalFlightPlanNM", "Distance", (mission.TotalFlightPlanDistance * HQTools.METERS_TO_NM).ToString("F0"))}\n";
            else
                text += $"- {Language.GetString("Briefing", "Remark.TotalFlightPlanKM", "Distance", (mission.TotalFlightPlanDistance / 1000.0).ToString("F0"))}\n";
            foreach (string t in mission.BriefingRemarks) text += $"- {t}\n";
            text += "\n";

            // Flight package
            //text += $"{GetString("Section.Package").ToUpperInvariant()}{Language.Semicolon}\n";
            //foreach (HQMissionBriefingFlightGroup fg in (from HQMissionBriefingFlightGroup f in mission.BriefingFlightPackage where !f.IsSupport select f).OrderBy(x => x.Task))
            //    text += $"- {fg.Callsign} ({fg.UnitCount}x {GetUnitName(fg.UnitType)}), {HQTools.ValToString(fg.Frequency, "F1")} Mhz\n";

            // Make sure endlines are in the proper format (escaped LF) or it can cause bugs.
            text = text.Replace("\r\n", "\n").Trim(' ', '\n', '\t').Replace("\n", "\\\n");
            mission.BriefingRawText = text;

            DebugLog.Instance.Log("");
        }

        public void GenerateHTMLBriefing(DCSMission mission, MissionTemplate template/*, DefinitionMissionObjective missionTask*/)
        {
            DebugLog.Instance.Log("Generating HTML briefing...");

            string htmlTemplateFile = HQTools.PATH_INCLUDE + "Briefing.html";
            string htmlTemplate = File.Exists(htmlTemplateFile) ? File.ReadAllText(htmlTemplateFile) : "$BRIEFING$";
            string html = "";

            string semiColon = Language.GetString("Misc", "Semicolon");

            html += $"<h1>{mission.BriefingName}</h1>";

            if (template.GetPlayerCount() == 1)
                html += $"<h3>{Language.GetString("Briefing", "Subtitle.SinglePlayer")}</h3>";
            else
                html += $"<h3>{Language.GetString("Briefing", "Subtitle.PvE").Replace("$PLAYERS$", HQTools.ValToString(template.GetPlayerCount()))}</h3>";

            // Header (objective/task, date, time...)
            html += "<p>";
            html += $"<strong>{Language.GetString("Briefing", "Section.Date")}{semiColon}</strong> {FormatDate(mission, true)}<br />";
            html += $"<strong>{Language.GetString("Briefing", "Section.Time")}{semiColon}</strong> {FormatTime(mission, true)}<br />";
            html += $"<strong>{Language.GetString("Briefing", "Section.Weather")}{semiColon}</strong> {Language.GetEnum(mission.WeatherLevel)}<br />";
            html += $"<strong>{Language.GetString("Briefing", "Section.Wind")}{semiColon}</strong> {Language.GetEnum(mission.WindLevel)}";
            html += $" ({mission.WeatherWindSpeedAverage.ToString("F0")} m/s)";
            html += "</p>";

            // Description
            html += $"<h2>{Language.GetString("Briefing", "Section.Description")}</h2>";
            html += $"<p>{mission.BriefingDescription}</p>";

            // Tasks
            html += $"<h2>{Language.GetString("Briefing", "Section.Tasks")}</h2>";
            html += "<ul>";
            foreach (string task in mission.BriefingTasks) html += $"<li>{task}</li>";
            if (mission.BriefingTasks.Count == 0) html += $"<li>{Language.GetString("Briefing", "Misc.None")}</li>";
            html += "</ul>";

            // Remarks
            html += $"<h2>{Language.GetString("Briefing", "Section.Remarks")}</h2>";
            html += "<ul>";
            if (mission.BriefingImperialUnits)
                html += $"<li>{Language.GetString("Briefing", "Remark.TotalFlightPlanNM", "Distance", (mission.TotalFlightPlanDistance * HQTools.METERS_TO_NM).ToString("F0"))}</li>";
            else
                html += $"<li>{Language.GetString("Briefing", "Remark.TotalFlightPlanKM", "Distance", (mission.TotalFlightPlanDistance / 1000.0).ToString("F0"))}</li>";
            foreach (string remark in mission.BriefingRemarks) html += $"<li>{remark}</li>";
            html += "</ul>";

            // mission.FlightPlanLength

            // Airbases
            //html += $"<h2>{Language.GetString("BriefingCommon", "Airbases")}</h2>";
            //html += "<table>";
            //html += "<tr><th></th><th>Airbase</th><th>TCN</th><th>ATC</th><th>RWY</th><th>ILS</th></tr>"; // FIXME: Localize
            //{
            //    string header; // FIXME: Localize
            //    if (i == 0) header = "DEP";
            //    else if (i == 1) header = "ARR";
            //    else header = "NAV";

            //    DefinitionTheaterAirbase airbase = mission.Airbases[i];

            //    html += $"<tr><th>{header}</th><td>{airbase.Name}</td><td>{airbase.TACAN}</td><td>{HQTools.ValToString(airbase.ATC, "F1")}</td><td>{airbase.Runways[0]}</td><td>{airbase.ILS}</td></tr>";
            //}
            //html += "</table>";

            // Flight package
            html += $"<h2>{Language.GetString("Briefing", "Section.FlightPackage")}</h2>";
            html += "<table>";
            html += $"<tr><th>{Language.GetString("Briefing", "Table.Header.Callsign")}</th><th>{Language.GetString("Briefing", "Table.Header.Aircraft")}</th><th>{Language.GetString("Briefing", "Table.Header.Task")}</th><th>{Language.GetString("Briefing", "Table.Header.Airbase")}</th><th>{Language.GetString("Briefing", "Table.Header.Radio")}</th></tr>";
            foreach (DCSMissionBriefingFlightGroup fg in (from DCSMissionBriefingFlightGroup f in mission.BriefingFlightPackage where !f.IsSupport select f).OrderBy(x => x.Task))
                html += // TODO: localize fg.Task
                    $"<tr><td>{fg.Callsign}</td><td>{fg.UnitCount}x {GetUnitName(fg.UnitType)}</td>" +
                    $"<td>{fg.Task}</td><td>{fg.AirbaseName}</td>" + 
                    $"<td>{HQTools.ValToString(fg.Frequency, "F1")}</td></tr>";
            html += "</table>";

            // Support flight groups
            //html += $"<h2>{Language.GetString("BriefingCommon", "Support")}</h2>";
            //html += "<table>";
            //html += "<tr><th></th><th>Aircraft</th><th>Callsign</th><th>UHF</th><th>TACAN</th></tr>"; // FIXME: Localize
            //foreach (HQMissionBriefingFlightGroup fg in (from HQMissionBriefingFlightGroup f in mission.BriefingFlightPackage where f.IsSupport select f).OrderBy(x => x.Task))
            //    html += $"<tr><th>{fg.Task}</th><td>{GetUnitName(fg.UnitType)}</td><td>{fg.Callsign}</td><td>{HQTools.ValToString(fg.Frequency, "F1")}</td><td>{fg.TACAN}</td></tr>";
            //html += "</table>";

            // Flight plan
            //html += $"<h2>{Language.GetString("BriefingCommon", "FlightPlan")}</h2>";
            //html += "<table>";
            //html += "<tr><th></th><th>ID</th><th>Action</th><th>Dist</th><th>Alt</th></tr>"; // FIXME: Localize
            //double totalWpDist = 0.0;
            //for (int i = 0; i < mission.Waypoints.Count; i++)
            //{
            //    HQMissionWaypoint wp = mission.Waypoints[i];
            //    if (i > 0) totalWpDist += wp.Coordinates.GetDistanceFrom(mission.Waypoints[i - 1].Coordinates);

            //    if (template.BriefingUnits == SpeedAndDistanceUnit.Metric)
            //        html += $"<tr><th>{i + 1}</th><td>{wp.Name}</td><td>NO ACTION FIXME</td><td>{((i == 0) ? "0" : Math.Round(totalWpDist / 1000.0).ToString("F0"))} Km</td><td>{wp.AltitudeMultiplier * 2000}</td></tr>";
            //    else
            //        html += $"<tr><th>{i + 1}</th><td>{wp.Name}</td><td>NO ACTION FIXME</td><td>{((i == 0) ? "0" : Math.Round(totalWpDist * HQTools.METERS_TO_NM).ToString("F0"))} nm</td><td>{wp.AltitudeMultiplier * 2000}</td></tr>";
            //}
            //html += "</table>";

            mission.BriefingHTML = htmlTemplate.Replace("$BRIEFING$", html);

            DebugLog.Instance.Log("");
        }

        private string FormatDate(DCSMission mission, bool longFormat)
        {
            string formattedString = Language.GetString("Briefing", longFormat ? "Format.Date.Long" : "Format.Date.Short");

            DateTime dt = new DateTime(2003, 5, 1);

            formattedString = formattedString
                .Replace("$W$", Language.GetEnum(new DateTime(mission.DateYear, (int)mission.DateMonth + 1, mission.DateDay).DayOfWeek))
                .Replace("$D$", HQTools.ValToString(mission.DateDay, "0"))
                .Replace("$DD$", HQTools.ValToString(mission.DateDay, "00"))
                .Replace("$DDD$", Language.GetOrdinalAdjective(mission.DateDay))
                .Replace("$M$", HQTools.ValToString((int)mission.DateMonth + 1, "0"))
                .Replace("$MM$", HQTools.ValToString((int)mission.DateMonth + 1, "00"))
                .Replace("$MMM$", Language.GetEnum(mission.DateMonth))
                .Replace("$YY$", HQTools.ValToString(mission.DateYear).Substring(2))
                .Replace("$YYYY$", HQTools.ValToString(mission.DateYear));

            return formattedString;
        }

        private string FormatTime(DCSMission mission, bool longFormat)
        {
            string formattedString = Language.GetString("Briefing", longFormat ? "Format.Time.Long" : "Format.Time.Short");

            formattedString = formattedString
                .Replace("$H$", HQTools.ValToString(mission.TimeHour, "0"))
                .Replace("$HH$", HQTools.ValToString(mission.TimeHour, "00"))
                .Replace("$M$", HQTools.ValToString(mission.TimeMinute, "0"))
                .Replace("$MM$", HQTools.ValToString(mission.TimeMinute, "00"));

            return formattedString;
        }

        private string GetUnitName(string unitType, bool useNATOName = true)
        {
            // When multiple values are provided, separated by a pipe (|), first value is the NATO designation, second value is the original name

            string[] names = (from n in Language.GetStringArray("UnitNames", unitType, '|') select n.Trim()).ToArray();

            if (names.Length == 0) return unitType;
            if (names.Length == 1) return names[0];
            return names[useNATOName ? 0 : 1];
        }
    }
}

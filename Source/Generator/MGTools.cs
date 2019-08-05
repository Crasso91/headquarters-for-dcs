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

namespace Headquarters4DCS.Generator
{
    /// <summary>
    /// A "toolbox" static class with some useful methods to help with mission generation.
    /// </summary>
    public static class MGTools
    {
        public static string GetDCSTaskNameString(DCSAircraftTask dcsTask)
        {
            // FIXME: "Ground attack" in two words, etc.

            //switch (dcsTask)
            //{
            //    //case  AircraftTask.
            //}

            return dcsTask.ToString();
        }

        public static string GetDCSTaskAdditionalTasksString(DCSAircraftTask dcsTask, int firstTaskIndex = 1)
        {
            string taskInfo = "";

            switch (dcsTask)
            {
                case DCSAircraftTask.CAS:
                    taskInfo =
                        "[$1$] = { [\"enabled\"] = true, [\"key\"] = \"CAS\", [\"id\"] = \"EngageTargets\", [\"number\"] = $1$, " +
                        "[\"auto\"] = true, [\"params\"] = { [\"targetTypes\"] = { [1] = \"Helicopters\", [2] = \"Ground Units\", [3] = \"Light armed ships\", }, [\"priority\"] = 0, }, },";
                    break;
            }

            taskInfo = taskInfo.Replace("$1$", HQTools.ValToString(firstTaskIndex));
            return taskInfo;
        }
    }
}

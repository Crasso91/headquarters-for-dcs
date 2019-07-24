using Headquarters4DCS.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Headquarters4DCS.TypeConverters
{
    public sealed class INIFileListTypeConverter<T> : StringConverter, IDisposable where T : Definition
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(GetINIFiles());
        }

        public static string GetDefaultValue()
        {
            string[] vals = GetINIFiles();
            return (vals.Length == 0) ? "" : vals[0];
        }

        public static string[] GetINIFiles()
        {
            string definitionsDirectory = HQLibrary.GetDirectoryFromType<T>();
            bool allowEmpty = false;
            //switch (definitionsDirectory)
            //{
            //    case "DefinitionCoalition": definitionsDirectory = "Coalitions"; break;
            //    case "DefinitionFeatures": definitionsDirectory = "Features"; break;
            //    case "DefinitionObjective": allowEmpty = true; definitionsDirectory = "Objectives"; break;
            //}

            List<string> fileNames = new List<string>();
            if (allowEmpty) fileNames.Add("");
            AddINIFilesToList(fileNames, $"{HQTools.PATH_LIBRARY}\\{definitionsDirectory}");
            fileNames.Sort();
            return fileNames.ToArray();
        }

        private static void AddINIFilesToList(List<string> fileNames, string iniDirectory)
        {
            if (!Directory.Exists(iniDirectory)) return;

            foreach (string f in Directory.GetFiles(iniDirectory, "*.ini"))
            {
                string id = Path.GetFileNameWithoutExtension(f);
                if (fileNames.Contains(id.ToLowerInvariant())) continue;
                fileNames.Add(id);
            }

            foreach (string iniSubDirectory in Directory.GetDirectories(iniDirectory))
                AddINIFilesToList(fileNames, iniSubDirectory);
        }

        public void Dispose() { }
    }
}

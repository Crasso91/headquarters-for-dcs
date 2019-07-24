using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Headquarters4DCS.Enums;
using Headquarters4DCS.Library;

namespace Headquarters4DCS.Template
{
    public sealed class HQTemplate : IDisposable
    {
        /// <summary>
        /// Default language
        /// </summary>
        public const string DEFAULT_LANGUAGE = "English";

        /// <summary>
        /// Default blue coalition.
        /// </summary>
        public const string DEFAULT_BLUE_COALITION = "USA";

        /// <summary>
        /// Default red coalition.
        /// </summary>
        public const string DEFAULT_RED_COALITION = "Russia";

        /// <summary>
        /// Default theater.
        /// </summary>
        public const string DEFAULT_THEATER = "Caucasus";

        /// <summary>
        /// Default player aircraft.
        /// </summary>
        public const string DEFAULT_AIRCRAFT = "Su-25T";

        /// <summary>
        /// Theater in which the mission takes place.
        /// </summary>
        public string Theater { get; private set; } = DEFAULT_THEATER;

        public readonly HQTemplateSettings Settings = null;

        public Dictionary<string, HQTemplateNode> Nodes = new Dictionary<string, HQTemplateNode>();

        public HQTemplate()
        {
            Settings = new HQTemplateSettings();

            Clear(DEFAULT_THEATER);
        }

        public void Clear(string theaterID)
        {
            Theater = HQLibrary.Instance.DefinitionExists<DefinitionTheater>(theaterID) ? theaterID : DEFAULT_THEATER;
            DefinitionTheater theaterDefinition = HQLibrary.Instance.GetDefinition<DefinitionTheater>(Theater);

            Nodes.Clear();

            foreach (DefinitionTheaterNode n in theaterDefinition.Nodes.Values)
            {
                if (n is DefinitionTheaterNodeAirbase)
                    Nodes.Add(n.ID, new HQTemplateNodeAirbase(n));
                //else if (n is DefinitionTheaterNodeCarrierLocation)
                //    Nodes.Add(n.ID, new HQTemplateNodeCarrierGroup(n));
                else if (n is DefinitionTheaterNodeRegion)
                    Nodes.Add(n.ID, new HQTemplateNodeRegion(n));
            }
        }

        public void InvertAirbasesCoalition()
        {
            string[] keys = Nodes.Keys.ToArray();

            foreach (string k in keys)
            {
                if (!(Nodes[k] is HQTemplateNodeAirbase)) continue;
                ((HQTemplateNodeAirbase)Nodes[k]).Coalition = 1 - ((HQTemplateNodeAirbase)Nodes[k]).Coalition;
            }
        }

        public bool LoadFromFile(string filePath)
        {
            using (INIFile ini = new INIFile(filePath))
            {
                string theater = ini.GetValue<string>("Settings", "Theater");
                Clear(theater);

                DefinitionTheater theaterDefinition = HQLibrary.Instance.GetDefinition<DefinitionTheater>(Theater);

                foreach (string k in Nodes.Keys)
                {
                    Nodes[k].LoadFromFile(ini);
                }
            }

            return true;
        }

        public bool SaveToFile(string filePath)
        {
            using (INIFile ini = new INIFile())
            {
                ini.SetValue("Settings", "Theater", Theater);
                Settings.SaveToFile(ini);

                foreach (string k in Nodes.Keys)
                    Nodes[k].SaveToFile(ini);

                ini.SaveToFile(filePath);
            }

            return true;
        }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose() { }
    }
}

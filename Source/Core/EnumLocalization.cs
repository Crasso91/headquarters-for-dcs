//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Headquarters4DCS
//{
//    public static class EnumLocalization
//    {
//        private static readonly Dictionary<string, string> STRINGS = new Dictionary<string, string>
//        {
//            { "PlayerFlightGroupTask.AntishipStrike", "Antiship strike" },
//            { "PlayerFlightGroupTask.FighterSweep", "Fighter sweep" },
//            { "PlayerFlightGroupTask.GroundAttack", "Ground attack" },
//            { "PlayerFlightGroupTask.PinpointStrike", "Pinpoint strike" },
//            { "PlayerFlightGroupTask.RunwayAttack", "Runway attack" },
//        };

//        public static string Localize<T>(T value) where T : struct
//        {
//            string key = $"{typeof(T).Name}.{value.ToString()}";
//            return STRINGS.ContainsKey(key) ? STRINGS[key] : key.ToString();
//        }

//        public static TaggedString LocalizeTagged<T>(T value) where T : struct
//        {
//            return new TaggedString(value.ToString(), Localize(value));
//        }

//        public static string[] LocalizeAll<T>() where T : struct
//        {
//            return (from T e in (T[])Enum.GetValues(typeof(T)) select Localize(e)).ToArray();
//        }

//        public static TaggedString[] LocalizeAllTagged<T>() where T : struct
//        {
//            return (from T e in (T[])Enum.GetValues(typeof(T)) select new TaggedString(e.ToString(), Localize(e))).ToArray();
//        }
//    }
//}

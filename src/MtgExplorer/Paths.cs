using System.IO;
using System.Text.RegularExpressions;
using MtgExplorer.Mtg;
using MtgExplorer.Properties;

namespace MtgExplorer
{
    /// <summary>
    ///   SetRoot
    ///       [Set Name]
    ///           [Cards] - card.json
    ///           [Data] - cardlink.json and card.html
    ///           [Images] - card.png
    ///           TextSpoiler.html
    ///           Checklist.html
    ///           Compact.html
    ///           Standard.html
    /// </summary>
    public class Paths
    {
        public static string SetRoot
        {
            get { return Settings.Default.RootDataFolder; }
        }

        public static string GetSetPath(SetNode set)
        {
            string safeSetName = SanitizeFileName(set.Name);
            return Path.Combine(SetRoot, safeSetName);
        }

        public static string GetSetTextSpoilerPath(SetNode set)
        {
            return GetSetDumpFile(set, "TextSpoiler");
        }

        public static string GetSetChecklistPath(SetNode set)
        {
            return GetSetDumpFile(set, "Checklist");
        }

        public static string GetSetCompactPath(SetNode set)
        {
            return GetSetDumpFile(set, "Compact");
        }

        public static string GetSetStandardPath(SetNode set)
        {
            return GetSetDumpFile(set, "Standard");
        }

        private static string GetSetDumpFile(SetNode set, string viewName)
        {
            string root = GetSetPath(set);
            return Path.Combine(root, string.Format("{0}.html", viewName));
        }

        public static void EnsurePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string SanitizeFileName(string path)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
            string result = Regex.Replace(path, invalidReStr, "_");
            return result;
        }

        public static string GetCardPath(SetNode set)
        {
            return Path.Combine(GetSetPath(set), "Cards");
        }

        public static string GetCardPath(SetNode set, string multiverseId)
        {
            return Path.Combine(GetCardPath(set), string.Format("{0}.json", multiverseId));
        }

        public static string GetCardLinkPath(SetNode set, string multiverseId)
        {
            return Path.Combine(GetCardDataPath(set), string.Format("{0}.json", multiverseId));
        }

        public static string GetCardHtmlPath(SetNode set, string multiverseId)
        {
            return Path.Combine(GetCardDataPath(set), string.Format("{0}.html", multiverseId));
        }

        public static string GetCardDataPath(SetNode set)
        {
            return Path.Combine(GetSetPath(set), "Data");
        }

        public static string GetCardImagePath(SetNode set)
        {
            return Path.Combine(GetSetPath(set), "Images");
        }

        public static string GetCardImagePath(SetNode set, string multiverseId)
        {
            return Path.Combine(GetCardImagePath(set), string.Format("{0}.png", multiverseId));
        }
    }
}
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MtgExplorer.Mtg;

namespace MtgExplorer.Gatherer
{
    public class GathererSetScraper : ScraperBase
    {
        public static void GetSetContents(SetNode set)
        {
            using (var client = new HttpClient())
            {
                Paths.EnsurePathExists("Standard");
                Paths.EnsurePathExists("Compact");
                Paths.EnsurePathExists("Checklist");
                Paths.EnsurePathExists("TextSpoiler");

                GetStandard(client, set);
                GetCompact(client, set);
                GetChecklist(client, set);
                GetTextSpoiler(client, set);
            }
        }

        public static void GetStandard(HttpClient client, SetNode set)
        {
            Uri uri = GathererQuery.CreateStandardUri(set.Name);
            DumpSetPageToFile(client, uri, Paths.GetSetStandardPath(set));
        }

        public static void GetCompact(HttpClient client, SetNode set)
        {
            Uri uri = GathererQuery.CreateCompactUri(set.Name);
            DumpSetPageToFile(client, uri, Paths.GetSetCompactPath(set));
        }

        public static void GetChecklist(HttpClient client, SetNode set)
        {
            Uri uri = GathererQuery.CreateChecklistUri(set.Name);
            DumpSetPageToFile(client, uri, Paths.GetSetChecklistPath(set));
        }

        public static void GetTextSpoiler(HttpClient client, SetNode set)
        {
            Uri uri = GathererQuery.CreateTextSpoilerUri(set.Name);
            DumpSetPageToFile(client, uri, Paths.GetSetTextSpoilerPath(set));
        }

        private static void DumpSetPageToFile(HttpClient client, Uri uri, string path)
        {
            Task<string> response = client.GetStringAsync(uri);
            Paths.EnsurePathExists(path);
            File.WriteAllText(path, response.Result, Encoding.UTF8);
        }
    }
}
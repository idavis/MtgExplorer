using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MtgExplorer.Entities;
using MtgExplorer.Generators;
using MtgExplorer.Mtg;

namespace MtgExplorer.Gatherer
{
    public class SetToCardRipper : ScraperBase
    {
        public static void ExtractCardDetailsGatherer()
        {
            foreach (SetNode set in SetGenerator.Sets)
            {
                ExtractCardDetailsGatherer(set);
            }
        }

        public static void ExtractCardDetailsGatherer(SetNode set)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Set {0}", set.Name);
                List<CardLinkInformation> cards = LoadCardsForSet(set);
                foreach (CardLinkInformation card in cards)
                {
                    Console.WriteLine("\t{0}", card.Name);
                    DumpCardPageToFile(client, set, card);
                }
            }
        }

        private static void DumpCardPageToFile(HttpClient client, SetNode set, CardLinkInformation card)
        {
            Task<string> response = client.GetStringAsync(card.CardUri);
            string filePath = Paths.GetCardHtmlPath(set, card.MultiverseId);
            Paths.EnsureFilePathExists(filePath);
            File.WriteAllText(filePath, response.Result, Encoding.UTF8);
        }
    }
}
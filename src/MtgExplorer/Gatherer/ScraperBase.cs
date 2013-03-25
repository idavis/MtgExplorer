using System.Collections.Generic;
using System.IO;
using System.Text;
using MtgExplorer.Entities;
using MtgExplorer.Mtg;
using Newtonsoft.Json;

namespace MtgExplorer.Gatherer
{
    public class ScraperBase
    {
        protected static List<CardLinkInformation> LoadCardsForSet(SetNode set)
        {
            string path = Paths.GetCardDataPath(set);
            string[] files = Directory.GetFiles(path, "*.json");
            var cards = new List<CardLinkInformation>();
            foreach (string file in files)
            {
                string contents = File.ReadAllText(file, Encoding.UTF8);
                var card = JsonConvert.DeserializeObject<CardLinkInformation>(contents);
                card.Reinitialize();
                cards.Add(card);
            }
            return cards;
        }

        protected static void DumpCardsToFile(IEnumerable<CardLinkInformation> cards, SetNode set)
        {
            string dataPath = Paths.GetCardDataPath(set);
            Paths.EnsurePathExists(dataPath);
            foreach (CardLinkInformation card in cards)
            {
                string filePath = Paths.GetCardLinkPath(set, card.MultiverseId);
                string value = JsonConvert.SerializeObject(card);
                File.WriteAllText(filePath, value, Encoding.UTF8);
            }
        }
    }
}
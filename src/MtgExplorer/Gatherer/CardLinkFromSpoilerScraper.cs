using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using MtgExplorer.Entities;
using MtgExplorer.Generators;
using MtgExplorer.Mtg;

namespace MtgExplorer.Gatherer
{
    public class CardLinkFromSpoilerScraper : ScraperBase
    {
        public static void ExtractCardDetailsFromTextSpoiler()
        {
            foreach (SetNode set in SetGenerator.Sets)
            {
                ExtractCardLinksFromTextSpoiler(set);
            }
        }

        public static void ExtractCardLinksFromTextSpoiler(SetNode set)
        {
            var doc = new HtmlDocument();
            string path = Paths.GetSetTextSpoilerPath(set);
            doc.Load(path, Encoding.UTF8);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a[@class=\"nameLink\"]");
            if (nodes == null)
            {
                Console.WriteLine("Not found in {0}", set);
                return;
            }

            var cards = new List<CardLinkInformation>();
            foreach (HtmlNode node in nodes)
            {
                string href = node.GetAttributeValue("href", "");
                string cardName = node.InnerText;
                string multiverseId = href.Substring(href.IndexOf("=", StringComparison.Ordinal) + 1);
                var cardInformation = new CardLinkInformation(multiverseId, cardName, set.Name);
                cards.Add(cardInformation);
            }
            DumpCardsToFile(cards, set);
            Console.WriteLine("Finished Processing {0}", set);
        }
    }
}
using System;
using System.Diagnostics;
using MtgExplorer.Generators;
using MtgExplorer.Mtg;

namespace MtgExplorer.Gatherer
{
    public class GathererScraper
    {
        public static void ExportAllGathererData()
        {
            foreach (SetNode set in SetGenerator.Sets)
            {
                if (set.Name == "Vanguard")
                {
                    continue;
                }
                Stopwatch stopwatch = Stopwatch.StartNew();
                ExportGathererSetData(set);
                stopwatch.Stop();
                Console.WriteLine("Processed Set {0} in {1}.", set.Name, stopwatch.Elapsed);
            }
        }

        public static void ExportGathererSetData(SetNode set)
        {
            GathererSetScraper.GetSetContents(set);
            CardLinkFromSpoilerScraper.ExtractCardLinksFromTextSpoiler(set);
            SetToCardRipper.ExtractCardDetailsGatherer(set);
            CardPageRipper.ExtractAllCardDataFromOracle(set, true);
            //CardImageRipper.ExtractCardImagesFromGatherer(set);
        }
    }
}
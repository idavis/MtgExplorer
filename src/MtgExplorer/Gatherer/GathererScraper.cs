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
                ExportGathererSetData(set);
            }
        }

        public static void ExportGathererSetData(SetNode set)
        {
            GathererSetScraper.GetSetContents(set);
            CardLinkFromSpoilerScraper.ExtractCardLinksFromTextSpoiler(set);
            SetToCardRipper.ExtractCardDetailsGatherer(set);
            CardPageRipper.ExtractAllCardDataFromOracle(set);
        }
    }
}
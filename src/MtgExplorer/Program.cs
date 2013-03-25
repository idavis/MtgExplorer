using System;
using MtgExplorer.Gatherer;
using MtgExplorer.Generators;

namespace MtgExplorer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GathererScraper.ExportGathererSetData(SetGenerator.Sets[0]);
            //ExtractAllCardsFromGatherer();
            //PopulateDatabase();
            Console.WriteLine("Finished...");
            Console.ReadLine();
        }

 
        private static void PopulateDatabase()
        {
            MtgUniverseGenerator.Create();
        }
    }
}
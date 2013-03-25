using System;
using MtgExplorer.Generators;

namespace MtgExplorer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            //ExtractAllCardsFromGatherer();
            //PopulateDatabase();
            Console.ReadLine();
        }

 
        private static void PopulateDatabase()
        {
            MtgUniverseGenerator.Create();
        }
    }
}
using System;
using System.Diagnostics;
using MtgExplorer.Gatherer;
using MtgExplorer.Generators;
using MtgExplorer.Mtg;

namespace MtgExplorer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // change to true when done testing
            // this will force json files to be written to disk.
            //CardPageRipper.ExtractAllCardDataFromOracle(writeCardsToDisk:false);
            //PopulateDatabase();
            PopulateDatabase();
            Console.WriteLine("Finished...");
            Console.ReadLine();
        }

 
        private static void PopulateDatabase()
        {
            MtgUniverseGenerator.Create();
        }
    }
}
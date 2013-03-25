using System.Collections.Generic;
using System.IO;
using System.Linq;
using MtgExplorer.Entities;
using MtgExplorer.Gatherer;
using MtgExplorer.Generators;
using MtgExplorer.Mtg;
using NUnit.Framework;

namespace MtgExplorer.Tests
{
    [TestFixture]
    public class PickNameOncePatternsAreFiguredOut
    {
        private const int AlaraReborn = 0;

        [Test]
        public void SingleTypeAndSubtypeAreSplitProperly()
        {
            SetNode set = SetGenerator.Sets[AlaraReborn];
            string setPath = Paths.GetCardDataPath(set);
            string file = Path.Combine(setPath, "144249.html");
            List<CardInstance> cards = CardPageRipper.ExtractCardDataHtml(file, set).ToList();
            Assert.AreEqual(1, cards.Count);
            CardInstance card = cards[0];
            Assert.AreEqual(1, card.Types.Length);
            Assert.AreEqual(Types.Creature, card.Types[0]);
            Assert.AreEqual(1, card.Subtypes.Length);
            Assert.AreEqual("Spider", card.Subtypes[0]);
        }
    }
}
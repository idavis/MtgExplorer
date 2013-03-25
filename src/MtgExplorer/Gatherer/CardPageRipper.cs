using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using MtgExplorer.Entities;
using MtgExplorer.Generators;
using MtgExplorer.Mtg;
using Newtonsoft.Json;

namespace MtgExplorer.Gatherer
{
    public class CardPageRipper : ScraperBase
    {
        public const string Ctl00Root = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_rightCol";
        public const string Ctl05Root = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_rightCol";
        public const string Ctl06Root = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_rightCol";

        private const string Ctl00NameRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_nameRow";
        private const string Ctl00ManaRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_manaRow";
        private const string Ctl00ConvertedManaCostRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_cmcRow";
        private const string Ctl00TypeRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_typeRow";
        private const string Ctl00TextRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_textRow";
        private const string Ctl00FlavorTextRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_flavorRow";
        private const string Ctl00RarityRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_rarityRow";
        private const string Ctl00OtherSetsRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_otherSetsValue";
        private const string Ctl00CardNumberInSetRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_numberRow";
        private const string Ctl00ArtistRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_artistRow";
        private const string Ctl00ArtistCredit = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ArtistCredit";
        private const string Ctl00PowerToughnessRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ptRow";
        private const string Ctl00WatermarkRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_markRow";

        private const string Ctl00ColorIndicatorRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_colorIndicatorRow";

        private const string Ctl00RulingsContainer =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_rulingsContainer";

        private const string Ctl00RulingDateRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_rulingsRepeater_ctl00_rulingDate";

        private const string Ctl00RulingTextRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_rulingsRepeater_ctl00_rulingText";

        private const string Ctl05NameRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_nameRow";
        private const string Ctl05ManaRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_manaRow";

        private const string Ctl05ConvertedManaCostRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_cmcRow";

        private const string Ctl05TypeRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_typeRow";
        private const string Ctl05TextRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_textRow";
        private const string Ctl05FlavorTextRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_flavorRow";
        private const string Ctl05RarityRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_rarityRow";

        private const string Ctl05OtherSetsRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_otherSetsValue";

        private const string Ctl05CardNumberInSetRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_numberRow";

        private const string Ctl05ArtistRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_artistRow";

        private const string Ctl05ArtistCredit =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_ArtistCredit";

        private const string Ctl05PowerToughnessRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_ptRow";
        private const string Ctl05WatermarkRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_markRow";

        private const string Ctl05ColorIndicatorRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_colorIndicatorRow";

        private const string Ctl05RulingsContainer =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl05_rulingsContainer";

        private const string Ctl05RulingDateRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_rulingsRepeater_ctl05_rulingDate";

        private const string Ctl05RulingTextRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_rulingsRepeater_ctl05_rulingText";

        private const string Ctl06NameRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_nameRow";
        private const string Ctl06ManaRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_manaRow";

        private const string Ctl06ConvertedManaCostRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_cmcRow";

        private const string Ctl06TypeRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_typeRow";
        private const string Ctl06TextRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_textRow";
        private const string Ctl06FlavorTextRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_flavorRow";
        private const string Ctl06RarityRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_rarityRow";

        private const string Ctl06OtherSetsRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_otherSetsValue";

        private const string Ctl06CardNumberInSetRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_numberRow";

        private const string Ctl06ArtistRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_artistRow";

        private const string Ctl06ArtistCredit =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_ArtistCredit";

        private const string Ctl06PowerToughnessRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_ptRow";
        private const string Ctl06WatermarkRow = "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_markRow";

        private const string Ctl06ColorIndicatorRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_colorIndicatorRow";

        private const string Ctl06RulingsContainer =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl06_rulingsContainer";

        private const string Ctl06RulingDateRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_rulingsRepeater_ctl06_rulingDate";

        private const string Ctl06RulingTextRow =
            "ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_rulingsRepeater_ctl06_rulingText";

        private static readonly string[] RootRows = new[] {Ctl00Root, Ctl05Root, Ctl06Root};
        private static readonly string[] NameRows = new[] {Ctl00NameRow, Ctl05NameRow, Ctl06NameRow};
        private static readonly string[] ManaRows = new[] {Ctl00ManaRow, Ctl05ManaRow, Ctl06ManaRow};

        private static readonly string[] ConvertedManaCostRows = new[]
            {Ctl00ConvertedManaCostRow, Ctl05ConvertedManaCostRow, Ctl06ConvertedManaCostRow};

        private static readonly string[] TypeRows = new[] {Ctl00TypeRow, Ctl05TypeRow, Ctl06TypeRow};
        private static readonly string[] TextRows = new[] {Ctl00TextRow, Ctl05TextRow, Ctl06TextRow};

        private static readonly string[] FlavorTextRows = new[]
            {Ctl00FlavorTextRow, Ctl05FlavorTextRow, Ctl06FlavorTextRow};

        private static readonly string[] RarityRows = new[] {Ctl00RarityRow, Ctl05RarityRow, Ctl06RarityRow};
        private static readonly string[] OtherSetsRows = new[] {Ctl00OtherSetsRow, Ctl05OtherSetsRow, Ctl06OtherSetsRow};

        private static readonly string[] CardNumberInSetRows = new[]
            {Ctl00CardNumberInSetRow, Ctl05CardNumberInSetRow, Ctl06CardNumberInSetRow};

        private static readonly string[] ArtistRows = new[] {Ctl00ArtistRow, Ctl05ArtistRow, Ctl06ArtistRow};
        private static readonly string[] ArtistCredits = new[] {Ctl00ArtistCredit, Ctl05ArtistCredit, Ctl06ArtistCredit};

        private static readonly string[] PowerToughnessRows = new[]
            {Ctl00PowerToughnessRow, Ctl05PowerToughnessRow, Ctl06PowerToughnessRow};

        private static readonly string[] WatermarkRows = new[] {Ctl00WatermarkRow, Ctl05WatermarkRow, Ctl06WatermarkRow};

        private static readonly string[] ColorIndicatorRows = new[]
            {Ctl00ColorIndicatorRow, Ctl05ColorIndicatorRow, Ctl06ColorIndicatorRow};

        private static readonly string[] RulingsContainerRows = new[]
            {Ctl00RulingsContainer, Ctl05RulingsContainer, Ctl06RulingsContainer};

        private static readonly string[] RulingDateRows = new[]
            {Ctl00RulingDateRow, Ctl05RulingDateRow, Ctl06RulingDateRow};

        private static readonly string[] RulingTextRows = new[]
            {Ctl00RulingTextRow, Ctl05RulingTextRow, Ctl06RulingTextRow};

        public static void ExtractAllCardDataFromOracle(bool writeCardsToDisk = true)
        {
            foreach (SetNode set in SetGenerator.Sets)
            {
                ExtractAllCardDataFromOracle(set, writeCardsToDisk);
            }
        }

        public static void ExtractAllCardDataFromOracle(SetNode set, bool writeCardsToDisk = false)
        {
            Console.WriteLine("Processing {0}", set.Name);

            string setPath = Paths.GetCardDataPath(set);
            string[] files = Directory.GetFiles(setPath, "*.html");
            var cards = new List<CardInstance>();

            foreach (string file in files)
            {
                IEnumerable<CardInstance> results = ExtractCardDataHtml(file, set);
                cards.AddRange(results);
            }

            if (writeCardsToDisk)
            {
                WriteCardsToDisk(cards, set);
            }
        }

        public static IEnumerable<CardInstance> ExtractCardDataHtml(string fileName, SetNode set)
        {
            var doc = new HtmlDocument();
            doc.Load(fileName, Encoding.UTF8);

            // Find the card root html tags, needed for flip cards
            IEnumerable<HtmlNode> roots = SelectAllNodes(doc.DocumentNode, "//td[@id=\"{0}\"]", RootRows);

            foreach (HtmlNode root in roots)
            {
                CardInstance card = CreateCardInstances(set, fileName, root);
                Console.WriteLine("\t{0}", card.Name);
                yield return card;
            }
        }

        private static CardInstance CreateCardInstances(SetNode set, string file, HtmlNode root)
        {
            var card = new CardInstance();
            card.MultiverseId = Path.GetFileNameWithoutExtension(file);
            card.Set = set.Name;
            card.Name = GetCardName(root);
            Color[] colorIndicator = GetColorIndicator(root);
            card.Cost = new Cost();
            card.Cost.ManaCost = GetManaCost(root);
            card.Cost.ConvertedManaCost = GetConvertedManaCost(root);
            card.Cost.Colors = ColorUtil.GetCardColor(card.Cost.ManaCost, colorIndicator);
            card.Types = GetCardTypes(root);
            card.Subtypes = GetCardSubTypes(root);
            card.OracleCardText = GetCardText(root);
            card.FlavorText = GetFlavorText(root);
            card.AbilityWords = AbilityRecognizer.GetAbilities(card);
            // TODO:
            //card.TextBox.PrintedCardText
            //card.TextBox.ReminderText
            // TODO: Parse abilities from text box as first step for ability detection.

            card.Rarity = GetCardRarity(root);
            card.Sets = GetCardSets(root).ToArray();
            card.CardNumber = GetCardNumberInSet(root);
            card.Artist = GetArtistName(root);
            card.PowerToughness = GetPowerToughness(root);
            card.Watermark = GetWatermark(root);
            card.Rulings = GetRulings(root).ToArray();
            return card;
        }

        private static void WriteCardsToDisk(IEnumerable<CardInstance> cards, SetNode set)
        {
            foreach (CardInstance card in cards)
            {
                string filePath = Paths.GetCardPath(set, card.MultiverseId);
                Paths.EnsureFilePathExists(filePath);
                string contents = JsonConvert.SerializeObject(card, Formatting.Indented);
                File.WriteAllText(filePath, contents, Encoding.UTF8);
            }
        }

        private static HtmlNodeCollection SelectNodes(HtmlNode doc, string format, IEnumerable<string> ids)
        {
            return ids.Select(id => doc.SelectNodes(string.Format(format, id))).FirstOrDefault(nodes => nodes != null);
        }

        private static IEnumerable<HtmlNode> SelectAllNodes(HtmlNode doc, string format, IEnumerable<string> ids)
        {
            return
                ids.Select(id => doc.SelectNodes(string.Format(format, id)))
                   .Where(item => item != null).Select(item => item.FirstOrDefault());
        }

        private static string GetCardName(HtmlNode doc)
        {
            // some ids change to a different id system which has _ct105 embedded in it
            // see Faithful Squire, 74093, Betrayers of Kamigawa which is also Kaiso, Memory of Loyalty
            // it is a split card meant to be flipped.

            HtmlNodeCollection nodes = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]", NameRows);
            HtmlNode node = nodes.First();
            string name = node.InnerText.Trim();
            return name;
        }

        private static string GetManaCost(HtmlNode doc)
        {
            // May be null, see Balduvian Trading Post, 3231, Alliances
            HtmlNodeCollection nodes = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]/img",
                                                   ManaRows);
            if (nodes == null)
            {
                return string.Empty;
            }

            IEnumerable<string> pieces = nodes.Select(item =>
                {
                    string value = item.GetAttributeValue("alt", string.Empty);
                    value = ReplaceManaWords(value);
                    return value;
                });
            string result = string.Join(string.Empty, pieces);
            return result;
        }

        private static string ReplaceManaWords(string value)
        {
            value = value.Replace("White", "W");
            value = value.Replace("Red", "R");
            value = value.Replace("Green", "G");
            value = value.Replace("Blue", "U");
            value = value.Replace("Black", "B");
            value = value.Replace("Phyrexian", "P");
            value = value.Replace("Tap", "T");
            return "{" + value + "}";
        }

        private static string GetConvertedManaCost(HtmlNode doc)
        {
            // May be null, see Balduvian Trading Post, 3231, Alliances
            // May be 0.5, see Little Girl, 74257, Unhinged
            HtmlNodeCollection nodes = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]",
                                                   ConvertedManaCostRows);
            if (nodes == null)
            {
                return string.Empty;
            }
            HtmlNode node = nodes.First();
            string value = node.InnerText.Trim();
            return value;
        }

        private static Types[] GetCardTypes(HtmlNode doc)
        {
            HtmlNode node = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]", TypeRows).First();
            string[] typeString = node.InnerText.Trim().Split(new[] {"â€”"}, StringSplitOptions.RemoveEmptyEntries);
            string[] typeStrings = typeString[0].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var types = new List<Types>(typeStrings.Length);
            foreach (string type in typeStrings)
            {
                Types typeValue;
                Enum.TryParse(type, true, out typeValue);
                types.Add(typeValue);
            }
            return types.ToArray();
        }

        private static string[] GetCardSubTypes(HtmlNode doc)
        {
            HtmlNode node = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]", TypeRows).First();
            string[] typeString = node.InnerText.Trim().Split(new[] {"â€”"}, StringSplitOptions.RemoveEmptyEntries);
            if (typeString.Length < 2)
            {
                return new string[0];
            }
            string[] types = typeString[1].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return types;
        }

        private static string[] GetCardText(HtmlNode doc)
        {
            // Text is optional, see Grizzled Leotau, 189647, Alara Reborn
            HtmlNodeCollection nodes = SelectNodes(doc,
                                                   "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]/div[@class=\"cardtextbox\"]",
                                                   TextRows);
            if (nodes == null)
            {
                return new string[0];
            }
            var boxes = new List<string>();
            foreach (HtmlNode node in nodes)
            {
                string text = HtmlConverter.ConvertHtml(node);
                boxes.Add(text);
            }

            return boxes.ToArray();
        }

        private static string GetFlavorText(HtmlNode doc)
        {
            HtmlNodeCollection nodes = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]",
                                                   FlavorTextRows);
            if (nodes == null)
            {
                return string.Empty;
            }
            HtmlNode node = nodes.First();

            string name = node.InnerText.Trim();
            return name;
        }

        private static string GetCardRarity(HtmlNode doc)
        {
            HtmlNode node = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]", RarityRows).First();
            string rarity = node.InnerText.Trim();
            return rarity;
        }

        private static IEnumerable<string> GetCardSets(HtmlNode doc)
        {
            HtmlNodeCollection nodes = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/a/img", OtherSetsRows);
            if (nodes == null)
                yield break;
            foreach (HtmlNode node in nodes)
            {
                string text = node.GetAttributeValue("alt", string.Empty);
                text = text.Split(new[] {'('}, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                yield return text;
            }
        }

        private static string GetCardNumberInSet(HtmlNode doc)
        {
            // Optional, see  Aesthir Glider, 3041, Alliances
            HtmlNodeCollection nodes = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]",
                                                   CardNumberInSetRows);
            if (nodes == null)
            {
                return string.Empty;
            }
            HtmlNode node = nodes.First();
            string value = node.InnerText.Trim();
            return value;
        }

        private static string GetArtistName(HtmlNode doc)
        {
            // may be null, see Bazaar of Baghdad, 984, Arabian Nights
            HtmlNodeCollection nodes =
                SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]/a", ArtistRows) ??
                SelectNodes(doc, "descendant::div[@id=\"{0}\" and @class=\"value\"]", ArtistCredits);
            HtmlNode node = nodes.First();
            string name = node.InnerText.Trim();
            return name;
        }

        private static string GetPowerToughness(HtmlNode doc)
        {
            HtmlNodeCollection nodes = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]",
                                                   PowerToughnessRows);
            if (nodes == null)
            {
                return string.Empty;
            }
            HtmlNode node = nodes.First();

            string pt = node.InnerText.Trim();
            return pt;
        }

        private static Watermark GetWatermark(HtmlNode doc)
        {
            HtmlNodeCollection nodes = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]",
                                                   WatermarkRows);
            if (nodes == null)
            {
                return Watermark.None;
            }
            HtmlNode node = nodes.First();
            if (node == null)
            {
                return Watermark.None;
            }
            string name = node.InnerText.Trim();
            Watermark watermark;
            return Enum.TryParse(name, true, out watermark) ? watermark : Watermark.None;
        }

        private static Color[] GetColorIndicator(HtmlNode doc)
        {
            // see Ghastly Haunting, 222178, Dark Ascension, Blue is the value
            // see Ravager of the Fells, 262699, Dark Ascension, Red, Green is the value
            HtmlNodeCollection nodes = SelectNodes(doc, "descendant::div[@id=\"{0}\"]/div[@class=\"value\"]",
                                                   ColorIndicatorRows);
            if (nodes == null)
            {
                return new Color[0];
            }
            HtmlNode node = nodes.First();

            string indicator = node.InnerText.Trim();
            indicator = ReplaceManaWords(indicator);
            IEnumerable<string> colorPieces =
                indicator.Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries)
                         .Select(item => "{" + item + "}");
            string colors = string.Join(string.Empty, colorPieces);
            Color[] value = ColorUtil.GetUniqueColors(colors);
            return value;
        }

        private static IEnumerable<Ruling> GetRulings(HtmlNode doc)
        {
            HtmlNodeCollection nodes = SelectNodes(doc, "//div[@id=\"{0}\"]", RulingsContainerRows);
            if (nodes == null)
            {
                yield break;
            }
            HtmlNode node = nodes.First();

            HtmlNodeCollection rulings = node.SelectNodes(string.Format("descendant::tr[starts-with(@class, \"post\")]"));
            foreach (HtmlNode ruling in rulings)
            {
                yield return GetRuling(ruling);
            }
        }

        private static Ruling GetRuling(HtmlNode doc)
        {
            List<HtmlNode> nodes = doc.SelectNodes("descendant::td").ToList();
            if (nodes.Count != 2)
            {
                throw new InvalidOperationException("Rulings must only have two td children");
            }
            DateTime date = GetRulingDate(nodes.First());
            string text = GetRulingText(nodes.Last());
            return new Ruling {Date = date, RulingText = text};
        }

        private static DateTime GetRulingDate(HtmlNode doc)
        {
            string value = doc.InnerText.Trim();
            DateTime result = DateTime.Parse(value); // Form 5/1/2009
            return result;
        }

        private static string GetRulingText(HtmlNode doc)
        {
            string text = doc.InnerText.Trim();
            return text;
        }
    }
}
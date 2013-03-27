using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using MtgExplorer.Entities;
using MtgExplorer.Mtg;
using MtgExplorer.Nodes;
using MtgExplorer.Relationships;
using Neo4jClient;
using Newtonsoft.Json;

namespace MtgExplorer.Generators
{
    public static class MtgUniverseGenerator
    {
        public static ConcurrentDictionary<string, NodeReference<CardNode>> Cards =
            new ConcurrentDictionary<string, NodeReference<CardNode>>(StringComparer.OrdinalIgnoreCase);

        public static void Create()
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data/"));
            client.Connect();
            AbilityGenerator.Create(client);
            SetGenerator.Create(client);
            BlockGenerator.Create(client);
            SetBlockRelationshipGenerator.Create(client);
            Create(client);
        }

        public static void Create(GraphClient client)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            SetGenerator.Sets.AsParallel().Select(item => Create(item, client)).ToList();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        private static TimeSpan Create(SetNode set, GraphClient client)
        {
            Console.WriteLine(set.Name);

            Stopwatch stopwatch = Stopwatch.StartNew();
            
            var setPath = Paths.GetCardPath(set);

            string[] files = Directory.GetFiles(setPath);

            foreach (string file in files)
            {
                string contents = File.ReadAllText(file, Encoding.UTF8);
                var card = JsonConvert.DeserializeObject<CardInstance>(contents);
                NodeReference<CardNode> cardRef = Cards.GetOrAdd(card.Name, name => CreateNodeReference(card, client));
                CardInstanceNode cardInstanceNode = GetInstanceNode(card);
                NodeReference<CardInstanceNode> cinstRef = client.Create(cardInstanceNode);
                client.CreateRelationship(cinstRef, new CardIsInstanceOfRelationship(cardRef));
                client.CreateRelationship(cinstRef, new CardIsInSetRelationship(SetGenerator.SetNodes[set.Name]));
                //Console.WriteLine(card.Name);
            }
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        private static NodeReference<CardNode> CreateNodeReference(CardInstance card, GraphClient client)
        {
            CardNode cardNode = GetCardNode(card);
            var cardRef = client.Create(cardNode);
            foreach (string ability in card.AbilityWords)
            {
                NodeReference<AbilityNode> abilityRef = AbilityGenerator.AbilityNodes[ability];
                client.CreateRelationship(cardRef, new CardHasAbilityRelationship(abilityRef));
            }
            return cardRef;
        }
        private static CardNode GetCardNode(CardInstance card)
        {
            var cardNode = new CardNode
                {
                    Name = card.Name,
                    PrintedCardText = card.PrintedCardText ?? string.Empty,
                    AbilityWords = card.AbilityWords,
                    FlavorText = card.FlavorText ?? string.Empty,
                    OracleCardText = card.OracleCardText,
                    ReminderText = card.ReminderText ?? string.Empty
                };
            return cardNode;
        }

        private static CardInstanceNode GetInstanceNode(CardInstance card)
        {
            var cardInstanceNode = new CardInstanceNode();
            cardInstanceNode.Name = card.Name;
            cardInstanceNode.Artist = card.Artist;
            cardInstanceNode.CardNumber = card.CardNumber;
            cardInstanceNode.MultiverseId = card.MultiverseId;

            if (!string.IsNullOrEmpty(card.PowerToughness))
            {
                string[] ptSplit = card.PowerToughness.Split('/');
                if (ptSplit.Length == 1)
                {
                    // planeswalker
                    cardInstanceNode.LoyaltyCounters = ptSplit[0];
                }
                else if (ptSplit.Length == 2)
                {
                    cardInstanceNode.Power = ptSplit[0].Trim();
                    cardInstanceNode.Toughness = ptSplit[1].Trim();
                }
            }

            cardInstanceNode.Rarity = card.Rarity;
            //cardInstanceNode.Set = set.Name;
            return cardInstanceNode;
        }
    }

    /*
    public class AbilityWordNode : AbilityNode
    {
        [JsonProperty("Name")]
        public string AbilityDescription { get; set; }
    }*/
}
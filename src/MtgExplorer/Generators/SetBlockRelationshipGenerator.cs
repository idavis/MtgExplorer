using System.Collections.Generic;
using MtgExplorer.Relationships;
using Neo4jClient;

namespace MtgExplorer.Generators
{
    public class SetBlockRelationshipGenerator
    {
        public static void Create(GraphClient client)
        {
            Dictionary<string,string[]> map = new Dictionary<string, string[]>();
            map["Ice Age"] = new[] {"Ice Age", "Alliances", "Coldsnap"};
            map["Mirage"] = new[] {"Mirage", "Visions", "Weatherlight"};
            map["Rath"] = new[] { "Tempest", "Stronghold", "Exodus" };
            map["Urzas"] = new[] { "Urza's Saga", "Urza's Legacy", "Urza's Destiny" };
            map["Masques"] = new[] { "Mercadian Masques", "Nemesis", "Prophecy" };
            map["Invasion"] = new[] { "Invasion", "Planeshift", "Apocalypse" };
            map["Odyssey"] = new[] { "Odyssey", "Torment", "Judgment" };
            map["Onslaught"] = new[] { "Onslaught", "Legions", "Scourge" };
            map["Mirrodin"] = new[] { "Mirrodin", "Darksteel", "Fifth Dawn" };
            map["Kamigawa"] = new[] { "Champions of Kamigawa", "Betrayers of Kamigawa", "Saviors of Kamigawa" };
            map["Ravnica"] = new[] { "Ravnica: City of Guilds", "Guildpact", "Dissension" };
            map["Time Spiral"] = new[] { "Time Spiral", "Planar Chaos", "Future Sight" };
            map["Lorwyn"] = new[] { "Lorwyn", "Morningtide" };
            map["Shadowmoor"] = new[] { "Shadowmoor", "Eventide" };
            map["Shards of Alara"] = new[] { "Shards of Alara", "Conflux", "Alara Reborn" };
            map["Zendikar"] = new[] { "Zendikar", "Worldwake", "Rise of the Eldrazi" };
            map["Scars of Mirrodin"] = new[] { "Scars of Mirrodin", "Mirrodin Besieged", "New Phyrexia" };
            map["Innistrad"] = new[] { "Innistrad", "Dark Ascension", "Avacyn Restored" };
            map["Return to Ravnica"] = new[] { "Return to Ravnica", "Gatecrash"/*, "Dragon's Maze" */};
            map["Core Sets"] = new[] { "Limited Edition Alpha", "Limited Edition Beta", "Unlimited Edition", "Revised Edition", "Fourth Edition", "Fifth Edition", "Classic Sixth Edition", "Seventh Edition", "Eighth Edition", "Ninth Edition", "Tenth Edition", "Magic 2010", "Magic 2011", "Magic 2012", "Magic 2013"/*, "Magic 2014" */};

            foreach (var key in map.Keys)
            {
                NodeReference blockRef = BlockGenerator.BlockNodes[key];
                var sets = map[key];
                
                foreach (var set in sets)
                {
                    var setRef = SetGenerator.SetNodes[set];
                    client.CreateRelationship(setRef, new SetIsInBlockRelationship(blockRef));
                }
            }
        }
    }
}
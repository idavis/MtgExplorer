using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MtgExplorer.Mtg;
using MtgExplorer.Nodes;
using Neo4jClient;

namespace MtgExplorer.Generators
{
    public static class AbilityGenerator
    {
        public static ReadOnlyDictionary<string, NodeReference<AbilityNode>> AbilityNodes { get; private set; }
        private static bool _Initialized;
        public static void Create(GraphClient client)
        {
            if (_Initialized)
            {
                return;
            }

            var abilityNodes = new Dictionary<string, NodeReference<AbilityNode>>(StringComparer.OrdinalIgnoreCase);
            foreach (var abilityWord in AbilityContainer.AbilityWords)
            {
                AbilityNode node = new KeywordAbilityNode() { Name = abilityWord };

                var nodeRef = client.Create(node);
                abilityNodes.Add(node.Name, nodeRef);
            }

            foreach (var abilityWord in AbilityContainer.CyclingAbilities)
            {
                AbilityNode node = new KeywordAbilityNode() { Name = abilityWord };
                var nodeRef = client.Create(node);
                abilityNodes.Add(node.Name, nodeRef);
            }

            foreach (var abilityWord in AbilityContainer.CyclingAbility)
            {
                AbilityNode node = new KeywordAbilityNode() { Name = abilityWord };
                var nodeRef = client.Create(node);
                abilityNodes.Add(node.Name, nodeRef);
            }

            foreach (var abilityWord in AbilityContainer.KeywordAbilities)
            {
                AbilityNode node = new KeywordAbilityNode() { Name = abilityWord };
                var nodeRef = client.Create(node);
                abilityNodes.Add(node.Name, nodeRef);
            }
            AbilityNodes = new ReadOnlyDictionary<string, NodeReference<AbilityNode>>(abilityNodes);
            _Initialized = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MtgExplorer.Nodes;
using Neo4jClient;

namespace MtgExplorer.Generators
{
    public static class BlockGenerator
    {
        public static ReadOnlyDictionary<string, NodeReference<BlockNode>> BlockNodes { get; private set; }
        private static bool _Initialized;
        public static void Create(GraphClient client)
        {
            if (_Initialized)
            {
                return;
            }

            var blockNodes = new Dictionary<string, NodeReference<BlockNode>>(StringComparer.OrdinalIgnoreCase);

            foreach (var node in Blocks)
            {
                var nodeRef = client.Create(node);
                blockNodes[node.Name] = nodeRef;
            }

            BlockNodes = new ReadOnlyDictionary<string, NodeReference<BlockNode>>(blockNodes);
            _Initialized = true;
        }

        public static readonly BlockNode[] Blocks = new []
            {
                new BlockNode(){Name = "Core Sets"},
                new BlockNode(){Name ="Early Expansions"},
                new BlockNode(){Name ="Ice Age"},
                new BlockNode(){Name ="Innistrad"},
                new BlockNode(){Name ="Invasion"},
                new BlockNode(){Name ="Kamigawa"},
                new BlockNode(){Name ="Lorwyn"},
                new BlockNode(){Name ="Masques"},
                new BlockNode(){Name ="Mirage"},
                new BlockNode(){Name ="Mirrodin"},
                new BlockNode(){Name ="Miscellaneous"},
                new BlockNode(){Name ="Odyssey"},
                new BlockNode(){Name ="Onslaught"},
                new BlockNode(){Name ="Rath"},
                new BlockNode(){Name ="Ravnica"},
                new BlockNode(){Name ="Return to Ravnica"},
                new BlockNode(){Name ="Scars of Mirrodin"},
                new BlockNode(){Name ="Shadowmoor"},
                new BlockNode(){Name ="Shards of Alara"},
                new BlockNode(){Name ="Time Spiral"},
                new BlockNode(){Name ="Urzas"},
                new BlockNode(){Name ="Zendikar"}
            };
    }
}
using MtgExplorer.Mtg;
using MtgExplorer.Nodes;
using Neo4jClient;

namespace MtgExplorer.Relationships
{
    public class SetIsInBlockRelationship : Relationship,
                                            IRelationshipAllowingSourceNode<SetNode>,
                                            IRelationshipAllowingTargetNode<BlockNode>
    {
        public SetIsInBlockRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return "IS_IN_BLOCK"; }
        }
    }
}
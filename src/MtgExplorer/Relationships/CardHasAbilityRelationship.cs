using MtgExplorer.Nodes;
using Neo4jClient;

namespace MtgExplorer.Relationships
{
    public class CardHasAbilityRelationship : Relationship,
                                              IRelationshipAllowingSourceNode<CardNode>,
                                              IRelationshipAllowingTargetNode<AbilityNode>
    {
        public CardHasAbilityRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return "HAS_ABILITY"; }
        }
    }
}
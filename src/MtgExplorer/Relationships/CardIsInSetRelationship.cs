using MtgExplorer.Mtg;
using MtgExplorer.Nodes;
using Neo4jClient;

namespace MtgExplorer.Relationships
{
    public class CardIsInSetRelationship : Relationship,
                                           IRelationshipAllowingSourceNode<CardInstanceNode>,
                                           IRelationshipAllowingTargetNode<SetNode>
    {
        public CardIsInSetRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return "IS_IN_SET"; }
        }
    }
}
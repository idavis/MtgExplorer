using MtgExplorer.Nodes;
using Neo4jClient;

namespace MtgExplorer.Relationships
{
    public class CardIsInstanceOfRelationship : Relationship,
                                                IRelationshipAllowingSourceNode<CardInstanceNode>,
                                                IRelationshipAllowingTargetNode<CardNode>
    {
        public CardIsInstanceOfRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return "IS_INSTANCE_OF"; }
        }
    }
}
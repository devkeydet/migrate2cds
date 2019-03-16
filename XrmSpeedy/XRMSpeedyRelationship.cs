using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk;

namespace XrmSpeedy
{
    public class XRMSpeedyRelationship
    {
        public string Entity1;
        public string Entity2;
        public string Entity1Display;
        public string Entity2Display;
        public string RelationshipType;
        public string SchemaName;
        public LookupAttributeMetadata PrimaryField;

        public XRMSpeedyRelationship(string entity1, string entity2, string entity1Display,
            string entity2Display, string relationshipType, string schemaName, string prefix)
        {
            Entity1 = entity1;
            Entity2 = entity2;
            Entity1Display = entity1Display;
            Entity2Display = entity2Display;
            RelationshipType = relationshipType;
            SchemaName = schemaName;
            if (relationshipType == "1:N Relationship")
            {
                PrimaryField = SetPrimaryField(prefix, entity1, entity1Display);
            }
        }

        private static LookupAttributeMetadata SetPrimaryField(string prefix, string entity1, string entity1Display)
        {
            return new LookupAttributeMetadata
            {
                SchemaName = string.Concat(prefix, "_", "parent", "_", entity1, "id"),
                DisplayName = new Label(entity1Display, 1033),
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None)
            };
        }
    }
}

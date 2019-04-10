using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk;
using System.ComponentModel;

namespace CDSTools
{

    public enum RelationshipType
    {
        [Description("1:N Relationship")]
        OneToNRelationship = 1,
        // [Description("N:1 Relationship")]
        // NToOneRelationship
        [Description("N:N Relationship")]
        NToNRelationship
    }

    public class MigrateRelationship
    {
        public string Prefix { get; private set; }

        public string Entity1;
        public string Entity2;
        public string Entity1Display;
        public string Entity2Display;
        public RelationshipType RelationType;
        public string SchemaName;
        public LookupAttributeMetadata PrimaryField;
        public int LanguageCode { get; set; } = 1033;


        public MigrateRelationship(string entity1, string entity2, string entity1Display, string entity2Display, 
            RelationshipType relationshipType, string prefix, int languageCode)
        {
            LanguageCode = languageCode;
            Entity1 = entity1;
            Entity2 = entity2;
            Entity1Display = entity1Display;
            Entity2Display = entity2Display;
            RelationType = relationshipType;
            SchemaName = GetRelationshipSchemaName();

            if (relationshipType == RelationshipType.OneToNRelationship)
            {
                PrimaryField = SetPrimaryField(prefix, entity1, entity1Display);
            }
        }
        private string GetRelationshipSchemaName()
        {
            return $"{Prefix}_{Entity1}_{Entity2}";
        }
        private LookupAttributeMetadata SetPrimaryField(string prefix, string entity1, string entity1Display)
        {
            return new LookupAttributeMetadata
            {
                SchemaName = string.Concat(prefix, "_", "parent", "_", entity1, "id"),
                DisplayName = new Label(entity1Display, LanguageCode),
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None)
            };
        }

        public override string ToString()
        {
            return $"{Settings.GetDescription(RelationType)}:{Entity1Display}->{Entity2Display}";
        }
    }
}

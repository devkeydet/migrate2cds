using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk;
using System.ComponentModel;
using System.Linq;

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

    /// <summary>
    /// Class the encapsulates a relationship to that will be added to the DB
    /// </summary>
    public class MigrateRelationship
    {
        bool _import = true;

        [Category("Properties")]
        public new bool Import
        {
            get => _import;
            set {
                _import = value;
            }
        }

        public string Prefix { get; private set; }

        [DisplayName("Entity One")]
        public string Entity1 { get; private set; }

        [DisplayName("Entity Two")]
        public string Entity2 { get; private set; }

        [DisplayName("Entity One Display Name")]
        public string Entity1Display { get; set; }

        [DisplayName("Entity Two Display Name")]
        public string Entity2Display { get; set; }

        [DisplayName("Entity Two Display Name")]
        public RelationshipType RelationType { get; private set; }

        [DisplayName("Schema Name")]
        public string SchemaName { get; set; }

        [Browsable(false)]
        public LookupAttributeMetadata PrimaryField { get; private set; }

        [DisplayName("Primary Field")]
        public string PrimaryFieldName {
            get 
            {
                return PrimaryField.DisplayName?.LocalizedLabels.Where(l => l.LanguageCode == LanguageCode).FirstOrDefault()?.Label;
            }
        }

        [DisplayName("Language Code")]
        public int LanguageCode { get; private set; } = 1033;

        /// <summary>
        /// Constructor for relation... some private sets so can't change
        /// </summary>
        /// <param name="entity1"></param>
        /// <param name="entity2"></param>
        /// <param name="entity1Display"></param>
        /// <param name="entity2Display"></param>
        /// <param name="relationshipType"></param>
        /// <param name="prefix"></param>
        /// <param name="languageCode"></param>
        public MigrateRelationship(string entity1, string entity2, string entity1Display, string entity2Display, 
            RelationshipType relationshipType, string prefix, int languageCode, bool removeExtraPrefix = true)
        {
            LanguageCode = languageCode;
            Entity1 = entity1;
            Entity2 = entity2;
            Entity1Display = entity1Display;
            Entity2Display = entity2Display;
            RelationType = relationshipType;
            Prefix = prefix;
            SchemaName = GetRelationshipSchemaName(removeExtraPrefix);

            if (relationshipType == RelationshipType.OneToNRelationship)
            {
                PrimaryField = SetPrimaryField(prefix, entity1, entity1Display);
            }
        }

        /// <summary>
        /// Format schema name using entity parts 
        /// </summary>
        /// <returns></returns>
        private string GetRelationshipSchemaName(bool removeExtraPrefix = false)
        {
            var pre = Prefix + "_";
            if (removeExtraPrefix)
            {
                var e1 = Entity1.Substring(Entity1.IndexOf('_') + 1);
                var e2 = Entity2.Substring(Entity2.IndexOf('_') + 1);
                return $"{pre}{e1}_{e2}";
            }
            else {
                return $"{Prefix}_{Entity1}_{Entity2}";
            }
        }
        /// <summary>
        /// Set the primary field for the relationship
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="entity1"></param>
        /// <param name="entity1Display"></param>
        /// <returns></returns>
        private LookupAttributeMetadata SetPrimaryField(string prefix, string entity1, string entity1Display)
        {
            return new LookupAttributeMetadata
            {
                SchemaName = string.Concat(prefix, "_", "parent", "_", entity1, "id"),
                DisplayName = new Label(entity1Display, LanguageCode),
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None)
            };
        }

        /// <summary>
        /// Make the display a bit more readable
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Settings.GetDescription(RelationType)}:{Entity1Display} -> {Entity2Display}";
        }
    }

}

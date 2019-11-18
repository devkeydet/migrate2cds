using System.Linq;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.ComponentModel;

namespace CDSTools
{
    public class MigrateDataBase
    {
        public const string PREFIX = "migrate2";

        #region Options 
        /// <summary>
        /// Flag indicating whether to remove extra prefixes when creating relationships 
        /// </summary>
        public bool RemoveExtraPrefixes { get; set; } = true;

        #endregion 
        public int LanguageCode { get; set; } = 1033;
        public bool AddNewFields { get; set; } = true;
        public IDbProvider Provider { get; private set; }
        public List<MigrateEntity> NewEntities { get; private set; } = new List<MigrateEntity>();
        public List<MigrateEntity> CreatedEntities { get; private set; } = new List<MigrateEntity>();
        public List<MigrateRelationship> NewRelationships { get; private set; } = new List<MigrateRelationship>();
        public List<MigrateRelationship> CreatedRelationships { get; private set; } = new List<MigrateRelationship>();

        private string _prefix = PREFIX;
        public string Prefix { get=> _prefix;
            set {
                _prefix = value.Trim().ToLower();
            }
        }

        public MigrateDataBase(IDbProvider provider) {
            Provider = provider;
        }

        /// <summary>
        /// Now that we have the DB selected and the Provider ready, read the DB
        /// </summary>
        public void ReadDataBase() {

            NewEntities.Clear();
            NewRelationships.Clear();

            List<string> tables = Provider.GetTableNames();

            foreach (string table in tables)
            {
                var newEntity = new MigrateEntity(table, Prefix.Trim(), false);
                var fields = Provider.GetFields(table);

                foreach (KeyValuePair<string, string> field in fields)
                {
                    string name = ValidateFieldSchemaName(field.Key, Prefix, table);
                    var fldType = Provider.DBTypeToCRMType(field.Value);
                    var fld = new MigrateField(name, fldType, Prefix, false);
                    newEntity.Fields.Add(fld);
                }

                NewEntities.Add(newEntity);
            }

            CreateInitialRelationships(tables);
        }

        #region Relationships 

        private void CreateInitialRelationships(List<string> tables)
        {
            foreach (string table in tables)
            {
                Dictionary<string, string> relationships = Provider.GetRelationships(table);

                foreach (KeyValuePair<string, string> relationship in relationships)
                {
                    var ent1 = NewEntities.Where(n => n.OriginalTable == relationship.Key).FirstOrDefault();
                    var ent2 = NewEntities.Where(n => n.OriginalTable == relationship.Value).FirstOrDefault();

                    AddRelationship(ent1, ent2, RelationshipType.OneToNRelationship);
                }
            }
        }

        /// <summary>
        ///  Add relationship using schema name of entity
        /// </summary>
        /// <param name="entity1"></param>
        /// <param name="entity2"></param>
        /// <param name="relationshipType"></param>
        public void AddRelationship(MigrateEntity entity1, MigrateEntity entity2, RelationshipType relationshipType)
        {
            var relationship = new MigrateRelationship(entity1.SchemaName, entity2.SchemaName, 
                entity1.DisplayName, entity2.DisplayName, 
                relationshipType, Prefix, LanguageCode, RemoveExtraPrefixes);

            NewRelationships.Add(relationship);

            // since these have relationships, include in import
            entity1.Import = true;
            entity1.InReleationship = true;
            entity2.Import = true;
            entity2.InReleationship = true;

        }

        /// <summary>
        /// Add a new relationship to the collection 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entity1Name"></param>
        /// <param name="entity2Name"></param>
        /// <param name="relationshipType"></param>
        public void AddRelationship(IOrganizationService service, string entity1Name, string entity2Name, RelationshipType relationshipType)
        {
            var entity1 = NewEntities.Where(n => n.OriginalTable == entity1Name).FirstOrDefault();
            var entity2 = NewEntities.Where(n => n.OriginalTable == entity2Name).FirstOrDefault();

            // call the shared method if we are using exisging entities
            if (entity1 != null && entity2 != null)
            {
                AddRelationship(entity1, entity2, relationshipType);
                return;
            }

            // check for existing entites metadta 
            var entity1SchemaName = entity1?.SchemaName;
            var entity1DisplayName = entity1?.DisplayName;

            var entity2SchemaName = entity2?.SchemaName;
            var entity2DisplayName = entity2?.DisplayName;

            // get the entity definition from metadata if not in our list of imported entities
            if (entity1 == null) {
                var entity1Meta = CDSOrgServiceCommon.RetrieveEntity(service, entity1Name);
                entity1SchemaName = entity1Meta.SchemaName;
                entity1DisplayName = entity1Meta.DisplayName?.LocalizedLabels.Where(l=>l.LanguageCode == LanguageCode).FirstOrDefault()?.Label;
                if (entity1DisplayName == null) {
                    entity1DisplayName = entity2SchemaName;
                };
            }

            if (entity2 == null)
            {
                var entity2Meta = CDSOrgServiceCommon.RetrieveEntity(service, entity2Name);
                entity2SchemaName = entity2Meta.SchemaName;
                entity2DisplayName = entity2Meta.DisplayName?.LocalizedLabels.Where(l => l.LanguageCode == LanguageCode).FirstOrDefault()?.Label;
                if (entity2DisplayName == null) {
                    entity2DisplayName = entity2SchemaName;
                };
            }
            var relationship = new MigrateRelationship(entity1SchemaName, entity2SchemaName, 
                entity1DisplayName, entity2DisplayName, 
                relationshipType, Prefix, LanguageCode);

            NewRelationships.Add(relationship);

            // since these have relationships, include in import
            if (entity1 != null) {
                entity1.Import = true;
                entity1.InReleationship = true;
            }
            if (entity2 != null) {
                entity2.Import = true;
                entity2.InReleationship = true;
            }
        }
        #endregion
        #region CRM CRUD Actions 
        /// <summary>
        /// Delete entities from CDS
        /// </summary>
        /// <param name="service"></param>
        /// <param name="addNewFields"></param>
        private void DeleteEntities(IOrganizationService service, bool addNewFields)
        {
            var ent = new CDSEntity();

            if (addNewFields)
            {
                var fxml = new CDSFormXML();
                foreach (var entity in CreatedEntities)
                {
                    fxml.RemoveCustomFields(service, entity.SchemaName);
                }
            }

            foreach (var relationship in CreatedRelationships)
            {
                ent.DeleteRelationship(service, relationship);
            }
            var createdEntities = NewEntities.Where(n => n.Import == true).ToList();
            foreach (var entity in createdEntities)
            {
                ent.DeleteEntity(service, entity);
            }
        }

        /// <summary>
        /// Create the custom entities for the selections made 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="publish"></param>
        /// <param name="addNewFields"></param>
        public void CreateEntities(IOrganizationService service, bool publish, bool addNewFields)
        {
            var ent = new CDSEntity();
            var fxml = new CDSFormXML();

            bool result = false;
            var importEntities = NewEntities.Where(n => n.Import == true).ToList();

            foreach (var entity in importEntities)
            {
                result = ent.CreateEntity(service, entity, LanguageCode);
                if (!result) {
                    // MessageBox.Show("Error Creating Items");
                    return;
                }
            }

            // capture entities that have been created
            CreatedEntities = NewEntities.Where(n => n.Import == true).ToList();

            var newRelations = NewRelationships.Where(r => r.Import == true).ToList();
            foreach (var relationship in newRelations)
            {
                switch (relationship.RelationType)
                {
                    case RelationshipType.OneToNRelationship:
                        result = ent.CreateOneToMany(service, relationship, Prefix, LanguageCode);
                        break;
                    case RelationshipType.NToNRelationship:
                        result = ent.CreateManyToMany(service, relationship, Prefix, LanguageCode);
                        break;
                }

                if (!result) {
                    // MessageBox.Show("Error Creating Relationships");
                    return;
                }
            }

            if (addNewFields)
            {
                foreach (var entity in NewEntities.Where(n => n.Import == true))
                {
                    fxml.AddCustomFields(service, entity.SchemaName);
                }
            }

            // capture relations that have been created
            CreatedRelationships = NewRelationships;

            if (publish) {
                ent.PublishEntities(service, CreatedEntities);
            }
        }

        #endregion

        #region Helper functions

        /// <summary>
        /// Helper method to display entity display name from DisplayName object 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="includeSchemaName"></param>
        /// <returns></returns>
        public string GetEntityDisplayName(EntityMetadata entity, bool includeSchemaName = true)
        {
            string displayName = entity.SchemaName;

            var label = entity.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == LanguageCode).FirstOrDefault();

            if (label != null)
            {
                if (includeSchemaName) {
                    displayName = $"{label.Label} ({entity.SchemaName})";
                }
                else {
                    displayName = label.Label;
                }
            }

            return displayName;
        }

        /// <summary>
        /// Validate whether schema name is uuni
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prefix"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private string ValidateFieldSchemaName(string name, string prefix, string table)
        {
            if ((prefix + "_" + name).ToLower() == (prefix + "_" + table + "id").ToLower())
                return name + "2";
            else
                return name;
        }

        #endregion
    }
}

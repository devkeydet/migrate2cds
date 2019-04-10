using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CDSTools
{
    public class MigrateEntity: MigrateItemBase
    {
        string _prefix = "";

        bool _import = true;
        public int LanguageCode { get; set; } = 1033;

        [Category("Entity Properties")]
        public string OriginalTable { get; set; }

        [Category("Entity Properties")]
        public new bool Import { get=> _import;
            set {
                _import = value;
                // update fields in entity to match
                Fields.All(f => 
                {
                    f.Import = Import;
                    return true;
                });
            }
        }

        public List<MigrateField> Fields { get; set; } = new List<MigrateField>();
        public List<MigrateRelationship> Relationships { get; set; }

        public bool InReleationship { get; set; } = false;

        public MigrateEntity(string originalTable, string prefix, bool import)
        {
            _prefix = prefix;
            
            OriginalTable = originalTable;
            Import = import;

            SchemaName = (prefix + "_" + originalTable).ToLower();
            DisplayName = originalTable;
            DisplayCollectionName = originalTable + "s";
            OwnershipType = OwnershipTypes.UserOwned;

            Fields = new List<MigrateField>();
            Relationships = new List<MigrateRelationship>();
        }

        #region EntityMetadata Fields 
        
        public string SchemaName { get; set; }
        public string DisplayName { get; set; }

        public string DisplayCollectionName { get; set; }

        public OwnershipTypes OwnershipType { get; set; } = OwnershipTypes.UserOwned;

        #endregion

        public override string ToString()
        {
            return $"{DisplayName} ({SchemaName})";
        }

        public EntityMetadata ToMetadata() {
            return new EntityMetadata()
            {
                SchemaName = SchemaName,
                DisplayName = new Microsoft.Xrm.Sdk.Label(DisplayName, LanguageCode),
                DisplayCollectionName = new Microsoft.Xrm.Sdk.Label(DisplayCollectionName, LanguageCode),
                OwnershipType = OwnershipTypes.UserOwned
            };
        }

    }

}

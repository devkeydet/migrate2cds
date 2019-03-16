using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace XrmSpeedy
{
    public class XRMSpeedyEntity : Microsoft.Xrm.Sdk.Metadata.MetadataBase
    {
        public string OriginalTable { get; set; }
        public EntityMetadata EntityMetadata { get; set; }
        public bool Import { get; set; }
        public List<XRMSpeedyField> Fields { get; set; }
        public List<XRMSpeedyRelationship> Relationships { get; set; }

        public XRMSpeedyEntity(string originalTable, string prefix, bool import)
        {
            OriginalTable = originalTable;
            Import = import;
            EntityMetadata = new EntityMetadata();
            EntityMetadata.SchemaName = (prefix + "_" + originalTable).ToLower();
            EntityMetadata.DisplayName = new Microsoft.Xrm.Sdk.Label(originalTable, 1033);
            EntityMetadata.DisplayCollectionName = new Microsoft.Xrm.Sdk.Label(originalTable + "s", 1033);
            EntityMetadata.OwnershipType = OwnershipTypes.UserOwned;
            Fields = new List<XRMSpeedyField>();
            Relationships = new List<XRMSpeedyRelationship>();
        }
    }
}

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace CDSTools
{
    public interface ICDSFormXML
    {
        void AddCustomFields(IOrganizationService service, string entity);
        string GetControlClassId(AttributeMetadata meta);
        bool RemoveCustomFields(IOrganizationService service, string entity);
    }
}

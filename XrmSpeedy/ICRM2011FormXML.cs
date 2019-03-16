using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace XrmSpeedy
{
    public interface ICRM2011FormXML
    {
        void AddCustomFields(IOrganizationService service, string entity);
        string GetControlClassId(AttributeMetadata meta);
        bool RemoveCustomFields(IOrganizationService service, string entity);
    }
}

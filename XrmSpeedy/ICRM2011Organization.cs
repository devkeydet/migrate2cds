using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace XrmSpeedy
{
    public interface ICRM2011Organization
    {
        int GetOrganizationPricingDecimalPrecision(IOrganizationService service);
        EntityMetadata[] GetEntityMetadata(IOrganizationService service);
    }
}

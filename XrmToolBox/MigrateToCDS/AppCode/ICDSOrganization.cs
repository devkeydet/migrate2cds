using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace CDSTools
{
    public interface ICDSOrganization
    {
        int GetOrganizationPricingDecimalPrecision(IOrganizationService service);
        EntityMetadata[] GetEntityMetadata(IOrganizationService service);
    }
}

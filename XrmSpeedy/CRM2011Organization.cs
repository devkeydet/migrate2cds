using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;
using System.ServiceModel;

namespace XrmSpeedy
{
    public class CRM2011Organization : XrmSpeedy.ICRM2011Organization
    {
        public int GetOrganizationPricingDecimalPrecision(IOrganizationService service)
        {
            try
            {
                QueryExpression query = new QueryExpression
                {
                    EntityName = "organization",
                    ColumnSet = new ColumnSet("pricingdecimalprecision")
                };

                Entity result = service.RetrieveMultiple(query).Entities.FirstOrDefault();
                return int.Parse(result.Attributes["pricingdecimalprecision"].ToString());
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        public EntityMetadata[] GetEntityMetadata(IOrganizationService service)
        {
            try
            {
                RetrieveAllEntitiesRequest request = new RetrieveAllEntitiesRequest()
                {
                    EntityFilters = EntityFilters.Entity,
                    RetrieveAsIfPublished = true
                };

                RetrieveAllEntitiesResponse response = (RetrieveAllEntitiesResponse)service.Execute(request);

                return response.EntityMetadata;
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }
    }
}

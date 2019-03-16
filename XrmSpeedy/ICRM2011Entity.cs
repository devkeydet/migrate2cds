using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace XrmSpeedy
{
    public interface ICRM2011Entity
    {
        bool CreateEntity(IOrganizationService service, XRMSpeedyEntity entity, int languageCode);
        bool DeleteEntity(IOrganizationService service, XRMSpeedyEntity entity);
        bool CreateOneToMany(IOrganizationService service, XRMSpeedyRelationship relationship, string prefix, int languageCode);
        bool CreateManyToMany(IOrganizationService service, XRMSpeedyRelationship relationship, string prefix, int languageCode);
        bool DeleteRelationship(IOrganizationService service, XRMSpeedyRelationship relationship);
        bool PublishEntities(IOrganizationService service, List<XRMSpeedyEntity> entities);
    }
}

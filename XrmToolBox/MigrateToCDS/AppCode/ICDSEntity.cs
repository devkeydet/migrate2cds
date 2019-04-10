using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace CDSTools
{
    public interface ICDSEntity
    {
        bool CreateEntity(IOrganizationService service, MigrateEntity entity, int languageCode);
        bool DeleteEntity(IOrganizationService service, MigrateEntity entity);
        bool CreateOneToMany(IOrganizationService service, MigrateRelationship relationship, string prefix, int languageCode);
        bool CreateManyToMany(IOrganizationService service, MigrateRelationship relationship, string prefix, int languageCode);
        bool DeleteRelationship(IOrganizationService service, MigrateRelationship relationship);
        bool PublishEntities(IOrganizationService service, List<MigrateEntity> entities);
    }
}

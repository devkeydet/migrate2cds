using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace CDSTools
{
    public class CDSEntity : ICDSEntity
    {
        public bool CreateEntity(IOrganizationService service, MigrateEntity entity, int languageCode)
        {
            try
            {
                var ent = entity.ToMetadata();
                ent.IsActivity = false;
                CreateEntityRequest createRequest = new CreateEntityRequest
                {
                    Entity = ent,
                    // Define the primary attribute for the entity
                    PrimaryAttribute = new StringAttributeMetadata
                    {
                        SchemaName = ent.SchemaName + "name",
                        RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.ApplicationRequired),
                        MaxLength = 100,
                        Format = StringFormat.Text,
                        DisplayName = new Label("Name", languageCode),
                        Description = new Label("The primary attribute for the entity.", languageCode)
                    }
                };

                CreateEntityResponse response = (CreateEntityResponse)service.Execute(createRequest);

                return CreateAttributes(service, entity, languageCode);
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        public bool CreateOneToMany(IOrganizationService service, MigrateRelationship relationship, string prefix, int languageCode)
        {
            try
            {
                CreateOneToManyRequest createOneToManyRelationshipRequest = new CreateOneToManyRequest
                {
                    OneToManyRelationship = new OneToManyRelationshipMetadata
                    {
                        ReferencedEntity = relationship.Entity1,
                        ReferencingEntity = relationship.Entity2,
                        SchemaName = relationship.SchemaName,
                        AssociatedMenuConfiguration = new AssociatedMenuConfiguration
                        {
                            Behavior = AssociatedMenuBehavior.UseLabel,
                            Group = AssociatedMenuGroup.Details,
                            Label = new Label(relationship.Entity1.Substring(0, 1).ToUpper() + relationship.Entity1.Substring(1), languageCode),
                            Order = 10000
                        },
                        CascadeConfiguration = new CascadeConfiguration
                        {
                            Assign = CascadeType.NoCascade,
                            Delete = CascadeType.RemoveLink,
                            Merge = CascadeType.Cascade,
                            Reparent = CascadeType.NoCascade,
                            Share = CascadeType.NoCascade,
                            Unshare = CascadeType.NoCascade
                        }
                    },
                    Lookup = relationship.PrimaryField
                };

                CreateOneToManyResponse createOneToManyRelationshipResponse =
                    (CreateOneToManyResponse)service.Execute(createOneToManyRelationshipRequest);

                return true;
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        public bool CreateManyToMany(IOrganizationService service, MigrateRelationship relationship, string prefix, int languageCode)
        {
            try
            {
                CreateManyToManyRequest createManyToManyRelationshipRequest = new CreateManyToManyRequest
                {
                    IntersectEntitySchemaName = relationship.SchemaName,
                    ManyToManyRelationship = new ManyToManyRelationshipMetadata
                    {
                        SchemaName = relationship.SchemaName,
                        Entity1LogicalName = relationship.Entity1,
                        Entity1AssociatedMenuConfiguration =
                        new AssociatedMenuConfiguration
                        {
                            Behavior = AssociatedMenuBehavior.UseLabel,
                            Group = AssociatedMenuGroup.Details,
                            Label = new Label(relationship.Entity1Display, languageCode),
                            Order = 10000
                        },
                        Entity2LogicalName = relationship.Entity2,
                        Entity2AssociatedMenuConfiguration =
                        new AssociatedMenuConfiguration
                        {
                            Behavior = AssociatedMenuBehavior.UseLabel,
                            Group = AssociatedMenuGroup.Details,
                            Label = new Label(relationship.Entity2Display, languageCode),
                            Order = 10000
                        }
                    }
                };

                CreateManyToManyResponse createManytoManyRelationshipResponse =
                    (CreateManyToManyResponse)service.Execute(createManyToManyRelationshipRequest);

                return true;
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        private bool CreateAttributes(IOrganizationService service, MigrateEntity entity, int languageCode)
        {
            try
            {
                foreach (MigrateField field in entity.Fields.Where(f => f.Import == true))
                {
                    CreateAttributeRequest request = new CreateAttributeRequest
                    {
                        EntityName = entity.SchemaName,
                        Attribute = field.ToMetadata() 
                    };

                    CreateAttributeResponse response = (CreateAttributeResponse)service.Execute(request);
                }
                return true;
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        public bool DeleteEntity(IOrganizationService service, MigrateEntity entity)
        {
            try
            {
                DeleteEntityRequest delete = new DeleteEntityRequest
                {
                    LogicalName = entity.SchemaName
                };

                service.Execute(delete);
                return true;
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        public bool DeleteRelationship(IOrganizationService service, MigrateRelationship relationship)
        {
            try
            {
                DeleteRelationshipRequest delete = new DeleteRelationshipRequest
                {
                    Name = relationship.SchemaName
                };

                service.Execute(delete);
                return true;
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        public bool PublishEntities(IOrganizationService service, List<MigrateEntity> entities)
        {
            try
            {
                PublishXmlRequest request = new PublishXmlRequest();
                request.ParameterXml = "<importexportxml><entities>";
                foreach (MigrateEntity entity in entities)
                {
                    request.ParameterXml += "<entity>" + entity.SchemaName + "</entity>";
                }
                request.ParameterXml += "</entities><nodes/><securityroles/><settings/><workflows/></importexportxml>";
                service.Execute(request);
                return true;
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }
    }
}

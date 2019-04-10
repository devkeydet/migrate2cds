using System.Linq;
using System.Collections.Generic;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace CDSTools
{
    /// <summary>
    /// Helper class for common org service actions: metadata retrieve and create
    /// </summary>
    public class CDSOrgServiceCommon
    {
        static bool _retrieveAllCalled = false;
        static Dictionary<string, EntityMetadata> _entityMetadata = new Dictionary<string, EntityMetadata>();

        #region Entities
        /// <summary>
        /// Rerieve all entities with the given filter conditions
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityFilters"></param>
        /// <param name="asIfPublished"></param>
        /// <returns></returns>
        public static List<EntityMetadata> RetrieveAllEntities(IOrganizationService service, List<EntityFilters> entityFilters = null, bool asIfPublished = true, bool forceDownload = false)
        {
            // return cached items if not forcing redownload
            if (_retrieveAllCalled && !forceDownload) {
                return _entityMetadata.Select(e=> e.Value).ToList();
            }

            if (entityFilters == null) {
                entityFilters = new List<EntityFilters>() { EntityFilters.Default };
            }
            // build the bitwise or list of the entity filters
            var filters = entityFilters.Aggregate<EntityFilters, EntityFilters>(0, (current, item) => current | item);

            var req = new RetrieveAllEntitiesRequest()
            {
                EntityFilters = filters,
                RetrieveAsIfPublished = asIfPublished
            };

            var resp = (RetrieveAllEntitiesResponse)service.Execute(req);

            // set the itemsource of the itembox equal to entity metadata that is customizable (takes out systemjobs and stuff like that)
            var entities = resp.EntityMetadata.Where(x => x.IsCustomizable.Value == true).ToList<EntityMetadata>();

            // All entities!  replace the entire metatata dictionary 
            _entityMetadata = entities.ToDictionary(e => e.LogicalName.ToLower(), e=> e);

            // keep a flag indicating 
            _retrieveAllCalled = true;
            return entities;
        }

        /// <summary>
        /// Retrieve an Entity Metadata and include the default entity Entity details
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <returns></returns>
        public static EntityMetadata RetrieveEntity(IOrganizationService service, string entityLogicalName, bool retrieveAsIfPublished = true)
        {
            return RetrieveEntity(service, entityLogicalName, retrieveAsIfPublished, new List<EntityFilters> { EntityFilters.Default });
        }

        /// <summary>
        /// Retrieve the Entity Metadata and include the details as provided by the entityFilter argument
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <param name="entityFilter"></param>
        /// <returns></returns>
        public static EntityMetadata RetrieveEntity(IOrganizationService service, string entityLogicalName, bool retrieveAsIfPublished, EntityFilters entityFilter)
        {
            return RetrieveEntity(service, entityLogicalName, retrieveAsIfPublished, new List<EntityFilters> { entityFilter });
        }
        /// <summary>
        /// Retrieve an Entity Metadata and include Entity details as specified in the EntityFilters provided 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <param name="entityFilters"></param>
        /// <returns></returns>
        public static EntityMetadata RetrieveEntity(IOrganizationService service, string entityLogicalName, bool retrieveAsIfPublished, List<EntityFilters> entityFilters)
        {
            var nameLower = entityLogicalName.ToLower();
            if (_entityMetadata.ContainsKey(nameLower))
            {
                return _entityMetadata[nameLower];
            }

            var filters = entityFilters.Aggregate<EntityFilters, EntityFilters>(0, (current, item) => current | item);
            
            var req = new RetrieveEntityRequest()
            {
                RetrieveAsIfPublished = retrieveAsIfPublished,
                EntityFilters = filters,
                LogicalName = entityLogicalName
            };

            var resp = (RetrieveEntityResponse)service.Execute(req);

            // cache the entity metadata 
            _entityMetadata.Add(nameLower, resp.EntityMetadata);

            return resp.EntityMetadata;
        }

        #endregion

        #region Additional Metadata helper methods 
        /// <summary>
        /// Rtrieve a list of Publishers with some common attributes
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static List<Entity> RetrievePublishers(IOrganizationService service)
        {
            var query = new QueryExpression("publisher")
            {
                ColumnSet = new ColumnSet("uniquename", "friendlyname", "isreadonly", "description", "customizationprefix", "publisherid", "organizationid")
            };
            var resp = service.RetrieveMultiple(query);

            return resp.Entities.ToList();
        }
        #endregion
    }
}

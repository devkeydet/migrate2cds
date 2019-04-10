using System.Collections.Generic;
using static CDSTools.MigrateField;

namespace CDSTools
{
    public interface IDbProvider
    {
        /// <summary>
        /// helper method for display of connection string 
        /// </summary>
        /// <returns></returns>
        string GetConnectionString();

        /// <summary>
        /// Extract the list of Table names from the DB
        /// </summary>
        /// <returns></returns>
        List<string> GetTableNames();

        /// <summary>
        /// Get Fields for a table
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        Dictionary<string, string> GetFields(string table);

        AttributeMetadataType DBTypeToCRMType(string datatype);

        /// <summary>
        /// Extract relationships between the existing tables 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        Dictionary<string, string> GetRelationships(string table);
    }
}

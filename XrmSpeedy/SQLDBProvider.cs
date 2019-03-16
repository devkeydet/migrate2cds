using System.Collections.Generic;
using System.Data.SqlClient;

namespace XrmSpeedy
{
    public class SQLDBProvider : IDbProvider
    {
        //private members
        string strCon;

        //Constructor
        public SQLDBProvider(string ConnectionString)
        {
            strCon = ConnectionString;
        }

        public SQLDBProvider()
        {
        }

        public List<string> GetTableNames()
        {
            List<string> lstTables = new List<string>();

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    string strSQL = "SELECT name FROM sys.Tables";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        lstTables.Add(rdr["name"].ToString());
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return lstTables;
        }

        public Dictionary<string, string> GetFields(string table)
        {
            Dictionary<string, string> fields = new Dictionary<string, string>();

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    string strSQL = string.Format("SELECT COLUMN_NAME, DATA_TYPE FROM information_schema.columns WHERE TABLE_NAME = '{0}'", table);
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        fields.Add(rdr["COLUMN_NAME"].ToString(), rdr["DATA_TYPE"].ToString());
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return fields;
        }

        public Dictionary<string, string> GetRelationships(string table)
        {
            Dictionary<string, string> relationships = new Dictionary<string, string>();

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    string strSQL = string.Format(@"SELECT f.name AS ForeignKey,
                                                           OBJECT_NAME(f.parent_object_id) AS TableName,
                                                           COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
                                                           OBJECT_NAME(f.referenced_object_id) AS ReferenceTableName,
                                                           COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferenceColumnName
                                                    FROM   sys.foreign_keys AS f
                                                           INNER JOIN
                                                           sys.foreign_key_columns AS fc
                                                           ON f.OBJECT_ID = fc.constraint_object_id
                                                    WHERE  OBJECT_NAME(f.parent_object_id) = '{0}'", table);
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //We are not allowing the same relationship multiple times
                        if (!relationships.ContainsKey(rdr["ReferenceTableName"].ToString()) && !relationships.ContainsValue(rdr["TableName"].ToString()))
                            relationships.Add(rdr["ReferenceTableName"].ToString(), rdr["TableName"].ToString());
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return relationships;
        }


        public string DBTypeToCRMType(string datatype)
        {
            if (datatype == "char" || datatype == "varchar" || datatype == "nchar" || datatype == "nvarchar")
            {
                return "Single Line of Text";
            }
            else if (datatype == "bigint" || datatype == "int" || datatype == "tinyint")
            {
                return "Whole Number";
            }
            else if (datatype == "smallmoney" || datatype == "money")
            {
                return "Currency";
            }
            else if (datatype == "decimal" || datatype == "numeric")
            {
                return "Decimal Number";
            }
            else if (datatype == "float" || datatype == "real")
            {
                return "Floating Point Number";
            }
            else if (datatype == "bit")
            {
                return "Two Options";
            }
            else if (datatype == "date" || datatype == "datetime2" || datatype == "datetime" || datatype == "datetimeoffset" || datatype == "smalldatetime"
                || datatype == "time")
            {
                return "Date and Time";
            }
            else if (datatype == "text" || datatype == "ntext")
            {
                return "Multiple Lines of Text";
            }
            else
            {
                return "Single Line of Text";
            }
        }
    }
}

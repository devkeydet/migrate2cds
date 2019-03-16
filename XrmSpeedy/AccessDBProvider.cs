using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace XrmSpeedy
{
    public class AccessDBProvider : IDbProvider
    {
        //private members
        string strCon;


        //Constructor
        public AccessDBProvider(string ConnectionString)
        {
            strCon = ConnectionString;
        }

        public AccessDBProvider()
        {
        }

        public List<string> GetTableNames()
        {
            List<string> lstTables = new List<string>();

            try
            {
                using (OleDbConnection con = new OleDbConnection(strCon))
                {
                    con.Open();

                    string[] restrictions = new string[] { null, null, null, "TABLE" };
                    DataTable dt = con.GetSchema("tables", restrictions);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lstTables.Add(dr[2].ToString());
                    }
                }
            }
            catch (OleDbException)
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
                using (OleDbConnection con = new OleDbConnection(strCon))
                {
                    con.Open();
                    DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, table, null });
                    foreach (DataRow dr in dt.Rows)
                    {
                        fields.Add(dr["COLUMN_NAME"].ToString(), dr["DATA_TYPE"].ToString());
                    }
                }
            }
            catch (OleDbException)
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
                using (OleDbConnection con = new OleDbConnection(strCon))
                {
                    con.Open();
                    DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new object[] { null, null, table });
                    foreach (DataRow dr in dt.Rows)
                    {
                        //We are not allowing the same relationship multiple times
                        if (!relationships.ContainsKey(dr.ItemArray[2].ToString()) && !relationships.ContainsValue(dr.ItemArray[8].ToString()))
                            relationships.Add(dr.ItemArray[2].ToString(), dr.ItemArray[8].ToString());
                    }
                }
            }
            catch (OleDbException)
            {
                throw;
            }

            return relationships;
        }

        public string DBTypeToCRMType(string datatype)
        {
            if (datatype == "8" || datatype == "129" || datatype == "130" || datatype == "200")
            {
                return "Single Line of Text";
            }
            else if (datatype == "2" || datatype == "3" || datatype == "16" || datatype == "17" || datatype == "18" || datatype == "19"
                || datatype == "20" || datatype == "21" || datatype == "64")
            {
                return "Whole Number";
            }
            else if (datatype == "6")
            {
                return "Currency";
            }
            else if (datatype == "14" || datatype == "131" || datatype == "139")
            {
                return "Decimal Number";
            }
            else if (datatype == "4" || datatype == "5")
            {
                return "Floating Point Number";
            }
            else if (datatype == "11")
            {
                return "Two Options";
            }
            else if (datatype == "7" || datatype == "133" || datatype == "134" || datatype == "135")
            {
                return "Date and Time";
            }
            else if (datatype == "201" || datatype == "202" || datatype == "203")
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

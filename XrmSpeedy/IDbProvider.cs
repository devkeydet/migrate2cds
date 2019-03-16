using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace XrmSpeedy
{
    interface IDbProvider
    {
         List<string> GetTableNames();
         Dictionary<string, string> GetFields(string table);
         string DBTypeToCRMType(string datatype);
    }
}

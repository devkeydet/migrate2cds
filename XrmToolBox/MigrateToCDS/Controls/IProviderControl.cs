using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDSTools.UserControls
{
    public interface IProviderControl
    {
        string GetConnectionString();

        IDbProvider GetProvider();

        event EventHandler ProviderChanged;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Neat.Procedure
{
    internal class CurrentContext
    {
        internal static SqlConnection Connection
        {
            get
            {
                return Transaction.IsNull() ? Neat.Procedure.Connection.Instance : Transaction.Instance.Connection;
            }
        }
    }
}

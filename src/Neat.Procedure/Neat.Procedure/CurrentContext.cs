using System.Data.SqlClient;

namespace Neat.Procedure
{
    internal class CurrentContext
    {
        internal static SqlConnection Connection
        {
            get
            {
                return Transaction.Instance != null && Transaction.Instance.Connection != null ?
                    Transaction.Instance.Connection : Connection;

            }
        }
    }
}

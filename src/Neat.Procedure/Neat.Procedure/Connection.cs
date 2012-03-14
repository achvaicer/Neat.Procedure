using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Neat.Procedure
{
    internal static class Connection
    {
        private static SqlConnection _connection;

        internal static bool EnsureIsOpened()
        {
            if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
            {
                Close();
                _connection = new SqlConnection(ConfigurationManager.ConnectionStrings[""].ConnectionString);
                _connection.Open();
                return true;
            }
            return false;
        }

        internal static SqlConnection Instance
        {
            get { return _connection; }
        }

        internal static void Close()
        {
            Transaction.Close();
            if (_connection == null) return;
            _connection.Close();
            _connection.Dispose();
        }
    }
}

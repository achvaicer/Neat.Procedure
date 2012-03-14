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
        private static readonly string _defaultConnectionStringName = "Neat.Procedure.Settings.ConnectionString.Default";
        private static string _connectionStringName;
        private static string _connectionString;

        private static string _ConnectionStringName
        {
            get { return _connectionStringName ?? _defaultConnectionStringName; }
        }

        private static string _ConnectionString
        {
            get { return _connectionString ?? ConfigurationManager.ConnectionStrings[_ConnectionStringName].ConnectionString; }
        }

        public static void ConnectionStringName(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public static void ConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal static bool EnsureIsOpened()
        {
            if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
            {
                Close();
                _connection = new SqlConnection(_ConnectionString);
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

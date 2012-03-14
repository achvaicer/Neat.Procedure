using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Neat.Procedure
{
    internal static class Transaction
    {
        private static SqlTransaction _transaction;

        internal static SqlTransaction Instance
        {
            get { return _transaction; } 
        }

        internal static bool IsNull()
        {
            return _transaction == null;
        }

        internal static void Close()
        {
            if (_transaction == null) return;
            _transaction.Dispose();
        }

        public static void Begin()
        {
            Connection.Close();
            Connection.EnsureIsOpened();
            _transaction = Connection.Instance.BeginTransaction();
        }

        public static void Commit()
        {
            if (_transaction != null)
                _transaction.Commit();
            Connection.Close();
        }

        public static void Rollback()
        {
            if (_transaction != null)
                _transaction.Rollback();
            Connection.Close();
        }
    }
}

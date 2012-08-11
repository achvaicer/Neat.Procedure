using System.Data.SqlClient;

namespace Neat.Procedure
{
    internal static class Transaction
    {
        internal static SqlTransaction Instance { get; private set; }

        internal static bool IsNull()
        {
            return Instance == null;
        }

        internal static void Close()
        {
            if (Instance == null) return;
            Instance.Dispose();
        }

        public static void Begin()
        {
            Connection.Close();
            Connection.EnsureIsOpened();
            Instance = Connection.Instance.BeginTransaction();
        }

        public static void Commit()
        {
            if (Instance != null)
                Instance.Commit();
            Connection.Close();
        }

        public static void Rollback()
        {
            if (Instance != null)
                Instance.Rollback();
            Connection.Close();
        }
    }
}

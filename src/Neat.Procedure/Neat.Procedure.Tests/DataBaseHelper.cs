using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace Neat.Procedure.Tests
{
    class DataBaseHelper
    {
        private static readonly string ConnectionStringMaster = ConfigurationManager.ConnectionStrings["Neat.Procedure.Settings.ConnectionString.Master"].ConnectionString;
        private static readonly string ConnectionStringDefault = ConfigurationManager.ConnectionStrings["Neat.Procedure.Settings.ConnectionString.Default"].ConnectionString;

        public static void ExecuteScript(string script, string connectionString)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(script, conn))
                    cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void CreateDataBase()
        {
            TryDropDataBase();

            ExecuteScript(DBScripts.CreateDataBase, ConnectionStringMaster);
            CreateStoredProcedures();
        }

        private static void CreateStoredProcedures()
        {
            var procs = DBScripts.CreateAllProcedures.Split(
                new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var proc in procs)
                ExecuteScript(proc, ConnectionStringDefault);
        }

        private static void TryDropDataBase()
        {
            try
            {
                DropDataBase();
            }
            catch
            { }
        }

        public static void DropDataBase()
        {
            ExecuteScript(DBScripts.DropDataBase, ConnectionStringMaster);
        }
    }
}

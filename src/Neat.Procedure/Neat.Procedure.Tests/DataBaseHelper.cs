using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Neat.Procedure.Tests
{
    class DataBaseHelper
    {
        private static string connectionStringMaster = ConfigurationManager.ConnectionStrings["Neat.Procedure.Settings.ConnectionString.Master"].ConnectionString;
        private static string connectionStringDefault = ConfigurationManager.ConnectionStrings["Neat.Procedure.Settings.ConnectionString.Default"].ConnectionString;

        public static void ExecuteScript(string script, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(script, conn))
                    cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void CreateDataBase()
        {
            ExecuteScript(DBScripts.CreateDataBase, connectionStringMaster);
            var procs = DBScripts.CreateAllProcedures.Split( 
                new string[]{"GO"}, StringSplitOptions.RemoveEmptyEntries );
            foreach (var proc in procs)
                ExecuteScript(proc, connectionStringDefault);
        }

        public static void DropDataBase()
        {
            ExecuteScript(DBScripts.DropDataBase, connectionStringMaster);
        }
    }
}

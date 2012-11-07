using System.Collections.Generic;
using System.Data.SqlClient;

namespace Neat.Procedure
{
    public class ProcedureExecuter
    {
        public static int? CommandTimeout
        {
            get { return Command.CommandTimeout; }
            set { Command.CommandTimeout = value; }
        }

        public static object ExecuteScalar(string storedProcedureName)
        {
            return ExecuteScalar(storedProcedureName, new object[] {});
        }
        public static object ExecuteScalar(string storedProcedureName, Dictionary<string, object> parameters)
        {
            object ret;
            var justopened = Connection.EnsureIsOpened();

            using (var cmd = CurrentContext.Connection.CreateCommand())
            {
                Command.Prepare(cmd, Transaction.Instance, storedProcedureName, parameters);
                var r = Command.AddReturnValueAsParameter(cmd);
                ret = cmd.ExecuteScalar() ?? r.Value;
            }
            if (justopened && Transaction.IsNull())
                Connection.Close();
            return ret;
        }

        

        public static object ExecuteScalar(string storedProcedureName, params object[] parameters)
        {
            object ret;
            var justopened = Connection.EnsureIsOpened();

            using (var cmd = CurrentContext.Connection.CreateCommand())
            {
                Command.Prepare(cmd, Transaction.Instance, storedProcedureName, parameters);
                var r = Command.AddReturnValueAsParameter(cmd);
                ret = cmd.ExecuteScalar() ?? r.Value;
            }
            if (justopened && Transaction.IsNull())
                Connection.Close();
            return ret;
        }

        public static int ExecuteNonQuery(string storedProcedure)
        {
            return ExecuteNonQuery(storedProcedure, new object[] {});
        }
        public static int ExecuteNonQuery(string storedProcedureName, Dictionary<string, object> parameters)
        {
            int count;
            var justopened = Connection.EnsureIsOpened();

            using (var cmd = CurrentContext.Connection.CreateCommand())
            {
                Command.Prepare(cmd, Transaction.Instance, storedProcedureName, parameters);
                count = cmd.ExecuteNonQuery();
            }
            if (justopened && Transaction.IsNull())
                Connection.Close();
            return count;
        }

        public static int ExecuteNonQuery(string storedProcedureName, params object[] parameters)
        {
            int count;
            var justopened = Connection.EnsureIsOpened();

            using (var cmd = CurrentContext.Connection.CreateCommand())
            {
                Command.Prepare(cmd, Transaction.Instance, storedProcedureName, parameters);
                count = cmd.ExecuteNonQuery();
            }
            if (justopened && Transaction.IsNull())
                Connection.Close();
            return count;
        }

        public static IEnumerable<T> ExecuteReader<T>(string storedProcedureName) where T : new()
        {
            return ExecuteReader<T>(storedProcedureName, new object[] {});
        }
        public static IEnumerable<T> ExecuteReader<T>(string storedProcedureName, Dictionary<string, object> parameters) where T : new()
        {
            IList<T> list = new List<T>();
            var justopened = Connection.EnsureIsOpened();
            var modelType = typeof(T);
            using (var cmd = CurrentContext.Connection.CreateCommand())
            {
                Command.Prepare(cmd, Transaction.Instance, storedProcedureName, parameters);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = ResultReader.ToDomainObject<T>(modelType, reader);
                        list.Add(item);
                    }
                }
            }
            if (justopened && Transaction.IsNull())
                Connection.Close();
            return list;
        }

        public static IEnumerable<T> ExecuteReader<T>(string storedProcedureName, params object[] parameters) where T : new()
        {
            IList<T> list = new List<T>();
            var justopened = Connection.EnsureIsOpened();
            var modelType = typeof(T);
            using (var cmd = CurrentContext.Connection.CreateCommand())
            {
                Command.Prepare(cmd, Transaction.Instance, storedProcedureName, parameters);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = ResultReader.ToDomainObject<T>(modelType, reader);
                        list.Add(item);
                    }
                }
            }
            if (justopened && Transaction.IsNull())
                Connection.Close();
            return list;
        }

        
    }
}
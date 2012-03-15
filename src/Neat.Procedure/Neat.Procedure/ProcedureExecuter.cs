using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Neat.Procedure
{
    public class ProcedureExecuter
    {
        public static object ExecuteScalar(string storedProcedureName)
        {
            return ExecuteScalar(storedProcedureName, null);
        }
        public static object ExecuteScalar(string storedProcedureName, Dictionary<string, object> parameters)
        {
            object ret;
            var justopened = Connection.EnsureIsOpened();

            using (var cmd = CurrentContext.Connection.CreateCommand())
            {
                PrepareCommand(storedProcedureName, parameters, Transaction.Instance, cmd);
                ret = cmd.ExecuteScalar();
            }
            if (justopened && Transaction.IsNull())
                Connection.Close();
            return ret;
        }

        public static int ExecuteNonQuery(string storedProcedure)
        {
            return ExecuteNonQuery(storedProcedure, null);
        }
        public static int ExecuteNonQuery(string storedProcedureName, Dictionary<string, object> parameters)
        {
            int count;
            var justopened = Connection.EnsureIsOpened();

            using (var cmd = CurrentContext.Connection.CreateCommand())
            {
                PrepareCommand(storedProcedureName, parameters, Transaction.Instance, cmd);
                count = cmd.ExecuteNonQuery();
            }
            if (justopened && Transaction.IsNull())
                Connection.Close();
            return count;
        }

        public static IEnumerable<T> ExecuteReader<T>(string storedProcedureName) where T : new()
        {
            return ExecuteReader<T>(storedProcedureName, null);
        }
        public static IEnumerable<T> ExecuteReader<T>(string storedProcedureName, Dictionary<string, object> parameters) where T : new()
        {
            IList<T> list = new List<T>();
            var justopened = Connection.EnsureIsOpened();
            var modelType = typeof(T);
            using (var cmd = CurrentContext.Connection.CreateCommand())
            {
                PrepareCommand(storedProcedureName, parameters, Transaction.Instance, cmd);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = ReaderToDomainObject<T>(modelType, reader);
                        list.Add(item);
                    }
                }
            }
            if (justopened && Transaction.IsNull())
                Connection.Close();
            return list;
        }

        private static T ReaderToDomainObject<T>(Type modelType, SqlDataReader reader) where T : new()
        {
            var item = new T();
            var properties = GetFieldNames(reader);

            foreach (var prop in properties)
            {
                SetPropertyValue<T>(modelType, reader, item, prop);
            }
            return item;
        }

        private static void SetPropertyValue<T>(Type modelType, SqlDataReader reader, T item, string prop) where T : new()
        {
            var o = reader[prop];
            var type = o.GetType();
            if (type != typeof(DBNull))
            {
                var p = modelType.GetProperty(prop);
                if (p != null)
                    p.SetValue(item, o, null);
            }
        }

        private static IEnumerable<string> GetFieldNames(IDataRecord dataRecord)
        {
            for (int i = 0; i < dataRecord.FieldCount; i++)
                yield return dataRecord.GetName(i);
        }

        private static void PrepareCommand(string storeProcedureName, Dictionary<string, object> parameters, SqlTransaction trans, SqlCommand cmd)
        {
            cmd.Transaction = trans;
            DictionaryToParameters(parameters, cmd);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;
        }

        private static void DictionaryToParameters(Dictionary<string, object> parameters, SqlCommand cmd)
        {
            if (parameters == null) return;
            foreach (var parameter in parameters)
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
        }
    }
}
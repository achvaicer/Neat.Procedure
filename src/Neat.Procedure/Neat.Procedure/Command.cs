using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Neat.Procedure
{
    internal class Command
    {
        internal static void Prepare(SqlCommand cmd, SqlTransaction trans, string storeProcedureName, Dictionary<string, object> parameters)
        {
            DictionaryToParameters(cmd, parameters);
            Prepare(cmd, trans, storeProcedureName);
        }

        private static void Prepare(SqlCommand cmd, SqlTransaction trans, string storeProcedureName)
        {
            cmd.Transaction = trans;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;
        }

        internal static void Prepare(SqlCommand cmd, SqlTransaction trans, string storeProcedureName, params object[] parameters)
        {
            ArgumentsToParameters(cmd, parameters);
            cmd.Transaction = trans;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;
        }

        private static void DictionaryToParameters(SqlCommand cmd, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;
            foreach (var parameter in parameters)
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
        }

        private static void ArgumentsToParameters(SqlCommand cmd, params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0) return;
            foreach (var parameter in parameters)
                cmd.Parameters.Add(parameter);
        }
    }
}

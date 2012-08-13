using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Neat.Procedure
{
    internal class Command
    {
        internal static SqlParameter AddReturnValueAsParameter(SqlCommand cmd)
        {
            var r = new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue };
            cmd.Parameters.Add(r);
            return r;
        }

        internal static void Prepare(SqlCommand cmd, SqlTransaction trans, string storeProcedureName, Dictionary<string, object> parameters)
        {
            DictionaryToParameters(cmd, parameters);
            Prepare(cmd, trans, storeProcedureName);
        }

        internal static void Prepare(SqlCommand cmd, SqlTransaction trans, string storeProcedureName, params object[] parameters)
        {
            ArgumentsToParameters(cmd, parameters);
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
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

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Neat.Procedure
{
    internal class Command
    {
        private const string ParametersQuery = "SELECT PARAMETER_NAME FROM INFORMATION_SCHEMA.Parameters WHERE SPECIFIC_NAME = @SpecificName ORDER BY ORDINAL_POSITION ASC";
        private const string ParameterNameColumn = "PARAMETER_NAME";

        internal static int? CommandTimeout { get; set; }

        internal static SqlParameter AddReturnValueAsParameter(SqlCommand cmd)
        {
            var r = new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue };
            cmd.Parameters.Add(r);
            return r;
        }

        internal static void Prepare(SqlCommand cmd, SqlTransaction trans, string storeProcedureName, IDictionary<string, object> parameters)
        {
            DictionaryToParameters(cmd, parameters);
            Prepare(cmd, trans, storeProcedureName);
        }

        internal static void Prepare(SqlCommand cmd, SqlTransaction trans, string storeProcedureName, params object[] parameters)
        {
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;
            cmd.CommandTimeout = CommandTimeout ?? 30;
            ArgumentsToParameters(cmd, parameters);
        }

        private static void DictionaryToParameters(SqlCommand cmd, IDictionary<string, object> parameters)
        {
            if (parameters == null) return;
            foreach (var parameter in parameters)
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            
        }

        private static void ArgumentsToParameters(SqlCommand cmd, params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0) return;
            using (var command = new SqlCommand(ParametersQuery, cmd.Connection))
            {
                command.Parameters.AddWithValue("@SpecificName", cmd.CommandText);
                var reader = command.ExecuteReader();
                var i = 0;
                while (reader.Read() && i < parameters.Length)
                    cmd.Parameters.AddWithValue(reader[ParameterNameColumn] as string, parameters[i++]);
            }
        }
    }
}

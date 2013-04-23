using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Neat.Procedure
{
    internal class ResultReader
    {
        internal static T ToDomainObject<T>(Type modelType, SqlDataReader reader) where T : new()
        {
            var item = new T();
            var count = reader.FieldCount;
            Parallel.For(0, count, i => SetPropertyValue(modelType, reader.GetName(i), reader.GetValue(i), item));
            return item;
        }

        private static void SetPropertyValue<T>(Type modelType, string name, object value, T item) where T : new()
        {
            if (value is DBNull) return;

            var p = modelType.GetProperty(name);
            if (p != null)
                p.SetValue(item, value, null);
        }
    }
}

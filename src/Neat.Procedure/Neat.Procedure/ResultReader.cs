using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Neat.Procedure
{
    internal class ResultReader
    {
        internal static T ToDomainObject<T>(Type modelType, SqlDataReader reader) where T : new()
        {
            var item = new T();
            for (int i = 0; i < reader.FieldCount; i++)
                SetPropertyValue<T>(modelType, reader.GetName(i), reader.GetValue(i), item);

            return item;
        }

        private static void SetPropertyValue<T>(Type modelType, string name, object value, T item) where T : new()
        {
            if (value.GetType() == typeof(DBNull)) return;

            var p = modelType.GetProperty(name);
            if (p != null)
                p.SetValue(item, value, null);
        }
    }
}

using System;
using Microsoft.Data.SqlClient;

namespace NetMatch.DAL.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static bool IsDBNull(this SqlDataReader reader, string columnName)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            return reader.IsDBNull(reader.GetOrdinal(columnName));
        }

        public static int GetInt32(this SqlDataReader reader, string columnName)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            return reader.GetInt32(reader.GetOrdinal(columnName));
        }

        public static string GetString(this SqlDataReader reader, string columnName)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            return reader.GetString(reader.GetOrdinal(columnName));
        }

        public static decimal GetDecimal(this SqlDataReader reader, string columnName)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            return reader.GetDecimal(reader.GetOrdinal(columnName));
        }
    }
}
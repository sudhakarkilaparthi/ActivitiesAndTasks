using System.Data;

namespace ActivitiesAndTasksAPI.Helpers
{
	public static class DataReaderExtensions
	{
		public static string? DDRGetNullableString(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.IsDBNull(ordinal) ? null : record.GetString(ordinal);
		}

		public static string DDRGetString(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.GetString(ordinal);
		}

		public static int DDRGetInt32(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.GetInt32(ordinal);
		}

		public static int? DDRGetNullableInt32(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.IsDBNull(ordinal) ? (int?)null : record.GetInt32(ordinal);
		}

		public static bool DDRGetBoolean(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.GetBoolean(ordinal);
		}

		public static bool? DDRGetNullableBoolean(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.IsDBNull(ordinal) ? (bool?)null : record.GetBoolean(ordinal);
		}

		public static DateTime DDRGetDateTime(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.GetDateTime(ordinal);
		}

		public static DateTime? DDRGetNullableDateTime(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.IsDBNull(ordinal) ? (DateTime?)null : record.GetDateTime(ordinal);
		}
	}
}

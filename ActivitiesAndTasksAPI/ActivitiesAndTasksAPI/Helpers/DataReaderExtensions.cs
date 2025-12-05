using System.Data;

namespace ActivitiesAndTasksAPI.Helpers
{
	public static class DataReaderExtensions
	{
		public static string? DREGetNullableString(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.IsDBNull(ordinal) ? null : record.GetString(ordinal);
		}

		public static string DREGetString(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.GetString(ordinal);
		}

		public static int DREGetInt32(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.GetInt32(ordinal);
		}

		public static int? DREGetNullableInt32(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.IsDBNull(ordinal) ? (int?)null : record.GetInt32(ordinal);
		}

		public static bool DREGetBoolean(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.GetBoolean(ordinal);
		}

		public static bool? DREGetNullableBoolean(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.IsDBNull(ordinal) ? (bool?)null : record.GetBoolean(ordinal);
		}

		public static DateTime DREGetDateTime(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.GetDateTime(ordinal);
		}

		public static DateTime? DREGetNullableDateTime(this IDataRecord record, string columnName)
		{
			int ordinal = record.GetOrdinal(columnName);
			return record.IsDBNull(ordinal) ? (DateTime?)null : record.GetDateTime(ordinal);
		}
	}
}

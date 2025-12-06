namespace ActivitiesAndTasksAPI.Helpers
{
	public class StringValueAttribute : Attribute
	{
		public string Value;
		public StringValueAttribute(string value)
		{
			Value = value;
		}

	}
}

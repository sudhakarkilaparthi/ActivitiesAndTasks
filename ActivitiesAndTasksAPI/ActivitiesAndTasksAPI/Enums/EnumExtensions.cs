using ActivitiesAndTasksAPI.Helpers;
using System.Reflection;

namespace ActivitiesAndTasksAPI.Enums
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var field = type.GetField(enumValue.ToString());

            if (field == null)
                return enumValue.ToString();

            var attribute = field.GetCustomAttribute<StringValueAttribute>();

            return attribute?.Value ?? enumValue.ToString();
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StudentTracking.Shared.Extensions;

public static class EnumExtension
{
    public static string DisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            .GetName();
    }
}
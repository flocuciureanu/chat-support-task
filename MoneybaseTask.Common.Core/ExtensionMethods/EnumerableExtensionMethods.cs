// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="EnumerableExtensionMethods.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.ExtensionMethods;

public static class EnumerableExtensionMethods
{
    public static bool HasValue<T>(this IEnumerable<T> enumerable)
    {
        return enumerable != null && enumerable.Any();
    }
}
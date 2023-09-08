// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="StringExtensionMethod.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace MoneybaseTask.Common.Core.ExtensionMethods;

public static class StringExtensionMethod
{
    public static bool IsValidEmailAddress(this string emailAddress)
    {
        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        var match = regex.Match(emailAddress);
        return match.Success;
    }
}
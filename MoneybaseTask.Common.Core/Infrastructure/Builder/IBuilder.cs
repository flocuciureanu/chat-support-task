// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IBuilder.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Infrastructure.Builder;

public interface IBuilder<out T> where T : class
{
    T Build();
}
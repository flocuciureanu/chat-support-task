// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IMappableFactory.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;

namespace MoneybaseTask.Common.Core.Infrastructure.Factory.Mapper;

public interface IMappableFactory<out TMappable> : IFactory where TMappable : IMappable
{
    TMappable Create();
}
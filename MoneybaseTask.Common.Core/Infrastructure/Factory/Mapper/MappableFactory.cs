// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MappableFactory.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;

namespace MoneybaseTask.Common.Core.Infrastructure.Factory.Mapper;

public class MappableFactory<TMappable> : IMappableFactory<TMappable> where TMappable : IMappable
{
    public TMappable Create()
    {
        var mappableItem = Activator.CreateInstance<TMappable>();

        return mappableItem;
    }
}
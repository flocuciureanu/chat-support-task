// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ICollectionBuilder.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Infrastructure.Persistence;

namespace MoneybaseTask.Common.Core.Infrastructure.Builder.CollectionBuilder;

public interface ICollectionBuilder<out TCollection> : IBuilder<TCollection> where TCollection : class, IBaseMongoEntity
{
}
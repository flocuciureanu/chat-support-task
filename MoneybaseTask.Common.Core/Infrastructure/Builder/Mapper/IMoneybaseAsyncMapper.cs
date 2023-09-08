// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IMoneybaseAsyncMapper.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Infrastructure.Persistence;

namespace MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;

public interface IMoneybaseAsyncMapper<in TSource, TResponse> 
    where TSource : IBaseMongoEntity
    where TResponse : IMappable
{
    IMoneybaseAsyncMapper<TSource, TResponse> AddSource(TSource source);
    IMoneybaseAsyncMapper<TSource, TResponse> AddMappingOptions(KeyValuePair<string, object> mappingOption);
    Task<TResponse> MapAsync();
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IMoneybaseMapper.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;

public interface IMoneybaseMapper<out TResponse> 
    where TResponse : IMappable
{
    IMoneybaseMapper<TResponse> AddSource<TSource>(TSource source);
    IMoneybaseMapper<TResponse> AddMappingOptions(KeyValuePair<string, object> mappingOption);
    TResponse Map();
}
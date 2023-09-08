// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MoneybaseMapper.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using MoneybaseTask.Common.Core.ExtensionMethods;
using MoneybaseTask.Common.Core.Infrastructure.Factory.Mapper;

namespace MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;

public class MoneybaseMapper<TResponse> : IMoneybaseMapper<TResponse> 
    where TResponse : IMappable
{
    private readonly IMapper _mapper;
    private readonly IMappableFactory<TResponse> _mappableFactory;

    private object _source;
    private IDictionary<string, object> _mappingOptions= new Dictionary<string, object>();

    public MoneybaseMapper(IMapper mapper, IMappableFactory<TResponse> mappableFactory)
    {
        _mapper = mapper;
        _mappableFactory = mappableFactory;
    }

    public IMoneybaseMapper<TResponse> AddSource<TSource>(TSource source)
    {
        _source = source;

        return this;
    }

    public IMoneybaseMapper<TResponse> AddMappingOptions(KeyValuePair<string, object> mappingOption)
    {
        _mappingOptions.Add(mappingOption);

        return this;        
    }

    public TResponse Map()
    {
        var response = CreateResponse();
            
        var typeMap = _mapper.ConfigurationProvider.BuildExecutionPlan(_source.GetType(), response.GetType());
        if (typeMap is null) 
            return response;
            
        if (_mappingOptions.HasValue())
        {
            var mappingOptions = new Action<IMappingOperationOptions<object, TResponse>>(Action);

            response = _mapper.Map(_source, response, mappingOptions);
        }
        else
        {
            response = _mapper.Map(_source, response);
        }

        Reset();
            
        return response;
    }
    
    private void Reset()
    {
        _source = default;
        _mappingOptions = new Dictionary<string, object>();
    }
        
    private void Action(IMappingOperationOptions<object, TResponse> obj)
    {
        foreach (var (key, value) in _mappingOptions)
        {
            obj.Items[key] = value;
        }
    }

    private TResponse CreateResponse()
    {
        var response = _mappableFactory.Create();

        return response;
    }
}
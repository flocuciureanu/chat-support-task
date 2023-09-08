// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MoneybaseAsyncMapper.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using MoneybaseTask.Common.Core.ExtensionMethods;
using MoneybaseTask.Common.Core.Infrastructure.Factory.Mapper;
using MoneybaseTask.Common.Core.Infrastructure.Persistence;

namespace MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;

public abstract class MoneybaseAsyncMapper<TSource, TResponse> : IMoneybaseAsyncMapper<TSource, TResponse> 
    where TSource : IBaseMongoEntity
    where TResponse : IMappable
{
    private readonly IMapper _mapper;
    private readonly IMappableFactory<TResponse> _mappableFactory;
        
    protected TSource Source;
    protected IDictionary<string, object> MappingOptions= new Dictionary<string, object>();
    protected readonly List<Task> TaskList = new();

    protected MoneybaseAsyncMapper(IMapper mapper, IMappableFactory<TResponse> mappableFactory)
    {
        _mapper = mapper;
        _mappableFactory = mappableFactory;
    }

    public IMoneybaseAsyncMapper<TSource, TResponse> AddSource(TSource source)
    {
        Source = source;

        return this;
    }

    public IMoneybaseAsyncMapper<TSource, TResponse> AddMappingOptions(KeyValuePair<string, object> mappingOption)
    {
        MappingOptions.Add(mappingOption);

        return this;
    }

    private TResponse Map()
    {
        var response = CreateResponse();

        var typeMap = _mapper.ConfigurationProvider.BuildExecutionPlan(Source.GetType(), response.GetType());
        if (typeMap is null) 
            return response;
            
        if (MappingOptions.HasValue())
        {
            var mappingOptions = new Action<IMappingOperationOptions<object, TResponse>>(Action);

            response = _mapper.Map(Source, response, mappingOptions);
        }
        else
        {
            response = _mapper.Map(Source, response);
        }

        Reset();

        return response;
    }

    public async Task<TResponse> MapAsync()
    {
        GenerateMappingOptionValues();
            
        await Task.WhenAll(TaskList);

        return Map();
    }
        
    protected abstract void GenerateMappingOptionValues();

    private void Reset()
    {
        MappingOptions = new Dictionary<string, object>();
    }
    
    private void Action(IMappingOperationOptions<object, TResponse> obj)
    {
        foreach (var (key, value) in MappingOptions)
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
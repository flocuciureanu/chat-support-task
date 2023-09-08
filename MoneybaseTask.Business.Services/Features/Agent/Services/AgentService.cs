// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="AgentService.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.Agent.MongoModel;
using MoneybaseTask.Business.Entities.Entities.Agent.Requests;
using MoneybaseTask.Common.Core.Common;
using MoneybaseTask.Common.Core.Common.Helpers;
using MoneybaseTask.Common.Core.ExtensionMethods;
using MoneybaseTask.Common.Core.Infrastructure.Persistence;
using MongoDB.Driver;

namespace MoneybaseTask.Business.Services.Features.Agent.Services;

public class AgentService : IAgentService
{
    private readonly IDatabaseRepository<AgentCollection> _agentRepository;

    public AgentService(IDatabaseRepository<AgentCollection> agentRepository)
    {
        _agentRepository = agentRepository;
    }

    public Task InsertOneAsync(AgentCollection agent)
        => _agentRepository.InsertOneAsync(agent);

    public Task<List<AgentCollection>> GetAllAsync()
        => _agentRepository.GetAllAsync();

    public Task<AgentCollection> GetOneAsync(FilterDefinition<AgentCollection> filter)
        => _agentRepository.GetOneAsync(filter);

    public Task<List<AgentCollection>> GetManyAsync(FilterDefinition<AgentCollection> filter)
        => _agentRepository.GetManyAsync(filter);

    public Task<AgentCollection> GetByIdAsync(string id)
        => _agentRepository.GetByIdAsync(id);

    public async Task<List<AgentCollection>> GetCurrentShiftAgents(GetAgentsRequestType requestType)
    {
        var currentShift = ShiftHelper.GetCurrentShift();

        var agentFilter = requestType switch
        {
            GetAgentsRequestType.AllAgents => Builders<AgentCollection>.Filter.Where(x =>
                x.Details.WorkingShift.Equals(currentShift)),
            GetAgentsRequestType.RegularAgents => Builders<AgentCollection>.Filter.Where(x =>
                x.Details.WorkingShift.Equals(currentShift) && !x.Details.IsPartOfOverFlowTeam),
            GetAgentsRequestType.OverFlowAgents => Builders<AgentCollection>.Filter.Where(x =>
                x.Details.WorkingShift.Equals(currentShift) && x.Details.IsPartOfOverFlowTeam),
            _ => Builders<AgentCollection>.Filter.Where(x =>
                x.Details.WorkingShift.Equals(currentShift))
        };

        var agents = await GetManyAsync(agentFilter);

        return agents;
    }

    public async Task<int> CalculateAgentsCapacityAsync(List<AgentCollection> agents = null)
    {
        const int zeroCapacity = 0;
        agents ??= await GetCurrentShiftAgents(GetAgentsRequestType.RegularAgents);
        
        return agents.HasValue() 
            ? agents.Sum(CalculateAgentCapacity) 
            : zeroCapacity;
    }
    
    public int CalculateAgentCapacity(AgentCollection agent)
    {
        const int zeroCapacity = 0;

        return agent is null
            ? zeroCapacity
            : (int)(AgentSeniorityHelper.GetSeniorityMultiplier(agent.Details.Seniority) * Constants.AgentMaximumConcurrency);
    }

    public async Task<int> CalculateAgentsQueueLengthAsync(List<AgentCollection> agents = null)
    {
        var teamCapacity = await CalculateAgentsCapacityAsync(agents);

        return (int)Math.Round(teamCapacity * Constants.MaximumQueueLengthMultiplier, MidpointRounding.ToEven);
    }
}
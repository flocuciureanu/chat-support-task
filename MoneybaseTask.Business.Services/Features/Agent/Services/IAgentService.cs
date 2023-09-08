// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IAgentService.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.Agent.MongoModel;
using MoneybaseTask.Business.Entities.Entities.Agent.Requests;
using MongoDB.Driver;

namespace MoneybaseTask.Business.Services.Features.Agent.Services;

public interface IAgentService
{
    Task InsertOneAsync(AgentCollection agent);
    Task<List<AgentCollection>> GetAllAsync();
    Task<AgentCollection> GetOneAsync(FilterDefinition<AgentCollection> filter);
    Task<List<AgentCollection>> GetManyAsync(FilterDefinition<AgentCollection> filter);
    Task<AgentCollection> GetByIdAsync(string id);
    Task<List<AgentCollection>> GetCurrentShiftAgents(GetAgentsRequestType requestType);
    int CalculateAgentCapacity(AgentCollection agent);
    Task<int> CalculateAgentsCapacityAsync(List<AgentCollection> agents = null);
    Task<int> CalculateAgentsQueueLengthAsync(List<AgentCollection> agents = null);
}
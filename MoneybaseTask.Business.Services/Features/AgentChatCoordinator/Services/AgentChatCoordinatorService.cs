// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="AgentChatCoordinatorService.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.Agent.MongoModel;
using MoneybaseTask.Business.Entities.Entities.Agent.Requests;
using MoneybaseTask.Business.Entities.Entities.AgentChatCoordinator;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Services.Features.Agent.Services;
using MoneybaseTask.Business.Services.Features.ChatSession.Services;
using MoneybaseTask.Common.Core.Common.Helpers;
using MoneybaseTask.Common.Core.ExtensionMethods;

namespace MoneybaseTask.Business.Services.Features.AgentChatCoordinator.Services;

public class AgentChatCoordinatorService : IAgentChatCoordinatorService
{
    private readonly IChatSessionService _chatSessionService;
    private readonly IAgentService _agentService;

    public AgentChatCoordinatorService(IChatSessionService chatSessionService,
        IAgentService agentService)
    {
        _chatSessionService = chatSessionService;
        _agentService = agentService;
    }
    
    public async Task<CapacityDetailsModel> GetCapacityDetailsAsync()
    {
        var agents = await _agentService.GetCurrentShiftAgents(GetAgentsRequestType.RegularAgents);
        if (!agents.HasValue())
            return new CapacityDetailsModel(false);

        var inProgressChatSessions = await _chatSessionService.GetChatSessionsByAgentsAndStatusAsync(
                agents.Select(x => x.Id), ChatSessionStatus.InProgress);
        var agentsCapacity = await _agentService.CalculateAgentsCapacityAsync(agents);
        
        if (agentsCapacity > inProgressChatSessions.Count)
        {
            var agent = await GetNextAvailableAgentAsync(agents);
            return agent is null 
                ? new CapacityDetailsModel(true) 
                : new CapacityDetailsModel(true, false, agent.Id, agent.Details.Name);
        }

        var pendingChatSessions = await _chatSessionService.GetChatSessionsByStatus(ChatSessionStatus.Pending);
        var agentsQueueLength = await _agentService.CalculateAgentsQueueLengthAsync(agents);
        
        if (agentsQueueLength > pendingChatSessions.Count)
            return new CapacityDetailsModel(true);

        if (!ShiftHelper.IsCurrentShiftDuringOfficeHours())
            return new CapacityDetailsModel(false);
        
        var overFlowAgents = await _agentService.GetCurrentShiftAgents(GetAgentsRequestType.OverFlowAgents);
        
        var overFlowAgentsCapacity = await _agentService.CalculateAgentsCapacityAsync(overFlowAgents);

        var inProgressOverFlowChatSessions = await _chatSessionService.GetChatSessionsByAgentsAndStatusAsync(
            overFlowAgents.Select(x => x.Id), ChatSessionStatus.InProgress);

        if (overFlowAgentsCapacity > inProgressOverFlowChatSessions.Count)
            return new CapacityDetailsModel(true, true);

        var overFlowAgentsQueueLength = await _agentService.CalculateAgentsQueueLengthAsync(overFlowAgents);
        return overFlowAgentsQueueLength > pendingChatSessions.Count 
            ? new CapacityDetailsModel(true) 
            : new CapacityDetailsModel(false);
    }

    public async Task TriggerOverFlowTeam()
    {
        var overFlowAgents = await _agentService.GetCurrentShiftAgents(GetAgentsRequestType.OverFlowAgents);
        var nextAvailableOverFlowAgent = await GetNextAvailableAgentAsync(overFlowAgents);
        
        await _chatSessionService.AssignAgentToNextPendingChatSession(nextAvailableOverFlowAgent);
    }

    public async Task FindAndAssignAgentToNextPendingChatSession()
    {
        var nextPendingChatSession = await _chatSessionService.GetNextPendingChatSession();
        if (nextPendingChatSession is null)
            return;
        
        var agents = await _agentService.GetCurrentShiftAgents(GetAgentsRequestType.RegularAgents);
        if (!agents.HasValue())
            return;
        
        var inProgressChatSessions = await _chatSessionService.GetChatSessionsByAgentsAndStatusAsync(
            agents.Select(x => x.Id), ChatSessionStatus.InProgress);
        
        var agentsCapacity = await _agentService.CalculateAgentsCapacityAsync(agents);
        if (agentsCapacity > inProgressChatSessions.Count)
        {
            var nextAvailableAgent = await GetNextAvailableAgentAsync(agents);
            await _chatSessionService.AssignAgentToNextPendingChatSession(nextAvailableAgent, nextPendingChatSession);
            
            return;
        }
        
        var overFlowAgents = await _agentService.GetCurrentShiftAgents(GetAgentsRequestType.OverFlowAgents);
        if (!overFlowAgents.HasValue())
            return;
        
        var inProgressOverFlowChatSessions = await _chatSessionService.GetChatSessionsByAgentsAndStatusAsync(
                overFlowAgents.Select(x => x.Id), ChatSessionStatus.InProgress);
        
        var overFlowAgentsCapacity = await _agentService.CalculateAgentsCapacityAsync(overFlowAgents);
        if (overFlowAgentsCapacity > inProgressOverFlowChatSessions.Count)
        {
            var nextAvailableOverFlowAgent = await GetNextAvailableAgentAsync(overFlowAgents);
            await _chatSessionService.AssignAgentToNextPendingChatSession(nextAvailableOverFlowAgent, nextPendingChatSession);
        }
    }

    private async Task<AgentCollection> GetNextAvailableAgentAsync(IEnumerable<AgentCollection> agents)
    {
        var orderedAgents = agents.OrderBy(agent => (int)agent.Details.Seniority);

        foreach (var agent in orderedAgents)
        {
            var agentCapacity = _agentService.CalculateAgentCapacity(agent);
            var assignedChats = await _chatSessionService.GetInProgressAssignedChatsCountByAgentIdAsync(agent.Id);

            if (agentCapacity > assignedChats)
                return agent;
        }

        return null;
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IAgentChatCoordinatorService.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.AgentChatCoordinator;

namespace MoneybaseTask.Business.Services.Features.AgentChatCoordinator.Services;

public interface IAgentChatCoordinatorService
{
    Task<CapacityDetailsModel> GetCapacityDetailsAsync();
    Task TriggerOverFlowTeam();
    Task FindAndAssignAgentToNextPendingChatSession();
}
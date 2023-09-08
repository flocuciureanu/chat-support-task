// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MarkChatSessionAsCompletedEventHandler.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Hangfire;
using MoneybaseTask.Business.Services.Features.AgentChatCoordinator.Services;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Status;

public class MarkChatSessionAsCompletedEventHandler : IEventHandler<MarkChatSessionAsCompletedCommand, ICommandResult>
{
    private readonly IAgentChatCoordinatorService _agentChatCoordinatorService;

    public MarkChatSessionAsCompletedEventHandler(IAgentChatCoordinatorService agentChatCoordinatorService)
    {
        _agentChatCoordinatorService = agentChatCoordinatorService;
    }
    
    public Task Process(MarkChatSessionAsCompletedCommand request, ICommandResult response, CancellationToken cancellationToken)
    {
        if (response is null || !response.Success)
            return Task.CompletedTask;

        BackgroundJob.Enqueue(() => _agentChatCoordinatorService.FindAndAssignAgentToNextPendingChatSession());

        return Task.CompletedTask;
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MarkChatSessionsAsClosedEventHandler.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Hangfire;
using MoneybaseTask.Business.Services.Features.AgentChatCoordinator.Services;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Status;

public class MarkChatSessionsAsClosedEventHandler : IEventHandler<MarkChatSessionsAsClosedCommand, ICommandResult>
{
    private readonly IAgentChatCoordinatorService _agentChatCoordinatorService;

    public MarkChatSessionsAsClosedEventHandler(IAgentChatCoordinatorService agentChatCoordinatorService)
    {
        _agentChatCoordinatorService = agentChatCoordinatorService;
    }

    public Task Process(MarkChatSessionsAsClosedCommand request, ICommandResult response, CancellationToken cancellationToken)
    {
        if (response is null || !response.Success)
            return Task.CompletedTask;

        BackgroundJob.Enqueue(() => _agentChatCoordinatorService.FindAndAssignAgentToNextPendingChatSession());

        return Task.CompletedTask;
    }
}
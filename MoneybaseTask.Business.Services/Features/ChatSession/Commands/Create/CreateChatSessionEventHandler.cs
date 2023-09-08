// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateChatSessionEventHandler.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Hangfire;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Requests.RequestCachedItems;
using MoneybaseTask.Business.Services.Features.AgentChatCoordinator.Services;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Create;

public class CreateChatSessionEventHandler : IEventHandler<CreateChatSessionCommand, ICommandResult>
{
    private readonly IAgentChatCoordinatorService _agentChatCoordinatorService;
    private readonly CommandCachedItems _cachedItems;

    public CreateChatSessionEventHandler(IAgentChatCoordinatorService agentChatCoordinatorService, 
        CommandCachedItems cachedItems)
    {
        _agentChatCoordinatorService = agentChatCoordinatorService;
        _cachedItems = cachedItems;
    }

    public Task Process(CreateChatSessionCommand request, ICommandResult response, CancellationToken cancellationToken)
    {
        if (!_cachedItems.TryGetValue(ChatSessionKeyConstants.CreateChatSessionRequestCachedItem, out var requestCachedItem) ||
            requestCachedItem is not CreateChatSessionRequestCachedItem createChatSessionRequestCachedItem)
        {
            //Log error here
            return Task.CompletedTask;
        }

        if (createChatSessionRequestCachedItem.TriggerOverFlowTeam)
        {
            BackgroundJob.Enqueue(() => _agentChatCoordinatorService.TriggerOverFlowTeam());
        }
        
        return Task.CompletedTask;
    }
}
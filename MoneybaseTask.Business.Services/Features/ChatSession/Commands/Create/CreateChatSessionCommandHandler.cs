// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateChatSessionCommandHandler.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Requests.RequestCachedItems;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;
using MoneybaseTask.Business.Services.Features.AgentChatCoordinator.Services;
using MoneybaseTask.Business.Services.Features.ChatSession.Builders;
using MoneybaseTask.Business.Services.Features.ChatSession.Services;
using MoneybaseTask.Common.Core.ExtensionMethods;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;
using MongoDB.Driver;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Create;

public class CreateChatSessionCommandHandler : ICommandHandler<CreateChatSessionCommand, ICommandResult>
{
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly IChatSessionService _chatSessionService;
    private readonly IChatSessionModelBuilder _chatSessionModelBuilder;
    private readonly IAgentChatCoordinatorService _agentChatCoordinatorService;
    private readonly CommandCachedItems _cachedItems;

    public CreateChatSessionCommandHandler(ICommandResultFactory commandResultFactory, 
        IChatSessionService chatSessionService, 
        IChatSessionModelBuilder chatSessionModelBuilder, 
        IAgentChatCoordinatorService agentChatCoordinatorService, 
        CommandCachedItems cachedItems)
    {
        _commandResultFactory = commandResultFactory;
        _chatSessionService = chatSessionService;
        _chatSessionModelBuilder = chatSessionModelBuilder;
        _agentChatCoordinatorService = agentChatCoordinatorService;
        _cachedItems = cachedItems;
    }

    public async Task<ICommandResult> Handle(CreateChatSessionCommand request, CancellationToken cancellationToken)
    {
        var emailAddressFilter = Builders<ChatSessionCollection>.Filter.Where(x =>
            x.Details.UserDetails.EmailAddress.Equals(request.Request.EmailAddress.ToLower()) &&
            (x.StatusHistory.Last().Status.Equals(ChatSessionStatus.Pending) ||
            x.StatusHistory.Last().Status.Equals(ChatSessionStatus.InProgress)));

        var chatSessionsForEmailAddress = await _chatSessionService.GetManyAsync(emailAddressFilter);
        if (chatSessionsForEmailAddress.HasValue())
            return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest,
                CommandResultCustomMessages.CreateChatSessionErrorDuplicateEmail);
        
        var chatSessionBuilder = _chatSessionModelBuilder
            .AddFirstName(request.Request.FirstName)
            .AddLastName(request.Request.LastName)
            .AddEmailAddress(request.Request.EmailAddress);
        
        var capacityDetails = await _agentChatCoordinatorService.GetCapacityDetailsAsync();
        if (capacityDetails.HasCapacity)
            chatSessionBuilder.AddStatus(ChatSessionStatus.Pending, "Chat session successfully created");
        else
            chatSessionBuilder.AddStatus(ChatSessionStatus.Refused, "No Agents available at the moment");

        var chatSession = chatSessionBuilder.Build();
        
        await _chatSessionService.InsertOneAsync(chatSession);

        if (!capacityDetails.HasCapacity)
            return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest, 
                CommandResultCustomMessages.CreateChatSessionErrorNoAgents);

        var response = new CreateChatSessionResponse(chatSession.Id);

        _cachedItems.Add(ChatSessionKeyConstants.CreateChatSessionRequestCachedItem,
            new CreateChatSessionRequestCachedItem(capacityDetails.TriggerOverFlowTeam));
        
        if (string.IsNullOrEmpty(capacityDetails.AgentId))
        {
            return _commandResultFactory.Create(true, StatusCodes.Status201Created,
                CommandResultCustomMessages.CreateChatSessionSuccess, response);
        }
        
        var updateList = new List<UpdateDefinition<ChatSessionCollection>>()
        {
            Builders<ChatSessionCollection>.Update.Set(x => x.Details.AgentId, capacityDetails.AgentId),
            Builders<ChatSessionCollection>.Update.Push(x => x.StatusHistory, new ChatSessionStatusDocument()
            {
                Status = ChatSessionStatus.InProgress,
                UpdatedOn = DateTime.UtcNow,
                Notes = $"Chat session assigned to: {capacityDetails.AgentId} at: {DateTime.UtcNow} and marked as: {ChatSessionStatus.InProgress}"
            }),
            Builders<ChatSessionCollection>.Update.Push(x => x.Details.Messages, new ChatSessionMessageDocument()
            {
                Content = $"Hi {request.Request.FirstName}! You are now talking to one of our experts, {capacityDetails.AgentName}",
                Type = ChatMessageType.System,
                SentAt = DateTime.UtcNow
            })
        };
        var filter = Builders<ChatSessionCollection>.Filter.Where(x => x.Id.Equals(chatSession.Id));
        var update = Builders<ChatSessionCollection>.Update.Combine(updateList);

        await _chatSessionService.FindOneAndUpdateAsync(filter, update);

        response.AgentDetails = new ChatSessionAgentDetailsResponse(capacityDetails.AgentId, capacityDetails.AgentName);
        
        return _commandResultFactory.Create(true, StatusCodes.Status201Created,
            CommandResultCustomMessages.CreateChatSessionSuccess, response);
    }
}
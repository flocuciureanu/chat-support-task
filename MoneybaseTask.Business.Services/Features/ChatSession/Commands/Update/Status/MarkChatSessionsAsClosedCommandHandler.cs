// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MarkChatSessionsAsClosedCommandHandler.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Services.Features.ChatSession.Services;
using MoneybaseTask.Common.Core.ExtensionMethods;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;
using MongoDB.Driver;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Status;

public class MarkChatSessionsAsClosedCommandHandler : ICommandHandler<MarkChatSessionsAsClosedCommand, ICommandResult>
{
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly IChatSessionService _chatSessionService;

    public MarkChatSessionsAsClosedCommandHandler(ICommandResultFactory commandResultFactory, 
        IChatSessionService chatSessionService)
    {
        _commandResultFactory = commandResultFactory;
        _chatSessionService = chatSessionService;
    }

    public async Task<ICommandResult> Handle(MarkChatSessionsAsClosedCommand request, CancellationToken cancellationToken)
    {
        FilterDefinition<ChatSessionCollection> filter;
        switch (request.MarkChatSessionsAsClosedRequest.Status)
        {
            case ChatSessionStatus.Pending:
                filter = Builders<ChatSessionCollection>.Filter.Where(x =>
                    x.StatusHistory.Last().Status.Equals(ChatSessionStatus.Pending) &&
                    x.CreatedOn < DateTime.UtcNow.AddHours(-24));
                break;
            case ChatSessionStatus.InProgress:
                filter = Builders<ChatSessionCollection>.Filter.Where(x =>
                    x.StatusHistory.Last().Status.Equals(ChatSessionStatus.InProgress) &&
                    x.Details.PollingDetails.PollingCount < 3);
                break;
            default:
                return _commandResultFactory.Create(true, StatusCodes.Status400BadRequest, CommandResultCustomMessages.UpdateStatusChatSessionWrongStatus);
        }
        
        var chatSessions = await _chatSessionService.GetManyAsync(filter);
        if (!chatSessions.HasValue())
            return _commandResultFactory.Create(true, CommandResultCustomMessages.NoChatSessionFound);

        var updateList = new List<string>();
        var listWrites = new List<WriteModel<ChatSessionCollection>>();
        
        foreach (var chatSession in chatSessions)
        {
            //Give the fact that the cron job is running every minute there might be the case when a chat session has
            //just been marked as 'In Progress' and it didn't get the chance to receive all the 3 polling requests
            if ((DateTime.UtcNow - chatSession.Details.PollingDetails.LastPollRequestReceivedAt).TotalSeconds <= 1)
                continue;
            
            var newStatus = new ChatSessionStatusDocument()
            {
                Status = ChatSessionStatus.Closed,
                UpdatedOn = DateTime.UtcNow,
            };

            newStatus.Notes = request.MarkChatSessionsAsClosedRequest.Status switch
            {
                ChatSessionStatus.InProgress =>
                    $"Chat session has received {chatSession.Details.PollingDetails.PollingCount} poll requests and the Last one was received at: {chatSession.Details.PollingDetails.LastPollRequestReceivedAt}. Chat session status updated to: {ChatSessionStatus.Closed} at: {DateTime.UtcNow} due to inactivity",
                ChatSessionStatus.Pending =>
                    $"Chat session hasn't been assigned to an agent since it was created at {chatSession.CreatedOn}. Chat session status updated to: {ChatSessionStatus.Closed} at: {DateTime.UtcNow} due to inactivity",
                _ => string.Empty
            };

            var chatSessionFilter = Builders<ChatSessionCollection>.Filter.Where(x => x.Id.Equals(chatSession.Id));
            var update = Builders<ChatSessionCollection>.Update.Push(x => x.StatusHistory, newStatus);
            
            listWrites.Add(new UpdateOneModel<ChatSessionCollection>(chatSessionFilter, update));
            
            updateList.Add(newStatus.Notes);
        }
        
        await _chatSessionService.BulkWriteAsync(listWrites);
        
        return _commandResultFactory.Create(true, updateList);
    }
}
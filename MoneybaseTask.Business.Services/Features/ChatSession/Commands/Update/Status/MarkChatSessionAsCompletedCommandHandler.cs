// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MarkChatSessionAsCompletedCommandHandler.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Services.Features.ChatSession.Services;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;
using MongoDB.Driver;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Status;

public class MarkChatSessionAsCompletedCommandHandler : ICommandHandler<MarkChatSessionAsCompletedCommand, ICommandResult>
{
    private readonly IChatSessionService _chatSessionService;
    private readonly ICommandResultFactory _commandResultFactory;

    public MarkChatSessionAsCompletedCommandHandler(IChatSessionService chatSessionService, 
        ICommandResultFactory commandResultFactory)
    {
        _chatSessionService = chatSessionService;
        _commandResultFactory = commandResultFactory;
    }

    public async Task<ICommandResult> Handle(MarkChatSessionAsCompletedCommand request, CancellationToken cancellationToken)
    {
        var chatSession = await _chatSessionService.GetByIdAsync(request.Request.Id);
        if (chatSession is null)
            return _commandResultFactory.Create(false, StatusCodes.Status404NotFound,
                $"{CommandResultCustomMessages.ChatSessionNotFoundForId}{request.Request.Id}");

        if (!chatSession.StatusHistory.Last().Status.Equals(ChatSessionStatus.InProgress))
            return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest,
                CommandResultCustomMessages.ChatSessionPollErrorWrongStatus);
        
        var newStatus = new ChatSessionStatusDocument()
        {
            Status = ChatSessionStatus.Completed,
            Notes = request.Request.Notes,
            UpdatedOn = DateTime.UtcNow
        };

        var filter = Builders<ChatSessionCollection>.Filter.Where(x => x.Id.Equals(request.Request.Id));
        var update = Builders<ChatSessionCollection>.Update.Push(x => x.StatusHistory, newStatus);

        await _chatSessionService.FindOneAndUpdateAsync(filter, update);
        
        return _commandResultFactory.Create(true, CommandResultCustomMessages.UpdateStatusChatSessionSuccess);
    }
}
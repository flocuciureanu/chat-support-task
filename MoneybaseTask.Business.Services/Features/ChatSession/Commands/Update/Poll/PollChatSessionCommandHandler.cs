// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="PollChatSessionCommandHandler.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Services.Features.ChatSession.Services;
using MoneybaseTask.Common.Core.Common;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;
using MongoDB.Driver;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Poll;

public class PollChatSessionCommandHandler : ICommandHandler<PollChatSessionCommand, ICommandResult>
{
    private readonly IChatSessionService _chatSessionService;
    private readonly ICommandResultFactory _commandResultFactory;

    public PollChatSessionCommandHandler(IChatSessionService chatSessionService, 
        ICommandResultFactory commandResultFactory)
    {
        _chatSessionService = chatSessionService;
        _commandResultFactory = commandResultFactory;
    }

    public async Task<ICommandResult> Handle(PollChatSessionCommand request, CancellationToken cancellationToken)
    {
        var chatSession = await _chatSessionService.GetByIdAsync(request.Request.Id);
        if (chatSession is null)
            return _commandResultFactory.Create(false, StatusCodes.Status404NotFound,
                $"{CommandResultCustomMessages.ChatSessionNotFoundForId}{request.Request.Id}");

        if (!chatSession.StatusHistory.Last().Status.Equals(ChatSessionStatus.InProgress))
            return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest,
                CommandResultCustomMessages.ChatSessionPollErrorWrongStatus);

        if (chatSession.Details.PollingDetails.PollingCount >= 3)
            return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest,
                $"{CommandResultCustomMessages.ChatSessionPollErrorPollCountEqualTo}{chatSession.Details.PollingDetails.PollingCount}");

        var utcNow = DateTime.UtcNow;
        
        if (!chatSession.Details.PollingDetails.LastPollRequestReceivedAt.Equals(DateTime.MinValue) &&
            chatSession.Details.PollingDetails.LastPollRequestReceivedAt < utcNow.AddSeconds(-Constants.PollingFrequencyInSeconds))
            return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest,
                $"{CommandResultCustomMessages.ChatSessionPollErrorLastPollRequestReceivedAt}{chatSession.Details.PollingDetails.LastPollRequestReceivedAt:G} which is more than {Constants.PollingFrequencyInSeconds} second(s)");

        chatSession.Details.PollingDetails.PollingCount++;
        chatSession.Details.PollingDetails.LastPollRequestReceivedAt = utcNow;
        var filter = Builders<ChatSessionCollection>.Filter.Where(x => x.Id.Equals(request.Request.Id));
        var update = Builders<ChatSessionCollection>.Update.Set(x => x.Details.PollingDetails, chatSession.Details.PollingDetails);

        await _chatSessionService.FindOneAndUpdateAsync(filter, update);

        return _commandResultFactory.Create(true);
    }
}
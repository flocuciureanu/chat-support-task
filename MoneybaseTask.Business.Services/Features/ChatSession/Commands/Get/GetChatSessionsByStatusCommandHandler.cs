// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="GetChatSessionsByStatusCommandHandler.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;
using MoneybaseTask.Business.Services.Features.ChatSession.Services;
using MoneybaseTask.Common.Core.ExtensionMethods;
using MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Get;

public class GetChatSessionsByStatusCommandHandler : IQueryHandler<GetChatSessionsByStatusCommand, ICommandResult>
{
    private readonly IChatSessionService _chatSessionService;
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly IMoneybaseAsyncMapper<ChatSessionCollection, GetChatSessionsByStatusResponseItem> _responseMapper;
    
    public GetChatSessionsByStatusCommandHandler(IChatSessionService chatSessionService, 
        ICommandResultFactory commandResultFactory, 
        IMoneybaseAsyncMapper<ChatSessionCollection, GetChatSessionsByStatusResponseItem> responseMapper)
    {
        _chatSessionService = chatSessionService;
        _commandResultFactory = commandResultFactory;
        _responseMapper = responseMapper;
    }

    public async Task<ICommandResult> Handle(GetChatSessionsByStatusCommand request, CancellationToken cancellationToken)
    {
        var chatSessions = await _chatSessionService.GetChatSessionsByStatus(request.GetChatSessionsByStatusRequest.Status);
        if (!chatSessions.HasValue())
            return _commandResultFactory.Create(false, StatusCodes.Status404NotFound);

        var responseItems = new List<GetChatSessionsByStatusResponseItem>();
        foreach (var chatSession in chatSessions)
        {
            var responseItem = await _responseMapper
                .AddSource(chatSession)
                .MapAsync();
            
            responseItems.Add(responseItem);
        }

        var response = new GetChatSessionsByStatusResponse(responseItems);
        
        return _commandResultFactory.Create(true, response);
    }
}
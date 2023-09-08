// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MarkChatSessionsAsClosedRecurringJob.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Hangfire;
using MediatR;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Requests;
using MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Status;

namespace MoneybaseTask.Hosting.WebApi.RecurringJobs;

internal class MarkChatSessionsAsClosedRecurringJob : BaseRecurringJob
{
    private readonly IMediator _mediator;
    private readonly ILogger<MarkChatSessionsAsClosedRecurringJob> _logger;

    public MarkChatSessionsAsClosedRecurringJob(
        ILogger<MarkChatSessionsAsClosedRecurringJob> logger, 
        IMediator mediator) 
        : base(logger)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [JobDisplayName("Inactivate chat sessions")]
    [AutomaticRetry(Attempts = 3)]
    public async Task Execute(ChatSessionStatus status)
    {
        try
        {
            var commandResult = await _mediator.Send(new MarkChatSessionsAsClosedCommand()
            {
                MarkChatSessionsAsClosedRequest = new MarkChatSessionsAsClosedRequest()
                {
                    Status = status
                }
            });
            
            HandleCommandResult(commandResult);
        }
        catch (Exception e)
        {
            _logger.LogError("Couldn't finish destination import job: {Message}", e.Message);
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionController.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Requests;
using MoneybaseTask.Business.Services.Features.ChatSession.Commands.Create;
using MoneybaseTask.Business.Services.Features.ChatSession.Commands.Get;
using MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Poll;
using MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Status;
using MoneybaseTask.Hosting.WebApi.Contracts.V1;

namespace MoneybaseTask.Hosting.WebApi.Controllers;

/// <summary>
/// This is the ChatSession Controller
/// </summary>
public class ChatSessionController : MoneybaseController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// This is the ChatSession Controller constructor
    /// </summary>
    public ChatSessionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new chat session
    /// </summary>
    /// <response code="400">Bad request</response>
    /// <response code="422">Validation error</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [Route(ApiRoutes.ChatSession.Create)]
    public async Task<IActionResult> CreateChatSessionAsync([FromBody] CreateChatSessionRequest request)
    {
        var commandResult = await _mediator.Send(new CreateChatSessionCommand()
        {
            Request = request
        });

        return StatusCode(commandResult.StatusCode, commandResult);
    }

    /// <summary>
    /// Polls a chat session
    /// </summary>
    /// <response code="400">Bad request</response>
    /// <response code="422">Validation error</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [Route(ApiRoutes.ChatSession.Poll)]
    public async Task<IActionResult> PollAsync([FromRoute] string id)
    {
        var commandResult = await _mediator.Send(new PollChatSessionCommand()
        {
            Request = new PollChatSessionRequest
            {
                Id = id
            }
        });
        
        return StatusCode(commandResult.StatusCode, commandResult);
    }
    
    /// <summary>
    /// Updates a chat session's status as completed
    /// </summary>
    /// <response code="400">Bad request</response>
    /// <response code="422">Validation error</response>
    /// <response code="500">Internal server error</response>
    [HttpPatch]
    [Route(ApiRoutes.ChatSession.UpdateStatusAsCompleted)]
    public async Task<IActionResult> UpdateStatusAsCompletedAsync([FromRoute] string id, [FromBody] string notes)
    {
        var commandResult = await _mediator.Send(new MarkChatSessionAsCompletedCommand()
        {
            Request = new MarkChatSessionAsCompletedRequest()
            {
                Id = id,
                Notes = notes
            }
        });
        
        return StatusCode(commandResult.StatusCode, commandResult);
    }
    
    /// <summary>
    /// Gets chat sessions details by status
    /// </summary>
    /// <response code="400">Bad request</response>
    /// <response code="422">Validation error</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route(ApiRoutes.ChatSession.GetByStatus)]
    public async Task<IActionResult> GetCurrentShiftAgentsAndChatSessionsAsync([FromQuery]ChatSessionStatus status)
    {
        var commandResult = await _mediator.Send(new GetChatSessionsByStatusCommand
        {
            GetChatSessionsByStatusRequest = new GetChatSessionsByStatusRequest
            {
                Status = status
            }
        });
        
        return StatusCode(commandResult.StatusCode, commandResult);
    }    
    
    /// <summary>
    /// This method mimics the 'MarkChatSessionsAsClosedRecurringJob'. Send in a status of InProgress to mark chat sessions without 3 polling requests as Closed or send a status of Pending to mark chats older than 24 hours as inactive
    /// </summary>
    /// <response code="400">Bad request</response>
    /// <response code="422">Validation error</response>
    /// <response code="500">Internal server error</response>
    [HttpPatch]
    [Route(ApiRoutes.ChatSession.MarkAsClosed)]
    public async Task<IActionResult> MarkChatSessionsAsClosedAsync([FromBody]ChatSessionStatus status)
    {
        var commandResult = await _mediator.Send(new MarkChatSessionsAsClosedCommand
        {
            MarkChatSessionsAsClosedRequest = new MarkChatSessionsAsClosedRequest
            {
                Status = status
            }
        });
        
        return StatusCode(commandResult.StatusCode, commandResult);
    }
}
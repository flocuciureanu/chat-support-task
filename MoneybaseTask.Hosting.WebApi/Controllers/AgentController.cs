// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="AgentController.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneybaseTask.Business.Entities.Entities.Agent.Requests;
using MoneybaseTask.Business.Services.Features.Agent.Commands.Create;
using MoneybaseTask.Hosting.WebApi.Contracts.V1;

namespace MoneybaseTask.Hosting.WebApi.Controllers;

/// <summary>
/// This is the ChatSession Controller
/// </summary>
public class AgentController : MoneybaseController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// This is the Agent Controller constructor
    /// </summary>
    public AgentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new agent
    /// </summary>
    /// <response code="400">Bad request</response>
    /// <response code="422">Validation error</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [Route(ApiRoutes.Agent.Create)]
    public async Task<IActionResult> CreateAgentAsync([FromBody] CreateAgentRequest request)
    {
        var commandResult = await _mediator.Send(new CreateAgentCommand()
        {
            CreateAgentRequest = request
        });

        return StatusCode(commandResult.StatusCode, commandResult);
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateAgentCommandHandler.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using MoneybaseTask.Business.Services.Features.Agent.Builders;
using MoneybaseTask.Business.Services.Features.Agent.Services;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.Agent.Commands.Create;

public class CreateAgentCommandHandler : ICommandHandler<CreateAgentCommand, ICommandResult>
{
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly IAgentModelBuilder _agentModelBuilder;
    private readonly IAgentService _agentService;

    public CreateAgentCommandHandler(ICommandResultFactory commandResultFactory, 
        IAgentModelBuilder agentModelBuilder, 
        IAgentService agentService)
    {
        _commandResultFactory = commandResultFactory;
        _agentModelBuilder = agentModelBuilder;
        _agentService = agentService;
    }

    public async Task<ICommandResult> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
    {
        var agentToInsert = _agentModelBuilder
            .AddName(request.CreateAgentRequest.Name)
            .AddSeniority(request.CreateAgentRequest.Seniority)
            .AddWorkingShift(request.CreateAgentRequest.WorkingShift)
            .AddOverFlowTeamFlag(request.CreateAgentRequest.IsPartOfOverFlowTeam)
            .Build();

        await _agentService.InsertOneAsync(agentToInsert);
        
        return _commandResultFactory.Create(true, StatusCodes.Status201Created);
    }
}
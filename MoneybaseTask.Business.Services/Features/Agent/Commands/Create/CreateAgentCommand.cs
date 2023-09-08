// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateAgentCommand.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.Agent.Requests;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.Agent.Commands.Create;

public record CreateAgentCommand : ICommand<ICommandResult>
{
    public CreateAgentRequest CreateAgentRequest { get; set; }
}
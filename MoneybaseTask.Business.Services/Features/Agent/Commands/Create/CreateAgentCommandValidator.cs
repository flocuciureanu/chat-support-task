// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateAgentCommandValidator.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;

namespace MoneybaseTask.Business.Services.Features.Agent.Commands.Create;

public class CreateAgentCommandValidator : AbstractValidator<CreateAgentCommand>
{
    public CreateAgentCommandValidator()
    {
        RuleFor(x => x.CreateAgentRequest.Name).NotEmpty();
        RuleFor(x => x.CreateAgentRequest.Seniority).IsInEnum();
        RuleFor(x => x.CreateAgentRequest.WorkingShift).IsInEnum();
    }
}
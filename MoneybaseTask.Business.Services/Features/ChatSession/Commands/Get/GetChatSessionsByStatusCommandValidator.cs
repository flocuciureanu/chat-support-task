// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="GetChatSessionsByStatusCommandValidator.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Get;

public class GetChatSessionsByStatusCommandValidator : AbstractValidator<GetChatSessionsByStatusCommand>
{
    public GetChatSessionsByStatusCommandValidator()
    {
        RuleFor(x => x.GetChatSessionsByStatusRequest.Status).NotEmpty().IsInEnum();
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="PollChatSessionCommandValidator.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Poll;

public class PollChatSessionCommandValidator : AbstractValidator<PollChatSessionCommand>
{
    public PollChatSessionCommandValidator()
    {
        RuleFor(x => x.Request.Id).NotEmpty();
    }
}
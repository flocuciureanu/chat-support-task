// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MarkChatSessionAsCompletedCommandValidator.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Status;

public class MarkChatSessionAsCompletedCommandValidator : AbstractValidator<MarkChatSessionAsCompletedCommand>
{
    public MarkChatSessionAsCompletedCommandValidator()
    {
        RuleFor(x => x.Request.Id).NotEmpty();
        RuleFor(x => x.Request.Notes).NotEmpty();
    }
}
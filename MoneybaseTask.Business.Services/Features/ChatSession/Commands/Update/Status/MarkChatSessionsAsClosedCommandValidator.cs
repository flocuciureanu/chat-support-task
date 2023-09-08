// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MarkChatSessionsAsClosedCommandValidator.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Status;

public class MarkChatSessionsAsClosedCommandValidator : AbstractValidator<MarkChatSessionsAsClosedCommand>
{
    public MarkChatSessionsAsClosedCommandValidator()
    {
        RuleFor(x => x.MarkChatSessionsAsClosedRequest.Status).NotNull().IsInEnum();
    }
}
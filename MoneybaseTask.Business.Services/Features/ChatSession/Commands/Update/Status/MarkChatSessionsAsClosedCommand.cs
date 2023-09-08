// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MarkChatSessionsAsClosedCommand.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.ChatSession.Requests;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Status;

public record MarkChatSessionsAsClosedCommand : ICommand<ICommandResult>
{
    public MarkChatSessionsAsClosedRequest MarkChatSessionsAsClosedRequest { get; set; }
}
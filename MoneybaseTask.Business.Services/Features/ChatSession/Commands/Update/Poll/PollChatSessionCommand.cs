// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="PollChatSessionCommand.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.ChatSession.Requests;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Update.Poll;

public record PollChatSessionCommand : ICommand<ICommandResult>
{
    public PollChatSessionRequest Request { get; set; }
}
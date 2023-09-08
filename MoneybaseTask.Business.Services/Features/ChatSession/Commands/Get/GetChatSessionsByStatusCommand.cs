// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="GetChatSessionsByStatusCommand.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.ChatSession.Requests;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Get;

public record GetChatSessionsByStatusCommand : IQuery<ICommandResult>
{
    public GetChatSessionsByStatusRequest GetChatSessionsByStatusRequest { get; set; }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateChatSessionCommand.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.ChatSession.Requests;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Create;

public record CreateChatSessionCommand : ICommand<ICommandResult>
{
    public CreateChatSessionRequest Request { get; set; }
}
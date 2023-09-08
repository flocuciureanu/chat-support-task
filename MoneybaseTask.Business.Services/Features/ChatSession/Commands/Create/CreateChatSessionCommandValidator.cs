// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateChatSessionCommandValidator.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;
using MoneybaseTask.Common.Core.ExtensionMethods;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Commands.Create;

public class CreateChatSessionCommandValidator : AbstractValidator<CreateChatSessionCommand>
{
    public CreateChatSessionCommandValidator()
    {
        RuleFor(x => x.Request.FirstName).NotEmpty();
        RuleFor(x => x.Request.LastName).NotEmpty();
        RuleFor(x => x.Request.EmailAddress).NotEmpty().Must(ValidateEmailAddress);
    }

    private static bool ValidateEmailAddress(string emailAddress)
    {
        return emailAddress.IsValidEmailAddress();
    }
}
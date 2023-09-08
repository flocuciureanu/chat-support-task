// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateChatSessionRequest.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Requests;

public class CreateChatSessionRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
}
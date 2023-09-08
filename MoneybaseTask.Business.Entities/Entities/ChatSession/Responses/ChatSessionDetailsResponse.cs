// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionDetailsResponse.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;

public class ChatSessionDetailsResponse
{
    public string ChatId { get; set; }
    public string CreatedOn { get; set; }
    public string Status { get; set; }
    public string StatusUpdatedOn { get; set; }   
    public string StatusNotes { get; set; }   
}
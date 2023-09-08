// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="GetChatSessionsByStatusResponse.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;

public class GetChatSessionsByStatusResponse
{
    public GetChatSessionsByStatusResponse(ICollection<GetChatSessionsByStatusResponseItem> items)
    {
        Count = items.Count;
        Items = items;
    }
    
    public int Count { get; set; }
    public IEnumerable<GetChatSessionsByStatusResponseItem> Items { get; set; }
}
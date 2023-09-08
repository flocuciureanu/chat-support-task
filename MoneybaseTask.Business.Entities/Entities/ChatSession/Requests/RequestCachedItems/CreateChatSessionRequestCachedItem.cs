// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateChatSessionRequestCachedItem.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Requests.RequestCachedItems;

public class CreateChatSessionRequestCachedItem
{
    public CreateChatSessionRequestCachedItem(bool triggerOverFlowTeam)
    {
        TriggerOverFlowTeam = triggerOverFlowTeam;
    }
    
    public bool TriggerOverFlowTeam { get; set; }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="AgentDetailsResponse.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;

public class AgentDetailsResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Seniority { get; set; }    
    public string WorkingShift { get; set; }
    public bool IsPartOfOverFlowTeam { get; set; }
}
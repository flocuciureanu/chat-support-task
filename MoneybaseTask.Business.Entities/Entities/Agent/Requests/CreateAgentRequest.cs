// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateAgentRequest.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.CommonMongoDocuments;

namespace MoneybaseTask.Business.Entities.Entities.Agent.Requests;

public class CreateAgentRequest
{
    public string Name { get; set; }
    public AgentSeniority Seniority { get; set; }    
    public Shift WorkingShift { get; set; }
    public bool IsPartOfOverFlowTeam { get; set; }
}
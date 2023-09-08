// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CapacityDetailsModel.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.AgentChatCoordinator;

public class CapacityDetailsModel
{
    public CapacityDetailsModel(bool hasCapacity, 
        bool triggerOverFlowTeam = false,
        string agentId = null, 
        string agentName = null)
    {
        HasCapacity = hasCapacity;
        TriggerOverFlowTeam = triggerOverFlowTeam;
        AgentName = agentName;
        AgentId = agentId;
    }

    public bool HasCapacity { get; set; }
    public bool TriggerOverFlowTeam { get; set; }
    public string AgentId { get; set; }
    public string AgentName { get; set; }
}
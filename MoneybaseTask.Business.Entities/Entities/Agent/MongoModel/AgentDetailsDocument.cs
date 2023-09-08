// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="AgentDetailsDocument.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.CommonMongoDocuments;

namespace MoneybaseTask.Business.Entities.Entities.Agent.MongoModel;

public class AgentDetailsDocument
{
    public string Name { get; set; }
    public AgentSeniority Seniority { get; set; }    
    public Shift WorkingShift { get; set; }
    public bool IsPartOfOverFlowTeam { get; set; }
}
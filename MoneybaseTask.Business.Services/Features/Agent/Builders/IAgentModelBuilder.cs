// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IAgentModelBuilder.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.Agent.MongoModel;
using MoneybaseTask.Common.Core.Common.CommonMongoDocuments;

namespace MoneybaseTask.Business.Services.Features.Agent.Builders;

public interface IAgentModelBuilder
{
    IAgentModelBuilder AddName(string name);
    IAgentModelBuilder AddSeniority(AgentSeniority seniority);
    IAgentModelBuilder AddWorkingShift(Shift shift);
    IAgentModelBuilder AddOverFlowTeamFlag(bool isPartOfOverFlowTeam);
    AgentCollection Build();
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="AgentModelBuilder.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.Agent.MongoModel;
using MoneybaseTask.Common.Core.Common.CommonMongoDocuments;
using MoneybaseTask.Common.Core.Infrastructure.Builder.CollectionBuilder;
using MongoDB.Bson;

namespace MoneybaseTask.Business.Services.Features.Agent.Builders;

public class AgentModelBuilder : CollectionBuilder<AgentCollection>, IAgentModelBuilder
{
    public AgentModelBuilder()
    {
        Reset();
    }

    public IAgentModelBuilder AddName(string name)
    {
        this.Collection.Details.Name = name;
        return this;
    }

    public IAgentModelBuilder AddSeniority(AgentSeniority seniority)
    {
        this.Collection.Details.Seniority = seniority;
        return this;
    }

    public IAgentModelBuilder AddWorkingShift(Shift shift)
    {
        this.Collection.Details.WorkingShift = shift;
        return this;
    }

    public IAgentModelBuilder AddOverFlowTeamFlag(bool isPartOfOverFlowTeam)
    {
        this.Collection.Details.IsPartOfOverFlowTeam = isPartOfOverFlowTeam;
        return this;
    }
    
    protected sealed override void Reset()
    {
        this.Collection = new AgentCollection
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Details = new AgentDetailsDocument()
        };
    }
}

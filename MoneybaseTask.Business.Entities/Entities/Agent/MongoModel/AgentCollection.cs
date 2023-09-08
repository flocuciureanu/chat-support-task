// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="AgentCollection.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Infrastructure.Persistence;

namespace MoneybaseTask.Business.Entities.Entities.Agent.MongoModel;

public class AgentCollection : BaseMongoCollection
{
    public AgentDetailsDocument Details { get; set; }
}
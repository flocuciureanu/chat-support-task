// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="AgentSeniorityHelper.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.CommonMongoDocuments;

namespace MoneybaseTask.Common.Core.Common.Helpers;

public static class AgentSeniorityHelper
{    
    public static double GetSeniorityMultiplier(AgentSeniority seniority)
    {
        return seniority switch
        {
            AgentSeniority.Junior => 0.4,
            AgentSeniority.MidLevel => 0.6,
            AgentSeniority.Senior => 0.8,
            AgentSeniority.TeamLead => 0.5,
            _ => 0
        };
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="GetChatSessionsByStatusResponseItemAsyncBuilder.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Responses.Builders;
using MoneybaseTask.Business.Services.Features.Agent.Services;
using MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;
using MoneybaseTask.Common.Core.Infrastructure.Factory.Mapper;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Mapper.Builders;

public class GetChatSessionsByStatusResponseItemAsyncBuilder : MoneybaseAsyncMapper<ChatSessionCollection, GetChatSessionsByStatusResponseItem>
{
    private readonly IAgentService _agentService;
    
    public GetChatSessionsByStatusResponseItemAsyncBuilder(IMapper mapper, 
        IMappableFactory<GetChatSessionsByStatusResponseItem> mappableFactory, 
        IAgentService agentService) 
        : base(mapper, mappableFactory)
    {
        _agentService = agentService;
    }

    private async Task<GetChatSessionsByStatusResponseItemAsyncBuilder> AddAgentDetailsTask()
    {
        var agentDetails = new AgentDetailsResponse();

        if (string.IsNullOrEmpty(Source.Details.AgentId))
        {
            AddMappingOptions(new KeyValuePair<string, object>(ChatSessionMappingOptionsConstants.GetChatSessionsByStatusResponse.AgentDetailsResponse, agentDetails));
            return this;
        }
        
        agentDetails.Id = Source.Details.AgentId;
        var agent = await _agentService.GetByIdAsync(Source.Details.AgentId);
        if (agent is null)
        {
            AddMappingOptions(new KeyValuePair<string, object>(ChatSessionMappingOptionsConstants.GetChatSessionsByStatusResponse.AgentDetailsResponse, agentDetails));
            return this;
        }
        
        agentDetails.Name = agent.Details.Name;
        agentDetails.Seniority = agent.Details.Seniority.ToString("G");
        agentDetails.WorkingShift = agent.Details.WorkingShift.ToString("G");
        agentDetails.IsPartOfOverFlowTeam = agent.Details.IsPartOfOverFlowTeam;
        
        AddMappingOptions(new KeyValuePair<string, object>(ChatSessionMappingOptionsConstants.GetChatSessionsByStatusResponse.AgentDetailsResponse, agentDetails));

        return this;
    }

    protected override void GenerateMappingOptionValues()
    {
        TaskList.AddRange(new List<Task>
        {
            AddAgentDetailsTask()
        });
    }
}

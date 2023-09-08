// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionService.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.Agent.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Common.Core.Infrastructure.Persistence;
using MongoDB.Driver;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Services;

public class ChatSessionService : IChatSessionService
{
    private readonly IDatabaseRepository<ChatSessionCollection> _chatSessionRepository;

    public ChatSessionService(IDatabaseRepository<ChatSessionCollection> chatSessionRepository)
    {
        _chatSessionRepository = chatSessionRepository;
    }

    public Task InsertOneAsync(ChatSessionCollection chatSession)
        => _chatSessionRepository.InsertOneAsync(chatSession);

    public Task<List<ChatSessionCollection>> GetAllAsync()
        => _chatSessionRepository.GetAllAsync();

    public Task<ChatSessionCollection> GetOneAsync(FilterDefinition<ChatSessionCollection> filter)
        => _chatSessionRepository.GetOneAsync(filter);

    public Task<List<ChatSessionCollection>> GetManyAsync(FilterDefinition<ChatSessionCollection> filter)
        => _chatSessionRepository.GetManyAsync(filter);

    public async Task<List<ChatSessionCollection>> GetChatSessionsByStatus(ChatSessionStatus status)
    {
        var filter = Builders<ChatSessionCollection>.Filter.Where(x => x.StatusHistory.Last().Status.Equals(status));
        var chatSessions = await GetManyAsync(filter);

        return chatSessions;
    }

    public Task FindOneAndUpdateAsync(
        FilterDefinition<ChatSessionCollection> filter,
        UpdateDefinition<ChatSessionCollection> update)
        => _chatSessionRepository.FindOneAndUpdateAsync(filter, update);

    public Task<ChatSessionCollection> GetByIdAsync(string id)
        => _chatSessionRepository.GetByIdAsync(id);

    public Task BulkWriteAsync(IEnumerable<WriteModel<ChatSessionCollection>> bulkWriteOperations)
        => _chatSessionRepository.BulkWriteAsync(bulkWriteOperations);

    public async Task<int> GetInProgressAssignedChatsCountByAgentIdAsync(string agentId)
    {
        var filter = Builders<ChatSessionCollection>.Filter.Where(x =>
            x.Details.AgentId.Equals(agentId) && 
            x.StatusHistory.Last().Status.Equals(ChatSessionStatus.InProgress));
        
        var chatSessions = await GetManyAsync(filter);

        return chatSessions.Count;
    }

    public async Task<ChatSessionCollection> GetNextPendingChatSession()
    {
        var pendingChatSessions = await GetChatSessionsByStatus(ChatSessionStatus.Pending);
        
        return pendingChatSessions.MinBy(x => x.CreatedOn);
    }

    public async Task<List<ChatSessionCollection>> GetChatSessionsByAgentsAndStatusAsync(IEnumerable<string> agentIds, ChatSessionStatus status)
    {
        var chatSessionAgentIdFilter = Builders<ChatSessionCollection>.Filter.In(x =>
            x.Details.AgentId, agentIds);
        var chatSessionStatusFilter = Builders<ChatSessionCollection>.Filter.Where(x =>
            x.StatusHistory.Last().Status.Equals(status));
        
        var filter = Builders<ChatSessionCollection>.Filter.And(chatSessionAgentIdFilter, chatSessionStatusFilter);
        
        var chatSessions = await GetManyAsync(filter);

        return chatSessions;
    }

    public async Task AssignAgentToNextPendingChatSession(AgentCollection agent, ChatSessionCollection nextPendingChatSession = null)
    {
        nextPendingChatSession ??= await GetNextPendingChatSession();
        
        if (nextPendingChatSession is null)
            return;
        
        var updateList = new List<UpdateDefinition<ChatSessionCollection>>()
        {
            Builders<ChatSessionCollection>.Update.Set(x => x.Details.AgentId, agent.Id),
            Builders<ChatSessionCollection>.Update.Push(x => x.StatusHistory, new ChatSessionStatusDocument()
            {
                Status = ChatSessionStatus.InProgress,
                UpdatedOn = DateTime.UtcNow,
                Notes = $"Chat session assigned to: {agent.Id} at: {DateTime.UtcNow} and marked as: {ChatSessionStatus.InProgress}"
            }),
            Builders<ChatSessionCollection>.Update.Push(x => x.Details.Messages, new ChatSessionMessageDocument()
            {
                Content = $"Hi {nextPendingChatSession.Details.UserDetails.FirstName}! You are now talking to one of our experts, {agent.Details.Name}",
                Type = ChatMessageType.System,
                SentAt = DateTime.UtcNow
            })
        };
        var filter = Builders<ChatSessionCollection>.Filter.Where(x => x.Id.Equals(nextPendingChatSession.Id));
        var update = Builders<ChatSessionCollection>.Update.Combine(updateList);

        await FindOneAndUpdateAsync(filter, update);
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IChatSessionService.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.Agent.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MongoDB.Driver;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Services;

public interface IChatSessionService
{
    Task InsertOneAsync(ChatSessionCollection chatSession);
    Task<List<ChatSessionCollection>> GetAllAsync();
    Task<ChatSessionCollection> GetOneAsync(FilterDefinition<ChatSessionCollection> filter);
    Task<List<ChatSessionCollection>> GetManyAsync(FilterDefinition<ChatSessionCollection> filter);
    Task<List<ChatSessionCollection>> GetChatSessionsByStatus(ChatSessionStatus status);
    Task FindOneAndUpdateAsync(FilterDefinition<ChatSessionCollection> filter, UpdateDefinition<ChatSessionCollection> update);
    Task<ChatSessionCollection> GetByIdAsync(string id);
    Task BulkWriteAsync(IEnumerable<WriteModel<ChatSessionCollection>> bulkWriteOperations);
    Task<int> GetInProgressAssignedChatsCountByAgentIdAsync(string agentId);
    Task<ChatSessionCollection> GetNextPendingChatSession();
    Task<List<ChatSessionCollection>> GetChatSessionsByAgentsAndStatusAsync(IEnumerable<string> agentIds, ChatSessionStatus status);
    Task AssignAgentToNextPendingChatSession(AgentCollection agent, ChatSessionCollection nextPendingChatSession = null);
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ApiRoutes.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Hosting.WebApi.Contracts.V1;

internal static class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";
    private const string Base = $"{Root}/{Version}";

    internal static class Agent
    {
        private const string AgentBase = $"{Base}/agents";
        
        public const string Create = AgentBase;
    }
    
    internal static class ChatSession
    {
        private const string ChatSessionBase = $"{Base}/chat-sessions";
        
        public const string Create = ChatSessionBase;
        public const string Poll = $"{ChatSessionBase}/{{id}}/poll";
        public const string UpdateStatusAsCompleted = $"{ChatSessionBase}/{{id}}/completed";
        public const string GetByStatus = $"{ChatSessionBase}/status";
        public const string MarkAsClosed = $"{ChatSessionBase}/mark-as-closed";
    }
}

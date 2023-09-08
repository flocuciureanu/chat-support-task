// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CustomServicesInstaller.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Services.Features.Agent.Builders;
using MoneybaseTask.Business.Services.Features.Agent.Services;
using MoneybaseTask.Business.Services.Features.AgentChatCoordinator.Services;
using MoneybaseTask.Business.Services.Features.ChatSession.Builders;
using MoneybaseTask.Business.Services.Features.ChatSession.Services;
using MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;
using MoneybaseTask.Common.Core.Infrastructure.Factory.Mapper;
using MoneybaseTask.Common.Core.Infrastructure.Persistence;
using MoneybaseTask.Common.Core.Infrastructure.Serialization;

namespace MoneybaseTask.Hosting.WebApi.Installers;

internal class CustomServicesInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IJsonSerializer, JsonSerializer>();
        services.AddTransient(typeof(IDatabaseRepository<>), typeof(DatabaseRepository<>));

        //ChatSessions
        services.AddTransient<IChatSessionModelBuilder, ChatSessionModelBuilder>();
        services.AddTransient<IChatSessionService, ChatSessionService>();
        //Agents
        services.AddTransient<IAgentModelBuilder, AgentModelBuilder>();
        services.AddTransient<IAgentService, AgentService>();
        //AgentChatCoordinator
        services.AddTransient<IAgentChatCoordinatorService, AgentChatCoordinatorService>();
        
        //CommandBus
        services.AddTransient<ICommandResultFactory, CommandResultFactory>();

        //Mapper
        services.AddTransient(typeof(IMoneybaseMapper<>), typeof(MoneybaseMapper<>));
        services.AddTransient(typeof(IMappableFactory<>), typeof(MappableFactory<>));
        
        services.AddScoped<CommandCachedItems>();
    }
}
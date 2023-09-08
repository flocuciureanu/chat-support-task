// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MiddlewareInstaller.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Hosting.WebApi.Middleware;

namespace MoneybaseTask.Hosting.WebApi.Installers;

internal class MiddlewareInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
    }
}
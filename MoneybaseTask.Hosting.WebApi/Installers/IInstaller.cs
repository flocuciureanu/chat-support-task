// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IInstaller.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Hosting.WebApi.Installers;

/// <summary>
/// This is the IInstaller interface
/// </summary>
public interface IInstaller
{
    /// <summary>
    /// This is the InstallServices method
    /// </summary>
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}   
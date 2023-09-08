// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MongoInstaller.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Security.Authentication;
using Microsoft.Extensions.Options;
using MoneybaseTask.Common.Core.Settings.DataAccess;
using MongoDB.Driver;

namespace MoneybaseTask.Hosting.WebApi.Installers;

internal class MongoInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

        var databaseSettings = new DatabaseSettings();
        configuration.GetSection(nameof(DatabaseSettings)).Bind(databaseSettings);

        var connectionStringsValue = databaseSettings.ConnectionString;
        var databaseNameValue = databaseSettings.DatabaseName;

        var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionStringsValue));
        settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
        var mongoClient = new MongoClient(settings);
        var database = mongoClient.GetDatabase(databaseNameValue);

        services.AddScoped(_ => database);

        services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
    }
}
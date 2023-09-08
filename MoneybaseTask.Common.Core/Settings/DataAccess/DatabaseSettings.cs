// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="DatabaseSettings.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Settings.DataAccess;

public class DatabaseSettings : IAppSettings, IDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public DatabaseCollections DatabaseCollections { get; set; }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IDatabaseSettings.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Settings.DataAccess;

public interface IDatabaseSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    DatabaseCollections DatabaseCollections { get; set; }
}
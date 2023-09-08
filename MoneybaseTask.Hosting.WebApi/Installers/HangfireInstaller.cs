// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="HangfireInstaller.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MoneybaseTask.Common.Core.Settings.Hangfire;

namespace MoneybaseTask.Hosting.WebApi.Installers;

internal class HangfireInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var hangfireSettings = new HangfireSettings();
        configuration.GetSection(nameof(HangfireSettings)).Bind(hangfireSettings);

        var connectionStrings = hangfireSettings.ConnectionString;
        var databaseName = hangfireSettings.DatabaseName;
        
        var options = new MongoStorageOptions
        {
            MigrationOptions = new MongoMigrationOptions
            {
                MigrationStrategy = new DropMongoMigrationStrategy(),
                BackupStrategy = new CollectionMongoBackupStrategy()
            },
            CheckConnection = false
        };
        services.AddHangfire(config =>
        {
            config.UseMongoStorage(connectionStrings, databaseName, options);
            
        });
        GlobalConfiguration.Configuration.UseMongoStorage(connectionStrings, databaseName, options);
        
        services.AddHangfireServer();

        //Recurring jobs
        //This job deals with chat sessions that have not received the minimum of 3 polling requests
        // RecurringJob.AddOrUpdate<MarkChatSessionsAsClosedRecurringJob>(job => job.Execute(ChatSessionStatus.InProgress), Cron.Minutely);
        
        //This job deals with chat sessions that have been in a pending state for more than 24 hours
        // RecurringJob.AddOrUpdate<MarkChatSessionsAsClosedRecurringJob>(job => job.Execute(ChatSessionStatus.Pending), Cron.Minutely);
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="BaseRecurringJob.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Infrastructure.CommandBus;

namespace MoneybaseTask.Hosting.WebApi.RecurringJobs;

internal abstract class BaseRecurringJob
{
    private readonly ILogger<BaseRecurringJob> _logger;

    protected BaseRecurringJob(ILogger<BaseRecurringJob> logger)
    {
        _logger = logger;
    }

    protected void HandleCommandResult(ICommandResult commandResult)
    {
        if (commandResult is null)
        {
            _logger.LogError($"Error! Null command result");
            return;
        }

        if (!commandResult.Success)
        {
            _logger.LogError("{Message}", commandResult.NotificationMessage);
        }

        _logger.LogInformation("Status code: {StatusCode}. Notification message: {NotificationMessage}", commandResult.StatusCode, commandResult.NotificationMessage);
    }
}
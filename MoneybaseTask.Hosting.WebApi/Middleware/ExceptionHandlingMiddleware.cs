// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ExceptionHandlingMiddleware.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Net;
using MoneybaseTask.Common.Core.Common.ErrorObject;
using MoneybaseTask.Common.Core.Common.Exceptions;
using MoneybaseTask.Common.Core.Infrastructure.CommandBus;
using MoneybaseTask.Common.Core.Infrastructure.Serialization;
using ApplicationException = MoneybaseTask.Common.Core.Common.Exceptions.ApplicationException;

namespace MoneybaseTask.Hosting.WebApi.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly IJsonSerializer _jsonSerializer;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger,
        ICommandResultFactory commandResultFactory, 
        IJsonSerializer jsonSerializer)
    {
        _logger = logger;
        _commandResultFactory = commandResultFactory;
        _jsonSerializer = jsonSerializer;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var commandResult = _commandResultFactory.Create(false, statusCode, exception.Message, GetErrors(exception));

        if (!httpContext.Response.HasStarted)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = statusCode;
        }

        await httpContext.Response.WriteAsync(_jsonSerializer.Serialize(commandResult));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            HttpRequestException requestException => (int)(requestException.StatusCode ?? HttpStatusCode.InternalServerError),
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static List<ErrorItem> GetErrors(Exception exception)
    {
        var errors = new List<ErrorItem>();

        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors.ToList();
        }

        return errors;
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MoneybaseController.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;

namespace MoneybaseTask.Hosting.WebApi.Controllers;

[Consumes("application/json")]
[Produces("application/json")]
// [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ICommandResult))]
// [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestSwaggerResponse))]
// [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ICommandResult))]
// [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedSwaggerResponse))]
[ApiController]
public class MoneybaseController : ControllerBase
{
    
}
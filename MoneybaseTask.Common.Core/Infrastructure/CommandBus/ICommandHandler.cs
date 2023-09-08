// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ICommandHandler.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MediatR;

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : IRequest<TResponse>
{
}
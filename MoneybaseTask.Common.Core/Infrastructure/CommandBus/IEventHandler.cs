// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IEventHandler.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MediatR.Pipeline;

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public interface IEventHandler<in TCommand, in TResponse> : IRequestPostProcessor<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
}
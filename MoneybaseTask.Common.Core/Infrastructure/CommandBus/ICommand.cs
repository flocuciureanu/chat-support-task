// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ICommand.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MediatR;

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
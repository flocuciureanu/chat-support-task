// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IQuery.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MediatR;

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
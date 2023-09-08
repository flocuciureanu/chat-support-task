// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IQueryHandler.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MediatR;

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IDatabaseRepository.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MongoDB.Driver;

namespace MoneybaseTask.Common.Core.Infrastructure.Persistence;

public interface IDatabaseRepository<T> where T : IBaseMongoCollection
{
    Task InsertOneAsync(T item);
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task<T> GetOneAsync(FilterDefinition<T> where);
    Task<List<T>> GetManyAsync(FilterDefinition<T> where);
    Task FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);
    Task BulkWriteAsync(IEnumerable<WriteModel<T>> bulkWriteOperations);
    Task<DeleteResult> DeleteAsync(string id);
}
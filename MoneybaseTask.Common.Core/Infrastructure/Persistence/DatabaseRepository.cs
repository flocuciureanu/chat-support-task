// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="DatabaseRepository.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Settings.DataAccess;
using MongoDB.Driver;

namespace MoneybaseTask.Common.Core.Infrastructure.Persistence;

public class DatabaseRepository<T> : IDatabaseRepository<T> where T : BaseMongoCollection
{
    private readonly IMongoCollection<T> _collection;

    public DatabaseRepository(IDatabaseSettings settings, IMongoDatabase database)
    {
        var collectionName = GetCollectionName(typeof(T).Name, settings.DatabaseCollections);
        _collection = database.GetCollection<T>(collectionName);
    }

    private static string GetCollectionName(string name, DatabaseCollections databaseCollections)
    {
        return databaseCollections.GetType().GetProperties()
            .Where(propertyInfo => propertyInfo.Name.Contains(name))
            .Select(propertyInfo => propertyInfo.GetValue(databaseCollections, null)?.ToString())
            .FirstOrDefault();
    }

    public Task InsertOneAsync(T item)
        => _collection.InsertOneAsync(item);

    public Task<List<T>> GetAllAsync()
        => _collection.Find(Builders<T>.Filter.Empty).ToListAsync();

    public Task<T> GetByIdAsync(string id)
        => _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

    public Task<T> GetOneAsync(FilterDefinition<T> where)
        => _collection.Find(where).FirstOrDefaultAsync();

    public Task<List<T>> GetManyAsync(FilterDefinition<T> where)
        => _collection.Find(where).ToListAsync();

    public Task FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        => _collection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<T>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            });

    public Task BulkWriteAsync(IEnumerable<WriteModel<T>> bulkWriteOperations)
        => _collection.BulkWriteAsync(bulkWriteOperations);
    
    public Task<DeleteResult> DeleteAsync(string id)
        => _collection.DeleteOneAsync(x => x.Id.Equals(id));
}
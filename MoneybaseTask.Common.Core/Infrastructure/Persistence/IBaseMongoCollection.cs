// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IBaseMongoCollection.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoneybaseTask.Common.Core.Infrastructure.Persistence;

public interface IBaseMongoCollection : IBaseMongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    string Id { get; set; }
}
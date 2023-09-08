// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="BaseMongoCollection.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoneybaseTask.Common.Core.Infrastructure.Persistence;

public class BaseMongoCollection : IBaseMongoCollection
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}
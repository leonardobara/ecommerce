using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using Services.ProductCatalog.Api.Core.Entities.MongoContext;

namespace Services.ProductCatalog.Api.Core.Entities
{
    [BsonCollection("products")]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("date_added")]
        public DateTime Date_Added { get; set; }
        [BsonElement("product_code")]
        public string Product_Code { get; set; }
        [BsonElement("active")]
        public bool Active { get; set; }
        [BsonElement("price")]
        public double Price { get; set; }
    }
}

using MongoDB.Driver;

namespace Services.ProductCatalog.Api.Core.Entities.MongoContext
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}

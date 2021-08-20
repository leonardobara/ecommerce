using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ProductCatalog.Api.Core.Entities.MongoContext
{
    public class ProductContext : IProductContext
    {
        private readonly IMongoDatabase _db;
        public ProductContext(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Product> Products => _db.GetCollection<Product>("Product");
    }
}

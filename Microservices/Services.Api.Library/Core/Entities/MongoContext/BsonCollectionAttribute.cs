using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ProductCatalog.Api.Core.Entities.MongoContext
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}

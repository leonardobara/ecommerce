using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Services.ProductCatalog.Api.Core;
using Services.ProductCatalog.Api.Core.Entities;
using Services.ProductCatalog.Api.Core.Entities.MongoContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.ProductCatalog.Api.Repository
{
    public class Repository<TEntity> : IMongoRepository<TEntity> where TEntity : class
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<TEntity> _collection;
        public Repository(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
            _collection = _db.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
        //public IMongoCollection<Product> Products => _db.GetCollection<Product>("Product");
        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _collection.AsQueryable();
        }
        
        public IQueryable<TEntity> GetAll()
        {
            return _collection.AsQueryable<TEntity>();
        }

        public TEntity GetById(object id)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc, id);
            return _collection.Find(filter).SingleOrDefault();
        }

        public void Insert(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            _collection.InsertMany(entities);
        }
        public void Save()
        {
        }

        public void Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc, entity);
             _collection.FindOneAndReplace(filter, entity);
        }

        public Task InsertAsync(TEntity entity)
        {
            return Task.Run(() => _collection.InsertOneAsync(entity));
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc, entity);
            await _collection.FindOneAndReplaceAsync(filter, entity);
        }

        public async Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
           await _collection.InsertManyAsync(entities);
        }

        public async Task<PaginationEntity<TEntity>> PaginationBy(Expression<Func<TEntity, bool>> filterExpression, PaginationEntity<TEntity> pagination)
        {
            var sort = Builders<TEntity>.Sort.Ascending(pagination.Sort);

            if (pagination.SortDirection == "desc")
            {
                sort = Builders<TEntity>.Sort.Descending(pagination.Sort);
            }

            if (string.IsNullOrEmpty(pagination.Filter))
            {
                pagination.Data = await _collection.Find(x => true).Sort(sort)
                                             .Skip((pagination.Page - 1) * pagination.PageSize)
                                             .Limit(pagination.PageSize)
                                             .ToListAsync();
            }

            else 
            {
                pagination.Data = await _collection.Find(filterExpression).Sort(sort)
                                             .Skip((pagination.Page - 1) * pagination.PageSize)
                                             .Limit(pagination.PageSize)
                                             .ToListAsync();
            }

            long totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<TEntity>.Empty);

            pagination.PageQuantity = Convert.ToInt32((Math.Ceiling(Convert.ToDecimal(totalDocuments) / pagination.PageSize)));


            return pagination;
        }
    }
}

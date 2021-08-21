using Services.ProductCatalog.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.ProductCatalog.Api.Repository
{
    public interface IMongoRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> AsQueryable();
        IQueryable<TEntity> GetAll();
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void InsertMany(IEnumerable<TEntity> entities);
        void Save();
        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
        Task InsertManyAsync(IEnumerable<TEntity> entitie);

        //Expression => Sirve para definir un metodo o
        //una funcionalidad que a futuro sera implementado
        Task<PaginationEntity<TEntity>> PaginationBy(
            Expression<Func<TEntity, bool>> filterExpression,
            PaginationEntity<TEntity> pagination
         );

    }
}

using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageLibrary
{
    public interface INoSqlStorage<TEntity>
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Get(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        IQueryable<TEntity> GetAll();
        // productRepository.Query(p => p.Price == 100); delegate
        IQueryable<TEntity> Query(Expression<Func<TEntity,bool>> expression);
    }
}

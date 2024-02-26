using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureStorageLibrary.Services
{
    public class TableStorage<TEntity> : INoSqlStorage<TEntity> where TEntity : class, ITableEntity, new()
    {
        private readonly TableClient _tableClient;

        public TableStorage()
        {
            var serviceClient = new TableServiceClient(ConnectionStrings.AzureStorageConnectionString);
            _tableClient = serviceClient.GetTableClient(typeof(TEntity).Name);
            _tableClient.CreateIfNotExists();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var response = await _tableClient.AddEntityAsync(entity);
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            await _tableClient.DeleteEntityAsync(entity.PartitionKey,entity.RowKey);
        }

        public async Task<TEntity> Get(TEntity entity)
        {
            return await _tableClient.GetEntityAsync<TEntity>(entity.PartitionKey,entity.RowKey);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            var queryable = _tableClient.Query<TEntity>().AsQueryable();
            return queryable.Where(query);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            await _tableClient.UpdateEntityAsync(entity, ETag.All, TableUpdateMode.Replace);
            return entity;
        }

        //public async Task<AsyncPageable<TEntity>> GetAll()
        //{
        //    AsyncPageable<TEntity> queryResultsSelect = _tableClient.QueryAsync<TEntity>(maxPerPage: 10);
        //    await foreach (TEntity qEntity in queryResultsSelect)
        //    {
        //        Console.WriteLine(qEntity);
        //    }

        //    return queryResultsSelect;
        //}

        public IQueryable<TEntity> GetAll()
        {
            var deneme = _tableClient.Query<TEntity>().AsQueryable();
            return deneme;
        }
    }
}

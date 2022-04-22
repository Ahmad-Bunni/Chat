using ChatApp.Domain.Interface;
using ChatApp.Domain.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace ChatApp.Data.Repository;

public abstract class CosmosRepository<TEntity> : ICosmosRepository<TEntity>, IDisposable
        where TEntity : BaseEntity
{
    private readonly CosmosClient _cosmosClient;
    private readonly Container _container;

    public abstract string DatabaseId { get; }
    public abstract string ContainerId { get; }

    public CosmosRepository(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
        _container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
    }

    public async Task<TEntity> GetItemByIdAsync(string id)
    {
        var itemResponse = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));

        return itemResponse.Resource;
    }

    public async Task<IEnumerable<TEntity>> GetItemsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _container.GetItemLinqQueryable<TEntity>();

        var iterator = query.Where(predicate).ToFeedIterator();

        var results = await iterator.ReadNextAsync();

        return results;
    }

    public async Task<TEntity> AddItemAsync(TEntity item)
    {
        var itemResponse = await _container.CreateItemAsync(item, new PartitionKey(item.Id));

        return itemResponse.Resource;
    }

    public async Task<bool> DeleteItemAsync(string id)
    {
        var itemResponse = await _container.CreateItemAsync(id, new PartitionKey(id));

        return itemResponse.StatusCode is HttpStatusCode.NoContent;
    }

    public async Task<TEntity> UpsertItemAsync(TEntity item)
    {
        var itemResponse = await _container.UpsertItemAsync(item, new PartitionKey(item.Id));

        return itemResponse.Resource;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
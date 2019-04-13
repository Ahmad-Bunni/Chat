using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using ChatApp.Domain.Enum;
using ChatApp.Domain.Interface;
using ChatApp.Domain.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace ChatApp.Data.Repository
{
    public class CosmosRepository<T> : ICosmosRepository<T> where T : BaseEntity
    {
        private readonly IDocumentClient _cosmosClient;
        public CosmosRepository(IDocumentClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                Document document = await _cosmosClient.ReadDocumentAsync(UriFactory.CreateDocumentUri($"{Documents.ChatDB}", typeof(T).Name, id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<T> FindItemAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                FeedOptions options = new FeedOptions
                {
                    EnableCrossPartitionQuery = true,
                };

                IDocumentQuery<T> query = _cosmosClient.CreateDocumentQuery<T>(
             UriFactory.CreateDocumentCollectionUri($"{Documents.ChatDB}", typeof(T).Name), options)
             .Where(predicate)
             .AsDocumentQuery();

                if (query.HasMoreResults)
                {
                    var items = await query.ExecuteNextAsync<T>();

                    return items.FirstOrDefault();
                }
                else return null;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IDocumentQuery<T> query = _cosmosClient.CreateDocumentQuery<T>(
             UriFactory.CreateDocumentCollectionUri($"{Documents.ChatDB}", typeof(T).Name), new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
             .Where(predicate)
             .AsDocumentQuery();

                List<T> results = new List<T>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }

                return results;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Document> InsertItemAsync(T item)
        {
            try
            {
                return await _cosmosClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri($"{Documents.ChatDB}", typeof(T).Name), item);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RemoveItemAsync(string id, string username)
        {
            try
            {
                Uri documentUri = UriFactory.CreateDocumentUri($"{Documents.ChatDB}", typeof(T).Name, id);
                await _cosmosClient.DeleteDocumentAsync(documentUri, new RequestOptions { PartitionKey = new PartitionKey(username) });

            }
            catch (Exception ex)
            {

                throw;
            }

            return true;
        }

        public async Task<Document> UpdateItemAsync(T item)
        {
            try
            {
                return await _cosmosClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri($"{Documents.ChatDB}", typeof(T).Name, item.Id), item);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}


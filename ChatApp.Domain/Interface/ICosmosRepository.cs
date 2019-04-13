using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interface
{
    public interface ICosmosRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate);
        Task<Document> InsertItemAsync(T item);
        Task<bool> RemoveItemAsync(string id, string username);
        Task<Document> UpdateItemAsync(T item);
        Task<T> FindItemAsync(Expression<Func<T, bool>> predicate);

    }
}

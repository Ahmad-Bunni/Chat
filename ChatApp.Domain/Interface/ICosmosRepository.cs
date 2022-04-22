using ChatApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interface;

public interface ICosmosRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    Task<TEntity> GetItemByIdAsync(string id);
    Task<IEnumerable<TEntity>> GetItemsAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddItemAsync(TEntity item);
    Task<bool> DeleteItemAsync(string id);
    Task<TEntity> UpsertItemAsync(TEntity item);
}

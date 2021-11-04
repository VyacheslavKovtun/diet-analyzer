using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interfaces.Interfaces
{
    public interface IRepositoryAsync<TKey, TValue>
        where TKey: struct
        where TValue: class
    {
        Task CreateAsync(TValue value);
        Task<IEnumerable<TValue>> GetAllAsync();
        Task<TValue> GetAsync(TKey id);
        Task UpdateAsync(TValue value);
        Task DeleteAsync(TKey id);
    }
}

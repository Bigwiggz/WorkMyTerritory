using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        void InsertAsync(T obj);
        void UpdateAsync(T obj);
        void DeleteAsync(object id);
        void SaveAsync();
    }
}

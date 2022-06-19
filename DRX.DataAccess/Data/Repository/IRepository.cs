using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> InsertAsync(T value);
        Task<T> UpdateAsync(T value);
        Task<bool> DeleteAsync(T value);
        Task<T> SearchByIdAsync(int id);
        IEnumerable<T> GetAll();
    }
}

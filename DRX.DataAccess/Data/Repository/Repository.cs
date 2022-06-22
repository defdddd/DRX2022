using Dapper.Contrib.Extensions;
using DRX.DataAccess.SqlDataAcces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected ISqlDataAccess sqlDataAccess;
        public async Task<bool> DeleteAsync(T value)
        {
            using var connection = new SqlConnection(sqlDataAccess.Connection);

            return await connection.DeleteAsync(value);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = new SqlConnection(sqlDataAccess.Connection);

            return await connection.GetAllAsync<T>() ?? Enumerable.Empty<T>();
        }

        public IEnumerable<T> GetAll()
        {
            using var connection = new SqlConnection(sqlDataAccess.Connection);

            return connection.GetAll<T>() ?? Enumerable.Empty<T>();
        }


        public async Task<T> InsertAsync(T value)
        {
            using var connection = new SqlConnection(sqlDataAccess.Connection);

            var id =  await connection.InsertAsync(value);

            return await connection.GetAsync<T>(id);
        }

        public async Task<T> SearchByIdAsync(int id)
        {
            using var connection = new SqlConnection(sqlDataAccess.Connection);

            return await connection.GetAsync<T>(id);
        }

        public async Task<T> UpdateAsync(T value)
        {
            using var connection = new SqlConnection(sqlDataAccess.Connection);

            if(await connection.UpdateAsync(value))
                return value;

            return null;
        }
    }
}

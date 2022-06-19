using DRX.DataAccess.Data.DTOs;
using DRX.DataAccess.Data.Interfaces;
using DRX.DataAccess.Data.Repository;
using DRX.DataAccess.SqlDataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data
{
    public class UserRepository : Repository<UserDTO>, IUserRepository
    {
        public UserRepository(ISqlDataAccess sqlDataAccess)
        {
              this.sqlDataAccess = sqlDataAccess;
        }

        public async Task<UserDTO> SearchByEmailAsync(string email)
        {
            return (await GetAllAsync()).Where(x => x.Email == email).FirstOrDefault();
        }

        public async Task<UserDTO> SearchByUserNameAsync(string username)
        {
            return (await GetAllAsync()).Where(x => x.UserName == username).FirstOrDefault();
        }
    }
}

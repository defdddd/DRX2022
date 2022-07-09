using DRX.DataAccess.Data.DTOs;
using DRX.DataAccess.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data.Interfaces
{
    public interface IUserRepository : IRepository<UserDTO>
    {
        Task<UserDTO> SearchByUserNameAsync(string username);
        Task<UserDTO> SearchByEmailAsync(string email);
        Task<bool> CheckEmailAsync(string email);
    }
}

using DRX.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices.Interfaces
{
    public interface IUserService : IService<UserDTO>
    {
        Task<BilingDTO> GetMyBilingDataAsync(int userId);
        Task<UserDTO> SearchByUserNameAsync(string userName);
        Task<UserDTO> SearchByEmailAsync(string email);

    }
}

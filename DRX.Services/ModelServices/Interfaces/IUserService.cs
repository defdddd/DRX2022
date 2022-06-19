using DRX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices.Interfaces
{
    public interface IUserService : IService<UserData>
    {
        Task<BilingData> GetMyBilingDataAsync(int userId);
        Task<UserData> SearchByUserNameAsync(string userName);
        Task<UserData> SearchByEmailAsync(string email);

    }
}

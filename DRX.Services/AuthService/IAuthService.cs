using DRX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.AuthService
{
    public interface IAuthService
    {
        Task<dynamic> GenerateTokenAsync(AuthData authData);
        Task<bool> IsValidUserNameAndPassowrdAsync(AuthData authData);
        Task<UserData> RegisterAsync(UserData user);
    }
}

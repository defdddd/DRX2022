using DRX.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.AuthService
{
    public interface IAuthService
    {
        Task<dynamic> GenerateTokenAsync(AuthDTO authData);
        Task<bool> IsValidUserNameAndPassowrdAsync(AuthDTO authData);
        Task<UserDTO> RegisterAsync(UserDTO user);
    }
}

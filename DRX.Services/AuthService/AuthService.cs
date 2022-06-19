using AutoMapper;
using DRX.DataAccess.Data.DTOs;
using DRX.DataAccess.UnitOfWork;
using DRX.Models;
using DRX.Validators.ModelValidator;
using DRX.Validators.ToolValidator;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IRepositories _repostiories;
        private readonly string _myKey;
        private readonly IMapper _mapper;

        public AuthService(IRepositories repositories, string myKey, IMapper mapper)
        {
            _myKey = myKey;
            _repostiories = repositories;
            _mapper = mapper;

         }
        private async Task<bool> CheckEmailAsync(string email)
        {
            return await _repostiories.UserRepository.SearchByUserNameAsync(email) != null;

        }

        private async Task<bool> CheckUserNameAsync(string userName)
        {
            return await _repostiories.UserRepository.SearchByUserNameAsync(userName) != null;
        }

        public async Task<dynamic> GenerateTokenAsync(AuthData authData)
        {
            await IsValidUserNameAndPassowrdAsync(authData);
            var username = authData.UserName;
            var user = await _repostiories.UserRepository.SearchByUserNameAsync(username);

            var claims = new List<Claim>
            {
                new Claim("Identifier",user.Id + ""),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(7)).ToUnixTimeSeconds().ToString())
            };

            if (user.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            else
                claims.Add(new Claim(ClaimTypes.Role, "User"));

            var token = new JwtSecurityToken
                (
                   new JwtHeader
                    (
                        new SigningCredentials
                        (
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_myKey)),
                            SecurityAlgorithms.HmacSha256
                        )
                     ),
                    new JwtPayload(claims)
                );
            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                user.UserName
            };

            return output;
        }


        public async Task<bool> IsValidUserNameAndPassowrdAsync(AuthData authData)
        {
            var user = await _repostiories.UserRepository.SearchByUserNameAsync(authData.UserName) ??
                           throw new ValidationException("Invalid username or password");

            if (user.Password != authData.Password)
                throw new ValidationException("Invalid username or password");
            return true;
        }

        public async Task<bool> RegisterAsync(UserData user)
        {
            if (await CheckEmailAsync(user.Email))
                throw new ValidationException("Invalid email");

            if(await CheckUserNameAsync(user.UserName))
                throw new ValidationException("Invalid username");

            user.IsAdmin = false;

            var validator = new UserValidator();

            await ValidatorTool.FluentValidate(validator, user);

            var result = _mapper.Map<UserData, UserDTO>(user);

            return null != await _repostiories.UserRepository.InsertAsync(result);
        }
    }
}

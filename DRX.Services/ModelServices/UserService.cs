using AutoMapper;
using DRX.DataAccess.Data.DTOs;
using DRX.DataAccess.UnitOfWork;
using DRX.Models;
using DRX.Services.ModelServices.Interfaces;
using DRX.Validators.ToolValidator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices
{
    public class UserService : IUserService
    {
        private readonly IRepositories _repositories;
        private readonly IValidator<UserData> _validator;
        private readonly IMapper _mapper;

        public UserService(IRepositories repositories, IValidator<UserData> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(UserData value)
        {
            var userDTO = _mapper.Map<UserDTO>(value);

            return await _repositories.UserRepository.DeleteAsync(userDTO);
        }

        public async Task<IEnumerable<UserData>> GetAllAsync()
        {
            var users = await _repositories.UserRepository.GetAllAsync();

            if(!users.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<UserData>>(users);
        }

        public async Task<BilingData> GetMyBilingDataAsync(int userId)
        {
            var bilingDTO = await _repositories.BilingRepository.GetBilingByUserIdAsync(userId);

            return _mapper.Map<BilingData>(bilingDTO);
        }

        public async Task<UserData> InsertAsync(UserData value)
        {
            if(await _repositories.UserRepository.SearchByUserNameAsync(value.UserName) is not null)
                throw new ValidationException("User already exists");

            await ValidatorTool.FluentValidate(_validator, value);

            var userDTO = await _repositories.UserRepository.InsertAsync(_mapper.Map<UserDTO>(value));

            return _mapper.Map<UserData>(userDTO);

        }

        public async Task<UserData> SearchByEmailAsync(string email)
        {
            var userDTO = await _repositories.UserRepository.SearchByEmailAsync(email);

            return _mapper.Map<UserData>(userDTO);
        }

        public async Task<UserData> SearchByIdAsync(int id)
        {
            var userDTO = await _repositories.UserRepository.SearchByIdAsync(id);

            return _mapper.Map<UserData>(userDTO);
        }

        public async Task<UserData> SearchByUserNameAsync(string userName)
        {
            var userDTO = await _repositories.UserRepository.SearchByUserNameAsync(userName);

            return _mapper.Map<UserData>(userDTO);
        }

        public async Task<UserData> UpdateAsync(UserData value)
        {
            if (await _repositories.UserRepository.SearchByIdAsync(value.Id) is  null)
                throw new ValidationException("User does not exists");

            var userDTOSearch = await _repositories.UserRepository.SearchByUserNameAsync(value.UserName);

            if (userDTOSearch is not null && value.Id != userDTOSearch.Id)
                throw new ValidationException("This Username is taken");

            await ValidatorTool.FluentValidate(_validator, value);

            var userDTO = await _repositories.UserRepository.UpdateAsync(_mapper.Map<UserDTO>(value));

            return _mapper.Map<UserData>(userDTO);
        }
    }
}

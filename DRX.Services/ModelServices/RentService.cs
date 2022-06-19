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
    public class RentService : IRentService
    {
        private readonly IRepositories _repositories;
        private readonly IValidator<RentData> _validator;
        private readonly IMapper _mapper;

        public RentService(IRepositories repositories, IValidator<RentData> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(RentData value)
        {
            var rentDTO = _mapper.Map<RentDTO>(value);

            return await _repositories.RentRepository.DeleteAsync(rentDTO);
        }

        public async Task<IEnumerable<RentData>> GetAllAsync()
        {
            var rents = await _repositories.RentRepository.GetAllAsync();

            if (!rents.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<RentData>>(rents);
        }

        public async Task<IEnumerable<RentData>> GetMyRentsAsync(int userId)
        {
            var rentsDto = await _repositories.RentRepository.GetMyRentsAsync(userId);

            if (!rentsDto.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<RentData>>(rentsDto);
        }

        public async Task<RentData> InsertAsync(RentData value)
        {
            if (await _repositories.RentRepository.CheckRentAsync(value.VehicleId))
                throw new ValidationException("This vehicle is allready in use");

            await ValidatorTool.FluentValidate(_validator, value);

            var rentDTO = await _repositories.RentRepository.InsertAsync(_mapper.Map<RentDTO>(value));

            return _mapper.Map<RentData>(rentDTO);

        }

        public async Task<RentData> SearchByIdAsync(int id)
        {
            var RentDTO = await _repositories.RentRepository.SearchByIdAsync(id);

            return _mapper.Map<RentData>(RentDTO);
        }

        public async Task<RentData> UpdateAsync(RentData value)
        {
            if (await _repositories.RentRepository.SearchByIdAsync(value.Id) is null)
                throw new ValidationException("Rent does not exists");

            await ValidatorTool.FluentValidate(_validator, value);

            var rentDTO = await _repositories.RentRepository.UpdateAsync(_mapper.Map<RentDTO>(value));

            return _mapper.Map<RentData>(rentDTO);
        }
    }
}

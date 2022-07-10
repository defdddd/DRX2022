using AutoMapper;
using DRX.DataAccess.Data.Domains;
using DRX.DataAccess.UnitOfWork;
using DRX.DTOs;
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
        private readonly IValidator<RentDTO> _validator;
        private readonly IMapper _mapper;

        public RentService(IRepositories repositories, IValidator<RentDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(RentDTO value)
        {
            var rentDTO = _mapper.Map<Rent>(value);

            return await _repositories.RentRepository.DeleteAsync(rentDTO);
        }

        public async Task<IEnumerable<RentDTO>> GetAllAsync()
        {
            var rents = await _repositories.RentRepository.GetAllAsync();

            if (!rents.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<RentDTO>>(rents);
        }

        public async Task<IEnumerable<RentDTO>> GetMyRentsAsync(int userId)
        {
            var rentsDto = await _repositories.RentRepository.GetMyRentsAsync(userId);

            if (!rentsDto.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<RentDTO>>(rentsDto);
        }

        public async Task<RentDTO> InsertAsync(RentDTO value)
        {
            if (await _repositories.RentRepository.CheckRentAsync(value.VehicleId))
                throw new ValidationException("This vehicle is allready in use");

            await ValidatorTool.FluentValidate(_validator, value);

            var rentDTO = await _repositories.RentRepository.InsertAsync(_mapper.Map<Rent>(value));

            return _mapper.Map<RentDTO>(rentDTO);

        }

        public async Task<RentDTO> SearchByIdAsync(int id)
        {
            var RentDTO = await _repositories.RentRepository.SearchByIdAsync(id);

            return _mapper.Map<RentDTO>(RentDTO);
        }

        public async Task<RentDTO> UpdateAsync(RentDTO value)
        {
            if (await _repositories.RentRepository.SearchByIdAsync(value.Id) is null)
                throw new ValidationException("Rent does not exists");

            await ValidatorTool.FluentValidate(_validator, value);


            var vehicleDTO = await _repositories.VehicleRepository.SearchByIdAsync(value.VehicleId);

            vehicleDTO.Location = value.LastLocation;

            _ = await _repositories.VehicleRepository.UpdateAsync(vehicleDTO) ?? throw new Exception("Could not update the vehicle");

            var rentDTO = await _repositories.RentRepository.UpdateAsync(_mapper.Map<Rent>(value));

            return _mapper.Map<RentDTO>(rentDTO);
        }
    }
}

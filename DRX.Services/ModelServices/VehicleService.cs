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
    public class VehicleService : IVehicleService
    {
        private readonly IRepositories _repositories;
        private readonly IValidator<VehicleData> _validator;
        private readonly IMapper _mapper;

        public VehicleService(IRepositories repositories, IValidator<VehicleData> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(VehicleData value)
        {
            var vehicleDTO = _mapper.Map<VehicleDTO>(value);

            return await _repositories.VehicleRepository.DeleteAsync(vehicleDTO);
        }

        public async Task<IEnumerable<VehicleData>> GetAllAsync()
        {
            var Vehicles = await _repositories.VehicleRepository.GetAllAsync();

            if (!Vehicles.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<VehicleData>>(Vehicles);
        }

        public async Task<IEnumerable<VehicleData>> GetAllSearchByVehiclesAsync(string type, string model)
        {
            if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(model)) throw new Exception("Search options are null");

            var resultsDTO =  await _repositories.VehicleRepository.GetAllBySearchFieldAsync(type, model);

            return _mapper.Map<IEnumerable<VehicleData>>(resultsDTO);
        }

        public async Task<IEnumerable<VehicleData>> GetAvailableVehiclesAsync(string type, string model)
        {
            return (await GetAllAsync()).Where( x => !( _repositories.RentRepository.CheckRent(x.Id)) 
                                                && x.Model.Equals(model) && x.Type.Equals(type));
        }

        public async Task<VehicleData> InsertAsync(VehicleData value)
        {

            await ValidatorTool.FluentValidate(_validator, value);

            var vehicleDTO = await _repositories.VehicleRepository.InsertAsync(_mapper.Map<VehicleDTO>(value));

            return _mapper.Map<VehicleData>(vehicleDTO);

        }

        public async Task<VehicleData> SearchByIdAsync(int id)
        {
            var vehicleDTO = await _repositories.VehicleRepository.SearchByIdAsync(id);

            return _mapper.Map<VehicleData>(vehicleDTO);
        }

        public async Task<VehicleData> UpdateAsync(VehicleData value)
        {
            if (await _repositories.VehicleRepository.SearchByIdAsync(value.Id) is null)
                throw new ValidationException("Vehicle does not exists");

            await ValidatorTool.FluentValidate(_validator, value);

            var VehicleDTO = await _repositories.VehicleRepository.UpdateAsync(_mapper.Map<VehicleDTO>(value));

            return _mapper.Map<VehicleData>(VehicleDTO);
        }

    }
}

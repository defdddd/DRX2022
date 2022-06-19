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
    public class InoviceService : IInoviceService
    {
        private readonly IRepositories _repositories;
        private readonly IValidator<InoviceData> _validator;
        private readonly IMapper _mapper;

        public InoviceService(IRepositories repositories, IValidator<InoviceData> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(InoviceData value)
        {
            var inoviceDTO = _mapper.Map<InoviceDTO>(value);

            return await _repositories.InoviceRepository.DeleteAsync(inoviceDTO);
        }

        public async Task<IEnumerable<InoviceData>> GetAllAsync()
        {
            var Inovices = await _repositories.InoviceRepository.GetAllAsync();

            if (!Inovices.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<InoviceData>>(Inovices);
        }

        public async Task<IEnumerable<InoviceData>> GetMyInovicesAsync(int userId)
        {
            var bilingId = (await _repositories.BilingRepository.GetBilingByUserIdAsync(userId)).Id;

            var inoviceDTOs= await _repositories.InoviceRepository.GetMyInoviceAsync(bilingId);

            return _mapper.Map<IEnumerable<InoviceData>>(inoviceDTOs);
        }

        public async Task<int> GetUserIdByBilingId(int bilingId)
        {
            var bilingDTO = await _repositories.BilingRepository.SearchByIdAsync(bilingId);

            return bilingDTO.UserId;
        }

        public async Task<InoviceData> InsertAsync(InoviceData value)
        {
            if (await _repositories.InoviceRepository.SearchByIdAsync(value.Id) is not null)
                throw new ValidationException("This vehicle is allready in use");

            await ValidatorTool.FluentValidate(_validator, value);

            var inoviceDTO = await _repositories.InoviceRepository.InsertAsync(_mapper.Map<InoviceDTO>(value));

            return _mapper.Map<InoviceData>(inoviceDTO);

        }

        public async Task<InoviceData> SearchByIdAsync(int id)
        {
            var InoviceDTO = await _repositories.InoviceRepository.SearchByIdAsync(id);

            return _mapper.Map<InoviceData>(InoviceDTO);
        }

        public async Task<InoviceData> UpdateAsync(InoviceData value)
        {
            if (await _repositories.InoviceRepository.SearchByIdAsync(value.Id) is null)
                throw new ValidationException("Inovice does not exists");

            await ValidatorTool.FluentValidate(_validator, value);

            var inoviceDTO = await _repositories.InoviceRepository.UpdateAsync(_mapper.Map<InoviceDTO>(value));

            return _mapper.Map<InoviceData>(inoviceDTO);
        }
    }
}

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
    public class BilingService : IBilingService
    {
        private readonly IRepositories _repositories;
        private readonly IValidator<BilingData> _validator;
        private readonly IMapper _mapper;
        public BilingService(IRepositories repositories, IValidator<BilingData> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(BilingData value)
        {
            var bilingDTO = _mapper.Map<BilingDTO>(value);

            return await _repositories.BilingRepository.DeleteAsync(bilingDTO);
        }

        public async Task<IEnumerable<BilingData>> GetAllAsync()
        {
            var Bilings = await _repositories.BilingRepository.GetAllAsync();

            if (!Bilings.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<BilingData>>(Bilings);
        }

        public async Task<BilingData> GetBilingByUserIdAsync(int id)
        {
            var bilingDto = await _repositories.BilingRepository.GetBilingByUserIdAsync(id);
            return _mapper.Map<BilingData>(bilingDto);
        }

        public async Task<BilingData> InsertAsync(BilingData value)
        {
            if ((await _repositories.BilingRepository.SearchByIdAsync(value.Id)) is not null)
                throw new ValidationException("Biling exists allready");

            if ((await _repositories.BilingRepository.GetBilingByUserIdAsync(value.UserId)) is not null)
                throw new ValidationException("Biling exists allready");

            await ValidatorTool.FluentValidate(_validator, value);

            var bilingDTO = await _repositories.BilingRepository.InsertAsync(_mapper.Map<BilingDTO>(value));

            return _mapper.Map<BilingData>(bilingDTO);

        }

        public async Task<BilingData> SearchByIdAsync(int id)
        {
            var BilingDTO = await _repositories.BilingRepository.SearchByIdAsync(id);

            return _mapper.Map<BilingData>(BilingDTO);
        }

        public async Task<BilingData> UpdateAsync(BilingData value)
        {
            if ((await _repositories.BilingRepository.SearchByIdAsync(value.Id)) is null)
                throw new ValidationException("Biling does not exists");

            await ValidatorTool.FluentValidate(_validator, value);

            var bilingDTO = await _repositories.BilingRepository.UpdateAsync(_mapper.Map<BilingDTO>(value));

            return _mapper.Map<BilingData>(bilingDTO);
        }
    }
}

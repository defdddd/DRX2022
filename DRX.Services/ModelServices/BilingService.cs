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
    public class BilingService : IBilingService
    {
        private readonly IRepositories _repositories;
        private readonly IValidator<BilingDTO> _validator;
        private readonly IMapper _mapper;
        public BilingService(IRepositories repositories, IValidator<BilingDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(BilingDTO value)
        {
            var bilingDTO = _mapper.Map<Biling>(value);

            return await _repositories.BilingRepository.DeleteAsync(bilingDTO);
        }

        public async Task<IEnumerable<BilingDTO>> GetAllAsync()
        {
            var Bilings = await _repositories.BilingRepository.GetAllAsync();

            if (!Bilings.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<BilingDTO>>(Bilings);
        }

        public async Task<BilingDTO> GetBilingByUserIdAsync(int id)
        {
            var bilingDto = await _repositories.BilingRepository.GetBilingByUserIdAsync(id);
            return _mapper.Map<BilingDTO>(bilingDto);
        }

        public async Task<BilingDTO> InsertAsync(BilingDTO value)
        {
            if ((await _repositories.BilingRepository.SearchByIdAsync(value.Id)) is not null)
                throw new ValidationException("Biling exists allready");

            if ((await _repositories.BilingRepository.GetBilingByUserIdAsync(value.UserId)) is not null)
                throw new ValidationException("Biling exists allready");

            await ValidatorTool.FluentValidate(_validator, value);

            var bilingDTO = await _repositories.BilingRepository.InsertAsync(_mapper.Map<Biling>(value));

            return _mapper.Map<BilingDTO>(bilingDTO);

        }

        public async Task<BilingDTO> SearchByIdAsync(int id)
        {
            var BilingDTO = await _repositories.BilingRepository.SearchByIdAsync(id);

            return _mapper.Map<BilingDTO>(BilingDTO);
        }

        public async Task<BilingDTO> UpdateAsync(BilingDTO value)
        {
            if ((await _repositories.BilingRepository.SearchByIdAsync(value.Id)) is null)
                throw new ValidationException("Biling does not exists");

            await ValidatorTool.FluentValidate(_validator, value);

            var bilingDTO = await _repositories.BilingRepository.UpdateAsync(_mapper.Map<Biling>(value));

            return _mapper.Map<BilingDTO>(bilingDTO);
        }
    }
}

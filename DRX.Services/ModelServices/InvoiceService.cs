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
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepositories _repositories;
        private readonly IValidator<InvoiceDTO> _validator;
        private readonly IMapper _mapper;

        public InvoiceService(IRepositories repositories, IValidator<InvoiceDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(InvoiceDTO value)
        {
            var inoviceDTO = _mapper.Map<Invoice>(value);

            return await _repositories.InoviceRepository.DeleteAsync(inoviceDTO);
        }

        public async Task<IEnumerable<InvoiceDTO>> GetAllAsync()
        {
            var Inovices = await _repositories.InoviceRepository.GetAllAsync();

            if (!Inovices.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<InvoiceDTO>>(Inovices);
        }

        public async Task<IEnumerable<InvoiceDTO>> GetMyInvoicesAsync(int userId)
        {
            var biling = (await _repositories.BilingRepository.GetBilingByUserIdAsync(userId)) ?? throw new Exception("No Data");

            var inoviceDTOs= await _repositories.InoviceRepository.GetMyInvoicesAsync(biling.Id);

            return _mapper.Map<IEnumerable<InvoiceDTO>>(inoviceDTOs);
        }

        public async Task<int> GetUserIdByBilingId(int bilingId)
        {
            var bilingDTO = await _repositories.BilingRepository.SearchByIdAsync(bilingId);

            return bilingDTO.UserId;
        }

        public async Task<InvoiceDTO> InsertAsync(InvoiceDTO value)
        {
            if (await _repositories.InoviceRepository.SearchByIdAsync(value.Id) is not null)
                throw new ValidationException("This vehicle is allready in use");

            await ValidatorTool.FluentValidate(_validator, value);

            var inoviceDTO = await _repositories.InoviceRepository.InsertAsync(_mapper.Map<Invoice>(value));

            return _mapper.Map<InvoiceDTO>(inoviceDTO);

        }

        public async Task<InvoiceDTO> SearchByIdAsync(int id)
        {
            var InoviceDTO = await _repositories.InoviceRepository.SearchByIdAsync(id);

            return _mapper.Map<InvoiceDTO>(InoviceDTO);
        }

        public async Task<InvoiceDTO> UpdateAsync(InvoiceDTO value)
        {
            if (await _repositories.InoviceRepository.SearchByIdAsync(value.Id) is null)
                throw new ValidationException("Inovice does not exists");

            await ValidatorTool.FluentValidate(_validator, value);

            var inoviceDTO = await _repositories.InoviceRepository.UpdateAsync(_mapper.Map<Invoice>(value));

            return _mapper.Map<InvoiceDTO>(inoviceDTO);
        }
    }
}

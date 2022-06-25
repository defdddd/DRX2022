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
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepositories _repositories;
        private readonly IValidator<InvoiceData> _validator;
        private readonly IMapper _mapper;

        public InvoiceService(IRepositories repositories, IValidator<InvoiceData> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(InvoiceData value)
        {
            var inoviceDTO = _mapper.Map<InvoiceDTO>(value);

            return await _repositories.InoviceRepository.DeleteAsync(inoviceDTO);
        }

        public async Task<IEnumerable<InvoiceData>> GetAllAsync()
        {
            var Inovices = await _repositories.InoviceRepository.GetAllAsync();

            if (!Inovices.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<IEnumerable<InvoiceData>>(Inovices);
        }

        public async Task<IEnumerable<InvoiceData>> GetMyInvoicesAsync(int userId)
        {
            var bilingId = (await _repositories.BilingRepository.GetBilingByUserIdAsync(userId)).Id;

            var inoviceDTOs= await _repositories.InoviceRepository.GetMyInvoicesAsync(bilingId);

            return _mapper.Map<IEnumerable<InvoiceData>>(inoviceDTOs);
        }

        public async Task<int> GetUserIdByBilingId(int bilingId)
        {
            var bilingDTO = await _repositories.BilingRepository.SearchByIdAsync(bilingId);

            return bilingDTO.UserId;
        }

        public async Task<InvoiceData> InsertAsync(InvoiceData value)
        {
            if (await _repositories.InoviceRepository.SearchByIdAsync(value.Id) is not null)
                throw new ValidationException("This vehicle is allready in use");

            await ValidatorTool.FluentValidate(_validator, value);

            var inoviceDTO = await _repositories.InoviceRepository.InsertAsync(_mapper.Map<InvoiceDTO>(value));

            return _mapper.Map<InvoiceData>(inoviceDTO);

        }

        public async Task<InvoiceData> SearchByIdAsync(int id)
        {
            var InoviceDTO = await _repositories.InoviceRepository.SearchByIdAsync(id);

            return _mapper.Map<InvoiceData>(InoviceDTO);
        }

        public async Task<InvoiceData> UpdateAsync(InvoiceData value)
        {
            if (await _repositories.InoviceRepository.SearchByIdAsync(value.Id) is null)
                throw new ValidationException("Inovice does not exists");

            await ValidatorTool.FluentValidate(_validator, value);

            var inoviceDTO = await _repositories.InoviceRepository.UpdateAsync(_mapper.Map<InvoiceDTO>(value));

            return _mapper.Map<InvoiceData>(inoviceDTO);
        }
    }
}

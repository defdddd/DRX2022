using DRX.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices.Interfaces
{
    public interface IInvoiceService : IService<InvoiceDTO>
    {
        Task<int> GetUserIdByBilingId(int bilingId);
        Task<IEnumerable<InvoiceDTO>> GetMyInvoicesAsync(int userId);
    }
}

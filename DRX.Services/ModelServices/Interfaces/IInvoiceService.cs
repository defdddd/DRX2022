using DRX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices.Interfaces
{
    public interface IInvoiceService : IService<InvoiceData>
    {
        Task<int> GetUserIdByBilingId(int bilingId);
        Task<IEnumerable<InvoiceData>> GetMyInvoicesAsync(int userId);
    }
}

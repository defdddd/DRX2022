using DRX.DataAccess.Data.DTOs;
using DRX.DataAccess.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data.Interfaces
{
    public interface IInvoiceRepository : IRepository<InvoiceDTO>
    {
        Task<IEnumerable<InvoiceDTO>> GetMyInvoicesAsync(int bilingId);
    }
}

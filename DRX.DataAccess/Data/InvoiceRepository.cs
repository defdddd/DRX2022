using DRX.DataAccess.Data.DTOs;
using DRX.DataAccess.Data.Interfaces;
using DRX.DataAccess.Data.Repository;
using DRX.DataAccess.SqlDataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data
{
    public class InvoiceRepository : Repository<InvoiceDTO>, IInvoiceRepository
    {
        public InvoiceRepository(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public async Task<IEnumerable<InvoiceDTO>> GetMyInvoicesAsync(int bilingId)
        {
            return (await GetAllAsync()).Where(x => x.BilingId == bilingId);
        }
    }
}

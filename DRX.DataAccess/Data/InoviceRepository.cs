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
    public class InoviceRepository : Repository<InoviceDTO>, IInoviceRepository
    {
        public InoviceRepository(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public async Task<IEnumerable<InoviceDTO>> GetMyInoviceAsync(int bilingId)
        {
            return (await GetAllAsync()).Where(x => x.BilingId == bilingId);
        }
    }
}

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
    public class BilingRepository : Repository<BilingDTO>, IBilingRepository
    {
        public BilingRepository(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public async Task<BilingDTO> GetBilingByUserIdAsync(int userId)
        {
            return (await GetAllAsync()).Where(x => x.UserId == userId).FirstOrDefault();
        }
    }
}

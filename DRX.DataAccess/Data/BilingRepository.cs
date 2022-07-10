using DRX.DataAccess.Data.Domains;
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
    public class BilingRepository : Repository<Biling>, IBilingRepository
    {
        public BilingRepository(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public async Task<Biling> GetBilingByUserIdAsync(int userId)
        {
            return (await GetAllAsync()).Where(x => x.UserId == userId).FirstOrDefault();
        }
    }
}

using DRX.DataAccess.Data.Domains;
using DRX.DataAccess.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data.Interfaces
{
    public interface IBilingRepository : IRepository<Biling>
    {
        Task<Biling> GetBilingByUserIdAsync(int id);
    }
}

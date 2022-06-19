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
    public class RentRepository : Repository<RentDTO>, IRentRepository
    {
        public RentRepository(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public bool CheckRent(int vehicleId)
        {
            return GetAll().Where(x => x.VehicleId == vehicleId && x.IsActive).Any();
        }

        public async Task<bool> CheckRentAsync(int vehicleId)
        {
            return (await GetAllAsync()).Where(x => x.VehicleId == vehicleId && x.IsActive).Any();
        }

        public async Task<IEnumerable<RentDTO>> GetMyRentsAsync(int userId)
        {
            return (await GetAllAsync()).Where((x) => x.UserId == userId);
        }
    }
}

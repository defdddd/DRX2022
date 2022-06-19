using DRX.DataAccess.Data.DTOs;
using DRX.DataAccess.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data.Interfaces
{
    public interface IRentRepository : IRepository<RentDTO>
    {
        Task<bool> CheckRentAsync(int vehicleId);
        bool CheckRent(int vehicleId);
        Task<IEnumerable<RentDTO>> GetMyRentsAsync(int userId);
    }
}

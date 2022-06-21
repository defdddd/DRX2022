using DRX.DataAccess.Data.DTOs;
using DRX.DataAccess.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data.Interfaces
{
    public interface IVehicleRepository : IRepository<VehicleDTO>
    {
        Task<IEnumerable<VehicleDTO>> GetAllBySearchFieldAsync(string type, string model);
    }
}

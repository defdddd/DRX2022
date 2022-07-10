using DRX.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices.Interfaces
{
    public interface IVehicleService : IService<VehicleDTO>
    {
        Task<IEnumerable<VehicleDTO>> GetAvailableVehiclesAsync(string type, string model);
        Task<IEnumerable<VehicleDTO>> GetAllSearchByVehiclesAsync(string type, string model);
        Task<IEnumerable<VehicleDTO>> GetAllAvailableVehiclesAsync();

    }
}

using DRX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices.Interfaces
{
    public interface IVehicleService : IService<VehicleData>
    {
        Task<IEnumerable<VehicleData>> GetAvailableVehiclesAsync(string type, string model);
        Task<IEnumerable<VehicleData>> GetAllSearchByVehiclesAsync(string type, string model);
        Task<IEnumerable<VehicleData>> GetAllAvailableVehiclesAsync();

    }
}

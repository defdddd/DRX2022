using DRX.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices.Interfaces
{
    public interface IRentService : IService<RentDTO>
    {
        Task<IEnumerable<RentDTO>> GetMyRentsAsync(int userId);
    }
}

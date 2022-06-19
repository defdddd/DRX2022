using DRX.DataAccess.Data.DTOs;
using DRX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices.Interfaces
{
    public interface IRentService : IService<RentData>
    {
        Task<IEnumerable<RentData>> GetMyRentsAsync(int userId);
    }
}

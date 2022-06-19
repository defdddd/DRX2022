using DRX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Services.ModelServices.Interfaces
{
    public interface IInoviceService : IService<InoviceData>
    {
        Task<int> GetUserIdByBilingId(int bilingId);
        Task<IEnumerable<InoviceData>> GetMyInovicesAsync(int userId);
    }
}

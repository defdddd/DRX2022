using Dapper.Contrib.Extensions;
using DRX.DataAccess.Data.DTOs;
using DRX.DataAccess.Data.Interfaces;
using DRX.DataAccess.Data.Repository;
using DRX.DataAccess.SqlDataAcces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data
{
    public class VehicleRepository : Repository<VehicleDTO>, IVehicleRepository
    {
        public VehicleRepository(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

    }
}

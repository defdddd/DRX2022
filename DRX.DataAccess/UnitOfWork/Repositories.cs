using DRX.DataAccess.Data;
using DRX.DataAccess.Data.Interfaces;
using DRX.DataAccess.SqlDataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.UnitOfWork
{
    public class Repositories : IRepositories
    {
        private ISqlDataAccess _sqlDataAccess;
        public IBilingRepository BilingRepository => new BilingRepository(_sqlDataAccess);
        public IInoviceRepository InoviceRepository => new InoviceRepository(_sqlDataAccess);
        public IRentRepository RentRepository => new RentRepository(_sqlDataAccess);
        public IUserRepository UserRepository => new UserRepository(_sqlDataAccess);
        public IVehicleRepository VehicleRepository => new VehicleRepository(_sqlDataAccess);
        public Repositories(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

    }
}

using DRX.DataAccess.Data.Interfaces;

namespace DRX.DataAccess.UnitOfWork
{
    public interface IRepositories
    {
        IBilingRepository BilingRepository { get; }
        IInvoiceRepository InoviceRepository { get; }
        IRentRepository RentRepository { get; }
        IUserRepository UserRepository { get; }
        IVehicleRepository VehicleRepository { get; }
    }
}
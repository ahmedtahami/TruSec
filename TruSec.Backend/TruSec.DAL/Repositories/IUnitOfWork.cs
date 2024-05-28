using System;
using System.Threading.Tasks;
using TruSec.DAL.Entities;

namespace TruSec.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Company> Companies { get; }
        IRepository<Truck> Trucks { get; }
        IRepository<TruckDataLog> TruckDataLogs { get; }
        IRepository<TruckSecret> TruckSecrets { get; }
        IRepository<UserCompany> UserCompanies { get; }
        Task<int> CompleteAsync();
    }
}

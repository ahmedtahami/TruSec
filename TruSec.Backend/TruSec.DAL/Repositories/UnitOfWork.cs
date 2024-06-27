using System.Threading.Tasks;
using TruSec.DAL.DbContexts;
using TruSec.DAL.Entities;
using TruSec.DAL.Repositories;

namespace TruSec.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Companies = new Repository<Company>(_context);
            Trucks = new Repository<Truck>(_context);
            TruckDataLogs = new Repository<TruckDataLog>(_context);
            TruckSecrets = new Repository<TruckSecret>(_context);
            UserCompanies = new Repository<UserCompany>(_context);
            ApplicationUsers = new Repository<ApplicationUser>(_context);
        }

        public IRepository<Company> Companies { get; private set; }
        public IRepository<Truck> Trucks { get; private set; }
        public IRepository<TruckDataLog> TruckDataLogs { get; private set; }
        public IRepository<TruckSecret> TruckSecrets { get; private set; }
        public IRepository<UserCompany> UserCompanies { get; private set; }
        public IRepository<ApplicationUser> ApplicationUsers { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

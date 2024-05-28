using TruSec.BLL.DTOs;

namespace TruSec.BLL.Interfaces
{
    public interface ITruckService
    {
        Task<TruckDto> GetAsync(int id);
        Task<IEnumerable<TruckDto>> GetAsync();
        Task AddAsync(TruckDto truckdto);
        Task UpdateAsync(TruckDto truckdto);
        Task DeleteAsync(int id);
    }
}

using TruSec.BLL.DTOs;

namespace TruSec.BLL.Interfaces
{
    public interface ITruckDataLogService
    {
        Task<TruckDataLogDto> GetAsync(int id);
        Task<IEnumerable<TruckDataLogDto>> GetAsync();
        Task AddAsync(TruckDataLogDto truckDataLogDto);
        Task UpdateAsync(TruckDataLogDto truckDataLogDto);
        Task DeleteAsync(int id);
    }
}

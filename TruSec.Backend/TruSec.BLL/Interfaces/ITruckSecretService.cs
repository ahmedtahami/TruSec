using TruSec.BLL.DTOs;

namespace TruSec.BLL.Interfaces
{
    public interface ITruckSecretService
    {
        Task<TruckSecretDto> GetAsync(int id);
        Task<IEnumerable<TruckSecretDto>> GetAsync();
        Task AddAsync(TruckSecretDto truckSecretDto);
        Task UpdateAsync(TruckSecretDto truckSecretDto);
        Task DeleteAsync(int id);
    }
}

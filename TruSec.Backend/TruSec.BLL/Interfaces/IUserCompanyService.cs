using TruSec.BLL.DTOs;

namespace TruSec.BLL.Interfaces
{
    public interface IUserCompanyService
    {
        Task<UserCompanyDto> GetAsync(int id);
        Task<IEnumerable<UserCompanyDto>> GetAsync();
        Task AddAsync(UserCompanyDto userCompanyDto);
        Task UpdateAsync(UserCompanyDto userCompanyDto);
        Task DeleteAsync(int id);
    }
}

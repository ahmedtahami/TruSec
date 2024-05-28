using System.Collections.Generic;
using System.Threading.Tasks;
using TruSec.BLL.DTOs;

namespace TruSec.BLL.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetAsync(int id);
        Task<IEnumerable<CompanyDto>> GetAsync();
        Task AddAsync(CompanyDto companyDto);
        Task UpdateAsync(CompanyDto companyDto);
        Task DeleteAsync(int id);
    }
}

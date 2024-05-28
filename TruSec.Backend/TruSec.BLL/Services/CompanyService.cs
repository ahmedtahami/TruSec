using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruSec.BLL.DTOs;
using TruSec.BLL.Interfaces;
using TruSec.DAL.Entities;
using TruSec.DAL.Repositories;

namespace TruSec.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CompanyDto> GetAsync(int id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<IEnumerable<CompanyDto>> GetAsync()
        {
            var companies = await _unitOfWork.Companies.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task AddAsync(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(CompanyDto companyDto)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync((int)companyDto.Id);
            if (company != null)
            {
                _mapper.Map(companyDto, company);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company != null)
            {
                _unitOfWork.Companies.Remove(company);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}

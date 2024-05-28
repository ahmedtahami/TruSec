using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruSec.BLL.DTOs;
using TruSec.BLL.Interfaces;
using TruSec.DAL.Entities;
using TruSec.DAL.Repositories;

namespace TruSec.BLL.Services
{
    public class UserCompanyService : IUserCompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserCompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserCompanyDto> GetAsync(int id)
        {
            var userCompany = await _unitOfWork.UserCompanies.GetByIdAsync(id);
            return _mapper.Map<UserCompanyDto>(userCompany);
        }

        public async Task<IEnumerable<UserCompanyDto>> GetAsync()
        {
            var userCompanies = await _unitOfWork.UserCompanies.GetAllAsync();
            return _mapper.Map<IEnumerable<UserCompanyDto>>(userCompanies);
        }

        public async Task AddAsync(UserCompanyDto userCompanyDto)
        {
            var userCompany = _mapper.Map<UserCompany>(userCompanyDto);
            await _unitOfWork.UserCompanies.AddAsync(userCompany);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(UserCompanyDto userCompanyDto)
        {
            var userCompany = await _unitOfWork.UserCompanies.GetByIdAsync((int)userCompanyDto.Id);
            if (userCompany != null)
            {
                _mapper.Map(userCompanyDto, userCompany);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var userCompany = await _unitOfWork.UserCompanies.GetByIdAsync(id);
            if (userCompany != null)
            {
                _unitOfWork.UserCompanies.Remove(userCompany);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}

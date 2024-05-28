using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruSec.BLL.DTOs;
using TruSec.BLL.Interfaces;
using TruSec.DAL.Entities;
using TruSec.DAL.Repositories;

namespace TruSec.BLL.Services
{
    public class TruckSecretService : ITruckSecretService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TruckSecretService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TruckSecretDto> GetAsync(int id)
        {
            var truckSecret = await _unitOfWork.TruckSecrets.GetByIdAsync(id);
            return _mapper.Map<TruckSecretDto>(truckSecret);
        }

        public async Task<IEnumerable<TruckSecretDto>> GetAsync()
        {
            var TruckSecrets = await _unitOfWork.TruckSecrets.GetAllAsync();
            return _mapper.Map<IEnumerable<TruckSecretDto>>(TruckSecrets);
        }

        public async Task AddAsync(TruckSecretDto truckSecretDto)
        {
            var truckSecret = _mapper.Map<TruckSecret>(truckSecretDto);
            await _unitOfWork.TruckSecrets.AddAsync(truckSecret);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(TruckSecretDto truckSecretDto)
        {
            var truckSecret = await _unitOfWork.TruckSecrets.GetByIdAsync((int)truckSecretDto.Id);
            if (truckSecret != null)
            {
                _mapper.Map(truckSecretDto, truckSecret);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var TruckSecretSecret = await _unitOfWork.TruckSecrets.GetByIdAsync(id);
            if (TruckSecretSecret != null)
            {
                _unitOfWork.TruckSecrets.Remove(TruckSecretSecret);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}

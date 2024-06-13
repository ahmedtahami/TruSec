using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            var truckSecret = await _unitOfWork.TruckSecrets.FindByConditionAsync(p => p.Id == id, false);
            var result = truckSecret.Include(p => p.Truck).FirstOrDefault();
            return _mapper.Map<TruckSecretDto>(result);
        }

        public async Task<IEnumerable<TruckSecretDto>> GetAsync()
        {
            var truckSecrets = await _unitOfWork.TruckSecrets.GetAllAsync(false);
            var result = truckSecrets.Include(p => p.Truck).ToList();
            return _mapper.Map<IEnumerable<TruckSecretDto>>(result);
        }
        public async Task<IEnumerable<TruckSecretDto>> GetByTruckAsync(int truckId)
        {
            var truckDataLogs = await _unitOfWork.TruckSecrets.GetAllAsync(false);
            var result = truckDataLogs.Where(p => p.TruckId == truckId).ToList();
            return _mapper.Map<IEnumerable<TruckSecretDto>>(result);
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

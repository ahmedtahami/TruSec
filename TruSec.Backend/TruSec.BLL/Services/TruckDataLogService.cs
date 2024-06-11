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
    public class TruckDataLogService : ITruckDataLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TruckDataLogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TruckDataLogDto> GetAsync(int id)
        {
            var truckDataLog = await _unitOfWork.TruckDataLogs.FindByConditionAsync(p => p.Id == id, false);
            var result = truckDataLog.Include(p => p.Truck).FirstOrDefault();
            return _mapper.Map<TruckDataLogDto>(result);
        }

        public async Task<IEnumerable<TruckDataLogDto>> GetAsync()
        {
            var truckDataLogs = await _unitOfWork.TruckDataLogs.GetAllAsync(false);
            var result = truckDataLogs.Include(p => p.Truck).ToList();
            return _mapper.Map<IEnumerable<TruckDataLogDto>>(result);
        }
        public async Task<IEnumerable<TruckDataLogDto>> GetByTruckAsync(int truckId)
        {
            var truckDataLogs = await _unitOfWork.TruckDataLogs.GetAllAsync(false);
            var result = truckDataLogs.Include(p => p.Truck).Where(p => p.TruckId == truckId).ToList();
            return _mapper.Map<IEnumerable<TruckDataLogDto>>(result);
        }

        public async Task AddAsync(TruckDataLogDto truckDataLogDto)
        {
            var truckDataLog = _mapper.Map<TruckDataLog>(truckDataLogDto);
            await _unitOfWork.TruckDataLogs.AddAsync(truckDataLog);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(TruckDataLogDto truckDataLogDto)
        {
            var truckDataLog = await _unitOfWork.TruckDataLogs.GetByIdAsync((int)truckDataLogDto.Id);
            if (truckDataLog != null)
            {
                _mapper.Map(truckDataLogDto, truckDataLog);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var TruckDataLog = await _unitOfWork.TruckDataLogs.GetByIdAsync(id);
            if (TruckDataLog != null)
            {
                _unitOfWork.TruckDataLogs.Remove(TruckDataLog);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}

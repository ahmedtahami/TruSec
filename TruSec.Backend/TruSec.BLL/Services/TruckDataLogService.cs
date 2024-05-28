using AutoMapper;
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
            var TruckDataLog = await _unitOfWork.TruckDataLogs.GetByIdAsync(id);
            return _mapper.Map<TruckDataLogDto>(TruckDataLog);
        }

        public async Task<IEnumerable<TruckDataLogDto>> GetAsync()
        {
            var TruckDataLogs = await _unitOfWork.TruckDataLogs.GetAllAsync();
            return _mapper.Map<IEnumerable<TruckDataLogDto>>(TruckDataLogs);
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

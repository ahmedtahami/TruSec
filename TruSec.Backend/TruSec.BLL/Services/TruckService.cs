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
    public class TruckService : ITruckService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TruckService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TruckDto> GetAsync(int id)
        {
            var truck = await _unitOfWork.Trucks.FindByConditionAsync(p => p.Id == id, false);
            var result = truck.Include(p => p.Company).Include(p => p.DataLogs).Include(p => p.Secrets).FirstOrDefault();
            return _mapper.Map<TruckDto>(truck);
        }

        public async Task<IEnumerable<TruckDto>> GetAsync()
        {
            var trucks = await _unitOfWork.Trucks.GetAllAsync(false);
            var result = trucks.Include(p => p.Company).Include(p => p.DataLogs).Include(p => p.Secrets).ToList();
            return _mapper.Map<IEnumerable<TruckDto>>(result);
        }

        public async Task AddAsync(TruckDto truckDto)
        {
            Truck truck = new Truck { CompanyId = (int)truckDto.CompanyId, ImageSrc = truckDto.ImageSrc, ModelName = truckDto.ModelName, RegistrationNumber = truckDto.RegistrationNumber };
            await _unitOfWork.Trucks.AddAsync(truck);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(TruckDto TruckDto)
        {
            var Truck = await _unitOfWork.Trucks.GetByIdAsync((int)TruckDto.Id);
            if (Truck != null)
            {
                _mapper.Map(TruckDto, Truck);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var Truck = await _unitOfWork.Trucks.GetByIdAsync(id);
            if (Truck != null)
            {
                _unitOfWork.Trucks.Remove(Truck);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}

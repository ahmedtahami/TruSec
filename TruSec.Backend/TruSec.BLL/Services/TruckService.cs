using AutoMapper;
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
            var Truck = await _unitOfWork.Trucks.GetByIdAsync(id);
            return _mapper.Map<TruckDto>(Truck);
        }

        public async Task<IEnumerable<TruckDto>> GetAsync()
        {
            var Trucks = await _unitOfWork.Trucks.GetAllAsync();
            return _mapper.Map<IEnumerable<TruckDto>>(Trucks);
        }

        public async Task AddAsync(TruckDto truckDto)
        {
            var truck = _mapper.Map<Truck>(truckDto);
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

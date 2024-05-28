using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruSec.BLL.DTOs;
using TruSec.DAL.Entities;

namespace TruSec.BLL.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Truck, TruckDto>().ReverseMap();
            CreateMap<TruckDataLog, TruckDataLogDto>().ReverseMap();
            CreateMap<TruckSecret, TruckSecretDto>().ReverseMap();
            CreateMap<UserCompany, UserCompanyDto>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            // Add other mappings here
        }
    }
}

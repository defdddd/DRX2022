using AutoMapper;
using DRX.DataAccess.Data.DTOs;
using DRX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleData, VehicleDTO>().ReverseMap();
            CreateMap<BilingData, BilingDTO>().ReverseMap();
            CreateMap<InoviceData, InoviceDTO>().ReverseMap();
            CreateMap<RentData, RentDTO>().ReverseMap();
            CreateMap<UserData, UserDTO>().ReverseMap();
        }
    }
}

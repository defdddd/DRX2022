using AutoMapper;
using DRX.DataAccess.Data.Domains;
using DRX.DTOs;
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
            CreateMap<VehicleDTO, Vehicle>().ReverseMap();
            CreateMap<BilingDTO, Biling>().ReverseMap();
            CreateMap<InvoiceDTO, Invoice>().ReverseMap();
            CreateMap<RentDTO, Rent>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}

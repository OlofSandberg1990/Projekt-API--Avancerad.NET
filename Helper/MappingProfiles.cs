using AutoMapper;
using BookingSystemLibrary;
using Projekt_API__Avancerad.NET.DTO;

namespace Projekt_API__Avancerad.NET.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();            
            CreateMap<ChangeLog, ChangeLogDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
        }
    }
}

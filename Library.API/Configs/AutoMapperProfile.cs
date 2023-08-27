using AutoMapper;
using Library.API.Data.Entities;
using Library.API.DTOs;

namespace Library.API.Configs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // AppUser profiling
            CreateMap<AddUserDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Email))
                .ReverseMap();

            CreateMap<AppUser, ReturnUserDto>().ReverseMap();

            // WeatherForecast profiling
            CreateMap<Book, ReturnBookDto>().ReverseMap();
        }
    }
}


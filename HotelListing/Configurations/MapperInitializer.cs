using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.Models;

namespace HotelListing.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Country, CountryTO>().ReverseMap();
            CreateMap<Country, CreateCountryTO>().ReverseMap();
            CreateMap<Country, UpdateCountryTO>().ReverseMap();
            CreateMap<Hotel, HotelTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelTO>().ReverseMap();
            CreateMap<Hotel, UpdateHotelTO>().ReverseMap();
            CreateMap<ApiUser, UserTO>().ReverseMap();
        }
    }
}
using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Models;

namespace DevIO.App.AutoMapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Hotel, HotelViewModel>().ReverseMap();
        CreateMap<Quarto, QuartoViewModel>().ReverseMap();
    }
}
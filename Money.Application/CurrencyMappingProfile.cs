using AutoMapper;
using Money.Domain.Models;
using Money.Application.DTOs;

namespace Money.Application
{
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            CreateMap<CurrencyConversion, CurrencyConversionResponseDTO>();
                
            CreateMap<CurrencyConversionRequestDTO, CurrencyConversion>()
                .ForMember(dest => dest.ConversionDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}

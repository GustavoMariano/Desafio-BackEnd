using AutoMapper;
using Desafio_BackEnd.Application.DTOs;
using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Motorcycle, MotorcycleDto>().ReverseMap();
        CreateMap<Courier, CourierDto>().ReverseMap();
        CreateMap<Rental, RentalDto>().ReverseMap();
    }
}

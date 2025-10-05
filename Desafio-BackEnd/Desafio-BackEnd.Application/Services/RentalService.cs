using AutoMapper;
using Desafio_BackEnd.Application.DTOs;
using Desafio_BackEnd.Application.Validators;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Enums;
using Desafio_BackEnd.Domain.Interfaces;

namespace Desafio_BackEnd.Application.Services;

public class RentalService
{
    private readonly IRentalRepository _rentalRepo;
    private readonly ICourierRepository _courierRepo;
    private readonly IMotorcycleRepository _motoRepo;
    private readonly IMapper _mapper;
    private readonly RentalValidator _validator;

    public RentalService(IRentalRepository rentalRepo, ICourierRepository courierRepo, IMotorcycleRepository motoRepo, IMapper mapper)
    {
        _rentalRepo = rentalRepo;
        _courierRepo = courierRepo;
        _motoRepo = motoRepo;
        _mapper = mapper;
        _validator = new RentalValidator();
    }

    public async Task<RentalDto> RentAsync(Guid courierId, Guid motorcycleId, int planDays)
    {
        var courier = await _courierRepo.GetByIdAsync(courierId)
            ?? throw new KeyNotFoundException("Courier not found.");

        if (courier.CnhType != ECnhType.A && courier.CnhType != ECnhType.AB)
            throw new InvalidOperationException("Only CNH type A or AB can rent.");

        var dailyRate = planDays switch
        {
            7 => 30m,
            15 => 28m,
            30 => 22m,
            45 => 20m,
            50 => 18m,
            _ => throw new InvalidOperationException("Invalid plan.")
        };

        var rental = new Rental
        {
            Id = Guid.NewGuid(),
            CourierId = courierId,
            MotorcycleId = motorcycleId,
            StartDate = DateTime.UtcNow.AddDays(1),
            ExpectedEndDate = DateTime.UtcNow.AddDays(planDays + 1),
            DailyRate = dailyRate
        };

        await _rentalRepo.AddAsync(rental);
        return _mapper.Map<RentalDto>(rental);
    }

    public async Task<decimal> ReturnAsync(Guid rentalId, DateTime returnDate)
    {
        return await _rentalRepo.CalculateTotalAsync(rentalId, returnDate);
    }
}

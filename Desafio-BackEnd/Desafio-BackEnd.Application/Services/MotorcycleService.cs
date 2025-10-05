using AutoMapper;
using Desafio_BackEnd.Application.DTOs;
using Desafio_BackEnd.Application.Validators;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Events;
using Desafio_BackEnd.Domain.Interfaces;
using Desafio_BackEnd.Infrastructure.Messaging;
using FluentValidation;

namespace Desafio_BackEnd.Application.Services;

public class MotorcycleService
{
    private readonly IMotorcycleRepository _repository;
    private readonly IMessagePublisher _publisher;
    private readonly IMapper _mapper;
    private readonly MotorcycleValidator _validator;

    public MotorcycleService(IMotorcycleRepository repository, IMessagePublisher publisher, IMapper mapper)
    {
        _repository = repository;
        _publisher = publisher;
        _mapper = mapper;
        _validator = new MotorcycleValidator();
    }

    public async Task<MotorcycleDto> CreateAsync(MotorcycleDto dto)
    {
        _validator.ValidateAndThrow(dto);

        var existing = await _repository.GetByPlateAsync(dto.Plate);
        if (existing != null)
            throw new InvalidOperationException("Plate already registered.");

        var entity = _mapper.Map<Motorcycle>(dto);
        await _repository.AddAsync(entity);

        var @event = new MotorcycleRegisteredEvent(entity.Id, entity.Year);
        await _publisher.PublishAsync(@event, "motorcycle-registered");

        return _mapper.Map<MotorcycleDto>(entity);
    }

    public async Task<IEnumerable<MotorcycleDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<MotorcycleDto>>(list);
    }

    public async Task UpdatePlateAsync(Guid id, string newPlate)
    {
        var moto = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Motorcycle not found.");

        moto.Plate = newPlate;
        await _repository.UpdateAsync(moto);
    }

    public async Task DeleteAsync(Guid id)
    {
        var hasRentals = await _repository.HasRentalsAsync(id);
        if (hasRentals)
            throw new InvalidOperationException("Cannot delete motorcycle with existing rentals.");

        await _repository.DeleteAsync(id);
    }
}

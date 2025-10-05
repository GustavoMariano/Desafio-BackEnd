using AutoMapper;
using Desafio_BackEnd.Application.DTOs;
using Desafio_BackEnd.Application.Validators;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Interfaces;
using Desafio_BackEnd.Infrastructure.Storage;
using FluentValidation;

namespace Desafio_BackEnd.Application.Services;

public class CourierService
{
    private readonly ICourierRepository _repository;
    private readonly IStorageService _storage;
    private readonly IMapper _mapper;
    private readonly CourierValidator _validator;

    public CourierService(ICourierRepository repository, IStorageService storage, IMapper mapper)
    {
        _repository = repository;
        _storage = storage;
        _mapper = mapper;
        _validator = new CourierValidator();
    }

    public async Task<CourierDto> CreateAsync(CourierDto dto)
    {
        _validator.ValidateAndThrow(dto);

        if (await _repository.GetByCnpjAsync(dto.Cnpj) != null)
            throw new InvalidOperationException("CNPJ already registered.");

        if (await _repository.GetByCnhNumberAsync(dto.CnhNumber) != null)
            throw new InvalidOperationException("CNH already registered.");

        var entity = _mapper.Map<Courier>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<CourierDto>(entity);
    }

    public async Task<string> UpdateCnhImageAsync(Guid courierId, Stream imageStream, string fileName)
    {
        var courier = await _repository.GetByIdAsync(courierId)
            ?? throw new KeyNotFoundException("Courier not found.");

        var path = await _storage.UploadAsync(fileName, imageStream);
        courier.CnhImagePath = path;

        await _repository.UpdateAsync(courier);
        return path;
    }
}

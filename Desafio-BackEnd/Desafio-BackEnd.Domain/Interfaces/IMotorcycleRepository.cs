using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Interfaces;

public interface IMotorcycleRepository : IRepository<Motorcycle>
{
    Task<Motorcycle?> GetByPlateAsync(string plate);
    Task<bool> HasRentalsAsync(Guid motorcycleId);
}

using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio_BackEnd.Infrastructure.Persistence.Repositories;

public class MotorcycleRepository : RepositoryBase<Motorcycle>, IMotorcycleRepository
{
    public MotorcycleRepository(AppDbContext context) : base(context) { }

    public async Task<Motorcycle?> GetByPlateAsync(string plate)
        => await _context.Motorcycles.FirstOrDefaultAsync(x => x.Plate == plate);

    public async Task<bool> HasRentalsAsync(Guid motorcycleId)
        => await _context.Rentals.AnyAsync(x => x.MotorcycleId == motorcycleId);
}

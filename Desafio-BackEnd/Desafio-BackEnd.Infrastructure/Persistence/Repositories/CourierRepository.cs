using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio_BackEnd.Infrastructure.Persistence.Repositories;

public class CourierRepository : RepositoryBase<Courier>, ICourierRepository
{
    public CourierRepository(AppDbContext context) : base(context) { }

    public async Task<Courier?> GetByCnpjAsync(string cnpj)
        => await _context.Couriers.FirstOrDefaultAsync(x => x.Cnpj == cnpj);

    public async Task<Courier?> GetByCnhNumberAsync(string cnhNumber)
        => await _context.Couriers.FirstOrDefaultAsync(x => x.CnhNumber == cnhNumber);
}

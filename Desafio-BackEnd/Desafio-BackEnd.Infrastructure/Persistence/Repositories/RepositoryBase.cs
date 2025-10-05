using Desafio_BackEnd.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio_BackEnd.Infrastructure.Persistence.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id)
        => await _context.Set<T>().FindAsync(id);

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}

using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio_BackEnd.Infrastructure.Persistence.Repositories;

public class RentalRepository : RepositoryBase<Rental>, IRentalRepository
{
    public RentalRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Rental>> GetByCourierAsync(Guid courierId)
        => await _context.Rentals.Where(x => x.CourierId == courierId).ToListAsync();

    public async Task<decimal> CalculateTotalAsync(Guid rentalId, DateTime returnDate)
    {
        var rental = await _context.Rentals.FindAsync(rentalId);
        if (rental == null) return 0;

        var totalDays = (returnDate - rental.StartDate).Days;
        var expectedDays = (rental.ExpectedEndDate - rental.StartDate).Days;

        decimal total = 0;

        if (returnDate <= rental.ExpectedEndDate)
        {
            total = totalDays * rental.DailyRate;
            if (returnDate < rental.ExpectedEndDate)
            {
                var remainingDays = expectedDays - totalDays;
                var penalty = rental.DailyRate * remainingDays * GetPenalty(expectedDays);
                total += penalty;
            }
        }
        else
        {
            var extraDays = totalDays - expectedDays;
            total = (expectedDays * rental.DailyRate) + (extraDays * 50);
        }

        return total;
    }

    private decimal GetPenalty(int days)
    {
        return days switch
        {
            7 => 0.20m,
            15 => 0.40m,
            _ => 0m
        };
    }
}

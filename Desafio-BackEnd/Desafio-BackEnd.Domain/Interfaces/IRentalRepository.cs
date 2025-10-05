using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Interfaces;

public interface IRentalRepository : IRepository<Rental>
{
    Task<IEnumerable<Rental>> GetByCourierAsync(Guid courierId);
    Task<decimal> CalculateTotalAsync(Guid rentalId, DateTime returnDate);
}

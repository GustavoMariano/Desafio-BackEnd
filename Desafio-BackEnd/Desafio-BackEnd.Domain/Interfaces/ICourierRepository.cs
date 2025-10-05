using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Interfaces;

public interface ICourierRepository : IRepository<Courier>
{
    Task<Courier?> GetByCnpjAsync(string cnpj);
    Task<Courier?> GetByCnhNumberAsync(string cnhNumber);
}

using Desafio_BackEnd.Domain.Enums;

namespace Desafio_BackEnd.Domain.Entities;

public class Courier
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; } = string.Empty;
    public ECnhType CnhType { get; set; }
    public string CnhImagePath { get; set; } = string.Empty;
    public ICollection<Rental>? Rentals { get; set; }
}

namespace Desafio_BackEnd.Domain.Entities;

public class Motorcycle
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public string Model { get; set; } = string.Empty;
    public string Plate { get; set; } = string.Empty;
    public ICollection<Rental>? Rentals { get; set; }
}

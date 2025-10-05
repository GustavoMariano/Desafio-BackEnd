namespace Desafio_BackEnd.Application.DTOs;

public class MotorcycleDto
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public string Model { get; set; } = string.Empty;
    public string Plate { get; set; } = string.Empty;
}

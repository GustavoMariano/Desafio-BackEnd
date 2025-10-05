namespace Desafio_BackEnd.Application.DTOs;

public class RentalDto
{
    public Guid Id { get; set; }
    public Guid CourierId { get; set; }
    public Guid MotorcycleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal DailyRate { get; set; }
}

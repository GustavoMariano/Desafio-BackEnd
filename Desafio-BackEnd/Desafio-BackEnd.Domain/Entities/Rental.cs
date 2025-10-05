namespace Desafio_BackEnd.Domain.Entities;

public class Rental
{
    public Guid Id { get; set; }
    public Guid CourierId { get; set; }
    public Guid MotorcycleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal DailyRate { get; set; }
    public Courier? Courier { get; set; }
    public Motorcycle? Motorcycle { get; set; }
}

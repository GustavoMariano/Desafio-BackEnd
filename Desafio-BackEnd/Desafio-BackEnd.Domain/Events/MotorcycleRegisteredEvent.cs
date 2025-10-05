namespace Desafio_BackEnd.Domain.Events;

public class MotorcycleRegisteredEvent
{
    public Guid MotorcycleId { get; set; }
    public int Year { get; set; }

    public MotorcycleRegisteredEvent(Guid motorcycleId, int year)
    {
        MotorcycleId = motorcycleId;
        Year = year;
    }
}

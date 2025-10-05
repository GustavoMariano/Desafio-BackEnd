using Desafio_BackEnd.Domain.Enums;

namespace Desafio_BackEnd.Application.DTOs;

public class CourierDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; } = string.Empty;
    public ECnhType CnhType { get; set; }
    public string? CnhImagePath { get; set; }
}

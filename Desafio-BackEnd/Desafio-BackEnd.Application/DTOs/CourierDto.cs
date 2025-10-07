using Desafio_BackEnd.Domain.Enums;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Application.DTOs;

public class CourierDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; } = string.Empty;

    [JsonPropertyName("cnhType")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ECnhType CnhType { get; set; }
    public string? CnhImagePath { get; set; }
}

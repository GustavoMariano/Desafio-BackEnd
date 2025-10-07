using Desafio_BackEnd.Application.DTOs;
using Desafio_BackEnd.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotorcycleController : ControllerBase
{
    private readonly MotorcycleService _service;

    public MotorcycleController(MotorcycleService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MotorcycleDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? plate = null)
    {
        var motorcycles = await _service.GetAllAsync(plate);
        return Ok(motorcycles);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var moto = await _service.GetByIdAsync(id);
        return Ok(moto);
    }

    [HttpPut("{id:guid}/plate")]
    public async Task<IActionResult> UpdatePlate(Guid id, [FromBody] string newPlate)
    {
        await _service.UpdatePlateAsync(id, newPlate);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}

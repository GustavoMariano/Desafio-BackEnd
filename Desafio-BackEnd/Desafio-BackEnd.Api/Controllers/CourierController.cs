using Desafio_BackEnd.Application.DTOs;
using Desafio_BackEnd.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourierController : ControllerBase
{
    private readonly CourierService _service;

    public CourierController(CourierService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourierDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpPost("{courierId:guid}/cnh")]
    public async Task<IActionResult> UploadCnh(Guid courierId, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required.");

        if (!new[] { ".png", ".bmp" }.Contains(Path.GetExtension(file.FileName).ToLower()))
            return BadRequest("Only PNG or BMP files are allowed.");

        await using var stream = file.OpenReadStream();
        var path = await _service.UpdateCnhImageAsync(courierId, stream, file.FileName);

        return Ok(new { filePath = path });
    }
}

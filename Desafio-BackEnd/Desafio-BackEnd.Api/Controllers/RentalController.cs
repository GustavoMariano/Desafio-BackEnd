using Desafio_BackEnd.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalController : ControllerBase
{
    private readonly RentalService _service;

    public RentalController(RentalService service)
    {
        _service = service;
    }

    [HttpPost("rent")]
    public async Task<IActionResult> Rent(Guid courierId, Guid motorcycleId, int planDays)
    {
        var rental = await _service.RentAsync(courierId, motorcycleId, planDays);
        return Ok(rental);
    }

    [HttpPost("return/{rentalId:guid}")]
    public async Task<IActionResult> Return(Guid rentalId, [FromQuery] DateTime returnDate)
    {
        var total = await _service.ReturnAsync(rentalId, returnDate);
        return Ok(new { total });
    }
}

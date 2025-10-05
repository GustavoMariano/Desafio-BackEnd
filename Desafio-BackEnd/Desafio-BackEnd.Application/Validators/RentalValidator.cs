using Desafio_BackEnd.Application.DTOs;
using FluentValidation;

namespace Desafio_BackEnd.Application.Validators;

public class RentalValidator : AbstractValidator<RentalDto>
{
    public RentalValidator()
    {
        RuleFor(x => x.CourierId).NotEmpty();
        RuleFor(x => x.MotorcycleId).NotEmpty();
        RuleFor(x => x.ExpectedEndDate).GreaterThan(x => x.StartDate);
    }
}

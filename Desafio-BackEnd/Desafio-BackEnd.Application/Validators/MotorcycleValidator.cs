using Desafio_BackEnd.Application.DTOs;
using FluentValidation;

namespace Desafio_BackEnd.Application.Validators;

public class MotorcycleValidator : AbstractValidator<MotorcycleDto>
{
    public MotorcycleValidator()
    {
        RuleFor(x => x.Year).GreaterThan(2000);
        RuleFor(x => x.Model).NotEmpty();
        RuleFor(x => x.Plate).NotEmpty().MaximumLength(10);
    }
}

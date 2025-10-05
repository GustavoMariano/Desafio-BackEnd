using Desafio_BackEnd.Application.DTOs;
using FluentValidation;

namespace Desafio_BackEnd.Application.Validators;

public class CourierValidator : AbstractValidator<CourierDto>
{
    public CourierValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Cnpj).NotEmpty().Matches(@"^\d{14}$").WithMessage("CNPJ must contain 14 digits.");
        RuleFor(x => x.CnhNumber).NotEmpty();
    }
}

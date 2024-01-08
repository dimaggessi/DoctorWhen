using DoctorWhen.Communication.Requests;
using DoctorWhen.Exception;
using FluentValidation;

namespace DoctorWhen.Application.Validators;
public class PacienteRegisterValidator : AbstractValidator<RequestPacienteJson>
{
    public PacienteRegisterValidator()
    {
        RuleFor(r => r.Nome).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_NAME);
        RuleFor(r => r.Idade).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_IDADE);
        RuleFor(r => r.Endereco).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_ENDERECO);
        RuleFor(r => r.Email).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_EMAIL);
        RuleFor(r => r.DataNascimento).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_DATA);

        When(u => !string.IsNullOrWhiteSpace(u.Email), () => { RuleFor(u => u.Email).EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL); });

        When(u => !string.IsNullOrWhiteSpace(u.Nome), () => { RuleFor(r => r.Nome.Length).GreaterThanOrEqualTo(3).WithMessage(ResourceErrorMessages.NAME_LENGTH); });
    }
}

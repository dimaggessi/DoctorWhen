using DoctorWhen.Communication.Requests;
using DoctorWhen.Exception;
using FluentValidation;

namespace DoctorWhen.Application.Validators;
public class MedicoRegisterValidator : AbstractValidator<RequestMedicoJson>
{
    public MedicoRegisterValidator()
    {
        RuleFor(r => r.Nome).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_NAME);
        RuleFor(r => r.Especialidade).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_ESPECIALIDADE);
        RuleFor(r => r.Email).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_EMAIL);

        When(u => !string.IsNullOrWhiteSpace(u.Email), () => { RuleFor(u => u.Email).EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL); });

        When(u => !string.IsNullOrWhiteSpace(u.Nome), () => { RuleFor(r => r.Nome.Length).GreaterThanOrEqualTo(3).WithMessage(ResourceErrorMessages.NAME_LENGTH); });
    }
}

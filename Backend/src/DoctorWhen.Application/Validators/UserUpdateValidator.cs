using DoctorWhen.Communication.Requests;
using DoctorWhen.Exception;
using FluentValidation;

namespace DoctorWhen.Application.Validators;
public class UserUpdateValidator : AbstractValidator<RequestUserUpdateJson>
{
    public UserUpdateValidator()
    {
        // Validar se os campos estão vazios.
        RuleFor(r => r.Nome).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_NAME);
        RuleFor(r => r.UserName).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_USERNAME);
        RuleFor(r => r.Email).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_EMAIL);
        RuleFor(r => r.NewPassword).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PASSWORD);

        // Condição "formato de email válido", quando o e-mail não for nulo ou espaços vazios.
        When(u => !string.IsNullOrWhiteSpace(u.Email), () => { RuleFor(u => u.Email).EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL); });

        // Condição "tamanho da senha válido", quando a senha não for nula ou espaços vazios.
        When(u => !string.IsNullOrWhiteSpace(u.NewPassword), () => { RuleFor(r => r.NewPassword.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceErrorMessages.INVALID_PASSWORD); });
        // Condição "nome" ter 3 ou mais caracteres
        When(u => !string.IsNullOrWhiteSpace(u.Nome), () => { RuleFor(r => r.Nome.Length).GreaterThanOrEqualTo(3).WithMessage(ResourceErrorMessages.NAME_LENGTH); });
    }
}

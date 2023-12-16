namespace DoctorWhen.Application.Interfaces;
public interface ITokenConfigurator
{
    Task<string> GetToken(Domain.Identity.User user);
}

namespace DoctorWhen.Domain.Repositories;
public interface IAtendenteRepository : IGeneralRepository
{
    Task<Entities.Atendente> GetByIdAsync(long id);
    Task<Entities.Atendente> GetByEmailAsync(string email);
}

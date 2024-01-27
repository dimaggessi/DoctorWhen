namespace DoctorWhen.Domain.Repositories;
public interface IAtendenteRepository : IGenericRepository
{
    Task<Entities.Atendente> GetByIdAsync(long id);
    Task<Entities.Atendente> GetByEmailAsync(string email);
    Task DeleteCascade(long id);
}

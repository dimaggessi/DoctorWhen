namespace DoctorWhen.Domain.Repositories;
public interface IMedicoRepository : IGeneralRepository
{   
    Task<Entities.Medico> GetMedicoByIdAsync(long id, bool includeConsultas = false);
    Task<Entities.Medico> GetMedicoByEmail(string email, bool includeConsultas = false);
    Task<IList<Entities.Medico>> GetAllMedicosAsync();
    Task<IList<Entities.Medico>> GetAllMedicosByEspecialidadeAsync(Enum.Especialidade especialidade);
}
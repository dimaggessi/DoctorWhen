namespace DoctorWhen.Domain.Repositories;
public interface IMedicoRepository
{   
    Task<Entities.Medico> GetMedicoByIdAsync(long id, bool includeConsultas);
    Task<IList<Entities.Medico>> GetAllMedicosAsync();
    Task<IList<Entities.Medico>> GetAllMedicosByEspecialidadeAsync(Enum.Especialidade especialidade);
}
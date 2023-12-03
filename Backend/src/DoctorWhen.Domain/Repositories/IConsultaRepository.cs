namespace DoctorWhen.Domain.Repositories;
public interface IConsultaRepository
{
    Task<IList<Entities.Consulta>> GetAllConsultasByDateAsync(DateTime date);
    Task<IList<Entities.Consulta>> GetAllConsultasByPacienteAsync(long pacienteId);
    Task<IList<Entities.Consulta>> GetAllConsultasByMedicoAsync(long medicoId);
    Task<Entities.Consulta> GetConsultaByIdAsync(long consultaId);
}

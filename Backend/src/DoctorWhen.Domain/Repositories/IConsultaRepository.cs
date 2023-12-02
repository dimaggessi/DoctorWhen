namespace DoctorWhen.Domain.Repositories;
public interface IConsultaRepository
{
    Task<IList<Entities.Consulta>> GetAllConsultasByDate(DateTime date);
    Task<IList<Entities.Consulta>> GetAllConsultasByPaciente(long pacienteId);
    Task<IList<Entities.Consulta>> GetAllConsultasByMedico(long medicoId);
    Task<Entities.Consulta> GetConsultaById(long consultaId);
}

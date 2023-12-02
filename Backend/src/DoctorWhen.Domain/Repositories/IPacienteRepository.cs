namespace DoctorWhen.Domain.Repositories;
public interface IPacienteRepository
{
    Task<Entities.Paciente> GetPacienteById(long id);
    Task<Entities.Paciente> GetPacienteByEmail(string email);
}

namespace DoctorWhen.Domain.Repositories;
public interface IPacienteRepository
{
    Task<Entities.Paciente> GetPacienteByIdAsync(long id);
    Task<Entities.Paciente> GetPacienteByEmailAsync(string email);
}

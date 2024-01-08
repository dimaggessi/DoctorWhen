namespace DoctorWhen.Domain.Repositories;
public interface IPacienteRepository : IGeneralRepository
{
    Task<Entities.Paciente> GetPacienteByIdAsync(long id);
    Task<Entities.Paciente> GetPacienteByEmailAsync(string email);
}

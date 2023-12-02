namespace DoctorWhen.Domain.Entities;
public class Medico : EntidadeBase
{
    public string Nome { get; set; }
    public string Especialidade { get; set;}
    public string Email { get; set; }
    public ICollection<Paciente> Pacientes { get; set; }
    public ICollection<Consulta> Consultas { get; set; }
}
namespace DoctorWhen.Domain.Entities;
public class Atendente : EntidadeBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public ICollection<Paciente> PacientesAtendidos { get; set; }
}
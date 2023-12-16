using DoctorWhen.Domain.Identity;

namespace DoctorWhen.Domain.Entities;
public class Atendente : EntidadeBase
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Nome { get; set; }
    public ICollection<Paciente> PacientesAtendidos { get; set; }
}
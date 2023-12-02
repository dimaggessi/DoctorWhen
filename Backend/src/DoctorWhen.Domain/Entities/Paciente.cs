namespace DoctorWhen.Domain.Entities;
public class Paciente : EntidadeBase
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public int Idade { get; set; }
    public string Endereco { get; set; }
    public string Email { get; set; }
    public ICollection<Atendente> Atendentes { get; set; }
    public ICollection<Medico> Medicos { get; set; }
    public ICollection<Consulta> Consultas { get; set; }
}

namespace DoctorWhen.Domain.Entities;
public class Consulta : EntidadeBase
{
    public DateTimeOffset DataConsulta { get; set; }
    public Medico Medico { get; set; }
    public Paciente Paciente { get; set; }
    public Atendente Atendente { get; set; }
    ICollection<Prescricao> Prescricoes { get; set; }
}

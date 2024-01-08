namespace DoctorWhen.Domain.Entities;
public class Prescricao : EntidadeBase
{
    public Consulta Consulta { get; set; }
    public string Receita { get; set; }
}

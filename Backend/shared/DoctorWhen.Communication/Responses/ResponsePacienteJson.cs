namespace DoctorWhen.Communication.Responses;
public class ResponsePacienteJson
{
    public string PacienteId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public List<ResponseAtendenteJson> Atendentes { get; set; }
    public List<ResponseConsultaJson> Consultas { get; set; }
    public List<ResponseMedicoJson> Medicos { get; set; }
}

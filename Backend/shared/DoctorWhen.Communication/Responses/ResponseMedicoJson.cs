namespace DoctorWhen.Communication.Responses;
public class ResponseMedicoJson
{
    public string MedicoId { get; set; }
    public string Nome { get; set; }
    public string Especialidade { get; set; }
    public string Email { get; set; }
    public List<ResponseConsultaJson> Consultas { get; set;}
}

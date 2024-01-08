namespace DoctorWhen.Communication.Responses;
public class ResponseAtendenteJson
{
    public string Nome { get; set; }
    public long UserId { get; set; }
    public List<ResponsePacienteJson> PacientesAtendidos { get; set; }
}

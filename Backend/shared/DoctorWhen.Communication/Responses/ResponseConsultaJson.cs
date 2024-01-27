namespace DoctorWhen.Communication.Responses;
public class ResponseConsultaJson
{
    public string ConsultaId { get; set; }
    public string PacienteId { get; set; }
    public string NomePaciente { get; set; }
    public string MedicoId { get; set; }
    public string NomeMedico { get; set; }
    public string DataConsulta { get; set; }
    public List<ResponsePrescricaoJson> Receitas { get; set; }
}

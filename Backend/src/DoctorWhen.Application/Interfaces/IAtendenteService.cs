using DoctorWhen.Communication.Requests;
using DoctorWhen.Communication.Responses;

namespace DoctorWhen.Application.Interfaces;
public interface IAtendenteService
{

    Task<ResponsePacienteJson> GetPacienteByEmailAsync(string email);
    Task<ResponseMedicoJson> GetMedicoByIdAsync(string id);
    Task<ResponseMedicoListJson> GetAllMedicosAsync();
    Task<ResponseConsultaJson> GetConsultaAsync(RequestConsultaJson request);
    Task<ResponseConsultaListJson> GetAllConsultasByPacienteIdAsync(string id);
    Task<ResponseMedicoJson> CreateMedicoAsync(RequestMedicoJson request);
    Task<ResponsePacienteJson> CreatePacienteAsync(RequestPacienteJson request, long id);
    Task<ResponseConsultaJson> CreateConsultaAsync(RequestConsultaRegisterJson request, long id);
    Task<ResponsePrescricaoJson> CreatePrescricaoAsync(RequestPrescricaoJson request);
}

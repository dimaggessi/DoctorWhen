using AutoMapper;

namespace DoctorWhen.Application.Utilities.AutoMapper;
public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Communication.Requests.RequestUserJson, Domain.Identity.User>()
           .ForMember(destinationMember => destinationMember.Email, memberOptions => memberOptions.MapFrom(request => request.Email))
           .ForMember(destinationMember => destinationMember.UserName, memberOptions => memberOptions.MapFrom(request => request.UserName))
           .ForSourceMember(sourceMember => sourceMember.Password, memberOptions => memberOptions.DoNotValidate())
           .ForSourceMember(sourceMember => sourceMember.Nome, memberOptions => memberOptions.DoNotValidate());

        CreateMap<Communication.Requests.RequestUserUpdateJson, Domain.Identity.User>()
           .ForMember(destinationMember => destinationMember.Email, memberOptions => memberOptions.MapFrom(request => request.Email))
           .ForMember(destinationMember => destinationMember.UserName, memberOptions => memberOptions.MapFrom(request => request.UserName))
           .ForSourceMember(sourceMember => sourceMember.CurrentPassword, memberOptions => memberOptions.DoNotValidate())
           .ForSourceMember(sourceMember => sourceMember.NewPassword, memberOptions => memberOptions.DoNotValidate())
           .ForSourceMember(sourceMember => sourceMember.Nome, memberOptions => memberOptions.DoNotValidate());

        CreateMap<Domain.Identity.User, Communication.Responses.ResponseUserJson>()
            .ForMember(response => response.UserName, memberOptions => memberOptions.MapFrom(user => user.UserName))
            .ForMember(response => response.Email, memberOptions => memberOptions.MapFrom(user => user.Email))
            .ForMember(response => response.UserId, memberOptions => memberOptions.MapFrom(user => user.Id));

        CreateMap<Domain.Entities.Paciente, Communication.Responses.ResponsePacienteJson>()
            .ForMember(response => response.PacienteId, memberOptions => memberOptions.MapFrom(p => p.Id))
            .ForMember(response => response.Nome, memberOptions => memberOptions.MapFrom(p => p.Nome))
            .ForMember(response => response.Email, memberOptions => memberOptions.MapFrom(p => p.Email))
            .ForMember(response => response.Atendentes, memberOptions => memberOptions.MapFrom(p => p.Atendentes))
            .ForMember(response => response.Consultas, memberOptions => memberOptions.MapFrom(p => p.Consultas))
            .ForMember(response => response.Medicos, memberOptions => memberOptions.MapFrom(p => p.Medicos));

        CreateMap<Domain.Entities.Atendente, Communication.Responses.ResponseAtendenteJson>()
            .ForMember(response => response.Nome, memberOptions => memberOptions.MapFrom(a => a.Nome))
            .ForMember(response => response.UserId, memberOptions => memberOptions.MapFrom(a => a.UserId))
            .ForSourceMember(sourceMember => sourceMember.PacientesAtendidos, memberOptions => memberOptions.DoNotValidate());

        CreateMap<Domain.Entities.Medico, Communication.Responses.ResponseMedicoJson>()
            .ForMember(destinationMember => destinationMember.MedicoId, memberOptions => memberOptions.MapFrom(m => m.Id))
            .ForMember(destinationMember => destinationMember.Nome, memberOptions => memberOptions.MapFrom(m => m.Nome))
            .ForMember(destinationMember => destinationMember.Especialidade, memberOptions => memberOptions.MapFrom(m => m.Especialidade))
            .ForMember(destinationMember => destinationMember.Email, memberOptions => memberOptions.MapFrom(m => m.Email))
            .ForMember(destinationMember => destinationMember.Consultas, memberOptions => memberOptions.MapFrom(m => m.Consultas));

        CreateMap<Domain.Entities.Consulta, Communication.Responses.ResponseConsultaJson>()
            .ForMember(destinationMember => destinationMember.DataConsulta, memberOptions => memberOptions.MapFrom(c => c.DataConsulta))
            .ForMember(destinationMember => destinationMember.MedicoId, memberOptions => memberOptions.MapFrom(c => c.Medico.Id))
            .ForMember(destinationMember => destinationMember.NomeMedico, memberOptions => memberOptions.MapFrom(c => c.Medico.Nome))
            .ForMember(destinationMember => destinationMember.PacienteId, memberOptions => memberOptions.MapFrom(c => c.Paciente.Id))
            .ForMember(destinationMember => destinationMember.NomePaciente, memberOptions => memberOptions.MapFrom(c => c.Paciente.Nome))
            .ForMember(destinationMember => destinationMember.Receitas, memberOptions => memberOptions.Ignore());

        CreateMap<Domain.Entities.Prescricao, Communication.Responses.ResponsePrescricaoJson>()
            .ForMember(destinationMember => destinationMember.Receita, memberOptions => memberOptions.MapFrom(p => p.Receita));




    }
}
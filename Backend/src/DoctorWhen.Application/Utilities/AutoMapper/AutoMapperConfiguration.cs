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
    }
}

using DoctorWhen.Communication.Requests;
using DoctorWhen.Communication.Responses;

namespace DoctorWhen.Application.Interfaces;
public interface IUserService
{
    Task<ResponseUserJson> GetUserById(long id);
    Task<ResponseUserJson> GetUserByEmail(RequestEmailJson request);
    Task<ResponseLoginJson> LoginAsync(RequestLoginJson request);
    Task<ResponseUserJson> CreateAccount(RequestUserJson request);
    Task<ResponseUserJson> UpdateAccount(RequestUserUpdateJson request);
    Task DeleteAsync(long id);
}

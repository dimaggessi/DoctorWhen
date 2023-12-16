using DoctorWhen.Communication.Requests;
using DoctorWhen.Communication.Responses;
using Microsoft.AspNetCore.Identity;

namespace DoctorWhen.Application.Interfaces;
public interface IUserService
{
    Task<ResponseUserJson> GetUserById(long id);
    Task<ResponseUserJson> GetUserByEmail(RequestEmailJson request);
    Task<ResponseLoginJson> LoginAsync(RequestLoginJson request);
    Task<ResponseUserJson> CreateAccount(RequestUserJson request);
    Task<ResponseUserJson> UpdateAccount(RequestUserUpdateJson request, long id);
    Task DeleteAsync(long id);
}

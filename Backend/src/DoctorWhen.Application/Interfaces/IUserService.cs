using DoctorWhen.Communication.Requests;
using DoctorWhen.Communication.Responses;
using Microsoft.AspNetCore.Identity;

namespace DoctorWhen.Application.Interfaces;
public interface IUserService
{
    Task<bool> CheckIfUserExists(string email);
    Task<ResponseUserJson> GetUserById(long id);
    Task<ResponseUserJson> GetUserByEmail(string email);
    Task<SignInResult> SignInAsync(RequestLoginJson request);
    Task<ResponseUserJson> CreateAccount(RequestUserJson request);
    Task<ResponseUserJson> UpdateAccount(RequestUserJson request);
}

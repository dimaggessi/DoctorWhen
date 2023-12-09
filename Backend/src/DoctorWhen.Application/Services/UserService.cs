using AutoMapper;
using DoctorWhen.Application.Interfaces;
using DoctorWhen.Application.Validators;
using DoctorWhen.Communication.Requests;
using DoctorWhen.Communication.Responses;
using DoctorWhen.Domain.Identity;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Exception;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoctorWhen.Application.Services;
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(UserManager<User> userManager,
                       SignInManager<User> signInManager,
                       IMapper mapper,
                       IUserRepository userRepository,
                       IUnitOfWork unitOfWork)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._mapper = mapper;
        this._userRepository = userRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task<bool> CheckIfUserExists(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        
        return user != null;
    }

    public async Task<ResponseUserJson> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new InvalidUserException(ResourceErrorMessages.USER_NOT_EXISTS);
        }

        return _mapper.Map<ResponseUserJson>(user);
    }

    public async Task<ResponseUserJson> GetUserById(long id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new InvalidUserException(ResourceErrorMessages.USER_NOT_EXISTS);
        }

        return _mapper.Map<ResponseUserJson>(user);
    }

    public async Task<SignInResult> SignInAsync(RequestLoginJson request)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == request.Email.ToLower());

        if (user == null)
        {
            throw new InvalidUserException(ResourceErrorMessages.INVALID_LOGIN);
        }

        // passa "false" como parâmetro para ele não bloquear o usuário caso haja falha no login
        return await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
    }

    public async Task<ResponseUserJson> CreateAccount(RequestUserJson request)
    {
        await RequestValidation(request);

        User user = _mapper.Map<User>(request);
        var existingUser = _userManager.FindByEmailAsync(user.Email);
        ResponseUserJson userToReturn = new();

        if (existingUser == null)
        {
            var result = await _userManager.CreateAsync(user, request.Senha);
            if (result.Succeeded)
            {
                userToReturn = _mapper.Map<ResponseUserJson>(result);
            }
            else
            {
                throw new InvalidUserException(ResourceErrorMessages.USER_ALREADY_EXISTS);
            }
        }

        return userToReturn;
    }

    public async Task<ResponseUserJson> UpdateAccount(RequestUserJson request)
    {
        await RequestValidation(request);

        var user = await _userRepository.GetUserByEmailAsync(request.Email.ToLower());
        if (user == null) throw new InvalidUserException(ResourceErrorMessages.USER_NOT_EXISTS);

        user = _mapper.Map<User>(request);
        _userRepository.Update(user);

        return _mapper.Map<ResponseUserJson>(user);
    }

    private async static Task RequestValidation(RequestUserJson request)
    {
        var validator = new UserRegisterValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidatorErrorsException(errorMessage);
        }
    }
}

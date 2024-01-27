using AutoMapper;
using DoctorWhen.Application.Interfaces;
using DoctorWhen.Application.Validators;
using DoctorWhen.Communication.Requests;
using DoctorWhen.Communication.Responses;
using DoctorWhen.Domain.Entities;
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
    private readonly ITokenConfigurator _tokenConfigurator;
    private readonly RoleManager<Role> _roleManager;
    private readonly IAtendenteRepository _atendenteRepository;

    public UserService(UserManager<User> userManager,
                       SignInManager<User> signInManager,
                       IMapper mapper,
                       IUserRepository userRepository,
                       IUnitOfWork unitOfWork,
                       ITokenConfigurator tokenConfigurator,
                       RoleManager<Role> roleManager,
                       IAtendenteRepository atendenteRepository)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._mapper = mapper;
        this._userRepository = userRepository;
        this._unitOfWork = unitOfWork;
        this._tokenConfigurator = tokenConfigurator;
        this._roleManager = roleManager;
        this._atendenteRepository = atendenteRepository;
    }

    public async Task<ResponseUserJson> GetUserByEmail(RequestEmailJson request)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email.ToLower());
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

    public async Task<ResponseLoginJson> LoginAsync(RequestLoginJson request)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == request.Email.ToLower());

        if (user == null) { throw new InvalidUserException(ResourceErrorMessages.INVALID_LOGIN); }

        // passa "false" como parâmetro para ele não bloquear o usuário caso haja falha no login
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
           throw new InvalidUserException(ResourceErrorMessages.INCORRECT_PASSWORD);
        }

        return new ResponseLoginJson
        {
            UserName = user.UserName,
            Token = _tokenConfigurator.GetToken(user).Result
        };
    }

    public async Task<ResponseUserJson> CreateAccount(RequestUserJson request)
    {
        await RequestCreateUserValidation(request);

        User user = _mapper.Map<User>(request);
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email.ToLower());

        if (existingUser != null) throw new InvalidUserException(ResourceErrorMessages.USER_ALREADY_EXISTS);

        // o AutoMapper está configurado em API.Services para ignorar o Password na hora de mapear
        // tendo em vista que o UserManager será responsável por gerá-lo
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) throw new InvalidUserException(ResourceErrorMessages.USER_PERSIST_ERROR);        

        var userToReturn = await _userRepository.GetUserByEmailAsync(request.Email.ToLower());

        var atendente = new Atendente
        {
            UserId = userToReturn.Id,
            User = userToReturn,
            Nome = request.Nome,
        };

        _atendenteRepository.Add(atendente);

        // cadastra o usuário como Atendente
        await _userManager.AddToRoleAsync(userToReturn, "Atendente");
        
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseUserJson>(userToReturn);
    }

    public async Task<ResponseUserJson> UpdateAccount(RequestUserUpdateJson request)
    {
        await RequestUpdateUserValidation(request);

        var user = await _userRepository.GetUserByEmailAsync(request.Email.ToLower());
        if (user == null) throw new InvalidUserException(ResourceErrorMessages.USER_NOT_EXISTS);

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (result.Succeeded)
        {
            user.UserName = request.UserName;
            _userRepository.Update(user);

            await _unitOfWork.Commit();
        }
        else
        {
            throw new InvalidUserException(ResourceErrorMessages.USER_PASSWORD_RESET_ERROR);
        }

        var atendente = await _atendenteRepository.GetByEmailAsync(request.Email.ToLower());
        atendente.Nome = request.Nome;

        _atendenteRepository.Update(atendente);

        await _unitOfWork.Commit();

        var updatedUser = await _userRepository.GetUserByEmailAsync(request.Email.ToLower());
        
        return _mapper.Map<ResponseUserJson>(updatedUser);
    }

    public async Task DeleteAsync(long id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null) throw new InvalidUserException(ResourceErrorMessages.USER_NOT_EXISTS);

            await _atendenteRepository.DeleteCascade(user.Id);

            var result = await _userManager.DeleteAsync(user);

            await _unitOfWork.Commit();
    }

    private async static Task RequestCreateUserValidation(RequestUserJson request)
    {
        var validator = new UserRegisterValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidatorErrorsException(errorMessage);
        }
    }

    private async static Task RequestUpdateUserValidation(RequestUserUpdateJson request)
    {
        var validator = new UserUpdateValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidatorErrorsException(errorMessage);
        }
    }
}

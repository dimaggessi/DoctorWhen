using DoctorWhen.Application.Interfaces;
using DoctorWhen.Communication.Requests;
using DoctorWhen.Communication.Responses;
using DoctorWhen.Domain.Identity;
using DoctorWhen.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoctorWhen.API.Controllers;

/// <summary>
/// Apenas o usuário com o status de Administrador tem acesso a esse controller.
/// </summary>
public class AdminController : DoctorWhenController
{
    [HttpGet]
    [Route("atendente/{id}")]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserByIdAsync([FromServices] IUserService userService, long id)
    {
        VerifyPermission();

        var response = await userService.GetUserById(id);

        return Ok(response);
    }

    [HttpPost]
    [Route("atendente/email")]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserByEmailAsync([FromServices] IUserService userService,
                                                         [FromBody] RequestEmailJson request)
    {
        VerifyPermission();

        var response = await userService.GetUserByEmail(request);

        return Ok(response);
    }

    [HttpPost]
    [Route("atendente/register")]
    /* É importante colocar o "Tipo de Resposta" aqui, pois o desenvolvedor do front-end vai saber
     exatamente o que esperar como resposta. */
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromServices] IUserService userService,
                                              [FromBody] RequestUserJson request)
    {
        VerifyPermission();

        var response = await userService.CreateAccount(request);

        return Created(string.Empty, response);
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromServices] IUserService userService,
                                           [FromBody] RequestLoginJson request) 
    {
        var response = await userService.LoginAsync(request);

        return Ok(response);
    }

    [HttpPut]
    [Route("atendente/update/{id}")]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromServices] IUserService userService,
                                            [FromBody] RequestUserUpdateJson request,
                                            long id)
    {
        VerifyPermission();

        var response = await userService.UpdateAccount(request, id);

        return NoContent();
    }

    [HttpDelete]
    [Route("atendente/delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete([FromServices] IUserService userService,
                                            long id)
    {
        VerifyPermission();

        await userService.DeleteAsync(id);

        return Ok("Usuário excluído com sucesso.");
    }

    private void VerifyPermission()
    {
        if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == Role.Admin))
        {
            throw new UnauthorizedUserException(ResourceErrorMessages.UNAUTHORIZED);
        }
    }
}
using DoctorWhen.API.Extensions;
using DoctorWhen.Application.Interfaces;
using DoctorWhen.Communication.Requests;
using DoctorWhen.Communication.Responses;
using DoctorWhen.Domain.Identity;
using DoctorWhen.Exception;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoctorWhen.API.Controllers;

/// <summary>
/// Apenas o usuário com a "role" de Administrador tem acesso a esse controller.
/// </summary>
public class AtendenteController : DoctorWhenController
{
    /// <summary>
    /// Efetua o login de usuário.
    /// </summary>
    /// <param name="request">Dados de login</param>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromServices] IUserService userService,
                                           [FromBody] RequestLoginJson request) 
    {
        var response = await userService.LoginAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Atualiza o Nome, UserName e Senha de um usuário registrado como atendente.
    /// </summary>
    /// <param name="request">Dados do usuário a ser atualizado</param>
    [HttpPut]
    [Route("update")]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromServices] IUserService userService,
                                            [FromBody] RequestUserUpdateJson request)
    {
        VerifyPermission();

        var response = await userService.UpdateAccount(request);

        return Ok(response);
    }

    /// <summary>
    /// Registra um paciente.
    /// </summary>
    /// <param name="request">Dados do paciente</param>
    [HttpPost]
    [Route("register/paciente")]
    [ProducesResponseType(typeof(ResponsePacienteJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreatePaciente([FromServices] IAtendenteService atendenteService,
                                                    [FromBody] RequestPacienteJson request)
    {
        VerifyPermission();
        long id = User.GetUserId();
        var response = await atendenteService.CreatePacienteAsync(request, id);

        return Ok(response);
    }

    /// <summary>
    /// Registra um médico.
    /// </summary>
    /// <param name="request">Dados do médico</param>
    [HttpPost]
    [Route("register/medico")]
    [ProducesResponseType(typeof(ResponseMedicoJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateMedico([FromServices] IAtendenteService atendenteService,
                                                [FromBody] RequestMedicoJson request)
    {
        VerifyPermission();
        var response = await atendenteService.CreateMedicoAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Registra uma consulta médica.
    /// </summary>
    /// <param name="request">Dados da consulta médica</param>
    [HttpPost]
    [Route("register/consulta")]
    [ProducesResponseType(typeof(ResponseConsultaJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateConsulta([FromServices] IAtendenteService atendenteService,
                                                    [FromBody] RequestConsultaRegisterJson request)
    {
        VerifyPermission();
        long id = User.GetUserId();
        var response = await atendenteService.CreateConsultaAsync(request, id);

        return Ok(response);
    }

    /// <summary>
    /// Registra uma receita e vincula a uma consulta médica.
    /// </summary>
    /// <param name="request">Dados da receita médica</param>
    [HttpPost]
    [Route("register/prescricao")]
    [ProducesResponseType(typeof(ResponsePrescricaoJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreatePrescricao([FromServices] IAtendenteService atendenteService,
                                                      [FromBody] RequestPrescricaoJson request)
    {
        VerifyPermission();
        var response = await atendenteService.CreatePrescricaoAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Obtém o paciente vinculado ao e-mail informado.
    /// </summary>
    [HttpPost]
    [Route("get/paciente/email")]
    [ProducesResponseType(typeof(ResponsePacienteJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPacienteByEmailAsync([FromServices] IAtendenteService atendenteService,
                                                             [FromBody] RequestEmailJson request)
    {
        VerifyPermission();
        var response = await atendenteService.GetPacienteByEmailAsync(request.Email);

        return Ok(response);
    }

    /// <summary>
    /// Obtém o médico vinculado ao ID informado.
    /// </summary>
    [HttpGet]
    [Route("get/medico/{id}")]
    [ProducesResponseType(typeof(ResponseMedicoJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMedicoByIdAsync([FromServices] IAtendenteService atendenteService, string id)
    {
        VerifyPermission();
        var response = await atendenteService.GetMedicoByIdAsync(id);

        return Ok(response);
    }

    /// <summary>
    /// Obtém uma lista de médicos registrados.
    /// </summary>
    [HttpGet]
    [Route("get/medico/all")]
    [ProducesResponseType(typeof(ResponseMedicoListJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMedicosAsync([FromServices] IAtendenteService atendenteService)
    {
        VerifyPermission();
        var response = await atendenteService.GetAllMedicosAsync();

        return Ok(response);
    }

    /// <summary>
    /// Obtém todas as consultas médicas vinculadas ao ID do paciente.
    /// </summary>
    [HttpGet]
    [Route("get/consulta/paciente/{id}")]
    [ProducesResponseType(typeof(ResponseConsultaListJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllConsultaByPacienteId([FromServices] IAtendenteService atendenteService,
                                                                string id)
    {
        VerifyPermission();
        var response = await atendenteService.GetAllConsultasByPacienteIdAsync(id);

        return Ok(response);
    }

    /// <summary>
    /// Obtém os dados completos da consulta médica registrada.
    /// </summary>
    /// <param name="request">Dados de requisição da consulta</param>
    [HttpPost]
    [Route("get/consulta")]
    [ProducesResponseType(typeof(ResponseConsultaJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConsultaId([FromServices] IAtendenteService atendenteService,
                                                   [FromBody] RequestConsultaJson request)
    {
        VerifyPermission();
        var response = await atendenteService.GetConsultaAsync(request);

        return Ok(response);
    }

    private void VerifyPermission()
    {
        if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == Role.Atendente))
        {
            throw new UnauthorizedUserException(ResourceErrorMessages.UNAUTHORIZED_ATENDENTE);
        }
    }
}
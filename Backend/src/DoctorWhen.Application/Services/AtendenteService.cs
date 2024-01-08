using AutoMapper;
using DoctorWhen.Application.Interfaces;
using DoctorWhen.Application.Validators;
using DoctorWhen.Communication.Requests;
using DoctorWhen.Communication.Responses;
using DoctorWhen.Domain.Entities;
using DoctorWhen.Domain.Enum;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Exception;
using System.Globalization;

namespace DoctorWhen.Application.Services;
public class AtendenteService : IAtendenteService
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IMedicoRepository _medicoRepository;
    private readonly IConsultaRepository _consultaRepository;
    private readonly IPrescricaoRepository _prescricaoRepository;
    private readonly IAtendenteRepository _atendenteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AtendenteService(IPacienteRepository pacienteRepository,
                            IMedicoRepository medicoRepository,
                            IConsultaRepository consultaRepository,
                            IPrescricaoRepository prescricaoRepository,
                            IAtendenteRepository atendenteRepository,
                            IUnitOfWork unitOfWork,
                            IMapper mapper)
    {
        this._pacienteRepository = pacienteRepository;
        this._medicoRepository = medicoRepository;
        this._consultaRepository = consultaRepository;
        this._prescricaoRepository = prescricaoRepository;
        this._atendenteRepository = atendenteRepository;
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }

    public async Task<ResponseConsultaJson> CreateConsultaAsync(RequestConsultaJson request, long atendenteUserId)
    {
        var date = request.DataConsulta;

        if (!DateTimeOffset.TryParse(date, out DateTimeOffset dateTimeOffset))
        {
            throw new InvalidOperationException(ResourceErrorMessages.INCORRECT_DATE);
        }

        var consulta = await _consultaRepository.GetConsultaByDate(DateTimeOffset.Parse(date));
        if (consulta != null) { throw new InvalidOperationException(ResourceErrorMessages.ALREADY_EXISTS_CONSULTA); }

        var medico = await _medicoRepository.GetMedicoByIdAsync(long.Parse(request.MedicoId), false);
        if (medico == null) { throw new InvalidOperationException(ResourceErrorMessages.MEDICO_NOT_FOUND); }

        var paciente = await _pacienteRepository.GetPacienteByIdAsync(long.Parse(request.PacienteId));
        if (paciente == null) { throw new InvalidOperationException(ResourceErrorMessages.PACIENTE_NOT_FOUND); }

        var atendente = await _atendenteRepository.GetByIdAsync(atendenteUserId);
        if (atendente == null) { throw new InvalidUserException(ResourceErrorMessages.INVALID_ATENDENTE); }

        consulta = new()
        {
            DataConsulta = DateTimeOffset.Parse(date),
            Medico = medico,
            Paciente = paciente,
            Atendente = atendente,
        };

        _consultaRepository.Add(consulta);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseConsultaJson>(consulta);
    }

    public async Task<ResponseMedicoJson> CreateMedicoAsync(RequestMedicoJson request)
    {
        await CreateMedicoRequestValidation(request);

        var medico = await _medicoRepository.GetMedicoByEmail(request.Email.ToLower());
        if (medico != null) { throw new InvalidOperationException(ResourceErrorMessages.ALREADY_EXISTS); }

        medico = new()
        {
            Nome = request.Nome,
            Especialidade = (Especialidade)Enum.Parse(typeof(Especialidade), request.Especialidade),
            Email = request.Email.ToLower()
        };

        _medicoRepository.Add(medico);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseMedicoJson>(medico);
    }

    public async Task<ResponsePacienteJson> CreatePacienteAsync(RequestPacienteJson request, long atendenteUserId)
    {
        await CreatePacienteRequestValidation(request);

        var paciente = await _pacienteRepository.GetPacienteByEmailAsync(request.Email.ToLower());

        if (paciente != null) { throw new InvalidOperationException(ResourceErrorMessages.ALREADY_EXISTS); }

        // Sempre um atendente vinculado a um usuário que irá registrar um paciente
        var atendenteId = await _atendenteRepository.GetByIdAsync(atendenteUserId);
        if (atendenteId == null) { throw new InvalidUserException(ResourceErrorMessages.INVALID_ATENDENTE); }

        paciente = new()
        {
            Nome = request.Nome,
            Email = request.Email.ToLower(),
            Idade = int.Parse(request.Idade.ToString()),
            Endereco = request.Endereco,
            DataNascimento = DateTime.ParseExact(request.DataNascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Atendentes = new List<Atendente>()
        };

        paciente.Atendentes.Add(atendenteId);

        _pacienteRepository.Add(paciente);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponsePacienteJson>(paciente);
    }

    public async Task<ResponsePrescricaoJson> CreatePrescricaoAsync(RequestPrescricaoJson request)
    {
        var consulta = await _consultaRepository.GetConsultaByIdAsync(long.Parse(request.ConsultaId));

        if (consulta == null) { throw new InvalidOperationException(ResourceErrorMessages.CONSULTA_NOT_FOUND); }


        Prescricao prescricao = new()
        {
            Consulta = consulta,
            Receita = request.Receita
        };

        _prescricaoRepository.Add(prescricao);
        consulta.Prescricoes.Add(prescricao);
        _consultaRepository.Update(consulta);

        await _unitOfWork.Commit();

        ResponsePrescricaoJson response = new()
        {
            Receita = prescricao.Receita
        };

        return response;
    }

    public async Task<ResponseConsultaJson> GetConsultaAsync(RequestConsultaJson request)
    {
        var consulta = await _consultaRepository.GetConsultaByIdAsync(long.Parse(request.ConsultaId));

        if (consulta == null) { throw new InvalidSearchException(ResourceErrorMessages.INVALID_SEARCH); }

        var response = _mapper.Map<ResponseConsultaJson>(consulta);

        var prescricoes = await _prescricaoRepository.GetByConsultaIdAsync(consulta.Id);

        response.Receitas = prescricoes.Select(p => _mapper.Map<ResponsePrescricaoJson>(p)).ToList();

        return response;
    }

    public async Task<ResponseConsultaListJson> GetAllConsultasByPacienteIdAsync(string id)
    {
        var consultas = await _consultaRepository.GetAllConsultasByPacienteAsync(long.Parse(id));

        if (consultas == null) { throw new InvalidSearchException(ResourceErrorMessages.INVALID_SEARCH); }

        var response = consultas.Select(c => _mapper.Map<ResponseConsultaJson>(c));

        return new ResponseConsultaListJson { Consultas = response.ToList() };
    }

    public async Task<ResponsePacienteJson> GetPacienteByEmailAsync(string email)
    {
        var paciente = await _pacienteRepository.GetPacienteByEmailAsync(email.ToLower());

        if (paciente == null) { throw new InvalidSearchException(ResourceErrorMessages.INVALID_SEARCH); }

        var response = _mapper.Map<ResponsePacienteJson>(paciente);

        return response;

    }

    public async Task<ResponseMedicoListJson> GetAllMedicosAsync()
    {
        var medicosList = await _medicoRepository.GetAllMedicosAsync();

        var responseList = medicosList.Select(i => _mapper.Map<ResponseMedicoJson>(i));

        return new ResponseMedicoListJson
        {
            Medicos = responseList.ToList()
        };
    }

    public async Task<ResponseMedicoJson> GetMedicoByIdAsync(string id)
    {
        var medico = await _medicoRepository.GetMedicoByIdAsync(long.Parse(id), true);

        if (medico == null) { throw new InvalidSearchException(ResourceErrorMessages.INVALID_SEARCH); }

        var response = _mapper.Map<ResponseMedicoJson>(medico);

        return response;
    }

    private async static Task CreateMedicoRequestValidation(RequestMedicoJson request)
    {
        var validator = new MedicoRegisterValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidatorErrorsException(errorMessage);
        }
    }

    private async static Task CreatePacienteRequestValidation(RequestPacienteJson request)
    {
        var validator = new PacienteRegisterValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidatorErrorsException(errorMessage);
        }
    }
}
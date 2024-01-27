using DoctorWhen.Domain.Entities;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;

namespace DoctorWhen.Persistence.Repository;
public class ConsultaRepository : GenericRepository, IConsultaRepository
{
    private readonly DoctorWhenContext _context;

    public ConsultaRepository(DoctorWhenContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<IList<Consulta>> GetAllConsultasByDateAsync(DateTime date)
    {
        IQueryable<Consulta> query = _context.Consultas
            .Include(c => c.Atendente)
            .Include(c => c.Medico)
            .Include(c => c.Paciente)
            .Where(c => c.DataConsulta == date);

        query = query.OrderBy(c => c.DataConsulta.Hour);

        return await query.ToListAsync();
    }

    public async Task<IList<Consulta>> GetAllConsultasByMedicoAsync(long medicoId)
    {
        IQueryable<Consulta> query = _context.Consultas
            .Include(c => c.Medico)
            .Where(c => c.Medico.Id == medicoId);

        return await query.ToListAsync();
    }

    public async Task<IList<Consulta>> GetAllConsultasByPacienteAsync(long pacienteId)
    {
        IQueryable<Consulta> query = _context.Consultas
            .Include (c => c.Paciente)
            .Where(c => c.Paciente.Id == pacienteId);

        query = query.OrderBy(c => c.DataConsulta.Day);

        return await query.ToListAsync();
    }

    public Task<Consulta> GetConsultaByDate(DateTimeOffset date)
    {
        IQueryable<Consulta> query = _context.Consultas
            .Include(c => c.Medico)
            .Include(c => c.Paciente)
            .Include(c => c.Atendente)
            .Where(c => c.DataConsulta.Equals(date));

        return query.SingleOrDefaultAsync();
    }

    public async Task<Consulta> GetConsultaByIdAsync(long consultaId)
    {
        IQueryable<Consulta> query = _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .Include(c => c.Atendente)
            .Where( c => c.Id == consultaId);

        return await query.FirstOrDefaultAsync();
    }


}

using DoctorWhen.Domain.Entities;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;

namespace DoctorWhen.Persistence.Repository;
public class PacienteRepository : IPacienteRepository
{
    private readonly DoctorWhenContext _context;

    public PacienteRepository(DoctorWhenContext context)
    {
        this._context = context;
    }

    public async Task<Paciente> GetPacienteByEmail(string email)
    {
        IQueryable<Paciente> query = _context.Pacientes
            .Include(p => p.Medicos)
            .Include(p => p.Consultas)
            .Include(p => p.Atendentes)
            .Where(p => p.Email == email);

        return await query.FirstOrDefaultAsync();
        
    }

    public async Task<Paciente> GetPacienteById(long id)
    {
        IQueryable<Paciente> query = _context.Pacientes
            .Include(p => p.Medicos)
            .Include(p => p.Consultas)
            .Include(p => p.Atendentes)
            .Where(p => p.Id == id);

        return await query.FirstOrDefaultAsync();
    }
}

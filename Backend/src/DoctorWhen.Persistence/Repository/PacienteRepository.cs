using DoctorWhen.Domain.Entities;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;

namespace DoctorWhen.Persistence.Repository;
public class PacienteRepository : GeneralRepository, IPacienteRepository
{
    private readonly DoctorWhenContext _context;

    public PacienteRepository(DoctorWhenContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<Paciente> GetPacienteByEmailAsync(string email)
    {
        IQueryable<Paciente> query = _context.Pacientes
            .Include(p => p.Medicos)
            .Include(p => p.Consultas)
            .Include(p => p.Atendentes)
            .Where(p => p.Email == email);

        return await query.FirstOrDefaultAsync();
        
    }

    public async Task<Paciente> GetPacienteByIdAsync(long id)
    {
        IQueryable<Paciente> query = _context.Pacientes
            .Include(p => p.Medicos)
            .Include(p => p.Consultas)
            .Include(p => p.Atendentes)
            .Where(p => p.Id == id);

        return await query.FirstOrDefaultAsync();
    }
}

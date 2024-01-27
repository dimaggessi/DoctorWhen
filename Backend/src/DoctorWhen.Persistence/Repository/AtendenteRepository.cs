using DoctorWhen.Domain.Entities;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;

namespace DoctorWhen.Persistence.Repository;
public class AtendenteRepository : GenericRepository, IAtendenteRepository
{
    private readonly DoctorWhenContext _context;

    public AtendenteRepository(DoctorWhenContext context) : base(context)
    {
        this._context = context;
    }
    public async Task<Atendente> GetByEmailAsync(string email)
    {
        IQueryable<Atendente> query = _context.Atendentes
            .Include(a => a.PacientesAtendidos)
            .Include(a => a.User)
            .Where(a => a.User.Email == email);

        return await query.SingleOrDefaultAsync();
    }

    public async Task<Atendente> GetByIdAsync(long id)
    {
        IQueryable<Atendente> query = _context.Atendentes
            .Include(a =>  a.PacientesAtendidos)
            .Include(a => a.User)
            .Where(a => a.UserId == id);

        return await query.SingleOrDefaultAsync();
    }

    public async Task DeleteCascade(long id)
    {
        IQueryable<Atendente> query =  _context.Atendentes
            .Include(a => a.PacientesAtendidos)
            .ThenInclude(p => p.Consultas)
            .Include(a => a.User)
            .Where(a => a.UserId == id);

        var atendente = await query.SingleOrDefaultAsync();

        _context.Remove(atendente);
    }
}
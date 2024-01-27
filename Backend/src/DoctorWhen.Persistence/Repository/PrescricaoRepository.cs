using DoctorWhen.Domain.Entities;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;

namespace DoctorWhen.Persistence.Repository;
public class PrescricaoRepository : GenericRepository, IPrescricaoRepository
{
    private readonly DoctorWhenContext _context;

    public PrescricaoRepository(DoctorWhenContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<IList<Prescricao>> GetByConsultaIdAsync(long consultaId)
    {
        IQueryable<Prescricao> query = _context.Prescricoes
            .Where(p => p.Consulta.Id == consultaId);

        return await query.ToListAsync();
    }

    public async Task<Prescricao> GetByPrescricaoIdAsync(long id)
    {
        IQueryable<Prescricao> query = _context.Prescricoes
            .Include(p => p.Consulta)
            .Where(p => p.Id == id);

        return await query.FirstOrDefaultAsync();
    }
}
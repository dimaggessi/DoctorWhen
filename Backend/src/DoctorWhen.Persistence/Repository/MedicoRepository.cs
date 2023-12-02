﻿using DoctorWhen.Domain.Entities;
using DoctorWhen.Domain.Enum;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;

namespace DoctorWhen.Persistence.Repository;
public class MedicoRepository : IMedicoRepository
{
    private readonly DoctorWhenContext _context;

    public MedicoRepository(DoctorWhenContext context)
    {
        this._context = context;
    }

    public async Task<IList<Medico>> GetAllMedicosAsync()
    {
        IQueryable<Medico> query = _context.Medicos
            .Include(m => m.Pacientes)
            .Include(m => m.Consultas);

        query = query.OrderBy(m => m.Nome);

        return await query.ToListAsync();
    }

    public async Task<IList<Medico>> GetAllMedicosByEspecialidadeAsync(Especialidade especialidade)
    {
        IQueryable<Medico> query = _context.Medicos
            .Where(m => m.Especialidade == especialidade);

        query = query.OrderBy(m => m.Nome);

        return await query.ToListAsync();
    }

    public async Task<Medico> GetMedicoById(long id, bool includeConsultas)
    {
        IQueryable<Medico> query = _context.Medicos
            .Include(m => m.Pacientes)
            .Where(m => m.Id == id);

        if (includeConsultas)
        {
            query = query.Include(m => m.Consultas);
        }

        return await query.FirstOrDefaultAsync();
    }
}
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;

namespace DoctorWhen.Persistence.Repository;
public class GeneralRepository : IGeneralRepository<T>
{
    private readonly DoctorWhenContext _context;
    public GeneralRepository(DoctorWhenContext context)
    {
        this._context = context;
        
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Update<T>(T entity)
    {
        _context.Update(entity);
    }

    public void Delete<T>(T entity)
    {
        _context.Remove(entity);
    }
}


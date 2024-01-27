using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;

namespace DoctorWhen.Persistence.Repository;
public class GenericRepository : IGenericRepository
{
    private readonly DoctorWhenContext _context;
    public GenericRepository(DoctorWhenContext context)
    {
        this._context = context;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public void Delete<T>(T entity) where T: class
    {
        _context.Remove(entity);
    }

    public void DeleteRange<T>(T[] entityArray) where T : class
    {
        _context.RemoveRange(entityArray);
    }
}
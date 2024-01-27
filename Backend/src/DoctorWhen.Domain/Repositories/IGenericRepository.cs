namespace DoctorWhen.Domain.Repositories;
public interface IGenericRepository
{
    void Add<T>(T entity) where T: class;
    void Update<T>(T entity) where T: class;
    void Delete<T>(T entity) where T: class;
    void DeleteRange<T>(T[] entityArray) where T: class;
}
namespace DoctorWhen.Domain.Repositories;
public interface IGeneralRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}
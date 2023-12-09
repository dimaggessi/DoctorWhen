using DoctorWhen.Domain.Identity;

namespace DoctorWhen.Domain.Repositories;
public interface IUserRepository : IGeneralRepository
{
    Task<ICollection<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(long id);
    Task<User> GetUserByEmailAsync(string email);
}

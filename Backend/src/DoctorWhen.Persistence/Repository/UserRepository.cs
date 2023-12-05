using DoctorWhen.Domain.Identity;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;

namespace DoctorWhen.Persistence.Repository;
public class UserRepository : GeneralRepository, IUserRepository
{
    private readonly DoctorWhenContext _context;

    public UserRepository(DoctorWhenContext context) : base(context)
    {
        this._context = context;
    }
    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(long id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.UserName.Equals(userName.ToLower()));
    }
}

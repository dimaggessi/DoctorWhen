using Microsoft.AspNetCore.Identity;

namespace DoctorWhen.Domain.Identity;
public class User : IdentityUser<long>
{
    public ICollection<UserRole> UserRoles { get; set; }
}

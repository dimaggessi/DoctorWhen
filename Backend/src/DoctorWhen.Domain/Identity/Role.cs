using Microsoft.AspNetCore.Identity;

namespace DoctorWhen.Domain.Identity;
public class Role : IdentityRole<long>
{
    public ICollection<UserRole> UserRoles { get; set; }
}

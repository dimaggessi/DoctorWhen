using Microsoft.AspNetCore.Identity;

namespace DoctorWhen.Domain.Identity;
public class UserRole : IdentityUserRole<long>
{
    public User User { get; set; }
    public Role Role { get; set; }
}

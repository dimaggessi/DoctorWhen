using Microsoft.AspNetCore.Identity;

namespace DoctorWhen.Domain.Identity;
public class Role : IdentityRole<long>
{
    public const string Admin = nameof(Admin);
    public const string Atendente = nameof(Atendente);
    public ICollection<UserRole> UserRoles { get; set; }
}
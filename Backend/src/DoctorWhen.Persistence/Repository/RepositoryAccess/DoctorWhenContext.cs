using DoctorWhen.Domain.Entities;
using DoctorWhen.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorWhen.Persistence.Repository.RepositoryAccess;

// Deve ser informado ao IdentityDbContext qual User e Role devem ser utilizados,
// pois do contrário ele usará o IdentityUser padrão cuja TKey é uma string
public class DoctorWhenContext : IdentityDbContext<User, Role, long, IdentityUserClaim<long>,
                                                   UserRole, IdentityUserLogin<long>, 
                                                   IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    // Em Persistence.Bootstrapper.cs foi definido a configuração de conexão que será passada ao Program.cs
    // A configuração de conexão com o banco de dados é passada pelo Program.cs através do construtor no momento de execução da API
    public DoctorWhenContext(DbContextOptions<DoctorWhenContext> options) : base(options)
    {
    }

    public DbSet<Atendente> Atendentes { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<Prescricao> Prescricoes { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // é necessário passar o modelBuilder para o base context
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();


            userRole.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            userRole.HasData(new UserRole() { UserId = 1, RoleId = 1 });
        });

        modelBuilder.Entity<User>(ConfigureUserAdmin);
        modelBuilder.Entity<Role>(ConfigureRoles);

    }

    private static void ConfigureUserAdmin(EntityTypeBuilder<User> entityBuilder)
    {
        var user = new User
        {
            Id = 1,
            UserName = "admin",
            Email = "admin@email.com"
        };

        var passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, "admin123");

        entityBuilder.HasData(user);
    }

    private static void ConfigureRoles(EntityTypeBuilder<Role> entityBuilder)
    {
        var roles = new Role[]
        {
            new Role() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new Role() { Id = 2, Name = "Atendente", NormalizedName = "ATENDENTE" }
        };

        entityBuilder.HasData(roles);
    }
}
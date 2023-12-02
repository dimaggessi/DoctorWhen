using DoctorWhen.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorWhen.Persistence.Repository.RepositoryAccess;
public class DoctorWhenContext : DbContext
{
    // A configuração de conexão com o banco de dados é passado pelo Program.cs através do construtor
    public DoctorWhenContext(DbContextOptions<DoctorWhenContext> options) : base(options)
    {
    }

    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Atendente> Atendentes { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<Prescricao> Prescricoes { get; set;}
}

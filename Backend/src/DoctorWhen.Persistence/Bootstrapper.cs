using DoctorWhen.Domain.Extensions;
using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorWhen.Persistence;
public static class Bootstrapper
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddContext(services, configurationManager);
        AddRepositories(services);
        AddUnitOfWork(services);
    }

    private static void AddContext(IServiceCollection services, IConfiguration configurationManager)
    {
        // builder.Configuration.GetConnectionString("nomeDaChave") encapsulado em Domain/Extensions (Pacote NuGet Microsoft.Extensions.Configuration)
        services.AddDbContext<DoctorWhenContext>(context => context.UseSqlServer(configurationManager.GetDatabaseConnection()));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IGeneralRepository, GeneralRepository>();
        services.AddScoped<IConsultaRepository, ConsultaRepository>();
        services.AddScoped<IMedicoRepository, MedicoRepository>();
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IPrescricaoRepository, PrescricaoRepository>();
        services.AddScoped<IAtendenteRepository, AtendenteRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
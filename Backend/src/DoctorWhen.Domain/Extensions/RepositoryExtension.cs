using Microsoft.Extensions.Configuration;

namespace DoctorWhen.Domain.Extensions;
public static class RepositoryExtension
{
    // Encapsula através de uma extensão o método GetConnectionString()
    public static string GetDatabaseConnection(this IConfiguration configurationManager)
    {
        var connection = configurationManager.GetConnectionString("DoctorWhen");
        return connection;
    }
}
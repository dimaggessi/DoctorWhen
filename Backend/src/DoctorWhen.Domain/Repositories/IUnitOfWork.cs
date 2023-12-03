namespace DoctorWhen.Domain.Repositories;
public interface IUnitOfWork
{
    // Padrão utilizado para externalizar o SaveChanges "para fora" dos repositórios
    // evitando assim inconsistência na base de dados
    // (quando uma transaction é precedida de operações em diferentes repositórios)
    Task Commit();
}
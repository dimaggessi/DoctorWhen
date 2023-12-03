using DoctorWhen.Domain.Repositories;
using DoctorWhen.Persistence.Repository.RepositoryAccess;

namespace DoctorWhen.Persistence.Repository;
public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly DoctorWhenContext _context;
    private bool _disposed; // valor padrão é false

    public UnitOfWork(DoctorWhenContext context)
    {
        this._context = context;
    }
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    // Dispose visa liberar recursos não gerenciados
    // deve ser feito, do contrário, eles não serão liberados
    // O garbage collector não sabe se deve ou não chamar o DeleteHandle() para esses recursos
    // por isso implementa-se o Dispose, para dizer que o recurso deve ser liberado (recursos obtidos em chamadas externas ao .NET Framework)
    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }
}

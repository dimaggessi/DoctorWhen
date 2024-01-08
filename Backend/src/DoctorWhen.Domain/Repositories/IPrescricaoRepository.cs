namespace DoctorWhen.Domain.Repositories;
public interface IPrescricaoRepository : IGeneralRepository
{
    Task<Entities.Prescricao> GetByPrescricaoIdAsync(long id);
    Task<IList<Entities.Prescricao>> GetByConsultaIdAsync(long consultaId);
}

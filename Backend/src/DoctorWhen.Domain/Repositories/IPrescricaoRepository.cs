namespace DoctorWhen.Domain.Repositories;
public interface IPrescricaoRepository
{
    Task<Entities.Prescricao> GetByPrescricaoIdAsync(long id);
    Task<Entities.Prescricao> GetByConsultaIdAsync(long consultaId);
}

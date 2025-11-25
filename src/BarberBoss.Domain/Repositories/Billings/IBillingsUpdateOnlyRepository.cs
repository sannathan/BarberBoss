using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billings
{
    public interface IBillingsUpdateOnlyRepository
    {
        Task<Billing?> GetById(Guid Id);
        void Update(Billing billing);


    }
}

using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billings
{
    public interface IBillingsReadOnlyRepository
    {
        Task<List<Billing>> GetAll();
        Task<Billing?> GetById(Guid Id);
        Task<List<Billing>> FilterByMonth(DateOnly date);
    }
}

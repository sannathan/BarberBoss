using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Billings;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAccess.Repositories.Billings
{
    internal class BilllingsRepository : IBillingsWriteOnlyRepository, IBillingsReadOnlyRepository
    {
        private readonly BarberBossDbContext _dbContext;
        public BilllingsRepository(BarberBossDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Billing billing)
        {
            await _dbContext.Billings.AddAsync(billing);
        }

        public async Task<bool> Delete(Guid Id)
        {
            var result = await _dbContext.Billings.FirstOrDefaultAsync(billing => billing.Id == Id);

            if(result is null)
            {
                return false;
            }

            _dbContext.Billings.Remove(result);

            return true;
        }

        public async Task<List<Billing>> GetAll()
        {
            return await _dbContext.Billings.AsNoTracking().ToListAsync();
        }

        public async Task<Billing?> GetById(Guid Id)
        {
            return await _dbContext.Billings.AsNoTracking().FirstOrDefaultAsync(billing => billing.Id == Id);
        }
    }
}

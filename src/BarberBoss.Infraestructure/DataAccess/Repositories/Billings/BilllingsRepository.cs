using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Billings;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BarberBoss.Infraestructure.DataAccess.Repositories.Billings
{
    internal class BilllingsRepository : IBillingsWriteOnlyRepository, IBillingsReadOnlyRepository, IBillingsUpdateOnlyRepository
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

         async Task<Billing?> IBillingsReadOnlyRepository.GetById(Guid Id)
        {
            return await _dbContext.Billings.AsNoTracking().FirstOrDefaultAsync(billing => billing.Id == Id);
        }

        async Task<Billing?> IBillingsUpdateOnlyRepository.GetById(Guid Id)
        {
            return await _dbContext.Billings.FirstOrDefaultAsync(billing => billing.Id == Id);
        }

        public void Update( Billing billing)
        {
            _dbContext.Billings.Update(billing);
        }

        public async Task<List<Billing>> FilterByMonth(DateOnly date)
        {
            var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
            var startDate = new DateTime(year: date.Year, month: date.Month, day: 1);
            var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth);

            return await _dbContext.Billings.AsNoTracking().Where(billing => billing.Date >= startDate && billing.Date <= endDate).OrderBy(billing => billing.Date).ThenBy(billing => billing.Amount).ToListAsync();
        }
    }
}

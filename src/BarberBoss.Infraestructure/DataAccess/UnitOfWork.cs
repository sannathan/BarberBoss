using BarberBoss.Domain.Repositories;
using System.Runtime.CompilerServices;

namespace BarberBoss.Infraestructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly BarberBossDbContext _dbContext;
        public UnitOfWork(BarberBossDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}

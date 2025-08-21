using System.Threading;
using System.Threading.Tasks;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Domain.Entities;
using TerminalManagement.Infrastructure.Persistence;

namespace TerminalManagement.Infrastructure.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly AppDbContext _dbContext;

        public StatisticRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TerminalStatistic statistic, CancellationToken cancellationToken)
        {
            await _dbContext.TerminalStatistics.AddAsync(statistic, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}


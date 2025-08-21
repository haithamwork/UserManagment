using System.Threading;
using System.Threading.Tasks;
using TerminalManagement.Domain.Entities;

namespace TerminalManagement.Application.Abstractions.Repositories
{
    public interface IStatisticRepository
    {
        Task AddAsync(TerminalStatistic statistic, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}


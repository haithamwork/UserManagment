using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Domain.Entities;
using TerminalManagement.Infrastructure.Persistence;

namespace TerminalManagement.Infrastructure.Repositories
{
    public class TerminalProfileRepository : ITerminalProfileRepository
    {
        private readonly AppDbContext _dbContext;

        public TerminalProfileRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TerminalProfile profile, CancellationToken cancellationToken)
        {
            await _dbContext.TerminalProfiles.AddAsync(profile, cancellationToken);
        }

        public async Task DeleteAsync(TerminalProfile profile, CancellationToken cancellationToken)
        {
            _dbContext.TerminalProfiles.Remove(profile);
            await Task.CompletedTask;
        }

        public async Task<TerminalProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.TerminalProfiles
                .Include(p => p.ApnSettings)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<TerminalProfile?> GetByTerminalIdAsync(string terminalId, CancellationToken cancellationToken)
        {
            return await _dbContext.TerminalProfiles
                .Include(p => p.ApnSettings)
                .FirstOrDefaultAsync(p => p.TerminalId == terminalId, cancellationToken);
        }

        public async Task<List<TerminalProfile>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.TerminalProfiles
                .Include(p => p.ApnSettings)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TerminalProfile profile, CancellationToken cancellationToken)
        {
            _dbContext.TerminalProfiles.Update(profile);
            await Task.CompletedTask;
        }
    }
}


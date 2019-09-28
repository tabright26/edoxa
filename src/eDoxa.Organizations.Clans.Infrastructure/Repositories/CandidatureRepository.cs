// Filename: ClanRepository.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Organizations.Clans.Infrastructure.Repositories
{
    public class CandidatureRepository : ICandidatureRepository
    {
        private readonly ClansDbContext _dbContext;

        public CandidatureRepository(ClansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Candidature candidature)
        {
            _dbContext.Candidatures.Add(candidature);
        }

        public void Delete(Candidature candidature)
        {
            _dbContext.Candidatures.Remove(candidature);
        }

        public async Task<IReadOnlyCollection<Candidature>> FetchAsync()
        {
            return await _dbContext.Candidatures.ToListAsync();
        }

        public async Task<Candidature?> FindAsync(CandidatureId candidatureId)
        {
            return await _dbContext.Candidatures.SingleOrDefaultAsync(candidature => candidature.Id == candidatureId);
        }

        public async Task<bool> ExistsAsync(UserId userId, ClanId clanId)
        {
            return await _dbContext.Candidatures.AnyAsync(candidature => candidature.UserId == userId && candidature.ClanId == clanId);
        }

        public async Task DeleteAllWith(UserId userId)
        {
            var candidatures = await _dbContext.Candidatures.Where(candidature => candidature.UserId == userId).ToListAsync();
            candidatures.Clear();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

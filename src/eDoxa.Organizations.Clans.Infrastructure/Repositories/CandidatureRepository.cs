// Filename: ClanRepository.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
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

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

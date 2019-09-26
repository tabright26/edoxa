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
            throw new NotImplementedException();
        }

        public void Delete(Candidature candidature)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Candidature>> FetchAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Candidature?> FindAsync(CandidatureId candidatureId)
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
}

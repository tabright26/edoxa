// Filename: CandidatureRepository.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Organizations.Clans.Infrastructure.Repositories
{
    public class CandidatureRepository : ICandidatureRepository
    {
        private readonly ClansDbContext _context;

        public CandidatureRepository(ClansDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Create(Candidature candidature)
        {
            _context.Candidatures.Add(candidature);
        }

        public void Delete(Candidature candidature)
        {
            _context.Candidatures.Remove(candidature);
        }

        public async Task<IReadOnlyCollection<Candidature>> FetchAsync(ClanId clanId)
        {
            return await _context.Candidatures.Where(candidature => candidature.ClanId == clanId).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Candidature>> FetchAsync(UserId userId)
        {
            return await _context.Candidatures.Where(candidature => candidature.UserId == userId).ToListAsync();
        }

        public async Task<Candidature?> FindAsync(CandidatureId candidatureId)
        {
            return await _context.Candidatures.SingleOrDefaultAsync(candidature => candidature.Id == candidatureId);
        }

        public async Task<bool> ExistsAsync(UserId userId, ClanId clanId)
        {
            return await _context.Candidatures.AnyAsync(candidature => candidature.UserId == userId && candidature.ClanId == clanId);
        }
    }
}

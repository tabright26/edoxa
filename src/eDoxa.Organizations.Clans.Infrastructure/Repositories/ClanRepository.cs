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
    public class ClanRepository : IClanRepository
    {
        private readonly ClansDbContext _dbContext;

        public ClanRepository(ClansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(ClanModel clanModel)
        {
            _dbContext.Clans.Add(clanModel);
        }

        public async Task<IReadOnlyCollection<ClanModel>> FetchClansAsync()
        {
            return await _dbContext.Clans.ToListAsync();
        }

        public async Task<ClanModel> FindClanAsync(Guid clanId)
        {
            return await _dbContext.Clans.Include(clan => clan.Members).SingleOrDefaultAsync(clan => clan.Id == clanId);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

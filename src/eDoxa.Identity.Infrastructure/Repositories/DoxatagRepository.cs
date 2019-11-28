// Filename: DoxatagRepository.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Infrastructure.Repositories
{
    public sealed partial class DoxatagRepository
    {
        private readonly IdentityDbContext _context;

        public DoxatagRepository(IdentityDbContext context)
        {
            _context = context;
        }

        private DbSet<Doxatag> DoxatagHistory => _context.Set<Doxatag>();

        public IUnitOfWork UnitOfWork => _context;

        private IQueryable<Doxatag> FetchDoxatagsByUserId(UserId userId)
        {
            return DoxatagHistory.AsExpandable().Where(doxatag => doxatag.UserId == userId).OrderBy(doxatag => doxatag.Timestamp);
        }

        private IQueryable<Doxatag> FetchDoxatagsByName(string name)
        {
            return DoxatagHistory.AsExpandable().Where(doxatag => doxatag.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }

    public sealed partial class DoxatagRepository : IDoxatagRepository
    {
        public void Create(Doxatag doxatag)
        {
            DoxatagHistory.Add(doxatag);
        }

        public async Task<IReadOnlyCollection<Doxatag>> FetchDoxatagHistoryAsync(UserId userId)
        {
            return await this.FetchDoxatagsByUserId(userId).ToListAsync();
        }

        public async Task<Doxatag?> FindDoxatagAsync(UserId userId)
        {
            return await this.FetchDoxatagsByUserId(userId).FirstOrDefaultAsync();
        }

        public async Task<IImmutableSet<int>> FetchDoxatagCodesByNameAsync(string name)
        {
            var codes = await this.FetchDoxatagsByName(name).AsNoTracking().Select(doxatag => doxatag.Code).Distinct().ToListAsync();

            return codes.ToImmutableSortedSet();
        }

        public async Task<IImmutableSet<Doxatag>> FetchDoxatagsAsync()
        {
            var doxatags = await DoxatagHistory.AsExpandable().ToListAsync();

            return doxatags
                .GroupBy(doxatag => doxatag.UserId)
                .Select(history => history.OrderBy(doxatag => doxatag.Timestamp).First())
                .ToImmutableHashSet();
        }
    }
}

// Filename: CredentialRepository.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Games.Infrastructure.Repositories
{
    public sealed class CredentialRepository : ICredentialRepository
    {
        private readonly GamesDbContext _context;

        public CredentialRepository(GamesDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void CreateCredential(Credential credential)
        {
            _context.Add(credential);
        }

        public void DeleteCredential(Credential credential)
        {
            _context.Remove(credential);
        }

        public async Task<IReadOnlyCollection<Credential>> FetchCredentialsAsync(UserId userId)
        {
            return await _context.Credentials.AsExpandable()
                .Where(credential => credential.UserId == userId)
                .OrderBy(credential => credential.Game)
                .ToListAsync();
        }

        public async Task<Credential?> FindCredentialAsync(UserId userId, Game game)
        {
            return await _context.Credentials.AsExpandable().SingleOrDefaultAsync(credential => credential.UserId == userId && credential.Game == game);
        }

        public async Task<bool> CredentialExistsAsync(UserId userId, Game game)
        {
            return await _context.Credentials.AsExpandable().AnyAsync(credential => credential.UserId == userId && credential.Game == game);
        }
    }
}

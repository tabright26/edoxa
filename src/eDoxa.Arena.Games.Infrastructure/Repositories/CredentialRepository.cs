// Filename: CredentialRepository.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Arena.Games.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Games.Infrastructure.Repositories
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
            return await _context.GameCredentials.AsExpandable()
                .Where(gameCredential => gameCredential.UserId == userId)
                .OrderBy(gameCredential => gameCredential.Game)
                .ToListAsync();
        }

        public async Task<Credential?> FindCredentialAsync(UserId userId, Game game)
        {
            return await _context.GameCredentials.AsExpandable()
                .SingleOrDefaultAsync(gameCredential => gameCredential.UserId == userId && gameCredential.Game == game);
        }

        public async Task<bool> CredentialExistsAsync(UserId userId, Game game)
        {
            return await _context.GameCredentials.AsExpandable().AnyAsync(gameCredential => gameCredential.UserId == userId && gameCredential.Game == game);
        }
    }
}

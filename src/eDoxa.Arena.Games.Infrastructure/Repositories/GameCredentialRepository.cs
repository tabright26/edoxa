// Filename: GameCredentialRepository.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.GameCredentialAggregate;
using eDoxa.Arena.Games.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Games.Infrastructure.Repositories
{
    public sealed class GameCredentialRepository : IGameCredentialRepository
    {
        private readonly GamesDbContext _context;

        public GameCredentialRepository(GamesDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void CreateGameCredential(GameCredential gameCredential)
        {
            _context.Add(gameCredential);
        }

        public void DeleteGameCredential(GameCredential gameCredential)
        {
            _context.Remove(gameCredential);
        }

        public async Task<IReadOnlyCollection<GameCredential>> FetchGameCredentialsAsync(UserId userId)
        {
            return await _context.GameCredentials.AsExpandable()
                .Where(gameCredential => gameCredential.UserId == userId)
                .OrderBy(gameCredential => gameCredential.Game)
                .ToListAsync();
        }

        public async Task<GameCredential?> FindGameCredentialAsync(UserId userId, Game game)
        {
            return await _context.GameCredentials.AsExpandable()
                .SingleOrDefaultAsync(gameCredential => gameCredential.UserId == userId && gameCredential.Game == game);
        }

        public async Task<bool> GameCredentialExistsAsync(UserId userId, Game game)
        {
            return await _context.GameCredentials.AsExpandable().AnyAsync(gameCredential => gameCredential.UserId == userId && gameCredential.Game == game);
        }
    }
}

// Filename: GameCredentialService.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.GameCredentialAggregate;
using eDoxa.Arena.Games.Domain.Repositories;
using eDoxa.Arena.Games.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Api.Areas.Games.Services
{
    public sealed class GameCredentialService : IGameCredentialService
    {
        private readonly IGameCredentialRepository _gameCredentialRepository;

        public GameCredentialService(IGameCredentialRepository gameCredentialRepository)
        {
            _gameCredentialRepository = gameCredentialRepository;
        }

        public async Task CreateGameCredentialAsync(UserId userId, Game game, PlayerId playerId)
        {
            if (await _gameCredentialRepository.GameCredentialExistsAsync(userId, game))
            {
                throw new InvalidOperationException(nameof(this.CreateGameCredentialAsync));
            }

            var credential = new GameCredential(
                userId,
                game,
                playerId,
                new UtcNowDateTimeProvider());

            _gameCredentialRepository.CreateGameCredential(credential);

            await _gameCredentialRepository.UnitOfWork.CommitAsync();
        }

        public async Task DeleteGameCredentialAsync(UserId userId, Game game)
        {
            var credential = await this.FindGameCredentialAsync(userId, game);

            if (credential == null)
            {
                throw new InvalidOperationException(nameof(this.DeleteGameCredentialAsync));
            }

            _gameCredentialRepository.DeleteGameCredential(credential!);

            await _gameCredentialRepository.UnitOfWork.CommitAsync();
        }

        public async Task<IReadOnlyCollection<GameCredential>> FetchGameCredentialsAsync(UserId userId)
        {
            return await _gameCredentialRepository.FetchGameCredentialsAsync(userId);
        }

        public async Task<GameCredential?> FindGameCredentialAsync(UserId userId, Game game)
        {
            return await _gameCredentialRepository.FindGameCredentialAsync(userId, game);
        }

        public async Task<bool> GameCredentialExistsAsync(UserId userId, Game game)
        {
            return await _gameCredentialRepository.GameCredentialExistsAsync(userId, game);
        }
    }
}

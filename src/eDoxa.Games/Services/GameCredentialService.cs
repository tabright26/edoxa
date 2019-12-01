// Filename: CredentialService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Services
{
    public sealed class GameCredentialService : IGameCredentialService
    {
        private readonly IGameCredentialRepository _gameCredentialRepository;
        private readonly IGameAuthenticationService _gameAuthenticationService;

        public GameCredentialService(IGameCredentialRepository gameCredentialRepository, IGameAuthenticationService gameAuthenticationService)
        {
            _gameCredentialRepository = gameCredentialRepository;
            _gameAuthenticationService = gameAuthenticationService;
        }

        public async Task<DomainValidationResult> LinkCredentialAsync(UserId userId, Game game)
        {
            if (await _gameCredentialRepository.CredentialExistsAsync(userId, game))
            {
                return DomainValidationResult.Failure($"{game} credential are already linked.");
            }

            if (!await _gameAuthenticationService.AuthenticationExistsAsync(userId, game))
            {
                return DomainValidationResult.Failure($"{game} authentication process not started.");
            }

            var authFactor = await _gameAuthenticationService.FindAuthenticationAsync(userId, game);

            var result = await _gameAuthenticationService.ValidateAuthenticationAsync(userId, game, authFactor);

            if (result.IsValid)
            {
                var credential = new Credential(
                    userId,
                    game,
                    authFactor.PlayerId,
                    new UtcNowDateTimeProvider());

                _gameCredentialRepository.CreateCredential(credential);

                await _gameCredentialRepository.UnitOfWork.CommitAsync();
            }

            return result;
        }

        public async Task<DomainValidationResult> UnlinkCredentialAsync(Credential credential)
        {
            credential.Delete();

            await _gameCredentialRepository.UnitOfWork.CommitAsync();

            _gameCredentialRepository.DeleteCredential(credential);

            await _gameCredentialRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult();
        }

        public async Task<IReadOnlyCollection<Credential>> FetchCredentialsAsync(UserId userId)
        {
            return await _gameCredentialRepository.FetchCredentialsAsync(userId);
        }

        public async Task<Credential?> FindCredentialAsync(UserId userId, Game game)
        {
            return await _gameCredentialRepository.FindCredentialAsync(userId, game);
        }

        public async Task<bool> CredentialExistsAsync(UserId userId, Game game)
        {
            return await _gameCredentialRepository.CredentialExistsAsync(userId, game);
        }

        public async Task<IReadOnlyCollection<string>> FetchGamesWithCredentialAsync(UserId userId)
        {
            var credentials = await this.FetchCredentialsAsync(userId);

            return credentials.Select(credential => credential.Game.Name.ToLowerInvariant()).ToList();
        }
    }
}

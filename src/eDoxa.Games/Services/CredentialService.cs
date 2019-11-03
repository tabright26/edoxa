// Filename: CredentialService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Games.Services
{
    public sealed class CredentialService : ICredentialService
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IAuthFactorService _authFactorService;

        public CredentialService(ICredentialRepository credentialRepository, IAuthFactorService authFactorService)
        {
            _credentialRepository = credentialRepository;
            _authFactorService = authFactorService;
        }

        public async Task<ValidationResult> LinkCredentialAsync(UserId userId, Game game)
        {
            if (await _credentialRepository.CredentialExistsAsync(userId, game))
            {
                return new ValidationFailure(string.Empty, $"{game} credential are already linked.").ToResult();
            }

            if (!await _authFactorService.AuthFactorExistsAsync(userId, game))
            {
                return new ValidationFailure(string.Empty, $"{game} authentication process not started.").ToResult();
            }

            var authFactor = await _authFactorService.FindAuthFactorAsync(userId, game);

            var result = await _authFactorService.ValidateAuthFactorAsync(userId, game, authFactor);

            if (result.IsValid)
            {
                var credential = new Credential(
                    userId,
                    game,
                    authFactor.PlayerId,
                    new UtcNowDateTimeProvider());

                _credentialRepository.CreateCredential(credential);

                await _credentialRepository.UnitOfWork.CommitAsync();
            }

            return result;
        }

        public async Task<ValidationResult> UnlinkCredentialAsync(Credential credential)
        {
            credential.Delete();

            await _credentialRepository.UnitOfWork.CommitAsync();

            _credentialRepository.DeleteCredential(credential);

            await _credentialRepository.UnitOfWork.CommitAsync();

            return new ValidationResult();
        }

        public async Task<IReadOnlyCollection<Credential>> FetchCredentialsAsync(UserId userId)
        {
            return await _credentialRepository.FetchCredentialsAsync(userId);
        }

        public async Task<Credential?> FindCredentialAsync(UserId userId, Game game)
        {
            return await _credentialRepository.FindCredentialAsync(userId, game);
        }

        public async Task<bool> CredentialExistsAsync(UserId userId, Game game)
        {
            return await _credentialRepository.CredentialExistsAsync(userId, game);
        }

        public async Task<IReadOnlyCollection<string>> FetchGamesWithCredentialAsync(UserId userId)
        {
            var credentials = await this.FetchCredentialsAsync(userId);

            return credentials.Select(credential => credential.Game.Name.ToLowerInvariant()).ToList();
        }
    }
}

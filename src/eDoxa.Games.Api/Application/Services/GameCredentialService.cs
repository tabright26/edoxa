﻿// Filename: CredentialService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Api.Application.Services
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

        public async Task<DomainValidationResult<Credential>> LinkCredentialAsync(UserId userId, Game game)
        {
            var result = new DomainValidationResult<Credential>();

            if (await _gameCredentialRepository.CredentialExistsAsync(userId, game))
            {
                return result.AddFailedPreconditionError($"{game} credential are already linked.");
            }

            if (!await _gameAuthenticationService.AuthenticationExistsAsync(userId, game))
            {
                return result.AddFailedPreconditionError($"{game} authentication process not started.");
            }

            var authFactor = await _gameAuthenticationService.FindAuthenticationAsync(userId, game);

            var authResult = await _gameAuthenticationService.ValidateAuthenticationAsync(userId, game, authFactor);

            if (!authResult.IsValid)
            {
                foreach (var error in authResult.Errors)
                {
                    result.AddFailedPreconditionError(error.ErrorMessage);
                }
            }

            if (result.IsValid)
            {
                var credential = new Credential(
                    userId,
                    game,
                    authFactor.PlayerId,
                    new UtcNowDateTimeProvider());

                _gameCredentialRepository.CreateCredential(credential);

                await _gameCredentialRepository.UnitOfWork.CommitAsync();

                return credential;
            }

            return result;
        }

        public async Task<DomainValidationResult<Credential>> UnlinkCredentialAsync(Credential credential)
        {
            var result = new DomainValidationResult<Credential>();

            if (credential.Timestamp > DateTime.UtcNow.AddMonths(-1))
            {
                result.AddFailedPreconditionError($"You will have the right to unlink your {credential.Game.DisplayName} credentials in {(credential.Timestamp - DateTime.UtcNow.AddMonths(-1)).Days} days.");
            }

            if (result.IsValid)
            {
                credential.Delete();

                await _gameCredentialRepository.UnitOfWork.CommitAsync();

                _gameCredentialRepository.DeleteCredential(credential);

                await _gameCredentialRepository.UnitOfWork.CommitAsync();

                return credential;
            }

            return result;
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
    }
}

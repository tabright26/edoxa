// Filename: CredentialService.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Api.HttpClients;
using eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Arena.Games.Domain.Repositories;
using eDoxa.Arena.Games.Domain.Services;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Arena.Games.Api.Services
{
    public sealed class CredentialService : ICredentialService
    {
        private readonly IGatewayHttpClient _gatewayHttpClient;
        private readonly ICredentialRepository _credentialRepository;

        public CredentialService(IGatewayHttpClient gatewayHttpClient, ICredentialRepository credentialRepository)
        {
            _gatewayHttpClient = gatewayHttpClient;
            _credentialRepository = credentialRepository;
        }

        public async Task<ValidationResult> LinkCredentialAsync(UserId userId, Game game)
        {
            if (await _credentialRepository.CredentialExistsAsync(userId, game))
            {
                return new ValidationFailure(string.Empty, $"{game} credential are already linked.").ToResult();
            }

            var response = await _gatewayHttpClient.GetCredentialAsync(game.ToString().ToLowerInvariant());

            var credential = new Credential(
                response.UserId,
                response.Game,
                response.PlayerId,
                new UtcNowDateTimeProvider());

            _credentialRepository.CreateCredential(credential);

            await _credentialRepository.UnitOfWork.CommitAsync();

            return new ValidationResult();
        }

        public async Task<ValidationResult> UnlinkCredentialAsync(Credential credential)
        {
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
    }
}

// Filename: AuthFactorService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Factories;
using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Games.Services
{
    public sealed class AuthFactorService : IAuthFactorService
    {
        private readonly IAuthFactorGeneratorFactory _authFactorGeneratorFactory;
        private readonly IAuthFactorValidatorFactory _authFactorValidatorFactory;
        private readonly IAuthFactorRepository _authFactorRepository;

        public AuthFactorService(
            IAuthFactorGeneratorFactory authFactorGeneratorFactory,
            IAuthFactorValidatorFactory authFactorValidatorFactory,
            IAuthFactorRepository authFactorRepository
        )
        {
            _authFactorGeneratorFactory = authFactorGeneratorFactory;
            _authFactorValidatorFactory = authFactorValidatorFactory;
            _authFactorRepository = authFactorRepository;
        }

        public async Task<AuthFactor> FindAuthFactorAsync(UserId userId, Game game)
        {
            return await _authFactorRepository.GetAuthFactorAsync(userId, game);
        }

        public async Task<bool> AuthFactorExistsAsync(UserId userId, Game game)
        {
            return await _authFactorRepository.AuthFactorExistsAsync(userId, game);
        }

        public async Task<ValidationResult> GenerateAuthFactorAsync(UserId userId, Game game, object request)
        {
            var adapter = _authFactorGeneratorFactory.CreateInstance(game);

            return await adapter.GenerateAuthFactorAsync(userId, request);
        }

        public async Task<ValidationResult> ValidateAuthFactorAsync(UserId userId, Game game, AuthFactor authFactor)
        {
            var adapter = _authFactorValidatorFactory.CreateInstance(game);

            return await adapter.ValidateAuthFactorAsync(userId, authFactor);
        }
    }
}

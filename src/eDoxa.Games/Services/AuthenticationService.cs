// Filename: AuthFactorService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Factories;
using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Games.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationGeneratorFactory _authenticationGeneratorFactory;
        private readonly IAuthenticationValidatorFactory _authenticationValidatorFactory;
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(
            IAuthenticationGeneratorFactory authenticationGeneratorFactory,
            IAuthenticationValidatorFactory authenticationValidatorFactory,
            IAuthenticationRepository authenticationRepository
        )
        {
            _authenticationGeneratorFactory = authenticationGeneratorFactory;
            _authenticationValidatorFactory = authenticationValidatorFactory;
            _authenticationRepository = authenticationRepository;
        }
        
        public async Task<GameAuthentication> FindAuthenticationAsync(UserId userId, Game game)
        {
            return await _authenticationRepository.GetAuthenticationAsync(userId, game);
        }

        public async Task<GameAuthentication<TAuthenticationFactor>> FindAuthenticationAsync<TAuthenticationFactor>(UserId userId, Game game)
        where TAuthenticationFactor :class, IGameAuthenticationFactor
        {
            return await _authenticationRepository.GetAuthenticationAsync<TAuthenticationFactor>(userId, game);
        }

        public async Task<bool> AuthenticationExistsAsync(UserId userId, Game game)
        {
            return await _authenticationRepository.AuthenticationExistsAsync(userId, game);
        }

        public async Task<ValidationResult> GenerateAuthenticationAsync(UserId userId, Game game, object request)
        {
            var adapter = _authenticationGeneratorFactory.CreateInstance(game);

            return await adapter.GenerateAuthenticationAsync(userId, request);
        }

        public async Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, Game game, GameAuthentication gameAuthentication)
        {
            var adapter = _authenticationValidatorFactory.CreateInstance(game);

            return await adapter.ValidateAuthenticationAsync(userId, gameAuthentication);
        }
    }
}

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
using eDoxa.Seedwork.Domain.Misc;

using FluentValidation.Results;

namespace eDoxa.Games.Services
{
    public sealed class GameAuthenticationService : IGameAuthenticationService
    {
        private readonly IGameAuthenticationGeneratorFactory _gameAuthenticationGeneratorFactory;
        private readonly IGameAuthenticationValidatorFactory _gameAuthenticationValidatorFactory;
        private readonly IGameAuthenticationRepository _gameAuthenticationRepository;

        public GameAuthenticationService(
            IGameAuthenticationGeneratorFactory gameAuthenticationGeneratorFactory,
            IGameAuthenticationValidatorFactory gameAuthenticationValidatorFactory,
            IGameAuthenticationRepository gameAuthenticationRepository
        )
        {
            _gameAuthenticationGeneratorFactory = gameAuthenticationGeneratorFactory;
            _gameAuthenticationValidatorFactory = gameAuthenticationValidatorFactory;
            _gameAuthenticationRepository = gameAuthenticationRepository;
        }
        
        public async Task<GameAuthentication> FindAuthenticationAsync(UserId userId, Game game)
        {
            return await _gameAuthenticationRepository.GetAuthenticationAsync(userId, game);
        }

        public async Task<GameAuthentication<TAuthenticationFactor>> FindAuthenticationAsync<TAuthenticationFactor>(UserId userId, Game game)
        where TAuthenticationFactor :class, IGameAuthenticationFactor
        {
            return await _gameAuthenticationRepository.GetAuthenticationAsync<TAuthenticationFactor>(userId, game);
        }

        public async Task<bool> AuthenticationExistsAsync(UserId userId, Game game)
        {
            return await _gameAuthenticationRepository.AuthenticationExistsAsync(userId, game);
        }

        public async Task<ValidationResult> GenerateAuthenticationAsync(UserId userId, Game game, object request)
        {
            var adapter = _gameAuthenticationGeneratorFactory.CreateInstance(game);

            return await adapter.GenerateAuthenticationAsync(userId, request);
        }

        public async Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, Game game, GameAuthentication gameAuthentication)
        {
            var adapter = _gameAuthenticationValidatorFactory.CreateInstance(game);

            return await adapter.ValidateAuthenticationAsync(userId, gameAuthentication);
        }
    }
}

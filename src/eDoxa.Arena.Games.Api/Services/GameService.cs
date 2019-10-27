// Filename: GameService.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Api.Infrastructure;
using eDoxa.Arena.Games.Domain.AggregateModels;
using eDoxa.Arena.Games.Domain.Services;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.Extensions.Options;

namespace eDoxa.Arena.Games.Api.Services
{
    public sealed class GameService : IGameService
    {
        private readonly IGameCredentialService _gameCredentialService;

        public GameService(IGameCredentialService gameCredentialService, IOptions<GamesOptions> options)
        {
            _gameCredentialService = gameCredentialService;
            Options = options.Value;
        }

        private GamesOptions Options { get; }

        public async Task<IImmutableSet<GameInfo>> FetchGameInfosAsync(UserId userId)
        {
            var credentials = await _gameCredentialService.FetchGameCredentialsAsync(userId);

            return this.FetchGameInfos()
                .Select(gameInfo => gameInfo.WithCredential(credentials.SingleOrDefault(credential => credential.Game == Game.FromName(gameInfo.Name))))
                .ToImmutableHashSet();
        }

        public IImmutableSet<GameInfo> FetchGameInfos()
        {
            return Options.Games.Select(
                    option => new GameInfo(
                        Game.FromName(option.Key),
                        option.Value.Enabled,
                        option.Value.Displayed,
                        new GameChallengeInfo(option.Value.Challenge.Displayed, option.Value.Challenge.Enabled),
                        new GameTournamentInfo(option.Value.Tournament.Displayed, option.Value.Tournament.Enabled)))
                .ToImmutableHashSet();
        }
    }
}

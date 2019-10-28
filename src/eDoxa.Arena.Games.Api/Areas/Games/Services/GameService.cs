// Filename: GameService.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels;
using eDoxa.Arena.Games.Domain.Services;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.Extensions.Options;

namespace eDoxa.Arena.Games.Api.Areas.Games.Services
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

        public async Task<IReadOnlyCollection<GameInfo>> FetchGameInfosAsync(UserId? userId)
        {
            var credentials = await _gameCredentialService.FetchGameCredentialsAsync(userId ?? UserId.Empty);

            return Options.Select(
                    option => new GameInfo(
                        Game.FromName(option.Key),
                        option.Value.ImageName,
                        option.Value.ReactComponent,
                        option.Value.Services.ToDictionary(
                            service => service.Key,
                            service => new ServiceInfo(service.Key, service.Value.Displayed, service.Value.Enabled))))
                .Select(gameInfo => gameInfo.TryGetGameCredential(credentials.SingleOrDefault(credential => credential.Game == Game.FromName(gameInfo.Name))))
                .OrderBy(gameInfo => gameInfo.DisplayName)
                .ToList();
        }
    }
}

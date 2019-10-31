// Filename: GameService.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Api.Areas.Games.Services.Abstractions;
using eDoxa.Arena.Games.Domain.Repositories;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Api.Areas.Games.Services
{
    public sealed class GameService : IGameService
    {
        private readonly ICredentialRepository _credentialRepository;

        public GameService(ICredentialRepository credentialRepository)
        {
            _credentialRepository = credentialRepository;
        }

        public async Task<IReadOnlyCollection<Game>> FetchGamesWithCredentialAsync(UserId userId)
        {
            var credentials = await _credentialRepository.FetchCredentialsAsync(userId);

            return credentials.Select(credential => credential.Game).ToList();
        }
    }
}

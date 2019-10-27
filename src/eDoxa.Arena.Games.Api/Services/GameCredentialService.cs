// Filename: GameCredentialService.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.GameCredentialAggregate;
using eDoxa.Arena.Games.Domain.Services;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Arena.Games.Api.Services
{
    public sealed class GameCredentialService : IGameCredentialService
    {
        public async Task<ValidationResult> CreateGameCredentialAsync(UserId userId, Game game, PlayerId playerId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ValidationResult> DeleteGameCredentialAsync(UserId userId, Game game)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyCollection<GameCredential>> FetchGameCredentialsAsync(UserId userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GameCredential> FindGameCredentialAsync(UserId userId, Game game)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> GameCredentialExistsAsync(UserId userId, Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}

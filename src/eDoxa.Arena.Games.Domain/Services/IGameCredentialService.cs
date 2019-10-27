// Filename: IGameCredentialService.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.GameCredentialAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Arena.Games.Domain.Services
{
    public interface IGameCredentialService
    {
        Task<ValidationResult> CreateGameCredentialAsync(UserId userId, Game game, PlayerId playerId);

        Task<ValidationResult> DeleteGameCredentialAsync(UserId userId, Game game);

        Task<IReadOnlyCollection<GameCredential>> FetchGameCredentialsAsync(UserId userId);

        Task<GameCredential?> FindGameCredentialAsync(UserId userId, Game game);

        Task<bool> GameCredentialExistsAsync(UserId userId, Game game);
    }
}

// Filename: IGameCredentialRepository.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.GameCredentialAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Domain.Repositories
{
    public interface IGameCredentialRepository : IRepository<GameCredential>
    {
        void CreateGameCredential(GameCredential gameCredential);

        void DeleteGameCredential(GameCredential gameCredential);

        Task<IReadOnlyCollection<GameCredential>> FetchGameCredentialsAsync(UserId userId);

        Task<GameCredential?> FindGameCredentialAsync(UserId userId, Game game);

        Task<bool> GameCredentialExistsAsync(UserId userId, Game game);
    }
}

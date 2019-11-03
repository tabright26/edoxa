// Filename: ICredentialRepository.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Games.Domain.Repositories
{
    public interface ICredentialRepository : IRepository<Credential>
    {
        void CreateCredential(Credential credential);

        void DeleteCredential(Credential credential);

        Task<IReadOnlyCollection<Credential>> FetchCredentialsAsync(UserId userId);

        Task<Credential?> FindCredentialAsync(UserId userId, Game game);

        Task<bool> CredentialExistsAsync(UserId userId, Game game);
    }
}

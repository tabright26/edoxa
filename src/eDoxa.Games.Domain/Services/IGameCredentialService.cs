// Filename: ICredentialService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Services
{
    public interface IGameCredentialService
    {
        Task<DomainValidationResult<object>> LinkCredentialAsync(UserId userId, Game game);

        Task<DomainValidationResult<Credential>> UnlinkCredentialAsync(Credential credential);

        Task<IReadOnlyCollection<Credential>> FetchCredentialsAsync(UserId userId);

        Task<Credential?> FindCredentialAsync(UserId userId, Game game);

        Task<bool> CredentialExistsAsync(UserId userId, Game game);
    }
}

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

namespace eDoxa.Games.Abstractions.Services
{
    public interface IGameCredentialService
    {
        Task<DomainValidationResult> LinkCredentialAsync(UserId userId, Game game);

        Task<DomainValidationResult> UnlinkCredentialAsync(Credential credential);

        Task<IReadOnlyCollection<Credential>> FetchCredentialsAsync(UserId userId);

        Task<Credential?> FindCredentialAsync(UserId userId, Game game);

        Task<bool> CredentialExistsAsync(UserId userId, Game game);
    }
}

// Filename: ICredentialService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Services
{
    public interface IGameCredentialService
    {
        Task<DomainValidationResult<Credential>> LinkCredentialAsync(UserId userId, Game game);

        Task<DomainValidationResult<Credential>> UnlinkCredentialAsync(Credential credential);

        Task<Credential?> FindCredentialAsync(UserId userId, Game game);

        Task<bool> CredentialExistsAsync(UserId userId, Game game);
    }
}

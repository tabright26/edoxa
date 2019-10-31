// Filename: IGameCredentialService.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Arena.Games.Api.Areas.Credentials.Services.Abstractions
{
    public interface ICredentialService
    {
        Task<ValidationResult> LinkCredentialAsync(UserId userId, Game game);

        Task<ValidationResult> UnlinkCredentialAsync(Credential credential);

        Task<IReadOnlyCollection<Credential>> FetchCredentialsAsync(UserId userId);

        Task<Credential?> FindCredentialAsync(UserId userId, Game game);

        Task<bool> CredentialExistsAsync(UserId userId, Game game);
    }
}

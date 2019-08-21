// Filename: IIdentityTestFileStorage.cs
// Date Created: 2019-08-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Immutable;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Models;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public interface IIdentityTestFileStorage
    {
        Task<IImmutableSet<UserClaim>> GetUserClaimsAsync();

        Task<IImmutableSet<User>> GetUsersAsync();

        Task<IImmutableSet<UserRole>> GetUserRolesAsync();
    }
}

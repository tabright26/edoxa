// Filename: IRoleManager.cs
// Date Created: 2019-11-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.Services
{
    public interface IRoleService
    {
        bool SupportsQueryableRoles { get; }

        bool SupportsRoleClaims { get; }

        Task<IdentityResult> CreateAsync(Role role);

        Task UpdateNormalizedRoleNameAsync(Role role);

        Task<IdentityResult> UpdateAsync(Role role);

        Task<IdentityResult> DeleteAsync(Role role);

        Task<bool> RoleExistsAsync(string roleName);

        Task<Role> FindByIdAsync(string roleId);

        Task<string> GetRoleNameAsync(Role role);

        Task<IdentityResult> SetRoleNameAsync(Role role, string name);

        Task<string> GetRoleIdAsync(Role role);

        Task<Role> FindByNameAsync(string roleName);

        Task<IdentityResult> AddClaimAsync(Role role, Claim claim);

        Task<IdentityResult> RemoveClaimAsync(Role role, Claim claim);

        Task<IList<Claim>> GetClaimsAsync(Role role);

        void Dispose();
    }
}

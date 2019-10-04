// Filename: IdentityDbContextSeeder.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using static eDoxa.Identity.Api.Infrastructure.Data.Storage.FileStorage;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    internal sealed class IdentityDbContextSeeder : DbContextSeeder
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        public IdentityDbContextSeeder(
            UserManager userManager,
            RoleManager roleManager,
            IHostingEnvironment environment,
            ILogger<IdentityDbContextSeeder> logger
        ) : base(environment, logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task SeedAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                foreach (var role in Roles)
                {
                    await _roleManager.CreateAsync(role);

                    foreach (var roleClaim in RoleClaims)
                    {
                        await _roleManager.AddClaimAsync(role, roleClaim.ToClaim());
                    }
                }

                Logger.LogInformation("The roles being populated:");
            }
            else
            {
                Logger.LogInformation("The roles already populated.");
            }
        }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!_userManager.Users.Any())
            {
                foreach (var testUser in Users)
                {
                    await _userManager.CreateAsync(testUser, "Pass@word1");

                    foreach (var testUserClaim in UserClaims.Where(userClaimModel => userClaimModel.UserId == testUser.Id))
                    {
                        await _userManager.AddClaimAsync(testUser, testUserClaim.ToClaim());
                    }

                    foreach (var testUserRole in UserRoles.Where(userRoleModel => userRoleModel.UserId == testUser.Id))
                    {
                        await _userManager.AddToRoleAsync(testUser, Roles.Single(roleModel => roleModel.Id == testUserRole.RoleId).Name);
                    }
                }

                Logger.LogInformation("The users being populated...");
            }
            else
            {
                Logger.LogInformation("The users already populated.");
            }
        }
    }
}

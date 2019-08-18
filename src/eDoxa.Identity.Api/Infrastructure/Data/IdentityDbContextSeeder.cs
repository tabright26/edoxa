// Filename: IdentityDbContextSeeder.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    internal sealed class IdentityDbContextSeeder : IDbContextSeeder
    {
        private readonly ILogger<IdentityDbContextSeeder> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        public IdentityDbContextSeeder(
            ILogger<IdentityDbContextSeeder> logger,
            IHostingEnvironment environment,
            UserManager userManager,
            RoleManager roleManager
        )
        {
            _logger = logger;
            _environment = environment;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var roles = IdentityStorage.Roles;

            var roleClaims = IdentityStorage.RoleClaims;

            var testUsers = IdentityStorage.TestUsers;

            var testUserClaims = IdentityStorage.TestUserClaims;

            var testUserRoles = IdentityStorage.TestUserRoles;

            if (!_roleManager.Roles.Any())
            {
                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);

                    foreach (var roleClaim in roleClaims)
                    {
                        await _roleManager.AddClaimAsync(role, roleClaim.ToClaim());
                    }
                }

                _logger.LogInformation("The roles being populated:");
            }
            else
            {
                _logger.LogInformation("The roles already populated.");
            }

            if (_environment.IsDevelopment())
            {
                if (!_userManager.Users.Any())
                {
                    foreach (var testUser in testUsers)
                    {
                        await _userManager.CreateAsync(testUser, "Pass@word1");

                        foreach (var testUserClaim in testUserClaims.Where(userClaimModel => userClaimModel.UserId == testUser.Id))
                        {
                            await _userManager.AddClaimAsync(testUser, testUserClaim.ToClaim());
                        }

                        foreach (var testUserRole in testUserRoles.Where(userRoleModel => userRoleModel.UserId == testUser.Id))
                        {
                            await _userManager.AddToRoleAsync(testUser, roles.Single(roleModel => roleModel.Id == testUserRole.RoleId).Name);
                        }
                    }

                    _logger.LogInformation("The users being populated...");
                }
                else
                {
                    _logger.LogInformation("The users already populated.");
                }
            }
        }
    }
}

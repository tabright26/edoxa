// Filename: IdentityDbContextSeeder.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Services;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using static eDoxa.Identity.Api.Infrastructure.Data.Storage.FileStorage;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    internal sealed class IdentityDbContextSeeder : DbContextSeeder
    {
        private readonly IUserService _userService;
        private readonly RoleService _roleService;

        public IdentityDbContextSeeder(
            IUserService userService,
            RoleService roleService,
            IWebHostEnvironment environment,
            IOptions<AdminOptions> options,
            ILogger<IdentityDbContextSeeder> logger
        ) : base(environment, logger)
        {
            Options = options.Value;
            _userService = userService;
            _roleService = roleService;
        }

        public AdminOptions Options { get; }

        protected override async Task SeedAsync()
        {
            if (!_roleService.Roles.Any())
            {
                foreach (var role in Roles)
                {
                    await _roleService.CreateAsync(role);

                    foreach (var roleClaim in RoleClaims)
                    {
                        await _roleService.AddClaimAsync(role, roleClaim.ToClaim());
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
            if (!_userService.Users.Any())
            {
                foreach (var testUser in Users)
                {
                    await _userService.CreateAsync(testUser, "Pass@word1");

                    foreach (var testUserClaim in UserClaims.Where(userClaimModel => userClaimModel.UserId == testUser.Id))
                    {
                        await _userService.AddClaimAsync(testUser, testUserClaim.ToClaim());
                    }

                    foreach (var testUserRole in UserRoles.Where(userRoleModel => userRoleModel.UserId == testUser.Id))
                    {
                        await _userService.AddToRoleAsync(testUser, Roles.Single(roleModel => roleModel.Id == testUserRole.RoleId).Name);
                    }
                }

                Logger.LogInformation("The users being populated...");
            }
            else
            {
                Logger.LogInformation("The users already populated.");
            }
        }

        protected override async Task SeedProductionAsync()
        {
            if (!_userService.Users.Any(user => user.Id == AppAdmin.Id))
            {
                var admin = Users.Single(x => x.Id == AppAdmin.Id);

                await _userService.CreateAsync(admin, Options.Password);

                foreach (var claim in UserClaims.Where(userClaim => userClaim.UserId == admin.Id))
                {
                    await _userService.AddClaimAsync(admin, claim.ToClaim());
                }

                foreach (var role in UserRoles.Where(userRole => userRole.UserId == admin.Id))
                {
                    await _userService.AddToRoleAsync(admin, Roles.Single(roleModel => roleModel.Id == role.RoleId).Name);
                }

                Logger.LogInformation("The admin being populated...");
            }
            else
            {
                Logger.LogInformation("The admin already populated.");
            }
        }
    }
}

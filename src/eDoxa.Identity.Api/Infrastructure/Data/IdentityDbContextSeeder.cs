// Filename: IdentityDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using static eDoxa.Identity.Api.Infrastructure.Data.Storage.FileStorage;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    internal sealed class IdentityDbContextSeeder : DbContextSeeder
    {
        private readonly IDoxatagService _doxatagService;
        private readonly IUserService _userService;
        private readonly RoleService _roleService;
        private readonly IOptions<IdentityAppSettings> _options;

        public IdentityDbContextSeeder(
            IDoxatagService doxatagService,
            IUserService userService,
            RoleService roleService,
            IdentityDbContext context,
            IWebHostEnvironment environment,
            IOptionsSnapshot<IdentityAppSettings> options,
            ILogger<IdentityDbContextSeeder> logger
        ) : base(context, environment, logger)
        {
            _doxatagService = doxatagService;
            _userService = userService;
            _roleService = roleService;
            _options = options;
        }

        private IdentityAppSettings Options => _options.Value;

        protected override async Task SeedAsync()
        {
            await this.SeedRolesAsync();
        }

        protected override async Task SeedDevelopmentAsync()
        {
            await this.SeedTestUsersAsync();

            await this.SeedAdministratorAsync();
        }

        protected override async Task SeedStagingAsync()
        {
            await this.SeedAdministratorAsync();
        }

        protected override async Task SeedProductionAsync()
        {
            await this.SeedAdministratorAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (!await _roleService.Roles.AnyAsync())
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

                Logger.LogInformation(JsonConvert.SerializeObject(await _roleService.Roles.ToListAsync(), Formatting.Indented));
            }
            else
            {
                Logger.LogInformation("The roles already populated.");
            }
        }

        private async Task SeedTestUsersAsync()
        {
            if (!await _userService.Users.AnyAsync())
            {
                foreach (var testUser in Users)
                {
                    if (testUser.Id != AppAdministrator.Id)
                    {
                        await _userService.CreateAsync(testUser);

                        foreach (var testUserClaim in UserClaims.Where(userClaim => userClaim.UserId == testUser.Id))
                        {
                            await _userService.AddClaimAsync(testUser, testUserClaim.ToClaim());
                        }

                        foreach (var testUserRole in UserRoles.Where(userRole => userRole.UserId == testUser.Id))
                        {
                            await _userService.AddToRoleAsync(testUser, Roles.Single(role => role.Id == testUserRole.RoleId).Name);
                        }

                        var user = await _userService.FindByIdAsync(testUser.Id.ToString());

                        await _doxatagService.ChangeDoxatagAsync(user, Doxatags.Single(doxatag => doxatag.UserId == user.Id.ConvertTo<UserId>()).Name);
                    }
                }

                Logger.LogInformation("The users being populated...");
            }
            else
            {
                Logger.LogInformation("The users already populated.");
            }
        }

        private async Task SeedAdministratorAsync()
        {
            if (!await _userService.Users.AnyAsync(user => user.Id == AppAdministrator.Id))
            {
                var administrator = Users.Single(user => user.Id == AppAdministrator.Id);

                await _userService.CreateAsync(administrator, Options.Administrator.Password);

                foreach (var claim in UserClaims.Where(userClaim => userClaim.UserId == administrator.Id))
                {
                    await _userService.AddClaimAsync(administrator, claim.ToClaim());
                }

                foreach (var role in UserRoles.Where(userRole => userRole.UserId == administrator.Id))
                {
                    await _userService.AddToRoleAsync(administrator, Roles.Single(roleModel => roleModel.Id == role.RoleId).Name);
                }

                Logger.LogInformation("The administrator as been created.");
            }
            else
            {
                var administrator = await _userService.FindByIdAsync(AppAdministrator.Id.ToString());

                if (!await _userService.CheckPasswordAsync(administrator, Options.Administrator.Password))
                {
                    await _userService.RemovePasswordAsync(administrator);

                    await _userService.AddPasswordAsync(administrator, Options.Administrator.Password);

                    Logger.LogInformation("The administrator password as been updated.");
                }
            }
        }
    }
}

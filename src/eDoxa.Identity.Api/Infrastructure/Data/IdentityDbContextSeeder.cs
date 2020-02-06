﻿// Filename: IdentityDbContextSeeder.cs
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
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using static eDoxa.Identity.Api.Infrastructure.Data.Storage.FileStorage;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    internal sealed class IdentityDbContextSeeder : DbContextSeeder
    {
        private readonly IDoxatagService _doxatagService;
        private readonly IUserService _userService;
        private readonly RoleService _roleService;
        private readonly IOptions<AdminOptions> _optionsSnapshot;

        public IdentityDbContextSeeder(
            IDoxatagService doxatagService,
            IUserService userService,
            RoleService roleService,
            IdentityDbContext context,
            IWebHostEnvironment environment,
            IOptionsSnapshot<AdminOptions> optionsSnapshot,
            ILogger<IdentityDbContextSeeder> logger
        ) : base(context, environment, logger)
        {
            _doxatagService = doxatagService;
            _userService = userService;
            _roleService = roleService;
            _optionsSnapshot = optionsSnapshot;
        }

        private AdminOptions Options => _optionsSnapshot.Value;

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
                    if (testUser.Id == AppAdmin.Id)
                    {
                        await _userService.CreateAsync(testUser, "Pass@word1");
                    }
                    else
                    {
                        await _userService.CreateAsync(testUser);
                    }

                    foreach (var testUserClaim in UserClaims.Where(userClaimModel => userClaimModel.UserId == testUser.Id))
                    {
                        await _userService.AddClaimAsync(testUser, testUserClaim.ToClaim());
                    }

                    foreach (var testUserRole in UserRoles.Where(userRoleModel => userRoleModel.UserId == testUser.Id))
                    {
                        await _userService.AddToRoleAsync(testUser, Roles.Single(roleModel => roleModel.Id == testUserRole.RoleId).Name);
                    }

                    var user = await _userService.FindByIdAsync(testUser.Id.ToString());

                    await _doxatagService.ChangeDoxatagAsync(user, Doxatags.Single(doxatag => doxatag.UserId == UserId.FromGuid(user.Id)).Name);
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

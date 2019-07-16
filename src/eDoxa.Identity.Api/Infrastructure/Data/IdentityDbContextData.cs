// Filename: IdentityDbContextData.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.Infrastructure;
using eDoxa.Identity.Infrastructure.Models;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    public sealed class IdentityDbContextData : IDbContextData
    {
        private readonly ILogger<IdentityDbContextData> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IdentityDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<RoleModel> _roleManager;

        public IdentityDbContextData(
            ILogger<IdentityDbContextData> logger,
            IHostingEnvironment environment,
            IdentityDbContext context,
            UserManager<UserModel> userManager,
            RoleManager<RoleModel> roleManager
        )
        {
            _logger = logger;
            _environment = environment;
            _context = context;
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
                        await _roleManager.AddClaimAsync(role, new Claim(roleClaim.ClaimType, roleClaim.ClaimValue));
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
                        await _userManager.CreateAsync(testUser);

                        foreach (var testUserClaim in testUserClaims.Where(userClaimModel => userClaimModel.UserId == testUser.Id))
                        {
                            await _userManager.AddClaimAsync(testUser, new Claim(testUserClaim.ClaimType, testUserClaim.ClaimValue));
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

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Users.RemoveRange(_context.Users);

                _context.Roles.RemoveRange(_context.Roles);

                await _context.SaveChangesAsync();
            }
        }
    }
}

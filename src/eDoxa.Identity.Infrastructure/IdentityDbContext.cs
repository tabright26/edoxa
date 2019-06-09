// Filename: IdentityDbContext.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Infrastructure.Configurations;
using eDoxa.Seedwork.Security.Constants;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Infrastructure
{
    public sealed partial class IdentityDbContext
    {
        public async Task Seed(ILogger logger, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var adminRole = new Role("Administrator");

            if (!await roleManager.RoleExistsAsync(adminRole.Name))
            {
                await roleManager.CreateAsync(adminRole);

                await roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, "*"));
            }

            var challengerRole = new Role("Challenger");

            if (!await roleManager.RoleExistsAsync(challengerRole.Name))
            {
                await roleManager.CreateAsync(challengerRole);

                await roleManager.AddClaimAsync(challengerRole, new Claim(CustomClaimTypes.Permission, "challenge.read"));

                await roleManager.AddClaimAsync(challengerRole, new Claim(CustomClaimTypes.Permission, "challenge.register"));
            }

            if (!Users.Any())
            {
                var admin = new User("Administrator", "admin@edoxa.gg", new PersonalName("eDoxa", "Admin"), new BirthDate(1970, 1, 1))
                {
                    Id = Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"),
                    EmailConfirmed = true,
                    PhoneNumber = "0000000000",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false
                };

                await userManager.CreateAsync(admin, "Pass@word1");

                await userManager.AddToRolesAsync(
                    admin,
                    new List<string>
                    {
                        adminRole.Name,
                        challengerRole.Name
                    }
                );

                logger.LogInformation("The users being populated.");
            }
            else
            {
                logger.LogInformation("The users already populated.");
            }
        }
    }

    public sealed partial class IdentityDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        public new DbSet<RoleClaim> RoleClaims => this.Set<RoleClaim>();

        public new DbSet<Role> Roles => this.Set<Role>();

        public new DbSet<UserClaim> UserClaims => this.Set<UserClaim>();

        public new DbSet<User> Users => this.Set<User>();

        public new DbSet<UserLogin> UserLogins => this.Set<UserLogin>();

        public new DbSet<UserRole> UserRoles => this.Set<UserRole>();

        public new DbSet<UserToken> UserTokens => this.Set<UserToken>();

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(nameof(eDoxa).ToLower());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new UserClaimConfiguration());

            builder.ApplyConfiguration(new UserLoginConfiguration());

            builder.ApplyConfiguration(new UserTokenConfiguration());

            builder.ApplyConfiguration(new UserRoleConfiguration());

            builder.ApplyConfiguration(new RoleConfiguration());

            builder.ApplyConfiguration(new RoleClaimConfiguration());
        }
    }
}

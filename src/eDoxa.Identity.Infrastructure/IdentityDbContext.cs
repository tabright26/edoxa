// Filename: IdentityDbContext.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Functional.Extensions;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Infrastructure.Configurations;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Infrastructure
{
    public sealed partial class IdentityDbContext
    {
        public void Seed(ILogger logger, IConfiguration configuration)
        {
            if (!Roles.Any())
            {
                var roles = configuration.GetSection("Roles").Get<HashSet<Role>>();

                Roles.AddRange(roles);

                this.SaveChanges();

                logger.LogInformation("The roles being populated.");
            }
            else
            {
                logger.LogInformation("The roles already populated.");
            }

            if (!Users.Any())
            {
                var users = configuration.GetSection("Users").Get<HashSet<User>>();

                users.ForEach(user => user.HashPassword("Pass@word1"));

                Users.AddRange(users);

                this.SaveChanges();

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
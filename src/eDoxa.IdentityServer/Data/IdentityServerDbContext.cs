// Filename: IdentityServerDbContext.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IdentityServer.Data.Configurations;
using eDoxa.IdentityServer.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.IdentityServer.Data
{
    public class IdentityServerDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public IdentityServerDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
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
// Filename: IdentityDbContext.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Infrastructure.Configurations;
using eDoxa.Identity.Infrastructure.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Infrastructure
{
    public sealed class IdentityDbContext : IdentityDbContext<UserModel, RoleModel, Guid, UserClaimModel, UserRoleModel, UserLoginModel, RoleClaimModel,
        UserTokenModel>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        public new DbSet<UserModel> Users => this.Set<UserModel>();

        public new DbSet<UserClaimModel> UserClaims => this.Set<UserClaimModel>();

        public new DbSet<UserLoginModel> UserLogins => this.Set<UserLoginModel>();

        public new DbSet<UserTokenModel> UserTokens => this.Set<UserTokenModel>();

        public new DbSet<UserRoleModel> UserRoles => this.Set<UserRoleModel>();

        public new DbSet<RoleModel> Roles => this.Set<RoleModel>();

        public new DbSet<RoleClaimModel> RoleClaims => this.Set<RoleClaimModel>();

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserModelConfiguration());

            builder.ApplyConfiguration(new UserClaimModelConfiguration());

            builder.ApplyConfiguration(new UserLoginModelConfiguration());

            builder.ApplyConfiguration(new UserTokenModelConfiguration());

            builder.ApplyConfiguration(new UserRoleModelConfiguration());

            builder.ApplyConfiguration(new RoleModelConfiguration());

            builder.ApplyConfiguration(new RoleClaimModelConfiguration());
        }
    }
}

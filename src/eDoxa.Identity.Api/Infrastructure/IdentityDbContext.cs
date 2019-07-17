// Filename: IdentityDbContext.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Infrastructure
{
    public sealed class IdentityDbContext : IdentityDbContext<UserModel, RoleModel, Guid, UserClaimModel, UserRoleModel, UserLoginModel, RoleClaimModel,
        UserTokenModel>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserModel>().ToTable("User");
            builder.Entity<UserClaimModel>().ToTable("UserClaim");
            builder.Entity<UserLoginModel>().ToTable("UserLogin");
            builder.Entity<UserTokenModel>().ToTable("UserToken");
            builder.Entity<UserRoleModel>().ToTable("UserRole");
            builder.Entity<RoleModel>().ToTable("Role");
            builder.Entity<RoleClaimModel>().ToTable("RoleClaim");
        }
    }
}

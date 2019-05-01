// Filename: RoleConfiguration.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Infrastructure;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.IdentityServer.Data.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure([NotNull] EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(IdentityDbContext.Roles));
        }
    }
}
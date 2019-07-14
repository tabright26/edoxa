// Filename: RoleClaimModelConfiguration.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Infrastructure.Models;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Identity.Infrastructure.Configurations
{
    internal sealed class RoleClaimModelConfiguration : IEntityTypeConfiguration<RoleClaimModel>
    {
        public void Configure([NotNull] EntityTypeBuilder<RoleClaimModel> builder)
        {
            builder.ToTable("RoleClaim");
        }
    }
}

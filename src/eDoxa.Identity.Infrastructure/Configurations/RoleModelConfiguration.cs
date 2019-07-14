// Filename: RoleModelConfiguration.cs
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
    internal sealed class RoleModelConfiguration : IEntityTypeConfiguration<RoleModel>
    {
        public void Configure([NotNull] EntityTypeBuilder<RoleModel> builder)
        {
            builder.ToTable("Role");
        }
    }
}

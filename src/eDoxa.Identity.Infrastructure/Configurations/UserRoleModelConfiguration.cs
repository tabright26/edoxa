// Filename: UserRoleModelConfiguration.cs
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
    internal sealed class UserRoleModelConfiguration : IEntityTypeConfiguration<UserRoleModel>
    {
        public void Configure([NotNull] EntityTypeBuilder<UserRoleModel> builder)
        {
            builder.ToTable("UserRole");
        }
    }
}

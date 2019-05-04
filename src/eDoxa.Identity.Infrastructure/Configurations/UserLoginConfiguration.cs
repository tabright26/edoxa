﻿// Filename: UserLoginConfiguration.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Identity.Infrastructure.Configurations
{
    internal sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure([NotNull] EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable(nameof(IdentityDbContext.UserLogins));
        }
    }
}
// Filename: UserModelConfiguration.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Infrastructure.Models;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    public sealed class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure([NotNull] EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("User");

            builder.Property(user => user.Id).IsRequired();

            builder.Property(user => user.ConnectAccountId).IsRequired();

            builder.Property(user => user.CustomerId).IsRequired();

            builder.Property(user => user.BankAccountId).IsRequired(false);

            builder.HasOne(user => user.Account).WithOne(account => account.User).HasForeignKey<AccountModel>("UserId").IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(user => user.Id);
        }
    }
}

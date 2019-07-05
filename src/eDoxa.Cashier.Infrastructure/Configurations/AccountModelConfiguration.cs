// Filename: AccountModelConfiguration.cs
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
    internal sealed class AccountModelConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure([NotNull] EntityTypeBuilder<AccountModel> builder)
        {
            builder.ToTable("Account");

            builder.Property(account => account.Id).IsRequired();

            builder.Property(account => account.UserId).IsRequired();

            builder.HasMany(account => account.Transactions).WithOne(transaction => transaction.Account).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(account => account.Id);
        }
    }
}

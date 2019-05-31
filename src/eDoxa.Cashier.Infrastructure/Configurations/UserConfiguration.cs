// Filename: UserConfiguration.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Infrastructure.Extensions;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure([NotNull] EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(CashierDbContext.Users));

            builder.EntityId(user => user.Id).IsRequired();

            builder.Property(user => user.ConnectAccountId).IsRequired();

            builder.Property(user => user.CustomerId).IsRequired();

            builder.Property(user => user.BankAccountId).IsRequired(false);

            builder.HasOne(user => user.Account).WithOne(account => account.User).HasForeignKey<Account>(nameof(UserId)).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(user => user.Id);
        }
    }
}

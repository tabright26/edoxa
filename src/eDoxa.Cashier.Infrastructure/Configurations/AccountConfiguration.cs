// Filename: AccountConfiguration.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Infrastructure.Extensions;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure([NotNull] EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(CashierDbContext.Accounts));

            builder.EntityId(account => account.Id).IsRequired();

            builder.Property<UserId>(nameof(UserId)).HasConversion(userId => userId.ToGuid(), userId => UserId.FromGuid(userId)).IsRequired();

            builder.HasMany(account => account.Transactions).WithOne().HasForeignKey(nameof(AccountId)).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(account => account.Id);

            builder.Metadata.FindNavigation(nameof(Account.Transactions)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

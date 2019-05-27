// Filename: TokenAccountConfiguration.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Seedwork.Domain.Entities;
using eDoxa.Seedwork.Infrastructure.Extensions;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class TokenAccountConfiguration : IEntityTypeConfiguration<TokenAccount>
    {
        public void Configure([NotNull] EntityTypeBuilder<TokenAccount> builder)
        {
            builder.ToTable(nameof(CashierDbContext.TokenAccounts));

            builder.EntityId(account => account.Id).IsRequired();

            builder.Property<UserId>(nameof(UserId)).HasConversion(userId => userId.ToGuid(), userId => UserId.FromGuid(userId)).IsRequired();

            builder.Ignore(account => account.Balance);

            builder.Ignore(account => account.Pending);

            builder.Property(account => account.LastDeposit).IsRequired(false);

            builder.HasMany(account => account.Transactions).WithOne().HasForeignKey(nameof(AccountId)).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(account => account.Id);

            builder.Metadata.FindNavigation(nameof(TokenAccount.Transactions)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

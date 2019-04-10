// Filename: AccountConfiguration.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(CashierDbContext.Accounts));

            builder.Property(account => account.Id)
                   .HasConversion(accountId => accountId.ToGuid(), value => AccountId.FromGuid(value))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<UserId>(nameof(UserId)).HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value)).IsRequired();

            builder.HasKey(account => account.Id);

            builder.OwnsOne(
                account => account.Funds,
                accountFunds =>
                {
                    accountFunds.Property(funds => funds.Balance)
                                .HasConversion(money => money.ToDecimal(), amount => Money.FromDecimal(amount))
                                .IsRequired()
                                .UsePropertyAccessMode(PropertyAccessMode.Field);

                    accountFunds.Property(funds => funds.Pending)
                                .HasConversion(money => money.ToDecimal(), amount => Money.FromDecimal(amount))
                                .IsRequired()
                                .UsePropertyAccessMode(PropertyAccessMode.Field);

                    accountFunds.ToTable(nameof(Account.Funds));
                }
            );

            builder.OwnsOne(
                account => account.Tokens,
                accountTokens =>
                {
                    accountTokens.Property(tokens => tokens.Balance)
                                 .HasConversion(token => token.ToDecimal(), amount => Token.FromDecimal(amount))
                                 .IsRequired()
                                 .UsePropertyAccessMode(PropertyAccessMode.Field);

                    accountTokens.Property(tokens => tokens.Pending)
                                 .HasConversion(token => token.ToDecimal(), amount => Token.FromDecimal(amount))
                                 .IsRequired()
                                 .UsePropertyAccessMode(PropertyAccessMode.Field);

                    accountTokens.ToTable(nameof(Account.Tokens));
                }
            );
        }
    }
}
// Filename: AccountModelConfiguration.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class AccountModelConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure( EntityTypeBuilder<AccountModel> builder)
        {
            builder.ToTable("Account");

            builder.Property(account => account.Id).IsRequired().ValueGeneratedNever();

            builder.HasMany(account => account.Transactions).WithOne(transaction => transaction.Account).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(account => account.Id);
        }
    }
}

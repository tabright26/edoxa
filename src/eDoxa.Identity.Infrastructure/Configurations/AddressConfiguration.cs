// Filename: AddressConfiguration.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Identity.Infrastructure.Configurations
{
    internal sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(address => address.Id)
                .HasConversion(addressId => addressId.ToGuid(), value => AddressId.FromGuid(value))
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(address => address.Type)
                .HasConversion(type => type != null ? type.Value : (int?) null, type => type.HasValue ? AddressType.FromValue(type.Value) : null)
                .IsRequired(false);

            builder.Property(address => address.Country).HasConversion(country => country.Name, name => Country.FromName(name)).IsRequired();

            builder.Property(address => address.Line1).IsRequired();

            builder.Property(address => address.Line2).IsRequired(false);

            builder.Property(address => address.City).IsRequired();

            builder.Property(address => address.State).IsRequired(false);

            builder.Property(address => address.PostalCode).IsRequired(false);

            builder.Property(address => address.UserId).IsRequired();

            builder.HasKey(address => address.Id);

            builder.ToTable("Address");
        }
    }
}

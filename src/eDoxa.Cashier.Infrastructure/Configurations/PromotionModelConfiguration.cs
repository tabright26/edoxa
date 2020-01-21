// Filename: PromotionModelConfiguration.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    public sealed class PromotionModelConfiguration : IEntityTypeConfiguration<PromotionModel>
    {
        public void Configure(EntityTypeBuilder<PromotionModel> builder)
        {
            builder.ToTable("Promotion");

            builder.Ignore(promotion => promotion.DomainEvents);

            builder.Property(promotion => promotion.Id).IsRequired().ValueGeneratedNever();

            builder.Property(promotion => promotion.Amount).HasColumnType("decimal(10, 2)").IsRequired();

            builder.Property(promotion => promotion.Currency).IsRequired();

            builder.OwnsMany(
                promotion => promotion.Recipients,
                promotionRecipients =>
                {
                    promotionRecipients.ToTable("PromotionRecipient");

                    promotionRecipients.WithOwner().HasForeignKey("PromotionId");

                    promotionRecipients.Property(promotionRecipient => promotionRecipient.UserId).ValueGeneratedNever();

                    promotionRecipients.HasKey("PromotionId", "UserId");
                });

            builder.HasKey(promotion => promotion.Id);
        }
    }
}

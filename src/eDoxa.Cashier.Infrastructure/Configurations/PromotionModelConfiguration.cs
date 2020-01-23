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

            builder.Property(promotion => promotion.PromotionalCode).IsRequired();

            builder.Property(promotion => promotion.Amount).IsRequired().HasColumnType("decimal(10, 2)");

            builder.Property(promotion => promotion.Currency).IsRequired();

            builder.Property(promotion => promotion.Duration).IsRequired();

            builder.Property(promotion => promotion.ExpiredAt).IsRequired();

            builder.Property(promotion => promotion.CanceledAt).IsRequired(false);

            builder.OwnsMany(
                promotion => promotion.Recipients,
                promotionRecipients =>
                {
                    promotionRecipients.ToTable("PromotionRecipient");

                    promotionRecipients.WithOwner().HasForeignKey(promotionRecipient => promotionRecipient.PromotionId);

                    promotionRecipients.Property(promotionRecipient => promotionRecipient.PromotionId).IsRequired();

                    promotionRecipients.Property(promotionRecipient => promotionRecipient.UserId).IsRequired().ValueGeneratedNever();

                    promotionRecipients.Property(promotionRecipient => promotionRecipient.RedeemedAt).IsRequired();

                    promotionRecipients.HasKey(
                        promotionRecipient => new
                        {
                            promotionRecipient.PromotionId,
                            promotionRecipient.UserId
                        });
                });

            builder.HasKey(promotion => promotion.Id);
        }
    }
}

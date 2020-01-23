// Filename: PromotionRepository.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Extensions;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class PromotionRepository : Repository<Promotion, PromotionModel>
    {
        public PromotionRepository(CashierDbContext context)
        {
            UnitOfWork = context;
            Promotions = context.Set<PromotionModel>();
        }

        private IUnitOfWork UnitOfWork { get; }

        private DbSet<PromotionModel> Promotions { get; }

        private async Task<IReadOnlyCollection<PromotionModel>> FetchPromotionModelsAsync()
        {
            var promotions = from promotion in Promotions.AsExpandable()
                             select promotion;

            return await promotions.ToListAsync();
        }

        private async Task<PromotionModel?> FindPromotionModelOrNullAsync(string promotionalCode)
        {
            var promotions = from promotion in await Promotions.AsExpandable().ToListAsync()
                             where string.Equals(promotion.PromotionalCode, promotionalCode, StringComparison.OrdinalIgnoreCase)
                             orderby promotion.ExpiredAt descending
                             select promotion;

            return promotions.FirstOrDefault();
        }
    }

    public sealed partial class PromotionRepository : IPromotionRepository
    {
        public void Create(Promotion promotion)
        {
            var promotionModel = promotion.ToModel();

            Promotions.Add(promotionModel);

            Mappings[promotion] = promotionModel;
        }

        public async Task<IReadOnlyCollection<Promotion>> FetchPromotionsAsync()
        {
            var promotions = await this.FetchPromotionModelsAsync();

            return promotions.Select(Selector).ToList();

            Promotion Selector(PromotionModel promotionModel)
            {
                var promotion = promotionModel.ToEntity();

                Mappings[promotion] = promotionModel;

                return promotion;
            }
        }

        public async Task<Promotion?> FindPromotionOrNullAsync(string promotionalCode)
        {
            var promotion = Mappings.Keys.Where(x => string.Equals(x.PromotionalCode, promotionalCode, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.ExpiredAt)
                .FirstOrDefault();

            if (promotion != null)
            {
                return promotion;
            }

            var promotionModel = await this.FindPromotionModelOrNullAsync(promotionalCode);

            if (promotionModel == null)
            {
                return null;
            }

            promotion = promotionModel.ToEntity();

            Mappings[promotion] = promotionModel;

            return promotion;
        }

        public async Task<bool> IsPromotionalCodeAvailableAsync(string promotionalCode)
        {
            var promotions = from promotion in await Promotions.AsExpandable().ToListAsync()
                             where string.Equals(promotion.PromotionalCode, promotionalCode, StringComparison.OrdinalIgnoreCase) &&
                                   !Promotion.IsCanceled(promotion.CanceledAt) &&
                                   !Promotion.IsExpired(promotion.ExpiredAt)
                             select promotion;

            return !promotions.Any();
        }

        public override async Task CommitAsync(bool dispatchDomainEvents = true, CancellationToken cancellationToken = default)
        {
            foreach (var (promotion, promotionModel) in Mappings)
            {
                this.CopyChanges(promotion, promotionModel);
            }

            await UnitOfWork.CommitAsync(dispatchDomainEvents, cancellationToken);
        }

        private void CopyChanges(Promotion promotion, PromotionModel promotionModel)
        {
            promotionModel.DomainEvents = promotion.DomainEvents.ToList();

            promotion.ClearDomainEvents();

            promotionModel.CanceledAt = promotion.CanceledAt;

            var recipients = promotion.Recipients.Where(
                recipient => promotionModel.Recipients.All(promotionRecipientModel => promotionRecipientModel.UserId != recipient.User.Id));

            foreach (var promotionRecipientModel in recipients.Select(recipient => recipient.ToModel()))
            {
                promotionModel.Recipients.Add(promotionRecipientModel);
            }
        }
    }
}

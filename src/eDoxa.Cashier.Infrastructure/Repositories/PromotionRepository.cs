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

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class PromotionRepository
    {
        private readonly IDictionary<Promotion, PromotionModel> _materializedObjects = new Dictionary<Promotion, PromotionModel>();
        private readonly CashierDbContext _context;

        public PromotionRepository(CashierDbContext context)
        {
            _context = context;
        }

        private IUnitOfWork UnitOfWork => _context;

        private DbSet<PromotionModel> Promotions => _context.Set<PromotionModel>();

        private async Task<IReadOnlyCollection<PromotionModel>> FetchPromotionModelsAsync()
        {
            var promotions = from promotion in Promotions.AsExpandable()
                             select promotion;

            return await promotions.ToListAsync();
        }

        private async Task<PromotionModel?> FindPromotionModelOrNullAsync(string promotionalCode)
        {
            var promotions = from promotion in Promotions.AsExpandable()
                             where promotion.PromotionalCode == promotionalCode && !promotion.CanceledAt.HasValue
                             select promotion;

            return await promotions.SingleOrDefaultAsync();
        }
    }

    public sealed partial class PromotionRepository : IPromotionRepository
    {
        public void Create(Promotion promotion)
        {
            var promotionModel = ToModel(promotion);

            Promotions.Add(promotionModel);

            _materializedObjects[promotion] = promotionModel;
        }

        public async Task<IReadOnlyCollection<Promotion>> FetchPromotionsAsync()
        {
            var promotions = await this.FetchPromotionModelsAsync();

            return promotions.Select(Selector).ToList();

            Promotion Selector(PromotionModel promotionModel)
            {
                var promotion = FromModel(promotionModel);

                _materializedObjects[promotion] = promotionModel;

                return promotion;
            }
        }

        public async Task<Promotion?> FindPromotionOrNullAsync(string promotionalCode)
        {
            if (_materializedObjects.TryGetValue(x => x.PromotionalCode == promotionalCode && !x.CanceledAt.HasValue, out var promotion))
            {
                return promotion;
            }

            var promotionModel = await this.FindPromotionModelOrNullAsync(promotionalCode);

            if (promotionModel == null)
            {
                return null;
            }

            promotion = FromModel(promotionModel);

            _materializedObjects[promotion] = promotionModel;

            return promotion;
        }

        public async Task CommitAsync(bool dispatchDomainEvents = true, CancellationToken cancellationToken = default)
        {
            foreach (var (promotion, promotionModel) in _materializedObjects)
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

            foreach (var promotionRecipientModel in recipients.Select(ToModel))
            {
                promotionModel.Recipients.Add(promotionRecipientModel);
            }
        }
    }

    public sealed partial class PromotionRepository
    {
        private static Promotion FromModel(PromotionModel promotionModel)
        {
            var promotion = new Promotion(
                promotionModel.PromotionalCode,
                Currency.FromValue(promotionModel.Currency).From(promotionModel.Amount),
                TimeSpan.FromTicks(promotionModel.Duration),
                new DateTimeProvider(promotionModel.ExpiredAt));

            promotion.SetEntityId(promotionModel.Id);

            foreach (var recipient in promotionModel.Recipients)
            {
                promotion.Redeem(FromModel(recipient));
            }

            if (promotionModel.CanceledAt.HasValue)
            {
                promotion.Cancel(new DateTimeProvider(promotionModel.CanceledAt.Value));
            }

            return promotion;
        }

        private static PromotionRecipient FromModel(PromotionRecipientModel promotionRecipientModel)
        {
            var user = new User(promotionRecipientModel.UserId.ConvertTo<UserId>());

            return new PromotionRecipient(user, new DateTimeProvider(promotionRecipientModel.RedeemedAt));
        }

        private static PromotionModel ToModel(Promotion promotion)
        {
            return new PromotionModel
            {
                Id = promotion.Id,
                PromotionalCode = promotion.PromotionalCode,
                Amount = promotion.Amount,
                Duration = promotion.Duration.Ticks,
                Currency = promotion.Currency.Value,
                CanceledAt = promotion.CanceledAt,
                ExpiredAt = promotion.ExpiredAt,
                Recipients = promotion.Recipients.Select(ToModel).ToList(),
                DomainEvents = promotion.DomainEvents.ToList()
            };
        }

        private static PromotionRecipientModel ToModel(PromotionRecipient promotionRecipient)
        {
            return new PromotionRecipientModel
            {
                UserId = promotionRecipient.User.Id,
                RedeemedAt = promotionRecipient.RedeemedAt
            };
        }
    }
}

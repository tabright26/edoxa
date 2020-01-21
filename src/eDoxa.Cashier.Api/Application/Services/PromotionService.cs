// Filename: PromotionService.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Application.Services
{
    public sealed class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<IReadOnlyCollection<Promotion>> FetchPromotionsAsync()
        {
            return await _promotionRepository.FetchPromotionsAsync();
        }

        public async Task<Promotion?> FindPromotionOrNullAsync(string code)
        {
            return await _promotionRepository.FindPromotionOrNullAsync(code);
        }

        public async Task<IDomainValidationResult> CreatePromotionAsync(
            string code,
            ICurrency currency,
            TimeSpan duration,
            DateTime expiredAt
        )
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                var promotion = new Promotion(
                    code,
                    currency,
                    duration,
                    new DateTimeProvider(expiredAt));

                _promotionRepository.Create(promotion);

                await _promotionRepository.CommitAsync();

                result.AddEntityToMetadata(promotion);
            }

            return result;
        }

        public async Task<IDomainValidationResult> RedeemPromotionAsync(Promotion promotion, UserId userId, IDateTimeProvider redeemedAt)
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                var user = new User(userId);

                var recipient = new PromotionRecipient(user, redeemedAt);

                promotion.Redeem(recipient);

                await _promotionRepository.CommitAsync();

                result.AddEntityToMetadata(promotion);
            }

            return result;
        }

        public async Task<IDomainValidationResult> CancelPromotionAsync(Promotion promotion, IDateTimeProvider canceledAt)
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                promotion.Cancel(canceledAt);

                await _promotionRepository.CommitAsync();

                result.AddEntityToMetadata(promotion);
            }

            return result;
        }
    }
}

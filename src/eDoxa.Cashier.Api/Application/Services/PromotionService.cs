﻿// Filename: PromotionService.cs
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

        public async Task<DomainValidationResult<Promotion>> CreatePromotionAsync(
            string promotionalCode,
            Currency currency,
            TimeSpan duration,
            DateTime expiredAt
        )
        {
            var result = new DomainValidationResult<Promotion>();

            if (!await _promotionRepository.IsPromotionalCodeAvailableAsync(promotionalCode))
            {
                result.AddFailedPreconditionError("The promotional code isn't available");
            }

            if (result.IsValid)
            {
                var promotion = new Promotion(
                    promotionalCode,
                    currency,
                    duration,
                    new DateTimeProvider(expiredAt));

                _promotionRepository.Create(promotion);

                await _promotionRepository.CommitAsync();

                return promotion;
            }

            return result;
        }

        public async Task<DomainValidationResult<Promotion>> RedeemPromotionAsync(Promotion promotion, UserId userId, IDateTimeProvider redeemedAt)
        {
            var result = new DomainValidationResult<Promotion>();

            var user = new User(userId);

            var recipient = new PromotionRecipient(user, redeemedAt);

            if (promotion.IsExpired())
            {
                result.AddFailedPreconditionError("The promotional code is expired");
            }

            if (!promotion.IsActive())
            {
                result.AddFailedPreconditionError("The promotional code is invalid");
            }

            if (promotion.IsRedeemBy(recipient))
            {
                result.AddFailedPreconditionError("The promotional code is already redeemed");
            }

            if (result.IsValid)
            {
                promotion.Redeem(recipient);

                await _promotionRepository.CommitAsync();

                return promotion;
            }

            return result;
        }

        public async Task<DomainValidationResult<Promotion>> CancelPromotionAsync(Promotion promotion, IDateTimeProvider canceledAt)
        {
            var result = new DomainValidationResult<Promotion>();

            if (promotion.IsCanceled())
            {
                result.AddFailedPreconditionError("The promotional code is canceled");
            }

            if (promotion.IsExpired())
            {
                result.AddFailedPreconditionError("The promotional code is expired");
            }

            if (result.IsValid)
            {
                promotion.Cancel(canceledAt);

                await _promotionRepository.CommitAsync();

                return promotion;
            }

            return result;
        }
    }
}

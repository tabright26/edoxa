// Filename: IPromotionService.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IPromotionService
    {
        Task<IReadOnlyCollection<Promotion>> FetchPromotionsAsync();

        Task<Promotion?> FindPromotionOrNullAsync(string promotionalCode);

        Task<IDomainValidationResult> CreatePromotionAsync(
            string promotionalCode,
            Currency currency,
            TimeSpan duration,
            DateTime expiredAt
        );

        Task<IDomainValidationResult> RedeemPromotionAsync(Promotion promotion, UserId userId, IDateTimeProvider redeemedAt);

        Task<IDomainValidationResult> CancelPromotionAsync(Promotion promotion, IDateTimeProvider canceledAt);
    }
}

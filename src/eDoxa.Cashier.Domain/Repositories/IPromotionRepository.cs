// Filename: IPromotionRepository.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IPromotionRepository
    {
        void Create(Promotion promotion);

        Task<IReadOnlyCollection<Promotion>> FetchPromotionsAsync();

        Task<Promotion?> FindPromotionOrNullAsync(string promotionalCode);

        Task CommitAsync(bool dispatchDomainEvents = true, CancellationToken cancellationToken = default);
    }
}

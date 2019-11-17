// Filename: IStripeRepository.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.AggregateModels.StripeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Payment.Domain.Stripe.Repositories
{
    public interface IStripeRepository
    {
        IUnitOfWork UnitOfWork { get; }

        void Create(StripeReference reference);

        Task<StripeReference> GetReferenceAsync(UserId userId);

        Task<StripeReference?> FindReferenceAsync(UserId userId);

        Task<bool> ReferenceExistsAsync(UserId userId);
    }
}

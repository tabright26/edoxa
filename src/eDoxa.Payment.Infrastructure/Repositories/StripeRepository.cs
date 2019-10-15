// Filename: StripeRepository.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.AggregateModels.StripeAggregate;
using eDoxa.Payment.Domain.Stripe.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Payment.Infrastructure.Repositories
{
    public sealed class StripeRepository : IStripeRepository
    {
        private readonly PaymentDbContext _context;

        public StripeRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Create(StripeReference reference)
        {
            _context.StripeReferences.Add(reference);
        }

        public async Task<StripeReference> GetReferenceAsync(UserId userId)
        {
            return await _context.StripeReferences.SingleAsync(reference => reference.UserId == userId);
        }

        public async Task<StripeReference?> FindReferenceAsync(UserId userId)
        {
            return await _context.StripeReferences.SingleOrDefaultAsync(reference => reference.UserId == userId);
        }

        public async Task<bool> ReferenceExistsAsync(UserId userId)
        {
            return await _context.StripeReferences.AnyAsync(reference => reference.UserId == userId);
        }
    }
}

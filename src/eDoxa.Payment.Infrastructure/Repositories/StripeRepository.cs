// Filename: StripeRepository.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;
using eDoxa.Payment.Domain.Repositories;

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

        public async Task<StripeReference> GetReferenceAsync(UserId userId)
        {
            return await _context.StripeReferences.SingleAsync(reference => reference.UserId == userId);
        }

        public async Task<StripeReference?> FindReferenceAsync(UserId userId)
        {
            return await _context.StripeReferences.SingleOrDefaultAsync(reference => reference.UserId == userId);
        }
    }
}

// Filename: IStripeRepository.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;

namespace eDoxa.Payment.Domain.Repositories
{
    public interface IStripeRepository
    {
        Task<StripeReference> GetReferenceAsync(UserId userId);

        Task<StripeReference?> FindReferenceAsync(UserId userId);
    }
}

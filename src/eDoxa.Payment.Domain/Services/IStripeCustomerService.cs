// Filename: ICustomerService.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;

namespace eDoxa.Payment.Domain.Services
{
    public interface IStripeCustomerService
    {
        Task<string> GetCustomerIdAsync(UserId userId);

        Task<string?> FindCustomerIdAsync(UserId userId);

        Task<string> CreateCustomerAsync(UserId userId, string email);
    }
}

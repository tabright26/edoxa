﻿// Filename: IConnectAccountService.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;

namespace eDoxa.Payment.Domain.Services
{
    public interface IStripeAccountService
    {
        Task<string> GetAccountIdAsync(UserId userId);

        Task<string?> FindAccountIdAsync(UserId userId);

        Task<string> CreateAccountAsync(UserId userId, string email, string country);

        Task<bool> AccountIsVerifiedAsync(string accountId);
    }
}

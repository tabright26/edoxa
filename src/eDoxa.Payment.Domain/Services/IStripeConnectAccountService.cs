﻿// Filename: IConnectAccountService.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;

namespace eDoxa.Payment.Domain.Services
{
    public interface IStripeConnectAccountService
    {
        Task<string> GetConnectAccountIdAsync(UserId userId);

        Task<string?> FindConnectAccountIdAsync(UserId userId);
    }
}
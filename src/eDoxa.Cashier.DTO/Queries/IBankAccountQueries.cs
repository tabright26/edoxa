// Filename: IBankAccountQueries.cs
// Date Created: 2019-05-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;

namespace eDoxa.Cashier.DTO.Queries
{
    public interface IBankAccountQueries
    {
        Task<Option<BankAccountDTO>> FindUserBankAccountAsync(CustomerId customerId);
    }
}
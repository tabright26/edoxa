﻿// Filename: ICardQueries.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.DTO.Queries
{
    public interface ICardQueries
    {
        Task<Option<CardListDTO>> FindUserCardsAsync(CustomerId customerId);

        Task<Option<CardDTO>> FindUserCardAsync(CustomerId customerId, CardId cardId);
    }
}
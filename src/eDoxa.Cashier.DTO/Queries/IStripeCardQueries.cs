// Filename: ICardQueries.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

namespace eDoxa.Cashier.DTO.Queries
{
    public interface IStripeCardQueries
    {
        Task<StripeCardListDTO> GetCardsAsync();
    }
}
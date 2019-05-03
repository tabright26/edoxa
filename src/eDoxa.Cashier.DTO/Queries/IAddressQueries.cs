// Filename: IAddressQueries.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.DTO.Queries
{
    public interface IAddressQueries
    {
        Task<Option<AddressDTO>> FindUserAddressAsync(CustomerId customerId);
    }
}
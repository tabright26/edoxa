// Filename: IAddressQueries.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;

namespace eDoxa.Cashier.DTO.Queries
{
    public interface IAddressQueries
    {
        Task<AddressDTO> FindUserAddressAsync(UserId userId);
    }
}
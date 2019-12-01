// Filename: IAddressService.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Services
{
    public interface IAddressService
    {
        Task<Address?> FindUserAddressAsync(User user, AddressId addressId);

        Task<IReadOnlyCollection<Address>> GetAddressBookAsync(User user);

        Task<IdentityResult> RemoveAddressAsync(User user, AddressId addressId);

        Task<IdentityResult> AddAddressAsync(
            User user,
            Country country,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        );

        Task<IdentityResult> UpdateAddressAsync(
            User user,
            AddressId addressId,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        );
    }
}

// Filename: IAddressService.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Identity.Domain.Services
{
    public interface IAddressService
    {
        Task<Address?> FindUserAddressAsync(User user, AddressId addressId);

        Task<IReadOnlyCollection<Address>> FetchAddressBookAsync(User user);

        Task<IDomainValidationResult> AddAddressAsync(
            UserId userId,
            Country country,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        );

        Task<IDomainValidationResult> UpdateAddressAsync(
            Address address,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        );

        Task<IDomainValidationResult> RemoveAddressAsync(Address address);
    }
}

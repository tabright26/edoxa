// Filename: IAddressRepository.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Identity.Domain.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        void Create(Address address);

        void Delete(Address address);

        Task<Address?> FindAddressAsync(UserId userId, AddressId addressId);

        Task<IReadOnlyCollection<Address>> FetchAddressBookAsync(UserId userId);
    }
}

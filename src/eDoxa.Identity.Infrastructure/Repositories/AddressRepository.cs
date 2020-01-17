// Filename: AddressRepository.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Infrastructure.Repositories
{
    public sealed partial class AddressRepository
    {
        private readonly IdentityDbContext _context;

        public AddressRepository(IdentityDbContext context)
        {
            _context = context;
        }

        private DbSet<Address> AddressBook => _context.Set<Address>();

        public IUnitOfWork UnitOfWork => _context;
    }

    public sealed partial class AddressRepository : IAddressRepository
    {
        public void Create(Address address)
        {
            AddressBook.Add(address);
        }

        public void Delete(Address address)
        {
            AddressBook.Remove(address);
        }

        public async Task<IReadOnlyCollection<Address>> FetchAddressBookAsync(UserId userId)
        {
            return await AddressBook.AsExpandable().Where(address => address.UserId == userId).ToListAsync();
        }

        public async Task<Address?> FindAddressAsync(UserId userId, AddressId addressId)
        {
            return await AddressBook.AsExpandable().SingleOrDefaultAsync(address => address.UserId == userId && address.Id == addressId);
        }

        public async Task<int> AddressCountAsync(UserId userId)
        {
            return await AddressBook.AsExpandable().CountAsync(address => address.UserId == userId);
        }
    }
}

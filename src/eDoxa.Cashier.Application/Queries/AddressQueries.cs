// Filename: AddressQueries.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Functional.Maybe;

using Microsoft.EntityFrameworkCore;

using Stripe;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class AddressQueries
    {
        private readonly CashierDbContext _context;
        private readonly CustomerService _customerService;
        private readonly IMapper _mapper;

        public AddressQueries(CashierDbContext context, CustomerService customerService, IMapper mapper)
        {
            _context = context;
            _customerService = customerService;
            _mapper = mapper;
        }

        private async Task<User> FindUserAsNoTrackingAsync(UserId userId)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(user => user.MoneyAccount.Transactions)
                .Include(user => user.TokenAccount.Transactions)
                .Where(user => user.Id == userId)
                .SingleOrDefaultAsync();
        }
    }

    public sealed partial class AddressQueries : IAddressQueries
    {
        public async Task<Option<AddressDTO>> FindUserAddressAsync(UserId userId)
        {
            var user = await this.FindUserAsNoTrackingAsync(userId);

            var customer = await _customerService.GetAsync(user.CustomerId.ToString());

            var address = customer?.Shipping?.Address;

            var mapper = _mapper.Map<AddressDTO>(address);

            return mapper != null ? new Option<AddressDTO>(mapper) : new Option<AddressDTO>();
        }
    }
}
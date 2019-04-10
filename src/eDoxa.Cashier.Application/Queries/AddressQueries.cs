// Filename: AddressQueries.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;

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
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<User> FindUserAsNoTrackingAsync(UserId userId)
        {
            return await _context.Users.AsNoTracking().Include(user => user.Account).Where(user => user.Id == userId).SingleOrDefaultAsync();
        }
    }

    public sealed partial class AddressQueries : IAddressQueries
    {
        public async Task<AddressDTO> FindUserAddressAsync(UserId userId)
        {
            var user = await this.FindUserAsNoTrackingAsync(userId);

            var customer = await _customerService.GetAsync(user.CustomerId.ToString());

            var address = customer?.Shipping?.Address;

            return _mapper.Map<AddressDTO>(address);
        }
    }
}
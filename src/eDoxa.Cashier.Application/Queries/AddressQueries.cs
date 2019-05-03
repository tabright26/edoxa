// Filename: AddressQueries.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional.Maybe;

using Stripe;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class AddressQueries
    {
        private readonly CustomerService _customerService;
        private readonly IMapper _mapper;

        public AddressQueries(CustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
    }

    public sealed partial class AddressQueries : IAddressQueries
    {
        public async Task<Option<AddressDTO>> FindUserAddressAsync(CustomerId customerId)
        {
            var customer = await _customerService.GetAsync(customerId.ToString());

            var address = customer?.Shipping?.Address;

            var mapper = _mapper.Map<AddressDTO>(address);

            return mapper != null ? new Option<AddressDTO>(mapper) : new Option<AddressDTO>();
        }
    }
}
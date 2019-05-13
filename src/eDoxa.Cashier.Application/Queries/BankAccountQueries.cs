// Filename: BankAccountQueries.cs
// Date Created: 2019-05-11
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

using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed class BankAccountQueries : IBankAccountQueries
    {
        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;

        public BankAccountQueries(IStripeService stripeService, IMapper mapper)
        {
            _stripeService = stripeService;
            _mapper = mapper;
        }

        public async Task<Option<BankAccountDTO>> FindUserBankAccountAsync(CustomerId customerId)
        {
            var option = await _stripeService.GetUserBankAccountAsync(customerId);

            return option
                .Select(bankAccount => new Option<BankAccountDTO>(_mapper.Map<BankAccountDTO>(bankAccount)))
                .DefaultIfEmpty(new Option<BankAccountDTO>())
                .Single();
        }
    }
}
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

using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional;

using Stripe;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed class BankAccountQueries : IBankAccountQueries
    {
        private readonly BankAccountService _bankAccountService;
        private readonly IMapper _mapper;

        public BankAccountQueries(BankAccountService bankAccountService, IMapper mapper)
        {
            _bankAccountService = bankAccountService;
            _mapper = mapper;
        }

        public async Task<Option<BankAccountDTO>> FindUserBankAccountAsync(CustomerId customerId)
        {
            var bankAccounts = await _bankAccountService.ListAsync(customerId.ToString(), new BankAccountListOptions());
             
            var bankAccount = bankAccounts.FirstOrDefault(x => !x.Deleted ?? true);

            return bankAccount != null ? new Option<BankAccountDTO>(_mapper.Map<BankAccountDTO>(bankAccount)) : new Option<BankAccountDTO>();
        }
    }
}
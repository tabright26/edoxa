// Filename: MoneyAccountQueries.cs
// Date Created: 2019-04-26
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

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class MoneyAccountQueries
    {
        private readonly CashierDbContext _context;
        private readonly IMapper _mapper;

        public MoneyAccountQueries(CashierDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<MoneyAccount> FindAccountAsNoTrackingAsync(UserId userId)
        {
            return await _context.MoneyAccounts.AsNoTracking().Include(transaction => transaction.Transactions).Where(account => account.User.Id == userId)
                .SingleOrDefaultAsync();
        }
    }

    public sealed partial class MoneyAccountQueries : IMoneyAccountQueries
    {
        public async Task<Maybe<MoneyAccountDTO>> FindAccountAsync(UserId userId)
        {
            var account = await this.FindAccountAsNoTrackingAsync(userId);

            var mapper = _mapper.Map<MoneyAccountDTO>(account);

            return mapper != null ? new Maybe<MoneyAccountDTO>(mapper) : new Maybe<MoneyAccountDTO>();
        }

        public async Task<Maybe<MoneyTransactionListDTO>> FindTransactionsAsync(UserId userId)
        {
            var account = await this.FindAccountAsNoTrackingAsync(userId);

            var mapper = _mapper.Map<MoneyTransactionListDTO>(account.Transactions);

            return mapper.Any() ? new Maybe<MoneyTransactionListDTO>(mapper) : new Maybe<MoneyTransactionListDTO>();
        }
    }
}
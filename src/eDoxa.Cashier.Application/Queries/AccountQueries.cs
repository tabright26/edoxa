// Filename: AccountQueries.cs
// Date Created: 2019-05-06
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

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Functional;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class AccountQueries
    {
        private readonly CashierDbContext _context;
        private readonly ICashierHttpContext _httpContext;
        private readonly IMapper _mapper;

        public AccountQueries(CashierDbContext context, ICashierHttpContext httpContext, IMapper mapper)
        {
            _context = context;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        internal async Task<MoneyAccount> GetMoneyAccountAsNoTrackingAsync(UserId userId)
        {
            return await _context.MoneyAccounts.AsNoTracking()
                                 .Include(transaction => transaction.Transactions)
                                 .Where(account => account.User.Id == userId)
                                 .SingleOrDefaultAsync();
        }

        internal async Task<TokenAccount> GetTokenAccountAsNoTrackingAsync(UserId userId)
        {
            return await _context.TokenAccounts.AsNoTracking()
                                 .Include(transaction => transaction.Transactions)
                                 .Where(account => account.User.Id == userId)
                                 .SingleOrDefaultAsync();
        }
    }

    public sealed partial class AccountQueries : IAccountQueries
    {
        public async Task<Option<AccountDTO>> GetAccountAsync(AccountCurrency currency)
        {
            var userId = _httpContext.UserId;

            AccountDTO mapper = null;

            if (currency.Equals(AccountCurrency.Money))
            {
                var account = await this.GetMoneyAccountAsNoTrackingAsync(userId);

                mapper = _mapper.Map<AccountDTO>(account);
            }

            if (currency.Equals(AccountCurrency.Token))
            {
                var account = await this.GetTokenAccountAsNoTrackingAsync(userId);

                mapper = _mapper.Map<AccountDTO>(account);
            }

            return mapper != null ? new Option<AccountDTO>(mapper) : new Option<AccountDTO>();
        }
    }
}

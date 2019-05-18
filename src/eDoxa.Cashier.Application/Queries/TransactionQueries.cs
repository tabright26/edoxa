// Filename: TransactionQueries.cs
// Date Created: 2019-05-16
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

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class TransactionQueries
    {
        private readonly CashierDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICashierSecurity _security;

        public TransactionQueries(CashierDbContext context, ICashierSecurity security, IMapper mapper)
        {
            _context = context;
            _security = security;
            _mapper = mapper;
        }

        private async Task<TransactionListDTO> GetMoneyTransactionsAsync(UserId userId)
        {
            var queries = new AccountQueries(_context, _security, _mapper);

            var account = await queries.GetMoneyAccountAsNoTrackingAsync(userId);

            return _mapper.Map<TransactionListDTO>(account.Transactions);
        }

        private async Task<TransactionListDTO> GetTokenTransactionsAsync(UserId userId)
        {
            var queries = new AccountQueries(_context, _security, _mapper);

            var account = await queries.GetTokenAccountAsNoTrackingAsync(userId);

            return _mapper.Map<TransactionListDTO>(account.Transactions);
        }
    }

    public sealed partial class TransactionQueries : ITransactionQueries
    {
        public async Task<TransactionListDTO> GetTransactionsAsync(AccountCurrency accountCurrency)
        {
            var userId = _security.UserId;

            var transactions = new TransactionListDTO();

            if (accountCurrency.Equals(AccountCurrency.Money) || accountCurrency.Equals(AccountCurrency.All))
            {
                transactions.Items.AddRange(await this.GetMoneyTransactionsAsync(userId));
            }

            if (accountCurrency.Equals(AccountCurrency.Token) || accountCurrency.Equals(AccountCurrency.All))
            {
                transactions.Items.AddRange(await this.GetTokenTransactionsAsync(userId));
            }

            return transactions
                .OrderBy(transaction => transaction.Currency)
                .ThenByDescending(transaction => transaction.Timestamp)
                .ToList();
        }
    }
}
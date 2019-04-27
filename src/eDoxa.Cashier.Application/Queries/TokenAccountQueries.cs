// Filename: TokenAccountQueries.cs
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
    public sealed partial class TokenAccountQueries
    {
        private readonly CashierDbContext _context;
        private readonly IMapper _mapper;

        public TokenAccountQueries(CashierDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<TokenAccount> FindAccountAsNoTrackingAsync(UserId userId)
        {
            return await _context.TokenAccounts.AsNoTracking()
                .Include(transaction => transaction.Transactions)
                .Where(account => account.User.Id == userId)
                .SingleOrDefaultAsync();
        }
    }

    public sealed partial class TokenAccountQueries : ITokenAccountQueries
    {
        public async Task<Maybe<TokenAccountDTO>> FindAccountAsync(UserId userId)
        {
            var account = await this.FindAccountAsNoTrackingAsync(userId);

            var mapper = _mapper.Map<TokenAccountDTO>(account);

            return mapper != null ? new Maybe<TokenAccountDTO>(mapper) : new Maybe<TokenAccountDTO>();
        }

        public async Task<Maybe<TokenTransactionListDTO>> FindTransactionsAsync(UserId userId)
        {
            var account = await this.FindAccountAsNoTrackingAsync(userId);

            var mapper = _mapper.Map<TokenTransactionListDTO>(account.Transactions);

            return mapper.Any() ? new Maybe<TokenTransactionListDTO>(mapper) : new Maybe<TokenTransactionListDTO>();
        }
    }
}
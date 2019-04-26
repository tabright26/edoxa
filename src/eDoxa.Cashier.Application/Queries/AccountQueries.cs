// Filename: AccountQueries.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;
using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class AccountQueries
    {
        private readonly CashierDbContext _context;
        private readonly IMapper _mapper;

        public AccountQueries(CashierDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<MoneyAccount> FindUserAccountAsNoTrackingAsync(UserId userId)
        {
            return (await _context.Users.AsNoTracking().Where(account => account.Id == userId).SingleOrDefaultAsync()).Funds;
        }
    }

    public sealed partial class AccountQueries : IAccountQueries
    {
        [ItemCanBeNull]
        public async Task<MoneyAccountDTO> FindUserAccountAsync(UserId userId)
        {
            var account = await this.FindUserAccountAsNoTrackingAsync(userId);

            return _mapper.Map<MoneyAccountDTO>(account);
        }
    }
}
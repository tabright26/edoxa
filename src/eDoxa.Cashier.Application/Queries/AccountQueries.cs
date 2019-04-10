// Filename: AccountQueries.cs
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

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class AccountQueries
    {
        private readonly CashierDbContext _context;
        private readonly IMapper _mapper;

        public AccountQueries(CashierDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private async Task<Account> FindUserAccountAsNoTrackingAsync(UserId userId)
        {
            return await _context.Accounts.AsNoTracking().Where(account => account.User.Id == userId).SingleOrDefaultAsync();
        }
    }

    public sealed partial class AccountQueries : IAccountQueries
    {
        public async Task<AccountDTO> FindUserAccountAsync(UserId userId)
        {
            var account = await this.FindUserAccountAsNoTrackingAsync(userId);

            return _mapper.Map<AccountDTO>(account);
        }
    }
}
// Filename: CashierScenarioDbContextData.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Abstractions;

namespace eDoxa.Cashier.IntegrationTests
{
    internal sealed class ScenarioDbContextData : IDbContextData
    {
        private readonly CashierDbContext _context;

        public ScenarioDbContextData(CashierDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!_context.Users.Any(user => user.Id == ScenarioConstants.TestUserId))
            {
                _context.Users.Add(new User(ScenarioConstants.TestUserId, ScenarioConstants.TestStripeConnectAccountId, ScenarioConstants.TestStripeConnectAccountId));

                await _context.CommitAndDispatchDomainEventsAsync();
            }
        }
    }
}

// Filename: CashierDbContextData.cs
// Date Created: 2019-06-01
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
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Infrastructure
{
    public sealed class CashierDbContextData : IDbContextData
    {
        private readonly CashierDbContext _context;
        private readonly ILogger<CashierDbContextData> _logger;
        private readonly IHostingEnvironment _environment;

        public CashierDbContextData(ILogger<CashierDbContextData> logger, IHostingEnvironment environment, CashierDbContext context)
        {
            _logger = logger;
            _environment = environment;
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Users.Any())
                {
                    var user = new User(UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"), "acct_1EbASfAPhMnJQouG", "cus_F5L8mRzm6YN5ma");

                    user.AddBankAccount("ba_1EbB3sAPhMnJQouGHsvc0NFn");

                    _context.Users.Add(user);

                    await _context.CommitAsync();

                    _logger.LogInformation("The user's being populated.");
                }
                else
                {
                    _logger.LogInformation("The user's already populated.");
                }
            }
        }
    }
}

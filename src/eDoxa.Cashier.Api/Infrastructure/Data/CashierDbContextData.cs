// Filename: CashierDbContextData.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.Infrastructure.Data
{
    internal sealed class CashierDbContextData : IDbContextData
    {
        private readonly CashierDbContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<CashierDbContextData> _logger;
        private readonly IHostingEnvironment _environment;

        public CashierDbContextData(
            ILogger<CashierDbContextData> logger,
            IHostingEnvironment environment,
            CashierDbContext context,
            IAccountRepository accountRepository
        )
        {
            _logger = logger;
            _environment = environment;
            _context = context;
            _accountRepository = accountRepository;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Accounts.Any())
                {
                    var accountFaker = new AccountFaker();

                    var adminAccount = accountFaker.FakeAdminAccount();

                    _accountRepository.Create(adminAccount);

                    await _accountRepository.CommitAsync();

                    _logger.LogInformation("The user's being populated.");
                }
                else
                {
                    _logger.LogInformation("The user's already populated.");
                }
            }
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Accounts.RemoveRange(_context.Accounts);

                await _context.SaveChangesAsync();
            }
        }
    }
}

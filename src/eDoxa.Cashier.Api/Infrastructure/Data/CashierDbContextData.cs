// Filename: CashierDbContextData.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.Infrastructure.Data
{
    internal sealed class CashierDbContextData : IDbContextData
    {
        private readonly CashierDbContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IChallengeRepository _challengeRepository;
        private readonly ILogger<CashierDbContextData> _logger;
        private readonly IHostingEnvironment _environment;

        public CashierDbContextData(
            ILogger<CashierDbContextData> logger,
            IHostingEnvironment environment,
            CashierDbContext context,
            IAccountRepository accountRepository,
            IChallengeRepository challengeRepository
        )
        {
            _logger = logger;
            _environment = environment;
            _context = context;
            _accountRepository = accountRepository;
            _challengeRepository = challengeRepository;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Accounts.Any())
                {
                    var accountFaker = new AccountFaker();

                    var adminAccount = accountFaker.Generate(AccountFaker.AdminAccount);

                    _accountRepository.Create(adminAccount);

                    await _accountRepository.CommitAsync();

                    _logger.LogInformation("The user's being populated.");
                }
                else
                {
                    _logger.LogInformation("The user's already populated.");
                }
            }

            if (_environment.IsDevelopment())
            {
                if (!_context.Challenges.Any())
                {
                    _challengeRepository.Create(CashierStorage.TestChallenges);

                    await _challengeRepository.CommitAsync();

                    _logger.LogInformation("The challenge's being populated.");
                }
                else
                {
                    _logger.LogInformation("The challenge's already populated.");
                }
            }
        }

        public void Cleanup()
        {
            if (!_environment.IsProduction())
            {
                _context.Accounts.RemoveRange(_context.Accounts);

                _context.Challenges.RemoveRange(_context.Challenges);

                _context.SaveChanges();
            }
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Accounts.RemoveRange(_context.Accounts);

                _context.Challenges.RemoveRange(_context.Challenges);

                await _context.SaveChangesAsync();
            }
        }
    }
}

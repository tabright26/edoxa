// Filename: CashierDbContextSeeder.cs
// Date Created: 2019-08-18
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
    internal sealed class CashierDbContextSeeder : IDbContextSeeder
    {
        private readonly CashierDbContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IChallengeRepository _challengeRepository;
        private readonly ILogger<CashierDbContextSeeder> _logger;
        private readonly IHostingEnvironment _environment;

        public CashierDbContextSeeder(
            ILogger<CashierDbContextSeeder> logger,
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
                    var storage = new CashierTestFileStorage();

                    var challenges = await storage.GetChallengesAsync();

                    _challengeRepository.Create(challenges);

                    await _challengeRepository.CommitAsync();

                    _logger.LogInformation("The challenge's being populated.");
                }
                else
                {
                    _logger.LogInformation("The challenge's already populated.");
                }
            }
        }
    }
}

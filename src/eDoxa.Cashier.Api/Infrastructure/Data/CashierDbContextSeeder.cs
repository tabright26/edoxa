// Filename: CashierDbContextSeeder.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
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

                    var moneyAccount = new MoneyAccount(adminAccount);

                    moneyAccount.Deposit(Money.FiveHundred).MarkAsSucceded(); // 500

                    moneyAccount.Charge(Money.Ten); // 490

                    moneyAccount.Charge(Money.Ten); // 480

                    moneyAccount.Charge(Money.Five); // 475

                    moneyAccount.Charge(Money.Fifty); // 425

                    moneyAccount.Payout(Money.Twenty); // 445

                    moneyAccount.Charge(Money.Ten); // 435

                    moneyAccount.Charge(Money.Ten); // 425

                    moneyAccount.Charge(Money.Ten); // 415

                    moneyAccount.Payout(Money.Twenty); // 435

                    moneyAccount.Withdrawal(Money.OneHundred).MarkAsSucceded(); // 335

                    moneyAccount.Charge(Money.Ten); // 325

                    moneyAccount.Charge(Money.Five); // 320

                    moneyAccount.Charge(Money.Fifty); // 270

                    moneyAccount.Charge(Money.Ten); // 260

                    var tokenAccount = new TokenAccount(adminAccount);

                    tokenAccount.Deposit(Token.OneMillion).MarkAsSucceded(); // 1000000

                    tokenAccount.Reward(Token.FiftyThousand); // 1050000

                    tokenAccount.Charge(Token.FiftyThousand); // 1000000

                    tokenAccount.Charge(Token.FiftyThousand); // 950000

                    tokenAccount.Charge(Token.TwoHundredFiftyThousand); // 700000

                    tokenAccount.Charge(Token.FiveHundredThousand); // 200000

                    tokenAccount.Charge(Token.FiftyThousand); // 150000

                    tokenAccount.Payout(Token.OneHundredThousand); // 250000

                    tokenAccount.Reward(Token.FiftyThousand); // 300000

                    tokenAccount.Charge(Token.OneHundredThousand); // 200000

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

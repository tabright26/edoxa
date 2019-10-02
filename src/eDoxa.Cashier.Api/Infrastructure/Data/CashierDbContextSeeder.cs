// Filename: CashierDbContextSeeder.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Abstractions;
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
                    IAccountFaker accountFaker = new AccountFaker();

                    var adminAccount = accountFaker.FakeAccount(AccountFaker.AdminAccount);

                    var moneyAccount = new MoneyAccount(adminAccount);

                    moneyAccount.Deposit(Money.FiveHundred).MarkAsSucceded(); // 500

                    moneyAccount.Charge(Money.Ten).MarkAsSucceded(); // 490

                    moneyAccount.Charge(Money.Ten).MarkAsSucceded(); // 480

                    moneyAccount.Charge(Money.Five).MarkAsSucceded(); // 475

                    moneyAccount.Charge(Money.Fifty).MarkAsSucceded(); // 425

                    moneyAccount.Payout(Money.Twenty).MarkAsSucceded(); // 445

                    moneyAccount.Charge(Money.Ten).MarkAsSucceded(); // 435

                    moneyAccount.Charge(Money.Ten).MarkAsSucceded(); // 425

                    moneyAccount.Charge(Money.Ten).MarkAsSucceded(); // 415

                    moneyAccount.Payout(Money.Twenty).MarkAsSucceded(); // 435

                    moneyAccount.Withdrawal(Money.OneHundred).MarkAsSucceded(); // 335

                    moneyAccount.Charge(Money.Ten).MarkAsSucceded(); // 325

                    moneyAccount.Charge(Money.Five).MarkAsSucceded(); // 320

                    moneyAccount.Charge(Money.Fifty).MarkAsSucceded(); // 270

                    moneyAccount.Charge(Money.Ten).MarkAsSucceded(); // 260

                    var tokenAccount = new TokenAccount(adminAccount);

                    tokenAccount.Deposit(Token.OneMillion).MarkAsSucceded(); // 1000000

                    tokenAccount.Reward(Token.FiftyThousand).MarkAsSucceded(); // 1050000

                    tokenAccount.Charge(Token.FiftyThousand).MarkAsSucceded(); // 1000000

                    tokenAccount.Charge(Token.FiftyThousand).MarkAsSucceded(); // 950000

                    tokenAccount.Charge(Token.TwoHundredFiftyThousand).MarkAsSucceded(); // 700000

                    tokenAccount.Charge(Token.FiveHundredThousand).MarkAsSucceded(); // 200000

                    tokenAccount.Charge(Token.FiftyThousand).MarkAsSucceded(); // 150000

                    tokenAccount.Payout(Token.OneHundredThousand).MarkAsSucceded(); // 250000

                    tokenAccount.Reward(Token.FiftyThousand).MarkAsSucceded(); // 300000

                    tokenAccount.Charge(Token.OneHundredThousand).MarkAsSucceded(); // 200000

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
                    _challengeRepository.Create(FileStorage.Challenges);

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

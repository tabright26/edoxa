// Filename: AccountFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Common.Abstactions;

namespace eDoxa.Cashier.Api.Application.Data.Fakers
{
    public sealed class AccountFaker : CustomFaker<Account>
    {
        private const string NewAccount = nameof(NewAccount);
        private const string AdminAccount = nameof(AdminAccount);

        private readonly TransactionFaker _transactionFaker = new TransactionFaker();

        public AccountFaker()
        {
            this.RuleSet(
                NewAccount,
                ruleSet =>
                {
                    ruleSet.RuleFor(account => account.Id, faker => AccountId.FromGuid(faker.Random.Guid()));

                    ruleSet.FinishWith(
                        (faker, account) =>
                        {
                            var transactions = _transactionFaker.FakePositiveTransactions(faker.Random.Int(0, 5));

                            foreach (var transaction in transactions)
                            {
                                account.CreateTransaction(transaction);
                            }
                        }
                    );
                }
            );

            this.RuleSet(
                AdminAccount,
                ruleSet =>
                {
                    ruleSet.RuleFor(account => account.Id, _ => AccountId.Parse("707b2535-ed60-4797-92a0-4dacd8d51f6c"));
                }
            );
        }

        public Account FakeNewAccount()
        {
            return this.Generate(NewAccount);
        }

        public Account FakeNewAccount(User user)
        {
            return this.RuleFor(account => account.User, user).Generate(NewAccount);
        }

        public Account FakeAdminAccount()
        {
            return this.Generate(AdminAccount);
        }

        public Account FakeAdminAccount(User user)
        {
            return this.RuleFor(account => account.User, user).Generate(AdminAccount);
        }
    }
}

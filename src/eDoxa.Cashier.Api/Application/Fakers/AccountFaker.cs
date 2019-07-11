// Filename: AccountFaker.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Cashier.Api.Application.Fakers.Extensions;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Api.Application.Fakers
{
    public sealed class AccountFaker : Faker<IAccount>
    {
        public const string AdminAccount = nameof(AdminAccount);

        private readonly TransactionFaker _transactionFaker = new TransactionFaker();

        public AccountFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    var account = new Account(faker.UserId());

                    account.SetEntityId(AccountId.FromGuid(faker.Random.Guid()));

                    _transactionFaker.UseSeed(faker.Random.Int());

                    var transactions = _transactionFaker.Generate(faker.Random.Int(0, 5), TransactionFaker.PositiveTransaction);

                    foreach (var transaction in transactions)
                    {
                        account.CreateTransaction(transaction);
                    }

                    return account;
                }
            );

            this.RuleSet(
                AdminAccount,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var account = new Account(UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));

                            account.SetEntityId(AccountId.Parse("707b2535-ed60-4797-92a0-4dacd8d51f6c"));

                            return account;
                        }
                    );
                }
            );
        }
    }
}

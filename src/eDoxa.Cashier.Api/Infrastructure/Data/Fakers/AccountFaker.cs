// Filename: AccountFaker.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Bogus;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Abstractions;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public sealed partial class AccountFaker : IAccountFaker
    {
        public IAccount FakeAccount(string? ruleSets = null)
        {
            return this.Generate(ruleSets);
        }
    }

    public sealed partial class AccountFaker : Faker<IAccount>
    {
        public const string AdminAccount = nameof(AdminAccount);

        private readonly TransactionFaker _transactionFaker = new TransactionFaker();

        public AccountFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    _transactionFaker.UseSeed(faker.Random.Int());

                    var transactions = _transactionFaker.Generate(faker.Random.Int(0, 5), TransactionFaker.PositiveTransaction);

                    return new Account(faker.User().Id(), new HashSet<ITransaction>(transactions));
                });

            this.RuleSet(
                AdminAccount,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(faker => new Account(UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));
                });
        }
    }
}

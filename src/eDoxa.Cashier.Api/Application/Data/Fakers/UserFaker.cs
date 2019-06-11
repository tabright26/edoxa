// Filename: UserFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Fakers;
using eDoxa.Stripe.Models;

namespace eDoxa.Cashier.Api.Application.Data.Fakers
{
    public sealed class UserFaker : CustomFaker<User>
    {
        private const string NewUser = nameof(NewUser);
        private const string AdminUser = nameof(AdminUser);

        private readonly UserIdFaker _userIdFaker = new UserIdFaker();
        private readonly AccountFaker _accountFaker = new AccountFaker();

        public UserFaker()
        {
            this.RuleSet(
                NewUser,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var userId = _userIdFaker.FakeUserId();

                            var connectAccountId = new StripeConnectAccountId($"acct_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

                            var customerId = new StripeCustomerId($"cus_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

                            return new User(userId, connectAccountId.ToString(), customerId.ToString());
                        }
                    );

                    ruleSet.RuleFor(user => user.Account, (faker, user) => _accountFaker.FakeNewAccount(user));

                    ruleSet.FinishWith(
                        (faker, user) =>
                        {
                            var bankAccountId = new StripeBankAccountId($"ba_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

                            user.AddBankAccount(bankAccountId.ToString());
                        }
                    );
                }
            );

            this.RuleSet(
                AdminUser,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var userId = UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091");

                            var connectAccountId = new StripeConnectAccountId("acct_1EbASfAPhMnJQouG");

                            var customerId = new StripeCustomerId("cus_F5L8mRzm6YN5ma");

                            return new User(userId, connectAccountId.ToString(), customerId.ToString());
                        }
                    );

                    ruleSet.RuleFor(user => user.Account, (faker, user) => _accountFaker.FakeAdminAccount(user));

                    ruleSet.FinishWith(
                        (faker, user) =>
                        {
                            var bankAccountId = new StripeBankAccountId("ba_1EbB3sAPhMnJQouGHsvc0NFn");

                            user.AddBankAccount(bankAccountId.ToString());
                        }
                    );
                }
            );
        }

        public List<User> FakeNewUsers(int count)
        {
            return this.Generate(count, NewUser);
        }

        public User FakeNewUser()
        {
            return this.Generate(NewUser);
        }

        public User FakeAdminUser()
        {
            return this.Generate(AdminUser);
        }
    }
}

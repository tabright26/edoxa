// Filename: UserFaker.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Security.Constants;

namespace eDoxa.Identity.Api.Application.Fakers
{
    public sealed class UserFaker : Faker<User>
    {
        private const string TestUser = "test";
        private const string AdminUser = "admin";

        public UserFaker()
        {
            this.RuleSet(
                TestUser,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var firstName = faker.Name.FirstName();

                            var lastName = faker.Name.LastName();

                            var user = new User(
                                faker.Internet.UserName(),
                                faker.Internet.Email(firstName, lastName),
                                new PersonalName(firstName, lastName),
                                new BirthDate(faker.Date.Past(18))
                            );

                            user.HashPassword("Pass@word1");

                            return user;
                        }
                    );

                    ruleSet.RuleFor(user => user.Id, faker => faker.UserId());

                    ruleSet.RuleFor(user => user.EmailConfirmed, faker => faker.Random.Bool());

                    ruleSet.RuleFor(user => user.PhoneNumber, faker => faker.Phone.PhoneNumber("##########"));

                    ruleSet.RuleFor(user => user.PhoneNumberConfirmed, faker => faker.Random.Bool());
                }
            );

            this.RuleSet(
                AdminUser,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        _ =>
                        {
                            var user = new User("Administrator", "admin@edoxa.gg", new PersonalName("eDoxa", "Admin"), new BirthDate(1970, 1, 1));

                            user.HashPassword("Pass@word1");

                            return user;
                        }
                    );

                    ruleSet.RuleFor(user => user.Id, Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));

                    ruleSet.RuleFor(user => user.EmailConfirmed, true);

                    ruleSet.RuleFor(user => user.PhoneNumber, "0000000000");

                    ruleSet.RuleFor(user => user.PhoneNumberConfirmed, true);

                    ruleSet.RuleFor(
                        user => user.Roles,
                        (faker, user) =>
                        {
                            var roleFaker = new RoleFaker();

                            var roles = roleFaker.FakeRoles();

                            return roles.Select(role => new UserRole(user.Id, role.Id)).ToList();
                        }
                    );

                    ruleSet.FinishWith(
                        (faker, user) =>
                        {
                            user.Claims.Add(new UserClaim(user.Id, CustomClaimTypes.StripeCustomerId, "cus_F5L8mRzm6YN5ma"));
                            user.Claims.Add(new UserClaim(user.Id, CustomClaimTypes.StripeConnectAccountId, "acct_1EbASfAPhMnJQouG"));
                        }
                    );
                }
            );
        }

        public IEnumerable<User> FakeTestUsers(int count = 1000)
        {
            UserFakerExtensions.ResetUserIds();

            return this.Generate(count, TestUser).ToList();
        }

        public User FakeTestUser()
        {
            UserFakerExtensions.ResetUserIds();

            this.UseSeed(FakerHub.Random.Int());

            return this.Generate(TestUser);
        }

        public User FakeAdminUser()
        {
            return this.Generate(AdminUser);
        }
    }
}
